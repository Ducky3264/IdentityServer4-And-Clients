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
        //Make sure you configure the client ids to something long and hard to guess, and keep them secret
        [HttpGet]
        public IActionResult Get()
        {
            var user = HttpContext.User;
           
            if (user.HasClaim("client_id", "m2m"))
            {
                //Add the content you want to return on authorized get here
                return Content("Authorized");
            }
            
            return Content("Unauthorized");
        }
   


       
        [HttpPost]
        public IActionResult PostFormLogin()
        {
            string UName = HttpContext.Request.Form["Username"];
            string UnsaltedPWord = HttpContext.Request.Form["Password"];
            var user = HttpContext.User;
            if (user.HasClaim("client_id", "m2m"))
            {
                //Add the content you want to return on authorized post here. Use HttpContext.Request.Form[String] to collect the data you are attempting to collect.
                return Ok();
            }
            return Unauthorized();
        }
        
    }
}
