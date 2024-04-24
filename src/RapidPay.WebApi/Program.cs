using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RapidPay.Domain.Services;
using RapidPay.Models.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
TokenValidationParameters tokenValidationParameters = new()
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = configuration.GetValue<string>("ConfigData:Jwt:Issuer")!,
    ValidAudience = configuration.GetValue<string>("ConfigData:Jwt:Audience"),
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("ConfigData:Jwt:Secret")!))
};

builder.Services.AddDbContext<RapidPayDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("RapidPay")));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IJwtManager, JwtManager>(s => new JwtManager(tokenValidationParameters));
// services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IPaymentFeesModule, PaymentFeesModule>();
builder.Services.AddScoped<ICardManagementService, CardManagementService>();
// services.AddScoped<CustomJwtBearerEvents>();
builder.Services.AddAuthentication().AddJwtBearer(options => {
    options.TokenValidationParameters = tokenValidationParameters;
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
