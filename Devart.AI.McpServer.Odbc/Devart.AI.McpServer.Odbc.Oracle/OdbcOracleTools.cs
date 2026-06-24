// --------------------------------------------------------------------------
// <copyright file="OdbcOracleTools.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

using System.Collections.Generic;
using ModelContextProtocol.Server;
using Devart.AI.McpServer.Odbc.Oracle.Tools;

namespace Devart.AI.McpServer.Odbc.Oracle
{
  internal static class OdbcOracleTools
  {
    public static List<McpServerTool> CreateTools(McpConfiguration configuration)
      => OdbcTools.CreateBuilder(configuration)
        .Add(new OdbcOraclePrimaryKeysTool(configuration))
        .Add(new OdbcOracleForeignKeysTool(configuration))
        .Build();
  }
}
