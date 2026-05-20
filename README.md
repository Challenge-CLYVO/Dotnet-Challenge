# 🐾 PetCare API

API REST desenvolvida em ASP.NET Core 9 para gerenciamento de pets, tutores, clínicas veterinárias, consultas, vacinas e histórico de saúde.

---

# 📚 Integrantes

- Lucas Rafael Solimene / RM: 565194
- Samyr Couto Oliveira / RM: 565562
- Henrique Teixeira Cesar / RM: 563088

---

# 🚀 Tecnologias Utilizadas

- ASP.NET Core 9
- Entity Framework Core
- Oracle Database
- AutoMapper
- Swagger / OpenAPI
- C#
- REST API

---

# 🏗️ Arquitetura do Projeto

O projeto segue arquitetura em camadas:

- PetCare.API → Controllers e configuração da aplicação
- PetCare.Application → Services, DTOs, Exceptions e AutoMapper
- PetCare.Domain → Entidades
- PetCare.Infrastructure → Contexto, Repositories e persistência

---

# 📦 Funcionalidades

✅ CRUD de Tutores  
✅ CRUD de Pets  
✅ CRUD de Clínicas  
✅ CRUD de Consultas  
✅ CRUD de Vacinas  
✅ CRUD de Aplicações de Vacina  
✅ CRUD de Histórico de Saúde  

---

# 🔥 Recursos Implementados

- Repository Pattern
- Service Layer
- DTOs
- AutoMapper
- Exception Middleware
- Validação com DataAnnotations
- Swagger Documentation
- XML Comments no Swagger
- Relacionamentos com Entity Framework
- Oracle Database Integration
- Migrations do EF Core
- Operações assíncronas com async/await

---

# 🗄️ Banco de Dados

Banco utilizado:

- Oracle Database

ORM:

- Entity Framework Core

---

# ⚙️ Como Executar o Projeto

## 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
```

## 2. Acesse a pasta do projeto

```bash
cd Dotnet-Challenge
```

## 3. Configurar credenciais

No arquivo:

```bash
appsettings.json
```

Configure sua connection string do Oracle:

```bash
"ConnectionStrings": {
  "RecommendaContextOracle": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=<ID DO USUARIO>;Password=<SENHA DO USUARIO>;"
}
```

## 4. Restaurar dependências

```bash
dotnet restore
```

---

# 🧹 Preparar o Banco de Dados

## Limpar banco atual

```bash
dotnet ef database drop --project PetCare.Infrastructure --startup-project PetCare.API --force
```

## Criar banco novamente

```bash
dotnet ef database update --project PetCare.Infrastructure --startup-project PetCare.API
```

## ▶️ Executar a API

```bash
dotnet run --project PetCare.API
```

## 📖 Swagger

Após executar o projeto, acesse:

```bash
http://localhost:5100/swagger
```

---

# 📌 Endpoints Principais

## Tutor

GET /api/tutor
GET /api/tutor/{id}
POST /api/tutor
PUT /api/tutor/{id}
DELETE /api/tutor/{id}


## Pet

GET /api/pet

GET /api/pet/{id}

POST /api/pet

PUT /api/pet/{id}

DELETE /api/pet/{id}


## Clínica

GET /api/clinica

GET /api/clinica/{id}

POST /api/clinica

PUT /api/clinica/{id}


## Consulta

GET /api/consulta

GET /api/consulta/{id}

POST /api/consulta

PUT /api/consulta/{id}

DELETE /api/consulta/{id}


## Vacina

GET /api/vacina

GET /api/vacina/{id}

POST /api/vacina

PUT /api/vacina/{id}

DELETE /api/vacina/{id}

DELETE /api/clinica/{id}


## Aplicação de Vacina

GET /api/aplicacaovacina

GET /api/aplicacaovacina/{id}

POST /api/aplicacaovacina

PUT /api/aplicacaovacina/{id}

DELETE /api/aplicacaovacina/{id}


## Histórico de Saúde

GET /api/historicosaude

GET /api/historicosaude/{id}

POST /api/historicosaude

PUT /api/historicosaude/{id}

DELETE /api/historicosaude/{id}


---

# 🛡️ Tratamento de Erros

A API possui middleware global para tratamento de exceções.

Exmplo: 

```bash
{
  "statusCode": 404,
  "message": "Tutor não encontrado."
}
```

---

# ✅ Validações

A API utiliza Bean Validation com DataAnnotations.

Exemplo:

```bash
[Required(ErrorMessage = "O nome do tutor é obrigatório.")]
[StringLength(100, ErrorMessage = "O nome deve possuir no máximo 100 caracteres.")]
public string Nome { get; set; }
```
