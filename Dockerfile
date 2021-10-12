#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["User-Service.Web/User-Service.Web.csproj", "User-Service.Web/"]
RUN dotnet restore "User-Service.Web/User-Service.Web.csproj"
COPY . .
WORKDIR "/src/User-Service.Web"
RUN dotnet build "User-Service.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User-Service.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User-Service.Web.dll"]