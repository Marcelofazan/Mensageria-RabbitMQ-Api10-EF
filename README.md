## 🔌 RabbitMQprodutor
Exemplo de comunicação API por mensageria com RabbitMQ em C# ASP.NET Core 10 com banco de dados SQLite.

| Tecnologia | Descrição     |
|----------------|-----------|
| **Consumidor**|A mensagem é armazenada com segurança em uma fila até que o consumidor (o serviço de destino) esteja pronto para pegá-la e processá-la. |
| **Mensageria**|  mecanismo de comunicação onde sistemas trocam dados enviando mensagens para filas (queues) ou tópicos (topics), em vez de fazerem chamadas diretas síncronas (como um HTTP REST tradicional) |
| **Produtor**| É a aplicação ou serviço que gera a mensagem e a envia para o RabbitMQ |

#### 🔍 Requisitos do Projeto
- Necessário **Docker** instalado.

## 📁 RabbitMQprodutor
#### 🔄 Executar a aplicação 

VSCode Terminal [1]
```bash 
docker-compose up --build 
```

VSCode Terminal [2]
```bash 
cd RabbitMQprodutor
dotnet run 
```

| Host | URL |
|-----------|-----------|
| **API**      | http://localhost:5010/swagger/index.html  |
| **RabbitMQ** | http://localhost:15672/ |

Queues and Streams -> Em Queues , existe uma tabela Overview na coluna name , clique em **(fila)**
Desça até o botão Get Message(s) , voçê encontrará o Json enviado.

## 📁  RabbitMQconsumir
#### 🔄 Executar a aplicação 

VSCode Terminal [3]
```bash 
cd RabbitMQconsumir
dotnet add RabbitMQconsumir.csproj package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add CriarBancoInicial
dotnet ef database update
dotnet run 
```
Os dados do arquivo **Json** serão gravados no SQLite. O consumer e a tabela somente são atualizados, se estiver em funcionamento, caso contrário, fica na fila de espera dentro do RabbitMQ.
