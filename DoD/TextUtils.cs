using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoD
{
    static class TextUtils
    {
        internal static void AnimateLine(string text, int sleep)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(sleep);
            }
            Console.WriteLine();
        }
        internal static void AnimateLine(string text)
        {
            AnimateLine(text, 10);
        }

    }
}
