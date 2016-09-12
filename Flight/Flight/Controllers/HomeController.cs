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
        //sql server login information
        public string connStr = "server=localhost;user=cis264;database=test;port=3306;password=P@ssword;";
        
        public ActionResult Index()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                //sql command for warning flag data
                string sql = "SELECT idTransaction, dateOfError, ErrorName FROM `warning_flags` where handled = 0";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                //data we take from the database is stored here
                List<string> data = new List<string>();
                //if no data is added into the list and we try to pull from it, we get a crash
                //this string exists mostly to prevent that
                data.Add("These are the warning flags raised while you were away:");
                //read loop, works like a file read loop, can go up to about 1million before my computer gives up
                //and visual studio crashes
                while (rdr.Read())
                {
                    data.Add(rdr[0] + " -- " + rdr[1] + " -- " + rdr[2]);
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }
                //pass that data to the view for output
                ViewBag.data = data;
                //close the datastream from the server
                rdr.Close();
            }
            //exception handleing 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //close connection to the server
            conn.Close();
            //call view to display output
            return View();
        }




        //call for the querry interface
        [HttpGet]
        public ActionResult querry()
        {
            return View();
        }

        //handleing the querry interface createing a querry and returning
        [HttpPost]
        public ActionResult querry(Models.TestData userData1)
        {
            MySqlConnection conn2 = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn2.Open();

                string sql = "SELECT * FROM transaction where `transaction`.`transactionDate` between str_to_date('" + userData1.Name+ "','%m/%d/%Y') and str_to_date('" + userData1.Email+ "','%m/%d/%Y')";
                MySqlCommand cmd = new MySqlCommand(sql, conn2);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<string> date_data = new List<string>();
                date_data.Add("The results are as follows:");
                while (rdr.Read())
                {
                    date_data.Add(rdr[0] + " -- " + rdr[1] + " -- " + rdr[3] + " -- " + rdr[4]);
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