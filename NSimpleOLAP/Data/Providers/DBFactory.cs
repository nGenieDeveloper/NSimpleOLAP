using System;
using System.Data;
using System.Data.Common;

namespace NSimpleOLAP.Data.Providers
{
  /// <summary>
  /// Description of DBFactory.
  /// </summary>
  public abstract class DBFactory
  {
    public static IDbConnection CreateConnection(string providername)
    {
      try
      {
        var factory = DbProviderFactories.GetFactory(providername);
        var connection = factory.CreateConnection();

        return connection;
      }
      catch (Exception ex)
      {
        throw;
        // todo some extra error handling
      }
    }
  }
}