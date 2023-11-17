using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01604007Assignment1.Controllers
{
    public class NumberMachineController : ApiController
    {
        [Route("api/NumberMachine/{id}")]
        [HttpGet]

        // The four mathematical operations are
        // (x^2 + 3X + 2)/2

        public int Get(int id)
        {
            //Get x^2
            int square = id * id;

            //Get 3x
            int product = id * 3;

            //Get sum of square, product and 2
            int sum = square + product + 2;

            //Get half of the sum
            int half = sum / 2;
            return (int)(half);
        }
    }
}
