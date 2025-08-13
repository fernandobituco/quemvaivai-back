# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia todos os arquivos do projeto
COPY . .

# Restaura pacotes NuGet
RUN dotnet restore

# Publica o projeto em modo Release
RUN dotnet publish -c Release -o /app

# Etapa final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copia arquivos publicados do build
COPY --from=build /app .

# Porta exposta no container
EXPOSE 8080

# Comando para rodar a API
ENTRYPOINT ["dotnet", "QuemVaiVai.Api.csproj"]
