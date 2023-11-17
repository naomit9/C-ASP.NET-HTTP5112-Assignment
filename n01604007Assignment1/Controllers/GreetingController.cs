using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01604007Assignment1.Controllers
{
    public class GreetingController : ApiController
    {

        [Route("api/Greeting")]
        [HttpGet]

        public string Post()
        {
            string Greeting = "Hello World!";
            return Greeting;
        }

        [Route("api/Greeting/{id}")]
        [HttpGet]

        public string Get(int id)
        {
            return "Greeting to " + id + " people!";
        }
    }
}
