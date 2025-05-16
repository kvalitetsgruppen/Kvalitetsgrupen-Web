using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AspnetCoreMvcStarter.Helpers
{
  public static class DatabaseHelper
  {
    private static string GetConnectionString()
    {
      var config = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json")
                      .Build();
      return config.GetConnectionString("DefaultConnection") ?? string.Empty;
    }

    public static bool ValidateUser(string username, string password)
    {
      try
      {
        using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
        {
          conn.Open();
          //string query = "SELECT COUNT(1) FROM login WHERE Username = @Username AND Password = @Password";
          string query = "SELECT COUNT(1) FROM secure WHERE Username = @Username AND Password = @Password";
          MySqlCommand cmd = new MySqlCommand(query, conn);
          cmd.Parameters.AddWithValue("@Username", username);
          cmd.Parameters.AddWithValue("@Password", password);


          bool count = Convert.ToBoolean(cmd.ExecuteScalar());
          Console.WriteLine($"Count: {count}");
          return count;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Database connection error: " + ex.Message);
        throw;
      }
    }
  }
}
