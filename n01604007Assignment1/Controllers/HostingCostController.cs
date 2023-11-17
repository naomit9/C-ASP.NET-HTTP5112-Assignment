using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01604007Assignment1.Controllers
{
    public class HostingCostController : ApiController
    {

        double tax = 0.13;
        double pricePerFN = 5.5;
        double fortnight = 14;
        double subtotal;
        double total;
        double numofDays;
        double numofDaysDown;
        double afterTax;


        /// <summary>
        /// GET /api/HostingCost/{id}
        /// </summary>
        /// <param name="id">The number of days</param>
        /// <returns>Three string describing the total hosting cost</returns>

                [Route("api/HostingCost/{id}")]
        [HttpGet]
        public IEnumerable<string> Get(int id)
        {
            numofDays = (id / 14) + 1;
            numofDaysDown = Math.Floor(numofDays);
            subtotal = numofDays * 5.5;
            afterTax = subtotal * 0.13;
            total = afterTax + subtotal;

            return new string[] 
            {
                numofDays + " fortnights at $5.5/FN = " + subtotal + " CAD", 
                " HST 13% = " + afterTax + " CAD",
                "Total = " + total + " CAD"
            };


        }
    }
}
