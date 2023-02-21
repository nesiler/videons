FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Videons.WebAPI.csproj", "Videons.WebAPI/"]
RUN dotnet restore "Videons.WebAPI/Videons.WebAPI.csproj"
COPY . .
WORKDIR "/src/Videons.WebAPI"
RUN dotnet build "Videons.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Videons.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Videons.WebAPI.dll"]
