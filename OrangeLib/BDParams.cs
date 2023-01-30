namespace OrangeLib
{
    public class BDParams
    {
        public string DOBMDY { get; set; } //"05/19/1970";
        public string BIRTHTIMEHHMMSS { get; set; } //"05:30:00";
        public string TIMEZONE { get; set; }//           bd[3] = "000:00";
        public string LONGITUDE { get; set; } //"000E00";//Long//75E22    10N42
        public string LATITUDE { get; set; } //"00N00";//Lat
        public string LOCATION { get; set; }//            bd[6] = "KOJICODE";
        public string NAMEPERSON { get; set; }//   bd[7] = "Orange Bear";
        public string AMPM { get; set; }//           bd[8] = "A";

    }
}
