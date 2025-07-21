# Quem Vai Vai - API (.NET)

Este é o repositório backend da aplicação **Quem Vai Vai**, um sistema para organização de encontros informais entre grupos de amigos, como bares, festas e eventos.

## 🛠️ Tecnologias

- ASP.NET Core 8 (Web API)
- Entity Framework Core (PostgreSQL)
- JWT (Autenticação)
- AutoMapper
- Swagger
- Redis (Cache)
- Docker (para ambiente de desenvolvimento)
- Deploy na Render

## 📁 Estrutura Inicial

QuemVaiVai.Api/  
├── Controllers/  
├── Models/  
├── Data/  
├── DTOs/  
├── Services/  
├── Repositories/  
├── Helpers/  
└── Program.cs

markdown

CopiarEditar

`## 🚀 Executando localmente  ### Pré-requisitos  - [.NET SDK 8](https://dotnet.microsoft.com/download) - PostgreSQL - Redis (opcional) - Docker (opcional)  ### Passos  ```bash # Restaurar pacotes dotnet restore  # Rodar migrações (se aplicável) dotnet ef database update  # Executar a aplicação dotnet run`

Acesse `https://localhost:5001/swagger` para ver a documentação interativa da API.

## 🧪 Testes

bash

CopiarEditar

`dotnet test`

## 🐳 Docker (opcional)

bash

CopiarEditar

`docker-compose up --build`

## 🔐 Variáveis de ambiente

Configure suas variáveis no arquivo `.env` (ou `appsettings.Development.json`):

json

CopiarEditar

`{   "JwtSettings": {     "SecretKey": "sua-chave-secreta-aqui",     "Issuer": "QuemVaiVai",     "Audience": "QuemVaiVaiUsers"   },   "ConnectionStrings": {     "DefaultConnection": "Host=localhost;Database=quemvaivai;Username=postgres;Password=senha"   } }`

---

### 🤝 Contribuindo

1. Fork o repositório
2. Crie sua branch: `git checkout -b minha-feature`
3. Commit: `git commit -m 'feat: minha nova feature'`
4. Push: `git push origin minha-feature`
5. Crie um Pull Request
