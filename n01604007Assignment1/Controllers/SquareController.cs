using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01604007Assignment1.Controllers
{
    public class SquareController : ApiController
    {


        /// <summary>
        /// Square the integer input
        /// </summary>
        /// <param name="id">The integer input</param>
        /// <returns>The squared value of the integer input</returns>

        [Route("api/Square/{id}")]
        [HttpGet]

        public int GetSqrt(int id)
        {
            double square = Math.Pow(id, 2);
            return (int)(square);
        }

    }
}
