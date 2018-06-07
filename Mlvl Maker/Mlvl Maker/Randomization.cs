using System;

namespace Mlvl_Maker
{
    public static class Randomization
    {

        /// <summary>
        /// Generate random delay between 20 and 30 ms
        /// </summary>
        /// <returns></returns>
        public static int GenerateKeyDelay()
        {
            Random _random = new Random();
            return _random.Next(20, 30);
        } 

        /// <summary>
        /// Generate random delay 1070-1100 ms
        /// </summary>
        /// <returns></returns>
        public static int GenerateWait()
        {
            Random _random = new Random();
            return _random.Next(1070, 1100);
        }



    }
}
