FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

EXPOSE 44319
WORKDIR /App

# Copy everything
COPY *.csproj ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
COPY ../ ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "DesafioFinal.dll"]