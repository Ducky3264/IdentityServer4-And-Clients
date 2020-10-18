using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDS4LoginEndpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginEndpoint : ControllerBase
    {
        //This method is responsible for the Hosted UI. Any application requesting that login be hosted on my servers will use this.
        [HttpGet]
        public IActionResult Get()
        {
            var user = HttpContext.User;
            //Naturally, once I make this service an actual service "Client" will become a list of strings referenced from a database. A foreach statement would be a good idea.
            if (user.HasClaim("client_id", "interactive"))
            {
                //These returns need to be modified to return views, not simple strings. 
                return Content("Authorized");
            }
            //Return login page
            return Content("Unauthorized");
        }
   


        //This method is responsible for Non hosted identity services. Any application requesting that the user inputs their login info on a server other than mine will use this. As of 7/8/20, I plan to not use any views for this, and have it be a strict web API. Subject to change.
        [HttpPost]
        public IActionResult PostFormLogin()
        {
            string UName = HttpContext.Request.Form["Username"];
            string UnsaltedPWord = HttpContext.Request.Form["Password"];
            var user = HttpContext.User;
            if (user.HasClaim("client_id", "m2m"))
            {
                return Ok();
            }
            return Unauthorized();
        }
        
    }
}
