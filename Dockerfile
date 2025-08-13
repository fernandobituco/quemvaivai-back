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

# Instala bibliotecas necessárias para PostgreSQL/Npgsql (libgssapi_krb5.so.2)
RUN apt-get update && apt-get install -y --no-install-recommends \
    libkrb5-3 \
    && rm -rf /var/lib/apt/lists/*

# Copia arquivos publicados do build
COPY --from=build /app .

# Porta exposta no container
EXPOSE 8080

# Comando para rodar a API
ENTRYPOINT ["dotnet", "QuemVaiVai.Api.dll"]
