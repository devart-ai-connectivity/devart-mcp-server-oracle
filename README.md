[![Devart MCP Server for Oracle](https://github.com/devart-ai-connectivity/.github/blob/main/assets/cover-banner-mcp-server-for-oracle.webp?raw=true)](https://www.devart.com/mcp/)

# Devart MCP Server for Oracle

Devart MCP Server for Oracle enables AI clients to interact with your data through a secure server running in your environment. It turns a regular AI chat into a practical way to work with real-world business data — and it is faster than conventional export or manual querying.

## Key benefits

Devart MCP Server for Oracle allows you to:

* Work with data intuitively through natural language.
* Retrieve the required data for analysis within minutes.
* Report on your data faster with AI-powered assistance.
* Minimize manual data handling and integration maintenance.

## How it works

Devart MCP Server for Oracle helps AI clients communicate directly with Oracle databases using natural-language prompts. It translates AI requests into structured queries, executes them through Devart connectivity drivers, and returns clean, structured results for seamless AI-powered data access.

![Devart MCP Server architecture](https://github.com/devart-ai-connectivity/.github/blob/main/assets/how_mcp_works_single.webp?raw=true)

## Quick start

To get started with Devart MCP Server for Oracle:

1\. [Download](https://www.devart.com/odbc/oracle/download.html) and [install](https://docs.devart.com/odbc-driver-for-oracle/installation/) Devart ODBC Driver for Oracle.

2\. [Download](https://www.devart.com/mcp/download.html) and [install](https://docs.devart.com/mcp/installation.html) Devart MCP Server for Oracle.

3\. In Devart MCP Server for Oracle, [configure your data connection and integration settings](https://docs.devart.com/mcp/connection-configuration.html).

![Devart MCP Server configuration](https://github.com/devart-ai-connectivity/.github/blob/main/assets/mcp-servers-gui.webp?raw=true)

4\. Run your first natural-language query.

[![Need an MCP Server for multiple data sources?](https://github.com/devart-ai-connectivity/.github/blob/main/assets/need-mcp-server-universal.webp?raw=true)](https://www.devart.com/mcp/universal/)


## Manual installation and configuration 

**Prerequisites** 

Before building and running Devart MCP Server for Oracle, ensure the following components are installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* **ADO.NET connection** — **Devart.AI.McpServer.AdoNet.Oracle.csproj** Devart dotConnect for Oracle (installed automatically via NuGet during build)
* **ODBC connection** — **Devart.AI.McpServer.Odbc.Oracle.csproj** [Devart ODBC Driver for Oracle](https://www.devart.com/odbc/oracle/download.html) (requires manual download and installation)

**Step 1: Clone the repository**

Clone the project repository and navigate to the project directory:

1\. Open **Command Prompt**.

2\. Enter the following command:

```cmd
git clone https://github.com/devart-ai-connectivity/devart-mcp-server-oracle.git
cd devart-mcp-server-oracle
```

**Step 2: Build the MCP Server from source**

You can build Devart MCP Server for Oracle from source using either of the supported database connectivity technologies: ADO.NET or ODBC.

* To build the MCP server with ADO.NET, run the following command:

```cmd
dotnet publish Devart.AI.McpServer.AdoNet/Devart.AI.McpServer.AdoNet.Oracle/Devart.AI.McpServer.AdoNet.Oracle.csproj -c ReleaseOracle /p:TargetFramework=net8.0
```
The Devart dotConnect for Oracle NuGet package is downloaded and restored automatically.

* To build the MCP server with ODBC, select the command based on the bitness of your data source.

For 64-bit data source, run the following command:

```cmd
dotnet publish Devart.AI.McpServer.Odbc/Devart.AI.McpServer.Odbc.Oracle/Devart.AI.McpServer.Odbc.Oracle.csproj -c ReleaseOracle -r "win-x64" /p:TargetFramework=net8.0
```

For 32-bit data source, run the following command:

```cmd
dotnet publish Devart.AI.McpServer.Odbc/Devart.AI.McpServer.Odbc.Oracle/Devart.AI.McpServer.Odbc.Oracle.csproj -c ReleaseOracle -r "win-x86" /p:TargetFramework=net8.0
```
>**Note**
>
>The target platform must match the bitness of your ODBC data source.

**Step 3: Configure the database connection for the MCP Server**

1\. Create an `mcpserver.json` configuration file in the directory containing the built MCP Server executable.

2\. In the file, configure the database connection.

* Configure a connection with ADO.NET.

Add the following configuration to the `mcpserver.json` file:

```json
{
  "Connections": [
    {
      "Name": "my_oracle",
      "ConnectionString": "Server=localhost;User Id=oracle;Password=your_password;Database=your_database;",
      "ProtocolType": "stdio"
    }
  ]
}
```

* Configure a connection with ODBC.

Add the following configuration to the `mcpserver.json` file:

```json
{
  "Connections": [
    {
      "Name": "my_oracle",
      "DsnName": "your_dsn_name",
      "ProtocolType": "stdio"
    }
  ]
}
```

* Configure a connection with ODBC using a connection string.

Add the following configuration to the `mcpserver.json` file:

```json
{
  "Connections": [
    {
      "Name": "my_oracle",
      "ConnectionString": "Driver={Devart ODBC Driver for Oracle};Server=localhost;User ID=oracle;Password=your_password;Database=your_database;",
      "ProtocolType": "stdio"
    }
  ]
}
```
where:

* `Name` — The connection name.

* `ConnectionString` (applies to ADO.NET) — A database-specific connection string used to establish a connection to the target database.

* `DsnName` (applies to ODBC) — The name of your data source.

* `ProtocolType` — A transport protocol. The possible options are: `stdio` or `http`.

* `HttpPort` (required if `ProtocolType` is set to `http`) — The port number for the `http` protocol. 

**Step 4: Run the MCP server**

After you configure the MCP Server, you can start it. 

>**Note**
>
>This step is required only when `ProtocolType` is configured as `http`. If you use the `stdio` transport protocol, your AI client starts the server automatically.

* To start the server with ADO.NET, run the following command:

```cmd
Devart.AI.McpServer.AdoNet.Oracle.exe run my_oracle
```

* To start the server with ODBC, run the following command:

```cmd
Devart.AI.McpServer.Odbc.Oracle.exe run my_oracle
```

where `my_oracle` is the name of the ODBC connection.

**Step 5: Integrate with Claude Desktop**

1\. Open `claude_desktop_config.json`, the Claude configuration file.

>**Tip**
>
>If you can't locate the configuration file, it may not exist yet. To create it, open Claude Desktop and navigate to **File** > **Settings** > **Developer**, then click **Edit Config**. The folder with the `claude_desktop_config.json` file opens.

2\. Add one of the following objects, depending on the transport protocol used by MCP Server:

* STDIO

```json
{
  "mcpServers": {
    "devart": {
      "command": "C:\\path\\to\\Devart.AI.McpServer.AdoNet.Oracle.exe",
      "args": [
        "run",
        "my_oracle"
      ]
    }
  }
}
```

 where:

  * `devart` is the connector name that will appear in Claude Desktop.
  * `C:\\path\\to\\Devart.AI.McpServer.AdoNet.Oracle.exe` is the path to the executable file. For an ODBC connection, use `Devart.AI.McpServer.Odbc.Oracle.exe`.
  * `my_oracle` is the connection name specified in the `mcpserver.json` configuration file.

* **HTTP**

  ```json
    "mcpServers": {
      "devart": {
        "command": "npx",
        "args": [
          "-y",
          "mcp-remote",
          "http://localhost:5000/sse"
        ]
      }
    }
  ```

  where:

  * `devart` is the connector name that will appear in Claude Desktop.
  * `5000` is the MCP Server listening port.

3\. Save the file.

4\. Restart Claude Desktop.

Devart MCP Server for Oracle is now integrated with Claude, and **devart** appears in the Claude Desktop app in **Customize** > **Connectors**.

You can also [integrate](https://docs.devart.com/mcp/ai-integration/) Devart MCP Server for Oracle with other AI clients such as Cline, Codex, Cursor, Visual Studio Code, Windsurf, Zed.

## Supported clients

Devart MCP Server for Oracle supports integration with the following AI clients: 
 
* Claude Desktop
* Visual Studio Code
* Cursor
* Codex
* Windsurf
* Cline
* Zed
* ...and other MCP-compatible AI clients

## Typical use cases

Devart MCP Server for Oracle is a practical fit for teams working with Oracle as their primary data source.

* **Enterprise ERP data access**  
  Let business users query Oracle ERP data, including financials, procurement, HR, and manufacturing records, in natural language without involving IT.

* **Core banking and financial services analytics**  
  Access account data, transaction records, risk indicators, and regulatory reporting data stored in Oracle databases used in banking and insurance.

* **DBA and developer productivity**  
  Help Oracle DBAs and developers explore complex schemas, analyze query plans, investigate data issues, and document database objects faster with AI.

* **Large-scale reporting without BI tools**  
  Answer ad-hoc business questions from Oracle data warehouses and data marts directly, without building custom reports or waiting for BI team capacity.

* **Data quality and governance investigation**  
  Use AI to scan Oracle tables for anomalies, inconsistencies, and data quality issues across large enterprise datasets.

* **Secure on-premises AI access to Oracle**  
  Maintain full data sovereignty over Oracle workloads while enabling controlled AI access to enterprise data without SaaS intermediaries.

## Licensing and activation

Devart MCP Server for Oracle is distributed as a free single-source MCP server.

To connect to Oracle, the server requires the corresponding [Devart ODBC Driver for Oracle](https://www.devart.com/odbc/oracle/), which is a paid product.

A 30-day free trial is available for the Devart ODBC Driver for Oracle.

See the product page and documentation for the latest installation and activation details.

## Support

* [Documentation](https://docs.devart.com/mcp/)
* [Submit a request](https://www.devart.com/company/contactform.html)
* [Suggest a feature](https://devart.uservoice.com/)
* [Join our forum](https://support.devart.com/portal/en/community)

## Other Devart connectivity solutions

* [MCP Server Universal](https://github.com/devart-ai-connectivity/devart-mcp-server-universal)
* [ODBC Driver for Oracle](https://www.devart.com/odbc/oracle/)
* [dotConnect ADO.NET Provider for Oracle](https://www.devart.com/dotconnect/oracle/)
