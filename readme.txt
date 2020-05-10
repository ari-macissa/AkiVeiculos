AkiVeiculos (testes e aprendizado)
Desenvolvido por Ariclines Massissa Antonio

Dados importantes:

1.0 Banco de dados utilizado: MySQL;
1.1 Nome do banco: akiveiculosappdb;
1.2 Usuário / senha: desenvolvedor / 1234567.

2.0 Framework utilizado: Asp.Net Core MVC 3.1;
2.1 ORM: Entity Framework 3.1;
2.2 Driver de banco: Pomelo.EntityFrameworkCore.MySql 3.1;

3.0 Usuário / senha para realizar os cruds na aplicação: admin / 123456;
3.1 URL para obter anúncio em formato JSON: https://localhost:5001/Api/Anuncio/<id> (substitua "<id>" pelo número correspondente, exemplo 1).

Importante: Para criar e setar dados na base precisa criar o usuário com a senha especificada acima, ou configurar com um usuário próprio já existente; depois é só  sincronizar a migration que já vem pronta: dotnet ef database update; e por fim rodar a aplicação: dotnet run

E é isso, breve novos experimentos e / ou melhorias.