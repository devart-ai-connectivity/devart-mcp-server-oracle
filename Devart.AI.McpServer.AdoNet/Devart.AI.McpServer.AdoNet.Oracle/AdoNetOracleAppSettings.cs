// --------------------------------------------------------------------------
// <copyright file="AdoNetOracleAppSettings.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

namespace Devart.AI.McpServer.AdoNet.Oracle
{
  internal sealed class AdoNetOracleAppSettings : McpAppSettings
  {
    public override string ServerName => $"Devart {Properties.ProductInfo.ProductFullName}";

    public override string SourceName => "Oracle";

    public override bool OnPremise => true;
  }
}
