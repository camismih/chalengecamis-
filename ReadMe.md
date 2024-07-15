# Case Transferência

Este repositório contém a implementação de uma API de cadastro de clientes/contas e transferências de valores entre eles.

## Tecnologias

As tecnologias utilizadas na implementação do projeto foram:

* [.NET 8](https://learn.microsoft.com/pt-br/dotnet/)
* [ASP.NET 8](https://learn.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-8.0)
* [Minimal APIs](https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-8.0)
* [EntityFramework Core 8](https://learn.microsoft.com/pt-br/ef/core/)
* [XUnit](https://xunit.net)

## Estrutura

A solução foi dividida em dois projetos:

* Camila.Api: implementação da API e as regras de negócio
* Camila.Api.Tests: testes de unidade e integrados da API e seus componentes

## Execução do projeto

O único requisito para execução da API é o [.NET 8 SDK](https://dot.net/downloads).

Para executar a APi, dentro da pasta `Camila.Api` digite:

```powershell
dotnet run -lp https
```

Já para executar os testes, dentro da pasta `Camila.Api.Tests` execute:

```powershell
dotnet test
```

## Notas

*Todos os endpoints foram definidos no arquivo `Program.cs` e de forma evolutiva sua implementação distribuida entre outros componentes
* Para facilitar o entendimento foi utilizada uma estrutura muito parecida com os projetos **MVC** onde os `DTOs` foram implementados na pasta `Models` e a camada de persistência na pasta `Data`
* Foi utilizada injeção de dependência para implementação de dois repositórios  para aumentar a testabilidade