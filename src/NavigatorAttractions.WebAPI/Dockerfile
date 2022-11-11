ARG APPLICATION_NAME='NavigatorAttractions.WebAPI'
ARG BUILD_DATE='1/1/2022'
ARG BUILD_NUMBER=0

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

LABEL org.label-schema.application=${APPLICATION_NAME}
LABEL org.label-schema.build-date=${BUILD_DATE}
LABEL org.label-schema.version=7.0.1.${BUILD_NUMBER}

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

ENV PATH="/root/.dotnet/tools:${PATH}"
RUN dotnet tool install --global dotnet-setversion --version 2.5.0
RUN dotnet tool list -g

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
ARG BUILD_NUMBER
#RUN setversion 7.0.1.${BUILD_NUMBER}
RUN dotnet publish "NavigatorAttractions.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NavigatorAttractions.WebAPI.dll"]