using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
namespace ExampleClient
{
    public class DatabaseFunctions
    {
        string connStr = "Redacted server string";
        MySqlConnection conn;

        public DatabaseFunctions()
        {
            conn = new MySqlConnection(connStr);


        }
        public int GetCurrentBal(string UID)
        {
            int Val = 0;
            conn.Open();
            string sql = $"SELECT Balance FROM Users WHERE Username = '{UID}';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0]);
                Val = (int)rdr[0];

            }
            rdr.Close();
            conn.Close();
            return Val;
        }
        public void CheckForPreviousEntry(string UID)
        {
            conn.Open();
            string sql = $"SELECT * FROM Users WHERE Username = '{UID}';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (!rdr.Read())
            {

                string sql2 = $"INSERT INTO Users Values ('{UID}', 100);";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                rdr.Close();
                cmd2.ExecuteNonQuery();
            }

            conn.Close();
            return;
        }
    }
    public class DepositBox
    {
        [Required]
        [StringLength(20, ErrorMessage = "Value is too large.")]
        public int DepositValue { get; set; }
    }
    public class WithdrawBox
    {
        [Required]
        [StringLength(20, ErrorMessage = "Value is too large.")]
        public int WithdrawlValue { get; set; }
    }
}
