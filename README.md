![balta](https://baltaio.blob.core.windows.net/static/images/dark/balta-logo.svg)

## 🎖️ Desafio
**Caça aos Bugs 2024** é a sexta edição dos **Desafios .NET** realizados pelo [balta.io](https://balta.io). Durante esta jornada, fizemos parte da equipe __BugBusters__ onde resolvemos todos os bugs de uma aplicação e aplicamos testes de unidade no projeto.

## 📱 Projeto
Depuração e solução de bugs, pensamento crítico e analítico, segurança e qualidade de software aplicando testes de unidade.

## Participantes
### 🚀 Líder Técnico
Henrique Weege - https://github.com/henriqueweege

### 👻 Caçadores de Bugs
* Jean Cesar - https://github.com/jeancesar
* Henrique Weege - https://github.com/henriqueweege
* Dhionys Soares - https://github.com/dhionys-soares
* Tiago de Carvalho - https://github.com/chapolin-sc

## ⚙️ Tecnologias
* C# 12
* .NET 8
* ASP.NET
* Minimal APIs
* Blazor Web Assembly
* xUnit
* Selenium WebDriver
* Testcontainers

## 🥋 Skills Desenvolvidas
* Comunicação
* Trabalho em Equipe
* Networking
* Muito conhecimento técnico

## 🧪 Como testar o projeto

**Dima**
Caso queira testar manualmente:
1. Clonar o projeto;
2. Caso esteja com o banco de dados criado, deletar;
3. Definir a connection string e a flag "ShouldRunMigrations" como true no appsettings.json do projeto Dima.Api;
5. Executar ambos os projetos Dima.Api e Dima.Web:
   - dotnet clean
   - dotnet restore
   - dotnet build
   - dotnet run

Caso queira testar através dos testes E2E **WINDOWS E LINUX**:
1. Clonar o projeto;
2. Caso não tenha o Docker instalado, instalar;
3. Rodar o Docker;
4. Definir a connection string e a flag "ShouldRunMigrations" como true no appsettings.json do projeto Dima.Api;
5. Executar ambos os projeto Dima.E2ETests:
   - dotnet clean
   - dotnet restore
   - dotnet build
   - dotnet test

Caso queira testar através dos testes E2E **OUTROS SO**:
1. Clonar o projeto;
2. Estar com banco de dados rodando;
3. Comentar as seguintes linhas **18, 20 e 21** da classe **InfrastructureHandler** do projeto **Dima.E2ETests**:
   
   ![image](https://github.com/user-attachments/assets/f2270702-4143-48af-9eb9-06f41fc1fc54)

4. Rodar o Docker;
5. Definir a connection string e a flag "ShouldRunMigrations" como true no appsettings.json do projeto Dima.Api
6. Executar ambos os projeto Balta.Domain.Test:
   - dotnet clean
   - dotnet restore
   - dotnet build
   - dotnet test


**IMPORTANTE**
O projeto de testes E2E foi desenvolvido pensando na pipeline de CI e faltam processos de CleanUp. Se for rodar local, é necessário encerrar os processos manualmente depois de rodar.
Caso queira ver o Selenium realizar os passos comente a linha 23 da classe **InfrastructureHandler** do projeto **Dima.E2ETests**

![image](https://github.com/user-attachments/assets/6d559e19-5660-4fed-a37b-449a43500666)


**Balta**
1. Clonar o projeto;
2. Executar ambos os projeto Dima.E2ETests:
   - dotnet clean
   - dotnet restore
   - dotnet build
   - dotnet test
# 💜 Participe
Quer participar dos próximos desafios? Junte-se a [maior comunidade .NET do Brasil 🇧🇷 💜](https://balta.io/discord)
