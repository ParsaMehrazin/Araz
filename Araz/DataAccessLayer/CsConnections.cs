using System.Data.Entity.Core.EntityClient;
using System.IO;

namespace Models
{
    class CsConnections
    {
        public string ArazConnetion()
        {
            string Con = "";

            try
            {
                using (StreamReader sr = new StreamReader("DataSourceLog.ini"))
                {
                    string connectionString = sr.ReadToEnd();
                    var entityConnection = new EntityConnectionStringBuilder
                    {
                        Provider = "System.Data.SqlClient",
                        ProviderConnectionString = connectionString,
                        Metadata = "res://*"
                    };
                    Con = entityConnection.ToString();
                }
            }
            catch
            {
                Con = "NOT FOUND";
            }
            return Con;
        }
    }
}
