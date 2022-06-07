using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Trivia_SQL.Controllers
{
    public class QuestionsController : ApiController
    {

        public class Question
        {
            public string questions;
            public int id;
            public string answer1;
            public string answer2;
            public string answer3;
            public string answer4;
            public string correctAnswer;
            public int score;
        }

        public Question Get(int id)
        {
            Question q = new Question();
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
                string sql = "select * from Questions where idQuestions = " + id + ";";

                cmd = new MySqlCommand(sql, con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    int.TryParse(rdr[0].ToString(), out q.id);
                    q.questions = rdr[1].ToString();
                    q.answer1 = rdr[2].ToString();
                    q.answer2 = rdr[3].ToString();
                    q.answer3 = rdr[4].ToString();
                    q.answer4 = rdr[5].ToString();
                    q.correctAnswer = rdr[6].ToString();
                    int.TryParse(rdr[6].ToString(), out q.score);
                    // return rdr[0] + " , " + rdr[1] + " , " + rdr[2] + " , " + rdr[3] + ", " + rdr[4] + ", " + rdr[5] + " , " + rdr[6] + " , " + rdr[7];
                }
            }
            return q;
        }

                    // GET: api/Questions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Questions/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Questions
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Questions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Questions/5
        public void Delete(int id)
        {
        }
    }
}
