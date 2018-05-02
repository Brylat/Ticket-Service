using System.Data;
using System.Data.SqlClient;

namespace Ticketomat.Infrastructure.Repositories
{
    public class BaseDatabaseRepository
    {
        protected IDbConnection con; 
        public BaseDatabaseRepository()
        {
            string connectionString = "Data Source=brylat.nazwa.pl;Initial Catalog=Ticketomat;Persist Security Info=True;User ID=ticketomat;Password=Ticketom@t45";  
            con = new SqlConnection(connectionString); 
        }
    }
}