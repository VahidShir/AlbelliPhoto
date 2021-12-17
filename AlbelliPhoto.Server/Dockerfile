#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AlbelliPhoto.Server/AlbelliPhoto.Server.csproj", "AlbelliPhoto.Server/"]
COPY ["AlbelliPhoto.Data/AlbelliPhoto.Data.csproj", "AlbelliPhoto.Data/"]
COPY ["AlbelliPhoto.Dto/AlbelliPhoto.Dto.csproj", "AlbelliPhoto.Dto/"]
COPY ["AlbelliPhoto.Abstraction/AlbelliPhoto.Abstraction.csproj", "AlbelliPhoto.Abstraction/"]
COPY ["AlbelliPhoto.Services/AlbelliPhoto.Services.csproj", "AlbelliPhoto.Services/"]
RUN dotnet restore "AlbelliPhoto.Server/AlbelliPhoto.Server.csproj"
COPY . .
WORKDIR "/src/AlbelliPhoto.Server"
RUN dotnet build "AlbelliPhoto.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AlbelliPhoto.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlbelliPhoto.Server.dll"]