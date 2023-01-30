using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OrangeLib
{
    public class TRN_HSE
    {
        public double rr { get; set; }
        public string hse { get; set; }
        public double ihse { get; set; }
        public string pl { get; set; }
        public string index5 { get; set; }
        public string group { get; set; }   

    }
    public class TRN_ASP
    {
        public string index5 { get; set; }
        public string group { get; set; }
        public string nplanet { get; set; }
        public string tplanet { get; set; }
        public double orb { get; set; }
        public double aspect { get; set; }
        public bool h_or_s { get; set; }
        public string date { get; set; }
    }

    public class STARS
    {
        public string index1 { get; set; }
        public string group1 { get; set; }
        public string planet { get; set; }
        public string symbol { get; set; }
        public string location { get; set; }
        public double measure { get; set; }
        public string index2 { get; set; }
        public string group2 { get; set; }
        public string house { get; set; }

        public override string ToString()
        {
            return $"{index1}|{group1}|{planet}|{symbol}|{location}|{measure}|{index2}|{group2}|{house}";
        }

    }
    public class HOUSE
    {
        public string index4 { get; set; }
        public string group { get; set; }
        public string house { get; set; }
        public string symbol { get; set; }
        public string location { get; set; }
        public double measure { get; set; }

        public override string ToString()
        {
            return $"{index4}|{group}|{house}|{symbol}|{location}|{measure}";
        }
    }
    public class SUBJECTS
    {
        public string name { get; set; }
        public string hse1 { get; set; }
        public string hse2 { get; set; }
        public string hse3 { get; set; }
        public string hse4 { get; set; }
        public string hse5 { get; set; }
        public string hse6 { get; set; }
        public string hse7 { get; set; }
        public string hse8 { get; set; }
        public string hse9 { get; set; }
        public string hse10 { get; set; }
        public string hse11 { get; set; }
        public string hse12 { get; set; }

        public override string ToString()
        {
            return $"{hse1}|{hse2}|{hse3}|{hse4}|{hse5}|{hse6}|{hse7}|{hse8}|{hse9}|{hse10}|{hse11}|{hse12}";
        }
    }

}
