using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Trivia_SQL.Controllers
{
    public class UpdatePlayerScoreController : ApiController
    {

        public int Get(int score, int id)
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
                string sql = $"UPDATE players SET TotalPoints = {score} WHERE idPlayers = {id};";

                cmd = new MySqlCommand(sql, con);
                rdr = cmd.ExecuteReader();
                return rdr.RecordsAffected;

            }
            return 0;
        }

        // GET: api/UpdatePlayerScore
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UpdatePlayerScore/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UpdatePlayerScore
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UpdatePlayerScore/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UpdatePlayerScore/5
        public void Delete(int id)
        {
        }
    }
}
