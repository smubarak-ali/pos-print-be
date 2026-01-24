FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o ./publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine
WORKDIR /app
COPY --from=build-env /app/publish .
#ENV ASPNETCORE_HTTP_PORTS=8084
EXPOSE 8080
CMD ["dotnet", "pos-print-be.dll"]
