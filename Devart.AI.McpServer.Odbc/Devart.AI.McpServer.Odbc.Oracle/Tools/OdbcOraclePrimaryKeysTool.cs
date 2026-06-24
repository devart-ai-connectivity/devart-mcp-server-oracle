// --------------------------------------------------------------------------
// <copyright file="OdbcOraclePrimaryKeysTool.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Devart.AI.McpServer.Extensions;
using Devart.AI.McpServer.Interfaces;
using Devart.AI.McpServer.Tools;

namespace Devart.AI.McpServer.Odbc.Oracle.Tools
{
  internal sealed class OdbcOraclePrimaryKeysTool(McpConfiguration serverConfiguration) : PrimaryKeysTool(serverConfiguration)
  {
    protected override async Task<DataTable> GetMetadataTable(
      DbConnection connection,
      string schema,
      string tableName,
      IServiceProvider services,
      CancellationToken cancellationToken)
    {
      const string sql =
"""
SELECT
  c.CONSTRAINT_NAME AS "PK_NAME",
  cc.COLUMN_NAME    AS "COLUMN_NAME"
FROM
  ALL_CONSTRAINTS c,
  ALL_CONS_COLUMNS cc
WHERE
  c.OWNER = ?
  AND c.TABLE_NAME = ?
  AND c.CONSTRAINT_TYPE = 'P'
  AND c.OWNER = cc.OWNER
  AND c.CONSTRAINT_NAME = cc.CONSTRAINT_NAME
ORDER BY c.CONSTRAINT_NAME, cc.POSITION
""";

      var database = services.GetRequiredService<IDatabase>();
      var commandHelper = services.GetRequiredService<ICommandHelper>();

      await using var reader = await database.ExecuteReaderAsync(
        connection,
        sql,
        cmd =>
        {
          commandHelper.AddParameter(cmd, schema);
          commandHelper.AddParameter(cmd, tableName);
        },
        cancellationToken
      ).ConfigureAwait(false);

      return await reader.ToDataTableAsync(OdbcConstants.PrimaryKeysCollectionName, cancellationToken).ConfigureAwait(false);
    }
  }
}
