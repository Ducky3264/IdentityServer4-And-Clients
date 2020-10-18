using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using MySql.Data.MySqlClient;
using MySql;
namespace ExampleClient
{
    
    
    public class DBController : Controller
    {
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var props = new AuthenticationProperties()
            {
                RedirectUri = "https://localhost:5001/Logout"
            };
            await HttpContext.SignOutAsync("Cookies");
            //await HttpContext.SignOutAsync("oidc", props);
            return Redirect("https://localhost:5001/Signout");
        }
        static string connStr = "Redacted server string";
        MySqlConnection conn = new MySqlConnection(connStr);
        [HttpPost]
        public async Task<IActionResult> Deposit()
        {
            Console.WriteLine("Deposit form data recieved");
            var a = HttpContext.Request.Form["DBox.DepositValue"];
            string UID = HttpContext.Request.Form["UID"];
            string CurrentBal = HttpContext.Request.Form["CurrBal"];
            if (Int32.TryParse(CurrentBal, out int NVal))
            {
                if (Int32.TryParse(a, out int SVal))
                {
                    conn.Open();
                    string sql = $"UPDATE Users SET Balance = '{NVal + SVal}' WHERE Username = '{UID}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    return Ok(NVal + SVal);
                }
                
            }
            return StatusCode(500);
        }
        [HttpPost]
        public IActionResult Withdrawl()
        {
            Console.WriteLine("Withdrawl form data recieved");
            var a = HttpContext.Request.Form["WBox.WithdrawlValue"];
            string UID = HttpContext.Request.Form["UID"];
            string CurrentBal = HttpContext.Request.Form["CurrBal"];
            if (Int32.TryParse(CurrentBal, out int NVal))
            {
                if (Int32.TryParse(a, out int SVal))
                {
                    conn.Open();
                    string sql = $"UPDATE Users SET Balance = '{NVal - SVal}' WHERE Username = '{UID}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    return Ok(NVal - SVal);
                }
            }
            return StatusCode(500);
        }
    }
    
}
