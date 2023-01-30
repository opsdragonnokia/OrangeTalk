using OrangeLib;
using System;
namespace OrangeDriver
{
    public class OrangeDriver
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            AstroLib lib = new AstroLib();

            string itrndate = "";
            if (args.Length > 0)
            {
                itrndate = args[0];
            }
            //KOZHIKODE  LONG 075E78  LAT 11.25
            BDParams par = new BDParams() { DOBMDY="05/19/1970", BIRTHTIMEHHMMSS="05:30:00", TIMEZONE="+05:30", LONGITUDE="075E78", LATITUDE="11N25", LOCATION="KOJIKODE", NAMEPERSON="ORANGE PERSON", AMPM="A" };
          

            AstroLib.natal(itrndate, par);

        }
        
    }
}
