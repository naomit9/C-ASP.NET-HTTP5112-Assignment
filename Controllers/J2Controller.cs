using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assign2.Controllers
{
    public class J2Controller : ApiController
    {
        /// <summary>
        /// To count how many ways to roll the sum of 10
        /// </summary>
        /// <param name="m">Positive integer representing the number of sides on the first die</param>
        /// <param name="n">Positive integer representing the number of sides on the second die</param>
        /// <returns>The number of ways to get the sum of 10</returns>
        /// <example>GET ../api/J2/DiceGame/6/8 --> There are 5 total ways to get the sum 10. </example>
        /// <example>GET ../api/J2/DiceGame/12/4 --> There are 4 total ways to get the sum 10. </example>
        /// <example>GET ../api/J2/DiceGame/3/3 --> There are 0 total ways to get the sum 10. </example>
        /// <example>GET ../api/J2/DiceGame/5/5 --> There are 1 total ways to get the sum 10.</example>
        //GET api/J2/DiceGame/{m}/{n}
        [Route("api/J2/DiceGame/{m}/{n}")]
        [HttpGet]

        public string DiceGame(int m, int n)
        {
            
            int count = 0;
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    int sum = i + j;
                    if (sum == 10)
                    {
                       count++;
                    }
                }
            }
            string message = "You have " + count + " total ways to get the sum 10.";
            return message;
        }
    }
}
