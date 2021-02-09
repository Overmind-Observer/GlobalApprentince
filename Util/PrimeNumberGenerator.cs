using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Global_Intern.Util
{
    public class PrimeNumberGenerator
    {
        public IWebHostEnvironment _env;

        public PrimeNumberGenerator(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IEnumerable<int> GetPrimeNumbers(int startInclusive, int endInclusive)
        {

            ConsoleLogs consoleLogs = new ConsoleLogs(_env);

            int[] primeNumbers = new int[25];


            int i;

            int j;

            int k = 0;

            bool loopEntered = false;


            consoleLogs.WriteDebugLog("Test");



            if (startInclusive > endInclusive)
            {

                consoleLogs.WriteDebugLog("The end of the range is smaller than the start");

                ArgumentException argumentException = new ArgumentException("The end of the range is smaller than the start");

                //throw argumentException;

                return new int[0];
            }

            else if (startInclusive == endInclusive)
            {
                consoleLogs.WriteDebugLog("The startInclusive number is equal to the endInclusive number");

                List<int> number = new List<int>();

                number.Add(3);

                return number;
 
            }


            else
            {
                for (i = startInclusive; i < endInclusive; i++)
                {
                    for (j = 2; j <= i; j++)
                    {
                        if (i % j == 0)
                        {
                            break;
                        }
                    }
                    if (i == j)
                    {
                        loopEntered = true;

                        primeNumbers[k] = i;

                        k++;

                    }
                }

                if (loopEntered)
                {
                    return primeNumbers;
                }
                else
                {
                    return new int[0];
                }
            }
        }
    }
}
