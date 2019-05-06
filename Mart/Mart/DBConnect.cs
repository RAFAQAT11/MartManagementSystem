using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Mart
{
    class DBConnect
    {
        MySqlConnection connect = new MySqlConnection("database=mart;uid=root;password=;");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        MySqlCommandBuilder cb;

        public DBConnect()
        {
            //null
        }

        
        
    }
}
