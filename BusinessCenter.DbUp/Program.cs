using System;
using System.Configuration;
using DbUp;

namespace BusinessCenter.Db
{
    /// <summary>
    /// Used for Uploading scripts to the database
    /// </summary>
   public  class Program
    {
       static string connectionString = ConfigurationManager.ConnectionStrings["MigrateDbConnection"].ConnectionString;
      public  static void Main(string[] args)
      {
          UpdateSqlServerScript();
      }

       /// <summary>
       /// Static Method Which Updates the sql script to the database
       /// </summary>
       /// <returns>Returns -1 for failed update of sql script into database.
       ///          Returns 0 if the script is successfully updated.
       /// </returns>
       private static int UpdateSqlServerScript()
       {
           var getValue = ConfigurationManager.AppSettings["ScriptLocation"];
           var upgrader =
                 DeployChanges.To
                     .SqlDatabase(connectionString)
                     .WithScriptsFromFileSystem(getValue)
                     .LogToConsole()
                     .Build();

           var result = upgrader.PerformUpgrade();

           if (!result.Successful)
           {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine(result.Error);
               Console.ResetColor();
               System.Console.Read();
               return -1;
           }

           Console.ForegroundColor = ConsoleColor.Green;
           Console.WriteLine("Success!");
           Console.ReadLine();
           Console.ResetColor();
           return 0;
       }
    }
}
