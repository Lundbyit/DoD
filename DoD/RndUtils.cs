using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    static class RndUtils
    {
        static Random rnd = new Random();

        public static int ReturnValue (int lower, int upper)
        {
            return rnd.Next(lower, upper);
        }
        public static bool Try()
        {
            if (rnd.Next (0,100) < 50)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
