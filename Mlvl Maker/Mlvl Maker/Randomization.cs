using System;

namespace Mlvl_Maker
{
    public static class Randomization
    {

        /// <summary>
        /// Generate random delay between 20 and 40 ms
        /// </summary>
        /// <returns></returns>
        public static int GenerateKeyDelay()
        {
            Random _random = new Random();
            return _random.Next(15, 25);
        } 

        /// <summary>
        /// Generate random delay 1010-1150 ms
        /// </summary>
        /// <returns></returns>
        public static int GenerateWait()
        {
            Random _random = new Random();
            return _random.Next(1010, 1150);
        }



    }
}
