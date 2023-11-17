using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assign2.Controllers
{
    public class J1Controller : ApiController
    {
        /// <summary>
        /// Calculate the final score based on the point system: Gain 50 points for every package delivered. Lose 10 points for every collision with an obstacle. 
        /// Earn a bonus 500 points if the number of packages delivered is greater than the number of collisions with obstacles.
        /// </summary>
        /// <param name="p">The number of package delivered</param>
        /// <param name="c">The number of collisions with obstacles</param>
        /// <returns>The final score</returns>
        /// <example>GET api/Robot/5/2 -> 730</example>
        /// <example>GET api/Robot/0/10 -> -100</example>

        //GET api/J1/Robot/{p}/{c}
        [Route("api/J1/Robot/{p}/{c}")]
        [HttpGet]
        public decimal GetRobot(int p, int c)
        {
            decimal gainPoint = p * 50;
            decimal losePoint = c * -10;
            decimal sum = gainPoint + losePoint;
            decimal bonus = 500;
            decimal final = 0;

            if (p > c)
            {
                final = sum + bonus;
            }
            else
            {
                final = sum;
            }

            return final;

        }
    }
}
