using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using mvcHELLO.Models;

namespace mvcHELLO.DAL
{
    public class DALPerson
    {
        private IConfiguration configuration;

        public DALPerson(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        internal int addUser(Person person)
        {
            //connessione al database
            string connStr = configuration.GetConnectionString("MyConnString");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            //creo un comando
            string query = "INSERT INTO [dbo].[Person]([name],[city])VALUES(@pName,@pCity) select SCOPE_IDENTITY() as id;";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@pName", person.UName);
            cmd.Parameters.AddWithValue("@pCity", person.UCity);

            //interrogo il database
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int uID = Convert.ToInt32( reader[0].ToString());

            //chiudo la connessione
            conn.Close();

            return uID;
        }
    }
}
