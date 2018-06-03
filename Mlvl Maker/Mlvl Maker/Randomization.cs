using System;

namespace Mlvl_Maker
{
    public static class Randomization
    {

        public static int GenerateKeyDelay()
        {
            Random _random = new Random();
            return _random.Next(20, 40);
        } 

        public static int GenerateWait()
        {
            Random _random = new Random();
            return _random.Next(1010, 1150);
        }



    }
}
