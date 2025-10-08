

# Future Now Streaming Platform

## Visão Geral

**Future Now** é uma plataforma de streaming que fornece acesso a faixas de música, conteúdo em vídeo e podcasts.
O sistema consiste em um backend de API REST construído com **ASP.NET Core** e um aplicativo móvel Android que consome essa API.
A plataforma permite que os usuários naveguem, pesquisem e filtrem conteúdo multimídia em diferentes categorias.

**Status**: ✅ MVP Concluído (6 de outubro de 2025)

* API RESTful com operações CRUD completas para **Music**, **Videos** e **Podcasts**
* Banco de dados em memória preenchido com conteúdo de exemplo
* Documentação Swagger disponível em [http://localhost:5000/](http://localhost:5000/)
* Aplicativo Android pronto para implantação no Android Studio

---

## Preferências do Usuário

Estilo de comunicação preferido: linguagem simples e cotidiana.

---

## Arquitetura do Sistema

### Arquitetura do Backend

**Framework**: ASP.NET Core 8.0 Web API

* Implementação utilizando o padrão **minimal API** ou **MVC**
* Configurado para endpoints HTTP e HTTPS (portas **5088** e **7219**)
* Integração com **Swagger/OpenAPI** para documentação e testes da API
* Ambientes de desenvolvimento e produção configurados separadamente

**Armazenamento de Dados**: Entity Framework Core com banco de dados em memória

* **Justificativa**: Usa **EF Core 9.0.9** com provedor in-memory para desenvolvimento rápido
* **Vantagens**: Zero configuração, sem dependência externa
* **Desvantagens**: Dados não persistem após reiniciar a aplicação
* **Alternativa**: Fácil migração futura para **SQL Server** ou **PostgreSQL** via abstração do EF Core

**Design da API**:

* Endpoints RESTful para **music**, **videos** e **podcasts**
* Suporte a busca e filtros
* Configuração de **CORS** para o app Android
* Serialização em **JSON**

**Log e Diagnóstico**:

* **Microsoft.Extensions.Logging** para registro de logs
* Diferenciação de níveis de log por ambiente
* Suporte a **DiagnosticSource** para monitoramento

---

### Arquitetura do Frontend

**Plataforma**: Aplicativo Android Nativo (Java)

* SDK mínimo e alvo configurados para versões recentes do Android
* Uso de **Material Design Components** para consistência visual

**Padrão de UI**: Interface com abas

* **MainActivity** gerencia a navegação entre abas
* **ContentDetailActivity** exibe detalhes dos conteúdos
* **RecyclerView** para exibição eficiente de listas

**Camada de Dados**:

* Classes modelo representando respostas da API (**music tracks**, **videos**, **podcasts**)
* **Retrofit 2.x** como cliente HTTP
* Conversores JSON integrados ao Retrofit

**Padrão Adapter**:

* Adapters personalizados para cada tipo de conteúdo
* Padrão **ViewHolder** para otimizar desempenho

---

### Comunicação Entre os Componentes

**Configuração do Cliente da API**:

* **Base URL** definida em `ApiClient.java`
* Emulador Android: `http://10.0.2.2:5000/` (mapeia o localhost da máquina hospedeira)
* Dispositivo físico: usar o IP local da máquina rodando a API
* Interfaces Retrofit definem os contratos dos endpoints

**Fluxo de Dados**:

1. O app Android envia requisições HTTP via Retrofit
2. A API processa as requisições e acessa o banco de dados via EF Core
3. A API retorna respostas em JSON
4. Retrofit desserializa os dados em objetos modelo
5. Os adapters exibem os dados nas RecyclerViews

---

## Dependências Externas

### Backend (ASP.NET Core)

**Pacotes NuGet**:

* `Microsoft.AspNetCore.OpenApi` (8.0.18)
* `Microsoft.EntityFrameworkCore.InMemory` (9.0.9)
* `Swashbuckle.AspNetCore` (6.6.2)
* `Microsoft.Extensions.Caching.Memory`
* `Microsoft.Extensions.Logging`

**Runtime**:

* .NET 8.0 runtime
* ASP.NET Core 8.0 runtime

---

### Android

**Bibliotecas**:

* Android SDK (Java-based)
* Retrofit 2
* Material Design Components
* RecyclerView

---

## Requisitos de Infraestrutura

**Ambiente de Desenvolvimento**:

* SDK do .NET 8.0
* Android Studio
* Emulador ou dispositivo físico Android

**Rede**:

* Conexão local necessária para dispositivos físicos
* Ajustar firewall se necessário
* CORS habilitado na API para aceitar o app Android

**Ferramentas de Build**:

* **MSBuild/.NET CLI** para o backend
* **Gradle** para o app Android

---

## 🧩 Como Instalar as Dependências (.NET e Java)

### 🟦 Backend (.NET 8)

1. **Instalar o SDK do .NET 8.0**

   * Acesse: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
   * Escolha o instalador de acordo com seu sistema operacional (Windows, Linux, macOS)

2. **Verificar instalação**

   ```bash
   dotnet --version
   ```

   Deve exibir algo como `8.0.x`.

3. **Restaurar dependências do projeto**
   No diretório do backend (onde está o `.csproj`):

   ```bash
   dotnet restore
   ```

4. **Executar a aplicação localmente**

   ```bash
   dotnet run
   ```

   A API estará disponível em:

   * HTTP: `http://localhost:5088`
   * HTTPS: `https://localhost:7219`
   * Swagger: `http://localhost:5000/`

---

### 🟩 Frontend (Android / Java)

1. **Instalar o Android Studio**

   * Baixe em: [https://developer.android.com/studio](https://developer.android.com/studio)

2. **Abrir o projeto no Android Studio**

   * Vá em **File → Open** e selecione o diretório do app Android.

3. **Sincronizar dependências**
   O Android Studio detectará o arquivo `build.gradle` e baixará automaticamente as bibliotecas necessárias (**Retrofit**, **RecyclerView**, etc.).

4. **Executar o aplicativo**

   * Selecione um **emulador Android** ou conecte um **dispositivo físico**.
   * Clique em **Run ▶** para iniciar o app.
   * O app se conectará ao backend em `http://10.0.2.2:5000/`.

---

## ⚙️ Como Acessar e Testar o Swagger no Localhost

1. **Certifique-se de que o backend está em execução**

   ```bash
   dotnet run
   ```

2. **Abra o navegador e acesse:**

   ```
   http://localhost:5000/
   ```

   ou, conforme a configuração,

   ```
   https://localhost:7219/swagger
   ```

3. **Interface Swagger**

   * A interface exibe todos os endpoints da API.
   * Cada rota possui botões **GET**, **POST**, **PUT**, **DELETE**.
   * Clique em **Try it out** para enviar requisições diretamente.

4. **Testes recomendados:**

   * `GET /api/music` → Lista de músicas
   * `GET /api/videos` → Lista de vídeos
   * `POST /api/podcasts` → Criação de novo podcast

5. **Verificação da comunicação com o app Android:**

   * Inicie o app no emulador
   * O app deve exibir o conteúdo retornado pela API via Retrofit

🛠️ **Solução de Problemas (Troubleshooting)**

🚫 Problema: O app Android não consegue se conectar à API

Causa possível: o emulador não reconhece localhost.
Solução:

Use http://10.0.2.2:5000/ no ApiClient.java.

Se estiver em um dispositivo físico, substitua por http://<IP_LOCAL_DA_MAQUINA>:5000/.

🔒 Problema: Erro de CORS (Cross-Origin Request Blocked)

Causa: A API não permite requisições externas.
Solução:
No arquivo Program.cs, adicione:

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");

⚠️ Problema: Porta já está em uso

Causa: Outro processo está usando a porta 5000, 5088 ou 7219.
Solução:

Pare o processo em uso ou

Edite o arquivo launchSettings.json e altere as portas manualmente.

📡 Problema: API funciona no navegador, mas não no dispositivo

Causa: Rede local ou firewall bloqueando o tráfego.
Solução:

Certifique-se de que o dispositivo e o servidor estão na mesma rede Wi-Fi.

Desative temporariamente o firewall ou adicione exceção para o dotnet.exe.

🧩 Problema: Erros ao compilar o projeto .NET

Causa: SDK incorreto ou pacotes ausentes.
Solução:

dotnet --list-sdks
dotnet restore
dotnet build

🧱 Problema: Dependências do Android não são baixadas

Solução:

Verifique se há internet.

Vá em File → Sync Project with Gradle Files.

Se persistir, limpe o cache:

./gradlew clean build

💡 Dica: Logs em tempo real

Para monitorar logs da API durante testes:

dotnet watch run

https://github.com/akiiller/FutureNowAPI.git
