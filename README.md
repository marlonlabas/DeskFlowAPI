# 🎧 DeskFlow API — Gestão de Chamados e Helpdesk de TI

## 🎯 Sobre o Projeto
A **DeskFlow API** é uma Web API RESTful construída em .NET 10 utilizando Entity Framework Core e SQL Server. O sistema automatiza o gerenciamento de chamados de suporte técnico, histórico de interações e acompanhamento de status do atendimento, com autenticação de usuários via Bearer Token.

## 🛠️ Tecnologias Utilizadas
- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10 (Code First + Migrations)
- SQL Server
- ASP.NET Core Identity — Identity API Endpoints nativos (autenticação via Bearer Token)
- Swagger / OpenAPI

## 🚀 Como Executar a Aplicação

### Pré-requisitos
- .NET SDK 10 (ou superior)
- SQL Server em execução (SQL Server Express, LocalDB ou Docker)
- Ferramenta global `dotnet-ef` instalada (necessária para rodar as Migrations):
  ```
  dotnet tool install --global dotnet-ef
  ```
  Se já estiver instalada em uma versão antiga, atualize com `dotnet tool update --global dotnet-ef`. Para confirmar, rode `dotnet ef --version`.

### Passo a Passo
1. Clone este repositório:
   ```
   git clone https://github.com/marlonlabas/DeskFlowAPI.git
   ```
2. Acesse a pasta do projeto:
   ```
   cd DeskFlowAPI
   ```
3. Confira a Connection String no arquivo `appsettings.json` (o valor padrão já aponta para uma instância local SQL Server Express; ajuste se o seu ambiente for diferente):
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=db-DeskFlowDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
4. Execute as Migrations para criar a estrutura no banco de dados (cria as tabelas de negócio — Chamados, Categorias, Interações — e as tabelas do Identity — AspNetUsers, AspNetRoles etc.):
   ```
   dotnet ef database update
   ```
   Se aparecer o erro `Could not execute because the specified command or file was not found` (ou "dotnet ef" não reconhecido), é sinal de que a ferramenta `dotnet-ef` não está instalada nessa máquina — volte ao pré-requisito acima.

5. Execute a API:
   ```
   dotnet run
   ```
6. Acesse a documentação do Swagger para testar os endpoints:
   ```
   http://localhost:5069/swagger
   ```

## 🔐 Autenticação
A API utiliza os **Identity API Endpoints** nativos do ASP.NET Core (.NET 10), com autenticação via Bearer Token — sem necessidade de implementação manual de JWT.

1. Registre um usuário: `POST /auth/register` com `{ "email": "...", "password": "..." }`.
2. Faça login: `POST /auth/login` com as mesmas credenciais — a resposta traz o `accessToken`.
3. No Swagger, clique no cadeado **"Authorize"** (topo da página) e cole apenas o `accessToken` (sem o prefixo "Bearer" — o Swagger adiciona automaticamente).
4. Em outras ferramentas (Postman, curl, etc.), envie o cabeçalho:
   ```
   Authorization: Bearer {accessToken}
   ```

Sem um token válido, os endpoints de `/api/chamados` e `/api/categorias` retornam `401 Unauthorized`.

## 📋 Endpoints Principais

**Autenticação**

| Verbo | Rota | Descrição |
|---|---|---|
| POST | `/auth/register` | Registra um novo usuário |
| POST | `/auth/login` | Autentica e retorna o `accessToken` |

**Categorias**

| Verbo | Rota | Descrição |
|---|---|---|
| GET | `/api/categorias` | Lista todas as categorias |
| GET | `/api/categorias/{id}` | Obtém uma categoria por Id |
| POST | `/api/categorias` | Cria uma nova categoria |
| PUT | `/api/categorias/{id}` | Atualiza uma categoria |
| DELETE | `/api/categorias/{id}` | Remove uma categoria (bloqueado se houver chamados vinculados) |

**Chamados**

| Verbo | Rota | Descrição |
|---|---|---|
| GET | `/api/chamados` | Lista chamados, com filtros opcionais `?status=&prioridade=&categoriaId=` |
| GET | `/api/chamados/{id}` | Obtém um chamado por Id, com Categoria e Interações |
| POST | `/api/chamados` | Abre um novo chamado (Status "Aberto" por padrão) |
| PUT | `/api/chamados/{id}` | Atualiza os dados do chamado |
| DELETE | `/api/chamados/{id}` | Remove um chamado |
| POST | `/api/chamados/{id}/iniciar` | Inicia o atendimento (Aberto → EmAndamento) |
| POST | `/api/chamados/{id}/encerrar` | Encerra o atendimento (EmAndamento → Fechado), exige a solução |
| POST | `/api/chamados/{id}/interacoes` | Adiciona uma interação/comentário (bloqueado em chamados Fechados) |

Todos os endpoints de `/api/chamados` e `/api/categorias` exigem o cabeçalho `Authorization: Bearer {accessToken}`.

## 🧠 Ciclo de Vida do Chamado
- **Aberto**: Chamado registrado pelo solicitante.
- **EmAndamento**: Suporte em atendimento ao chamado.
- **Fechado**: Chamado encerrado com texto de solução e data de conclusão.

## 🧱 Arquitetura em Camadas
- **Controllers**: Recebem as requisições HTTP e definem os Status Codes.
- **Services**: Contêm as regras de negócio e validação dos status.
- **Repositories**: Executam comandos e consultas de banco via EF Core.
- **Models/Data**: Entidades de domínio e o `AppDbContext` (herda de `IdentityDbContext`).
- **Middlewares**: Tratamento e padronização de erros globais da API.
- **Exceptions**: Exceções de domínio (`NotFoundException`, `RegraDeNegocioException`) convertidas em respostas HTTP semânticas pelo middleware.

## 🎥 Vídeo de Apresentação
[Link](https://)
