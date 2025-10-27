FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o /out
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_URLS_HTTP=http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet","AutoNego.dll"]
