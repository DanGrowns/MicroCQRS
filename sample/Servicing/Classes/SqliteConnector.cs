using System.Reflection;
using Servicing.Interfaces;

namespace Servicing.Classes
{
    public class SqliteConnector : IDbConnector
    {
        public string GetConnection()
        {
            var location =
                Assembly.GetExecutingAssembly().Location
                    .Replace(@"\View\bin\Debug\net5.0\Servicing.dll", @"\Servicing\blogging.db");

            return $"Filename={location}";
        }
    }
}