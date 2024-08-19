## O Projeto

O projeto consiste em uma aplicação frontend em Angular que insere e consulta tarefas em uma API.
A API por sua vez consulta a base de dados para mostrar as tarefas cadastradas e envia para fila (RabbitMQ) as novas tarefas para que sejam cadastradas.
Um Worker fica responsável por ler a fila e cadastrar as novas tarefas.<br>
O desenho abaixo visa mostrar esse fluxo:

![image](https://github.com/user-attachments/assets/d157ef8d-d23e-4429-bbcf-da265ccac2fc)


## O que foi utilizado nesse projeto?

Entre projetos e bibliotecas foram utilizados:

<ul>
  <li>ASP.NET Core Web API: net 8.0</li>
  <li>Worker Service: net 8.0</li>
  <li>RabbitMQ.Client: 6.8.1</li>
  <li>Microsoft.EntityFrameworkCore: 8.0.8</li>
  <li>Microsoft.EntityFrameworkCore.Tools: 8.0.8</li>
  <li>Microsoft.EntityFrameworkCore.Design: 8.0.8</li>
  <li>Pomelo.EntityFrameworkCore.MySql: 8.0.2</li>
  <li>Serilog.Sinks.File: 6.0.0</li>
</ul>

## Prerequisitos

<ul>
  <li>Docker: https://docs.docker.com/engine/install/</li>
  <li>Dotnet core CLI: https://learn.microsoft.com/pt-br/dotnet/core/tools/</li>
</ul>

## Passos para executar/explicações:

Esse projeto tem o intuito de funcionar localmente, para isso siga os passos abaixo.

## RabbitMQ

Para utilização do RabbitMQ foi criado um container com a imagem oficial do RabbitMQ conforme comando abaixo:
	
```bash
docker run -d --hostname my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

Caso queira, poderá se conectar ao gerenciador acessando o link abaixo com o usuário e senha indicados.

```bash
http://localhost:15672/#/
username:  guest
password: guest
```

## MySQL

O MySQL foi escolhido como banco de dados e algumas configurações se fazem necessárias.

<b>ConnectionString</b>

No arquivo appsettings.json dos projetos de API e Worker estão configuradas as connectionstrings com usuário e senha "guest" (apenas para efeitos de testes), caso precise alterar é lá que irá encontrar essas configurações.
Para executar o MySQL utilizei também um containner docker, executando o comando abaixo para instalar/executar a imagem mais rececente do MySQL.

```bash
docker run -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=elumini-it -e MYSQL_USER=guest -e MYSQL_PASSWORD=guest mysql
```
Após o containner ser gerado e ser iniciado, utilize o Entity Framework através do comando abaixo para executar o Migration criado no projeto de API com o intuito de criação da tabela que iremos utilizar.

```bash
dotnet ef database update
```

<b>Importante:</b> Caso tenha o MySQL instalado localmente é necessário parar o serviço, para isso acesse o services.msc e pare o serviço correspondente ao MySQL.

Agora basta executar todos os projetos contidos na solução Elumini-it!

<b>Dica:</b> Utilizando o visual studio é possível alterar as propriedades da solution e permitir que multiplos projetos sejam executados de uma só vez.

![image](https://github.com/user-attachments/assets/5c86c125-9b98-490f-b3b1-b4127f2cbc61)
