// --------------------------------------------------------------------------
// <copyright file="OdbcOracleForeignKeysTool.cs" company="Devart">
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
using Devart.AI.McpServer.Tools;
using Devart.AI.McpServer.Interfaces;

namespace Devart.AI.McpServer.Odbc.Oracle.Tools
{
  internal sealed class OdbcOracleForeignKeysTool(McpConfiguration serverConfiguration) : ForeignKeysTool(serverConfiguration)
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
    CONS.CONSTRAINT_NAME AS "FK_NAME",
    COLS.COLUMN_NAME     AS "FKCOLUMN_NAME",
    CONS_R.OWNER         AS "PKTABLE_SCHEM",
    CONS_R.TABLE_NAME    AS "PKTABLE_NAME",
    COLS_R.COLUMN_NAME   AS "PKCOLUMN_NAME"
    ''                   AS "UPDATE_RULE",
    ''                   AS "DELETE_RULE"
  FROM ALL_CONSTRAINTS CONS,
    ALL_CONS_COLUMNS COLS,
    ALL_CONSTRAINTS CONS_R,
    ALL_CONS_COLUMNS COLS_R
  WHERE
    CONS.OWNER = ?
    AND CONS.TABLE_NAME = ?
    AND CONS.CONSTRAINT_NAME = COLS.CONSTRAINT_NAME(+)
    AND CONS.R_CONSTRAINT_NAME = CONS_R.CONSTRAINT_NAME (+)
    AND CONS.R_CONSTRAINT_NAME = COLS_R.CONSTRAINT_NAME (+)
    AND CONS.CONSTRAINT_TYPE = 'R'
    AND CONS.OWNER = COLS.OWNER
    AND CONS.OWNER = CONS_R.OWNER
    AND CONS_R.OWNER = COLS_R.OWNER
    AND CONS_R.CONSTRAINT_TYPE in ('P', 'U')
    AND COLS.POSITION = COLS_R.POSITION
  ORDER BY 1, 2';
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

      return await reader.ToDataTableAsync(OdbcConstants.ForeignKeysCollectionName, cancellationToken).ConfigureAwait(false);
    }
  }
}
