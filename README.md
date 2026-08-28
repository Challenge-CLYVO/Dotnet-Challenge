# PetCare API

API REST desenvolvida em **ASP.NET Core** para gerenciamento de informações relacionadas a pets, tutores, clínicas, consultas, vacinas e histórico de saúde.

O projeto utiliza uma arquitetura organizada em camadas, integração com **Oracle Database**, autenticação via **JWT**, documentação através do **Swagger** e recursos de monitoramento, observabilidade e testes automatizados.

---

# Integrantes

* Lucas Rafael Solimene / RM: 565194

* Samyr Couto Oliveira / RM: 565562

* Henrique Teixeira Cesar / RM: 563088

---

## Tecnologias utilizadas

* .NET 9
* ASP.NET Core
* Entity Framework Core
* Oracle Database
* JWT Authentication
* Swagger / OpenAPI
* AutoMapper
* Serilog
* OpenTelemetry
* xUnit
* Moq
* WebApplicationFactory
* Health Checks
* Prometheus
* Git / GitHub

---

# Arquitetura

O projeto utiliza uma arquitetura dividida em camadas:

```text
PetCare
│
├── PetCare.API
│   ├── Controllers
│   ├── Middlewares
│   ├── HealthChecks
│   └── Metrics
│
├── PetCare.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   ├── Exceptions
│   └── Mappings
│
├── PetCare.Domain
│   └── Entities
│
├── PetCare.Infrastructure
│   ├── Data
│   └── Repositories
│
├── PetCare.Tests.Unit
│
└── PetCare.Tests.Integration
```

---
# Como executar o projeto

## 1. Clonar o repositório

Clone o projeto utilizando:

```bash
git clone https://github.com/Challenge-CLYVO/Dotnet-Challenge.git
```

Depois entre na pasta do projeto:

```bash
cd Dotnet-Challenge
```

---

## 2. Restaurar as dependências

Execute:

```bash
dotnet restore
```

---

## 3. Configurar o banco Oracle

Antes de executar a aplicação, é necessário configurar as credenciais de acesso ao banco Oracle.

Abra o arquivo:

```text
PetCare.API/appsettings.json
```

Localize a configuração:

```json
"ConnectionStrings": {
  "RecommendaContextOracle": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=<USUARIO>;Password=<SENHA>;"
}
```

Substitua `<USUARIO>` pelo seu usuário do Oracle e `<SENHA>` pela sua senha.

Exemplo:

```json
{
  "ConnectionStrings": {
    "RecommendaContextOracle": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=SEU_USUARIO;Password=SUA_SENHA;"
  }
}
```

> **Importante:** não compartilhe ou publique suas credenciais reais no GitHub. Utilize suas próprias credenciais do Oracle e mantenha informações sensíveis fora do repositório.

---

## 4. Compilar o projeto

Execute:

```bash
dotnet build
```

Se a compilação for concluída com sucesso, a aplicação estará pronta para ser executada.

---

## 5. Executar os testes

Para executar todos os testes:

```bash
dotnet test
```

Para executar somente os testes unitários:

```bash
dotnet test PetCare.Tests.Unit
```

Para executar somente os testes de integração:

```bash
dotnet test PetCare.Tests.Integration
```

Para gerar a cobertura dos testes:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

## 6. Executar a API

Execute:

```bash
dotnet run --project PetCare.API
```

A API será iniciada na porta configurada pelo projeto.

---

## 7. Acessar o Swagger

Com a API em execução, acesse:

```text
http://localhost:5100/swagger
```

O Swagger permite visualizar e testar os endpoints da API.

---

## 8. Testar o Health Check

Com a API em execução, acesse:

```text
http://localhost:5100/health
```

O endpoint verifica:

* Saúde da API;
* Conectividade com o Oracle;
* Disponibilidade do serviço externo.

---

## 9. Visualizar as métricas

As métricas podem ser acessadas através de:

```text
http://localhost:5100/metrics
```

Esse endpoint disponibiliza as métricas coletadas pelo OpenTelemetry para monitoramento da aplicação.

---

## 10. Logs

Durante a execução da API, os logs podem ser visualizados diretamente no console.

Os arquivos de log são armazenados na pasta:

```text
logs/
```

Os logs registram informações sobre:

* Requisições HTTP;
* Operações realizadas;
* Warnings;
* Erros;
* Tempo de resposta;
* Correlation ID;
* Exceções.

---

### Responsabilidade das camadas

**API**

Responsável pelos Controllers, autenticação, middlewares, Health Checks, logging, métricas e exposição dos endpoints HTTP.

**Application**

Contém as regras de aplicação, Services, DTOs, interfaces, exceções e mapeamentos.

**Domain**

Contém as entidades principais do sistema e representa o domínio da aplicação.

**Infrastructure**

Responsável pelo acesso ao banco de dados Oracle e implementação dos repositories.

**Tests.Unit**

Contém os testes unitários das camadas Domain e Application.

**Tests.Integration**

Contém os testes de integração dos endpoints da API utilizando `WebApplicationFactory`.

---

# Funcionalidades

A API possui funcionalidades para gerenciamento de:

* Pets
* Tutores
* Clínicas
* Consultas
* Vacinas
* Aplicações de vacinas
* Histórico de saúde

Além das operações CRUD, a aplicação possui:

* Autenticação JWT
* Swagger
* Health Checks
* Logging estruturado
* Correlation ID
* Monitoramento de requisições HTTP
* Distributed Tracing
* Métricas da aplicação
* Testes unitários
* Testes de integração
* Cobertura de testes

---

# Banco de Dados

A aplicação utiliza **Oracle Database** através do Entity Framework Core.

A connection string deve ser configurada no arquivo:

```text
appsettings.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "RecommendaContextOracle": "SUA_CONNECTION_STRING"
  }
}
```

Não versionar credenciais reais no repositório.

---

# Executando o projeto

Na pasta raiz do projeto:

```powershell
dotnet restore
```

Depois:

```powershell
dotnet build
```

Para executar a API:

```powershell
dotnet run --project PetCare.API
```

A aplicação ficará disponível conforme a porta configurada no projeto.

---

# Swagger

Durante o desenvolvimento, a API disponibiliza a documentação Swagger.

Através do Swagger é possível visualizar e testar os endpoints disponíveis.

Exemplo:

```text
http://localhost:5100/swagger
```

---

# Autenticação

A API utiliza **JWT (JSON Web Token)** para autenticação.

Primeiro deve ser realizado o login através do endpoint:

```http
POST /api/Auth/login
```

Após realizar o login, a API retorna um token JWT.

Esse token deve ser enviado nas requisições protegidas através do header:

```http
Authorization: Bearer SEU_TOKEN
```

No Swagger, o botão **Authorize** pode ser utilizado para informar o token.

---

# Endpoints de Pets

## Listar pets

```http
GET /api/Pet
```

Retorna todos os pets cadastrados.

---

## Buscar pet por ID

```http
GET /api/Pet/{id}
```

Exemplo:

```http
GET /api/Pet/1
```

---

## Criar pet

```http
POST /api/Pet
```

Exemplo de corpo:

```json
{
  "nome": "Rex",
  "idade": 5,
  "especie": "Cachorro",
  "raca": "Labrador",
  "idTutor": 1
}
```

---

## Atualizar pet

```http
PUT /api/Pet/{id}
```

Exemplo:

```http
PUT /api/Pet/1
```

Corpo:

```json
{
  "nome": "Rex Atualizado",
  "idade": 6,
  "especie": "Cachorro",
  "raca": "Labrador",
  "idTutor": 1
}
```

---

## Excluir pet

```http
DELETE /api/Pet/{id}
```

Exemplo:

```http
DELETE /api/Pet/1
```

---

# Health Checks

A aplicação possui Health Checks para monitorar os principais componentes da solução.

Endpoint:

```http
GET /health
```

O endpoint verifica:

* Saúde da API
* Conectividade com Oracle
* Disponibilidade de serviço externo

Exemplo de resposta:

```json
{
  "status": "Healthy",
  "checks": [
    {
      "name": "api",
      "status": "Healthy",
      "description": "API está funcionando normalmente."
    },
    {
      "name": "oracle",
      "status": "Healthy",
      "description": null
    },
    {
      "name": "external-service",
      "status": "Healthy",
      "description": "Serviço externo está disponível."
    }
  ]
}
```

Os Health Checks também foram testados em cenários de indisponibilidade do Oracle e do serviço externo.

---

# Logging

O projeto utiliza **Serilog** para logging estruturado.

Os logs possuem diferentes níveis:

* `Information`
* `Warning`
* `Error`

São registrados eventos relacionados a:

* Requisições HTTP
* Operações dos Services
* Criação de registros
* Atualização de registros
* Exclusão de registros
* Recursos não encontrados
* Erros internos
* Exceções

---

## Console

Os logs são exibidos diretamente no console da aplicação.

Exemplo:

```text
[INF] Consultando pet. PetId: 1
```

---

## Arquivo

Os logs também são armazenados em arquivos dentro da pasta:

```text
logs/
```

Os arquivos são organizados por data através do rolling interval do Serilog.

---

# Correlation ID

A aplicação possui um middleware responsável por gerar e propagar um **Correlation ID** para cada requisição.

O Correlation ID permite identificar e acompanhar uma requisição específica durante seu processamento.

Isso facilita a investigação de erros e o acompanhamento de operações nos logs.

---

# Logging de requisições HTTP

As requisições HTTP são registradas automaticamente pelo Serilog.

São registrados dados como:

* Método HTTP
* Endpoint
* Status Code
* Tempo de resposta
* Informações relacionadas à requisição

Exemplo:

```text
HTTP GET /api/Pet responded 200
```

---

# Tratamento de erros

A aplicação possui um middleware global de exceções.

Exceções de recurso não encontrado são registradas como `Warning`.

Exceções não tratadas são registradas como `Error`.

Exemplo:

```text
Recurso não encontrado. Path: /api/Pet/999999
```

E:

```text
Erro interno não tratado. Path: /api/Pet
```

---

# OpenTelemetry

A aplicação utiliza **OpenTelemetry** para observabilidade.

Foi configurado Distributed Tracing para acompanhar as requisições durante seu processamento.

A instrumentação contempla:

* ASP.NET Core
* HttpClient
* Entity Framework Core
* Oracle

---

# Distributed Tracing

As requisições recebem informações de rastreamento através de:

* Trace ID
* Span ID
* Parent Span ID
* Duração
* Método HTTP
* Endpoint
* Status da resposta

Isso permite acompanhar o fluxo de uma requisição entre as diferentes camadas da aplicação.

---

# Métricas

A aplicação expõe métricas através do endpoint:

```text
/metrics
```

As métricas permitem acompanhar o comportamento da aplicação.

São monitorados dados relacionados a:

* Quantidade de requisições
* Tempo de resposta
* Erros HTTP
* Runtime da aplicação

Também existe uma métrica específica para erros HTTP:

```text
petcare_http_errors_total
```

---

# Prometheus

As métricas podem ser coletadas pelo **Prometheus** através do endpoint:

```http
GET /metrics
```

Exemplo:

```text
http://localhost:5100/metrics
```

Esse endpoint pode ser utilizado como fonte de métricas para ferramentas de monitoramento.

---

# Testes Automatizados

O projeto possui dois projetos separados de testes:

```text
PetCare.Tests.Unit
PetCare.Tests.Integration
```

Os testes utilizam **xUnit**.

---

# Testes Unitários

Os testes unitários validam principalmente as camadas:

* Domain
* Application

As dependências dos Services são isoladas utilizando mocks.

Os testes seguem o padrão **AAA**:

```text
Arrange
Act
Assert
```

### Arrange

Preparação dos dados e dependências necessárias para o teste.

### Act

Execução do método que está sendo testado.

### Assert

Validação do resultado esperado.

---

# Testes de Integração

Os testes de integração utilizam:

```text
WebApplicationFactory
```

para executar a API em um ambiente de testes e validar o fluxo HTTP completo.

São testados cenários como:

* Requisições autenticadas
* Requisições sem autenticação
* Token inválido
* Criação de pet
* Consulta de pet
* Atualização de pet
* Exclusão de pet
* Recursos inexistentes
* Dados inválidos
* Fluxo completo de CRUD

---

# Fixtures

Os testes de integração utilizam **Fixtures** e **Collection Fixtures** para compartilhar o contexto necessário entre os testes.

A organização evita duplicação de configuração e permite reutilizar a infraestrutura de testes.

---

# Nomenclatura dos testes

Os testes seguem o padrão:

```text
MetodoTestado_Cenario_ResultadoEsperado
```

Exemplos:

```text
GetById_PetExistente_RetornaSucesso
```

```text
GetById_PetNaoExistente_RetornaNotFound
```

```text
Create_DadosInvalidos_RetornaBadRequest
```

```text
Update_PetExistente_RetornaNoContent
```

---

# Executando os testes

Para executar todos os testes:

```powershell
dotnet test
```

Para executar somente os testes unitários:

```powershell
dotnet test PetCare.Tests.Unit
```

Para executar somente os testes de integração:

```powershell
dotnet test PetCare.Tests.Integration
```

---

# Cobertura de testes

A cobertura pode ser gerada utilizando:

```powershell
dotnet test --collect:"XPlat Code Coverage"
```

Após a execução, são gerados arquivos:

```text
coverage.cobertura.xml
```

nos projetos de teste.

A execução atual possui **28 testes automatizados**, todos passando:

```text
Total: 28
Falharam: 0
Bem-sucedidos: 28
Ignorados: 0
```

---

# Validações realizadas

Durante a Sprint 3 foram realizados testes e validações dos seguintes recursos:

* Health Check da API
* Health Check do Oracle
* Health Check de serviço externo
* Cenário de Oracle indisponível
* Cenário de serviço externo indisponível
* Logs Information
* Logs Warning
* Logs Error
* Logs no Console
* Logs em arquivo
* Correlation ID
* Logging de requisições HTTP
* Logging de exceções
* Distributed Tracing
* Instrumentação do ASP.NET Core
* Instrumentação HTTP
* Instrumentação Entity Framework / Oracle
* Métricas de requisições
* Métricas de tempo de resposta
* Métricas de erros
* Autenticação JWT
* Testes unitários
* Testes de integração
* Testes de autenticação
* Testes de CRUD
* Testes AAA
* Fixtures
* Collection Fixtures
* Cobertura de testes

