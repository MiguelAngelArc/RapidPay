# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the project file and restore any dependencies (use .csproj for the project name)
RUN mkdir -p ./src/RapidPay.WebApi/
COPY ./src/RapidPay.WebApi/*.csproj ./src/RapidPay.WebApi/

RUN mkdir -p ./src/RapidPay.Models/
COPY ./src/RapidPay.Models/*.csproj ./src/RapidPay.Models/

COPY RapidPay.sln ./

RUN echo "MaxProtocol = TLSv1.2" >> /etc/ssl/openssl.cnf && dotnet restore

COPY ./src ./src

# Publish the application
WORKDIR /app/src/RapidPay.WebApi
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/src/RapidPay.WebApi/out ./

# Expose the port your application will run on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "RapidPay.WebApi.dll"]
