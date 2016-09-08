using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public string connStr = "server=24.124.62.140;user=Cis264;database=test;port=3306;password=P@ssword;";
        
        public ActionResult Index()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT * FROM transaction limit 10";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<string> ListofColors = new List<string>();
                List<string> data = new List<string>();
                ListofColors.Add("red");
                ListofColors.Add("green");
                ListofColors.Add("blue");

                ViewBag.ListColors = ListofColors;
               
                while (rdr.Read())
                {
                    data.Add(rdr[0] + " -- " + rdr[1] + " -- " + rdr[3] + " -- " + rdr[4]);
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }
                ViewBag.data = data;

                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return View();
        }



        [HttpGet]
        public ActionResult TestLink()
        {
            return View();
        }


        [HttpPost]
        public ActionResult TestLink(Models.TestData userData1)
        {
            MySqlConnection conn2 = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn2.Open();

                string sql = "SELECT * FROM transaction where `transaction`.`transactionDate` between str_to_date('" + userData1.Name+ "','%m/%d/%Y') and str_to_date('" + userData1.Email+ "','%m/%d/%Y')";
                MySqlCommand cmd = new MySqlCommand(sql, conn2);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<string> ListofColors = new List<string>();
                List<string> date_data = new List<string>();
                date_data.Add("Null Prevention test");
                ViewBag.ListColors = ListofColors;
                while (rdr.Read())
                {
                    ViewBag.datesingle=(rdr[0] + " -- " + rdr[1] + " -- " + rdr[3] + " -- " + rdr[4]);
                    date_data.Add(rdr[0] + " -- " + rdr[1] + " -- " + rdr[3] + " -- " + rdr[4]);
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }
                ViewBag.date_data = date_data;

                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn2.Close();
            return View("Thanks", userData1);
        }
    }
}