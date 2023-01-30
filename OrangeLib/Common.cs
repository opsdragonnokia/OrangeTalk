using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace OrangeLib
{
    public class Common
    {
        public static double atx(double a, double b)
        {
            return a;
        }
        public static double aty(double a, double b)
        {
            return a;
        }

        public static void drawline(double x1, double y1, double x2,  double y2)
        {
            Console.WriteLine($"LINE:{x1}:{y1}:{x2}:{y2}");
        }
    }
}
