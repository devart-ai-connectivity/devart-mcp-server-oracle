// --------------------------------------------------------------------------
// <copyright file="OdbcOracleAppSettings.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

namespace Devart.AI.McpServer.Odbc.Oracle
{
  internal sealed class OdbcOracleAppSettings : McpAppSettings
  {
    public override string ServerName => $"Devart {Properties.ProductInfo.ProductFullName}";

    public override string SourceName => "Oracle";

    public override bool OnPremise => true;
  }
}
