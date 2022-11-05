#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/NavigatorAttractions.WebAPI/NavigatorAttractions.WebAPI.csproj", "src/NavigatorAttractions.WebAPI/"]
COPY ["src/NavigatorAttractions.Service/NavigatorAttractions.Service.csproj", "src/NavigatorAttractions.Service/"]
COPY ["src/NavigatorAttractions.Data/NavigatorAttractions.Data.csproj", "src/NavigatorAttractions.Data/"]
COPY ["src/NavigatorAttractions.Core/NavigatorAttractions.Core.csproj", "src/NavigatorAttractions.Core/"]
RUN dotnet restore "src/NavigatorAttractions.WebAPI/NavigatorAttractions.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/NavigatorAttractions.WebAPI"
RUN dotnet build "NavigatorAttractions.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NavigatorAttractions.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NavigatorAttractions.WebAPI.dll"]