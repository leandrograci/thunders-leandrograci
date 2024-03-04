# Sistema TASKGO de Lista de Tarefas da Thunders em C# com PostgreSQL com Docker

Este é um projeto de backend que implementa um Sistema de Lista de Tarefas implementado em ASP.NET Core e Postgresql em Docker para avaliação de processo seletivo.

## **Funcionalidades**
* Cadastro de Usuário: Os usuários devem ser cadastrados com um código único, login, senha e status (ativo ou inativo).
* Atualização de Informações de Usuários: É possível atualizar as informações de usuário, somente senha e status (ativo ou inativo).
* Listagem de Usuários: O sistema oferece a funcionalidade de listar todos os usuário cadastrados, exibindo seus login e status.

* * Cadastro de Tarefas: As tarefas devem ser cadastradas com um código único, nome e período. Toda tarefa deve ter um usuário relacionado.
* Atualização de Informações de uma Tarefa: É possível atualizar as informações da tarefa.
* Remoção de Tarefa: As tarefas podem ser removidas do sistema.
* Listagem de Tarefas: O sistema oferece a funcionalidade de listar todas as tarefas cadastradas, exibindo seus códigos, nomes e demais informações.


## **Requisitos**
* SO Windows e Ter instalado o Docker no Windows.

## Passos para execução do sistema
* Abrir o Terminal e navegar até a pasta Thunders.TaskGo.API
* Executar o comando no Terminal: docker-compose up -d
* Após isso executar os comandos de criação da estrutura do banco de dados:
* dotnet ef migrations add TaskGoMigration --verbose --project .\Thunders.TaskGo.Infra\   --startup-project .\Thunders.TaskGo.API\
* dotnet ef database update --verbose --project .\Thunders.TaskGo.Infra\   --startup-project .\Thunders.TaskGo.API\
* Após isso Buildar a solução e executar o projeto API em http ou https
* Todas as rotas estão tratadas para utilização do swagger.
* Inicialmente criar um Token chamando o método GerarTokenAcesso
* Utilizar o token retornado para logar no swagger e poder chamar os demais métodos para teste.
