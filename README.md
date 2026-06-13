# Mensageria-RabbitMQ-SQLite
Exemplo de criação API de Comunicação por Mensagaria com RabbitMQ em C# NET Core 10 com banco de dados SQLite.

VSCode Terminal [1]
```bash 
docker-compose up --build 
```

VSCode Terminal [2]
```bash 
cd RabbitMQprodutor
dotnet run 
```

VSCode Terminal [3]
```bash 
cd RabbitMQconsumir
dotnet add RabbitMQconsumir.csproj package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add CriarBancoInicial
dotnet ef database update
dotnet run 
```


http://localhost:5010/swagger/index.html

http://localhost:15672/



RabbitMQ -> Queues and Streams

Em Queues , existe uma tabela Overview na coluna name , clique em **(fila)**

Desça até o botão Get Message(s) , voçê encontrará o Json enviado. 