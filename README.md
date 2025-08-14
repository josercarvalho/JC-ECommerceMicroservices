# JC-ECommerceMicroservices

Um sistema de e-commerce com arquitetura de microserviços para gerenciamento de estoque de produtos e vendas, utilizando .NET 8, DDD, CQRS e Docker.

## Visão Geral da Arquitetura

Este projeto implementa uma arquitetura de microserviços para simular um sistema de e-commerce. Ele é composto por:

-   **Microserviço de Gestão de Estoque (`Stock.API`):** Responsável por cadastrar produtos, controlar o estoque e fornecer informações sobre a quantidade disponível.
-   **Microserviço de Gestão de Vendas (`Sales.API`):** Responsável por gerenciar os pedidos e interagir com o serviço de estoque para verificar a disponibilidade de produtos ao realizar uma venda.
-   **API Gateway (`ApiGateway`):** Atua como o ponto de entrada unificado para todas as requisições, roteando-as para os microserviços apropriados.
-   **RabbitMQ:** Utilizado para comunicação assíncrona entre os microserviços, especialmente para notificações de vendas que impactam o estoque.
-   **Autenticação JWT:** Garante que apenas usuários autenticados possam interagir com os endpoints dos serviços.

## Tecnologias Utilizadas

-   **.NET 8 (C#):** Framework principal para o desenvolvimento dos microserviços.
-   **Domain-Driven Design (DDD):** Abordagem para modelar o software com base no domínio de negócio.
-   **Command Query Responsibility Segregation (CQRS):** Padrão que separa as operações de leitura (queries) das operações de escrita (commands).
-   **Entity Framework Core:** ORM para interação com o banco de dados.
-   **SQLite:** Banco de dados relacional leve, ideal para desenvolvimento e testes.
-   **MediatR:** Biblioteca para implementação do padrão Mediator, facilitando o CQRS.
-   **FluentValidation:** Biblioteca para validação de objetos de forma fluente.
-   **xUnit:** Framework para testes unitários e de integração.
-   **Docker & Docker Compose:** Para containerização e orquestração dos microserviços.
-   **Ocelot:** API Gateway para roteamento de requisições.
-   **RabbitMQ:** Message broker para comunicação assíncrona.
-   **JWT (JSON Web Tokens):** Para autenticação e autorização.

## Funcionalidades

### Microserviço de Estoque (`Stock.API`)

-   **Cadastro de Produtos:** Adicionar novos produtos com nome, descrição, preço e quantidade em estoque.
-   **Consulta de Produtos:** Consultar o catálogo de produtos e a quantidade disponível.
-   **Atualização de Estoque:** O estoque é atualizado automaticamente após uma venda (via comunicação com o serviço de Vendas).

### Microserviço de Vendas (`Sales.API`)

-   **Criação de Pedidos:** Permitir que clientes façam pedidos, com validação de estoque antes da confirmação.
-   **Consulta de Pedidos:** Consultar o status dos pedidos realizados.
-   **Notificação de Venda:** Notifica o serviço de estoque sobre a redução de produtos após a confirmação de um pedido.

## Primeiros Passos

### Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas em sua máquina:

-   [Docker Desktop](https://www.docker.com/products/docker-desktop)
-   [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

### Configuração

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/seu-usuario/JC-ECommerceMicroservices.git
    cd JC-ECommerceMicroservices
    ```

2.  **Construa a solução (opcional, mas recomendado para verificar a compilação):**
    ```bash
    dotnet build
    ```

### Executando a Aplicação com Docker Compose

Para subir todos os microserviços e o RabbitMQ:

```bash
docker-compose up --build
```

Este comando irá:
-   Construir as imagens Docker para `stock.api`, `sales.api` e `apigateway`.
-   Baixar a imagem do `rabbitmq`.
-   Iniciar todos os contêineres e configurar a rede interna.

### Acessando os Serviços

Após a inicialização, os serviços estarão acessíveis através do API Gateway:

-   **API Gateway:** `http://localhost:5000`
-   **Endpoints de Estoque (via Gateway):**
    -   `http://localhost:5000/stock/api/products` (GET, POST, PUT, DELETE)
    -   `http://localhost:5000/stock/api/products/{id}/decrease-stock` (PUT)
    -   `http://localhost:5000/stock/api/products/{id}/increase-stock` (PUT)
-   **Endpoints de Vendas (via Gateway):**
    -   `http://localhost:5000/sales/api/orders` (GET, POST)
    -   `http://localhost:5000/sales/api/orders/{id}/status` (PUT)

**Observação sobre Autenticação:** Todos os endpoints da API exigem autenticação JWT. Você precisará de um token válido para interagir com eles. A implementação atual não inclui um serviço de autenticação/login, mas os microsserviços estão configurados para validar tokens JWT emitidos por um `Issuer` e `Audience` definidos em seus `appsettings.json`.

### Parando a Aplicação

Para parar e remover todos os contêineres, redes e volumes criados pelo Docker Compose:

```bash
docker-compose down -v
```

## Estrutura do Projeto

```
JC-ECommerceMicroservices/
├── src/
│   ├── ApiGateway/
│   │   └── ApiGateway/             # Projeto do API Gateway (Ocelot)
│   ├── Sales/
│   │   ├── Sales.API/             # Camada de Apresentação (REST API)
│   │   ├── Sales.Application/     # Camada de Aplicação (CQRS, Handlers, Validators)
│   │   ├── Sales.Domain/          # Camada de Domínio (Entidades, Interfaces)
│   │   └── Sales.Infrastructure/  # Camada de Infraestrutura (EF Core, Repositórios)
│   └── Stock/
│       ├── Stock.API/
│       ├── Stock.Application/
│       ├── Stock.Domain/
│       └── Stock.Infrastructure/
├── tests/
│   ├── Sales/
│   │   ├── Sales.Tests.Integration/ # Testes de Integração para Sales.API
│   │   └── Sales.Tests.Unit/        # Testes Unitários para Sales.Application/Domain
│   └── Stock/
│       ├── Stock.Tests.Integration/
│       └── Stock.Tests.Unit/
├── docker-compose.yml             # Orquestração Docker
└── README.md                      # Este arquivo de documentação
```

## Executando Testes

Para executar todos os testes unitários e de integração:

```bash
dotnet test
```

## Próximos Passos e Melhorias Potenciais

-   **Serviço de Autenticação:** Implementar um microserviço dedicado para autenticação e geração de tokens JWT.
-   **Comunicação Assíncrona Completa:** Finalizar a integração do RabbitMQ para notificação de estoque e outras comunicações assíncronas.
-   **Interface de Usuário (UI):** Desenvolver uma aplicação frontend (e.g., React, Angular, Vue) para interagir com o API Gateway.
-   **Monitoramento e Logging:** Integrar ferramentas como Prometheus/Grafana ou ELK Stack para monitoramento e análise de logs.
-   **Serviço de Pagamento:** Adicionar um novo microserviço para processamento de pagamentos.
-   **Testes de Carga:** Realizar testes de carga para avaliar a escalabilidade do sistema.
-   **CI/CD:** Configurar pipelines de Integração Contínua/Entrega Contínua para automação de builds e deploys.
