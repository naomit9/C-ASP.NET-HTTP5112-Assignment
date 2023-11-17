using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01604007Assignment1.Controllers
{
    public class AddTenController : ApiController
    {

        [Route("api/AddTen/{id}")]
        [HttpGet]
        public int Get(int id)
        {
            int sum = id + 10;
            return sum;
        }
           
    }
}
