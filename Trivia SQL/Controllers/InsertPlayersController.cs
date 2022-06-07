using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Trivia_SQL.Controllers
{
    public class InsertPlayersController : ApiController
    {

        public int Get(string name)
        {
            MySqlConnection con;
            MySqlCommand cmd;
            MySqlDataReader rdr;
            string con_string = "server=127.0.0.1;uid=root;database=classproject;Charset=utf8";
            con = new MySqlConnection();
            con.ConnectionString = con_string;
            con.Open();
            string result = "";
            if (con.State == System.Data.ConnectionState.Open)
            {
                string sql = $"INSERT INTO players (Name, TotalPoints) VALUES (\"{name}\",0)";

                cmd = new MySqlCommand(sql, con);
                rdr = cmd.ExecuteReader();
                return rdr.RecordsAffected;

            }
            return 0;
        }

            // GET: api/Players
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Players/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Players
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Players/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Players/5
        public void Delete(int id)
        {
        }
    }
}
