namespace OrangeLib
{
    public class AstroLib
    {
        static string[] planets = new string[] {
           "UWUWUWUWU",
           "SUN",
           "MERCURY",
           "VENUS",
           "MARS",
           "JUPITER",
           "SATURN",
           "URANUS",
           "NEPTUNE",
           "PLUTO",
           "MOON",
            "ASCENDENT",
            "MIDHEAVEN",
            "NODE",
            "P.F." };
        static string[] signs = new string[]   {
            "XXX000XXXXX",
            "ARIES",
            "TAURUS",
            "GEMINI",
            "CANCER",
            "LEO",
            "VIRGO",
            "LIBRA",
            "SCORPIO",
            "SAGITTARIUS",
            "CAPRICORN",
            "AQUARIUS",
            "PISCES     " };
        static double[,] aspects = new double[3, 11];
        static double[,] orbs = new double[3, 11];
        static double[,] trn_orbs = new double[3, 11];
        static double[] m = new double[19];
        static double[,] k = new double[8, 18];
        static string[] bd = new string[9];
        static double[] hse = new double[13];
        static double[] mnatal = new double[15];
        static double[,] mnat_pos = new double[15, 3];

        static double[] mhouse = new double[13];
        static double[] mtransit = new double[11];
        static double[,] mtrn_pos = new double[11, 3];

        static double[] morb_asp = new double[73];
        static double[,] morb_pos = new double[73, 3];

        static double[,] masplines = new double[73, 5];

        //static int istatus = 1;
        static double p = 3.141592653589793;
        static double q = p + p;
        static double u = 360.0;
        static double v = 180.0;
        static double s = 3600.0;
        static double r = p / v;
        static int ihouse = 1;
        static string house = "Placidus";
        //static int imenu = 1;
        //static int intervals = 10;
        //static int trn_days = 1;
        //static int plottype = 1;
        //static bool lpause = false;
        //static bool pause_trn = true;
        //static bool save_trn = false;
        //static bool grx_done = false;
        //static bool done_befor = false;
        //static bool first_time = false;
        //static bool leave = false;
        //static bool vu_port = true;

        static string person = "";

        static int ct, cb, dv;

        //---
        static double t, ln, ms, ob, nu, ra, aa, pf, mc;

        static double[] mooadata = new double[(72 + 1)];
        static double[] ll = new double[(15 + 1)];

        static double[] c = new double[(9 + 1)];
        static double[] f = new double[(9 + 1)];
        static double[] w = new double[(9 + 1)];


        public AstroLib()
        {
            Init();
        }

        public static void natal(string intrn_date, BDParams bdparams)
        {



            Init();

            
            AstroData.aspts.Clear();


            //bd[1] = "05/19/1970";
            //bd[2] = "05:30:00";
            //bd[3] = "000:00";
            //bd[4] = "000E00";//Long//75E22    10N42
            //bd[5] = "00N00";//Lat
            //bd[6] = "KOJICODE";
            //bd[7] = "Orange Bear";
            //bd[8] = "A";


            bd[1] = bdparams.DOBMDY;
            bd[2] = bdparams.BIRTHTIMEHHMMSS;
            bd[3] = bdparams.TIMEZONE;
            bd[4] = bdparams.LONGITUDE;
            bd[5] = bdparams.LATITUDE;
            bd[6] = bdparams.LOCATION;
            bd[7] = bdparams.NAMEPERSON;
            bd[8] = bdparams.AMPM;


            calculate("natal", true);
            string ex = gen_ex(1);//profile
            ex += "\n\n" + gen_ex(2);//life
            ex += "\n\n" + gen_ex(3);//Career
            ex += "\n\n" + gen_ex(4);//Family
            ex += "\n\n" + gen_ex(5);//Relations
            ex += "\n\n" + gen_ex(6);//Self

            Console.WriteLine("-------------------------------");
            Console.WriteLine(ex);
            Console.WriteLine("-------------------------------");
            if (string.IsNullOrEmpty(intrn_date))
                trn_date = DateTime.Now.ToString("MM/dd/yyyy");
            else
                trn_date = intrn_date;

            horoscope(trn_date);

            
            bioindex(0, 0, true, true, true, true, true, 7, 1, trn_date, "11:59:59", "A", "+00:00", false, false, "", false, "");




        }
        static double x0, y0, xx0, yy0, pixelx, pixely, r1, r3, r4;
        static int ip_color, ia_color1, ia_color2, il_color, ib_color, iu_color;
        static double r2a, r2b, r5, r6, r7;

     
        static void horoscope(string thedate)
        {
            bool do_it;
            int i;
            string bdstore1, bdstore2, bdstore3, bdstore21, bdstore22, bdstore23;



            ia_color1 = 15;
            ia_color2 = 9;
            il_color = 11;
            ib_color = 22;
            iu_color = 20;
            //= set_font("astro_20", 0, 2, 1, 1, 2, iu_color)
            r2a = r1 * (169.0 / 200);
            r2b = r1 * (41.0 / 50);
            r3 = r1 * (173.0 / 400);
            r4 = r1 * (677.0 / 2000);
            bdstore1 = "";
            bdstore2 = "";
            bdstore3 = "";
            bdstore21 = "";
            bdstore22 = "";
            bdstore23 = "";
            trn_date = thedate;
            do_it = true;//IIF(RECNO() > 0, .T.,   .F.)
            if (do_it)
            {
                trn_save(ref bdstore1, ref bdstore2, ref bdstore3);
                init_trdat();
                set_data(1);
                calculate("transit", true);
                trn_save2(ref bdstore21, ref bdstore22, ref bdstore23);
                trn_call(ref bdstore1, ref bdstore2, ref bdstore3);

                asp_erase();
                trn_call2(ref bdstore21, ref bdstore22, ref bdstore23);
                chart2(false, true);
                trn_hse();

                string ex = xplain_trn();
                Console.WriteLine($"\n\n\n\n\nHOROZ-{ex}");

                trn_call(ref bdstore1, ref bdstore2, ref bdstore3);
                for (i = 1; i <= 10; i++)
                {
                    mtrn_pos[i, 1] = 0.0;
                    mtrn_pos[i, 2] = 0.0;
                }
            }


        }
        /*
       
        PROCEDURE chart
PARAMETER prtscreen, aspt_dbf,   replot
PRIVATE r2a, r2b, r5, r6, r7
PRIVATE ir_color, it_color,   ip_color, il_color,   ib_color
PRIVATE ie_color1, ie_color2,   ie_color3, ie_color4,   ia_color1, ia_color2
PRIVATE itime, ijk
PRIVATE hdpi, vdpi, hpage, vpage,   vhandp
IF done_befor
     = view_show(vhand,1)
ELSE
     done_befor = .T.
ENDIF
vhand = view_open(57,0,628,404 +   16 * istatus,15,0,1,  IIF(vu_port,   'Birth Chart',   'Printing Birth Chart, please wait...' ))
= view_set(vhand)
= view_show(vhand,3)
= view_show(vhand,4)
= view_scale(vhand,0,0,571,420)
= INKEY(0.1 )
ib_color = 0
ie_color1 = 0
ie_color2 = 0
ie_color3 = 0
ie_color4 = 0
ir_color = 0
il_color = 4
it_color = 0
ip_color = 0
ia_color1 = 4
ia_color2 = 4
= colours(prtscreen,@ir_color,  @it_color,@ip_color,@il_color,  @ib_color,@ie_color1,@ie_color2,  @ie_color3,@ie_color4,  @ia_color1,@ia_color2)
= set_font("astro_20",0,2,1,1,2,  il_color)
= set_font("astro_20p",0,2,1,1,2,  ip_color)
= set_font("astro_09",0,3,1,0,2,  il_color)
= set_font("astro_9a1",0,3,1,1,2,  ia_color1)
= set_font("astro_9a2",0,3,1,1,2,  ia_color2)
= set_font("astro_12",0,2,1,1,2,  ip_color)
= set_font("astro_13",0,3,1,0,2,  il_color)
= set_font("astro_18",0,2,1,1,2,  il_color)
= set_font("arial_08",0,3,1,1,2,  il_color)
= set_font("arial_09",0,3,1,1,2,  il_color)
= set_font("arial_12",0,2,1,1,2,  il_color)
= set_font("arial_8r",0,2,1,0,2,  il_color)
= set_font("arial_9r",0,2,1,0,2,  il_color)
IF vu_port .OR. prtscreen
     x0 = 356 - 6 * istatus
     y0 = 202 + 8 * istatus
     r1 = 199 + 8 * istatus
     xx0 = x0 - r1 * (IIF(istatus =   1, 1.625 , 1.725 ))
     yy0 = y0 - r1 + 5
ELSE
     x0 = 413
     y0 = 208 + 8 * istatus
     r1 = 206 + 8 * istatus
     xx0 = 65
     yy0 = 8
ENDIF
pixelx = 015
pixely = (47.0/4)
r2a = r1 * (169.0/200)
r2b = r1 * (41.0/50)
r3 = r1 * (173.0/400)
r4 = r1 * (677.0/2000)
r5 = r1 - (r3 + r2b) * 0.5 
r7 = (r3 + r4) * 0.5 
r6 = (r1 + r2a) * 0.5 
= show_logo()
= circles(r2a,r2b,r3,r4,r5,r7,  ir_color,it_color,prtscreen)
= calibrate(r2b,r2a)
= draw_sign(it_color,ie_color1,  ie_color2,ie_color3,ie_color4,  r6,prtscreen)
= draw_hse(r4,r3)
= draw_star(r2b,r3,'natal')
= draw_aspt(.F.,r4,ia_color1,  ia_color2,'natal',aspt_dbf,  .F.)
= draw_aspt(.T.,r4,ia_color1,  ia_color2,'natal',aspt_dbf,  .F.)
= tables()
= signelem(ie_color1,ie_color2,  ie_color3,ie_color4,it_color,  prtscreen)
= show_name()
IF prtscreen
     = print_view(600,400,100,  100)
ENDIF
IF prtscreen
     = view_show(vhand,1)
ENDIF
RETURN





       PROCEDURE colours
PARAMETER prtscreen, ir_color,   it_color, ip_color,   il_color, ib_color,   ie_color1, ie_color2,   ie_color3, ie_color4,   ia_color1, ia_color2
IF prtscreen
     ib_color = 0
     ie_color1 = 0
     ie_color2 = 0
     ie_color3 = 0
     ie_color4 = 0
     ir_color = 0
     il_color = 11
     it_color = 0
     ip_color = 11
     ia_color1 = 11
     ia_color2 = 11
ELSE
     ib_color = 0
     ie_color1 = 16
     ie_color2 = 10
     ie_color3 = 15
     ie_color4 = 21
     ir_color = 15
     il_color = 11
     it_color = 19
     ip_color = 18
     ia_color1 = 8
     ia_color2 = 9
ENDIF
RETURN


        PROCEDURE circles
PARAMETER r2a, r2b, r3, r4, r5,   r7, ir_color, it_color,   prtscreen
PRIVATE i, x1, x2, r
= set_pen(il_color,0,1)
= drawcurve(0,atx(x0,0),aty(y0,0),  atx(r1,3),atx(r1,3))
r = r1 * (399.0/400)
= drawcurve(0,atx(x0,0),aty(y0,0),  atx(r,3),atx(r,3))
= drawcurve(0,atx(x0,0),aty(y0,0),  atx(r2a,3),atx(r2a,3))
= drawcurve(0,atx(x0,0),aty(y0,0),  atx(r2b,3),atx(r2b,3))
= drawcurve(0,atx(x0,0),aty(y0,0),  atx(r3,3),atx(r3,3))
= drawcurve(0,atx(x0,0),aty(y0,0),  atx(r4,3),atx(r4,3))
IF  .NOT. prtscreen
     = set_brush(1,ir_color,2,  ir_color)
     = fillarea(atx(x0 - r7,0),  aty(y0,0),il_color,1)
     = set_brush(1,it_color,2,  it_color)
     = fillarea(atx(x0 - (r2a +   r2b) * 0.5 ,0),aty(y0,0),  il_color,1)
     = set_brush(0,0,2)
ENDIF
FOR i = 1 TO 12
     inc = ((i - 1) * 30 - aa) *   PI() / 180.0 
     x1 = x0 + r2b * COS(inc)
     y1 = y0 - r2b * SIN(inc)
     x2 = x0 + r1 * COS(inc)
     y2 = y0 - r1 * SIN(inc)
* SMEG = drawline(atx(x1,0),aty(y1,  0),atx(x2,0),aty(y2,0))
ENDFOR
RETURN




   
        PROCEDURE calibrate
PARAMETER r2b, r2a
PRIVATE inc, i, x1, y1, y2, x2
FOR i = 1 TO 72
     inc = ((i - 1) * 5 - aa) *   PI() / 180.0 
     x1 = x0 + r2b * COS(inc)
     y1 = y0 - r2b * SIN(inc)
     x2 = x0 + r2a * COS(inc)
     y2 = y0 - r2a * SIN(inc)

** was commented before
 SMEG     = drawline(atx(x1,0),aty(y1, 0),atx(x2,0),aty(y2,0))
ENDFOR
RETURN




         PROCEDURE draw_sign
PARAMETER it_color, ie_color1,   ie_color2, ie_color3,   ie_color4, r6,   prtscreen
PRIVATE if_color, x1, x2, y1, y2,   i, glyph, inc, xshift,   yshift
xshift = 6
yshift = 13
FOR i = 1 TO 12
     inc = (i - 1) * 30 + 15 +   180 - aa
     inc = inc
     inc = inc * PI() / 180.0 
     DO CASE
          CASE i = 1
               glyph = 'a'
               x2 = xshift
               y2 = yshift
               if_color = ie_color1
          CASE i = 2
               glyph = 'T'
               x2 = xshift - 2
               y2 = yshift - 2
               if_color = ie_color2
          CASE i = 3
               glyph = 'g'
               x2 = xshift
               y2 = yshift - 2
               if_color = ie_color3
          CASE i = 4
               glyph = 'c'
               x2 = xshift - 2
               y2 = yshift
               if_color = ie_color4
          CASE i = 5
               glyph = 'l'
               x2 = xshift - 2
               y2 = yshift - 2
               if_color = ie_color1
          CASE i = 6
               glyph = 'U'
               x2 = xshift
               y2 = yshift - 1
               if_color = ie_color2
          CASE i = 7
               glyph = 'L'
               x2 = xshift - 2
               y2 = yshift - 5
               if_color = ie_color3
          CASE i = 8
               glyph = 'S'
               x2 = xshift - 2
               y2 = yshift - 1
               if_color = ie_color4
          CASE i = 9
               glyph = 's'
               x2 = xshift - 2
               y2 = yshift
               if_color = ie_color1
          CASE i = 10
               glyph = 'C'
               x2 = xshift
               y2 = yshift
               if_color = ie_color2
          CASE i = 11
               glyph = 'A'
               x2 = xshift
               y2 = yshift
               if_color = ie_color3
          CASE i = 12
               glyph = 'p'
               x2 = xshift
               y2 = yshift
               if_color = ie_color4
     ENDCASE
     x1 = x0 + r6 * COS(inc) + x2
     y1 = y0 - r6 * SIN(inc) + y2
     IF  .NOT. prtscreen
          = set_brush(1,if_color,  2,if_color)
          = fillarea(atx(x1,0),  aty(y1,0),il_color,  1)
          = set_brush(0,0)
     ENDIF
     = font_say(IIF(i = 7,   "astro_18", "astro_20"),atx(x1,  0),aty(y1,0),glyph)
ENDFOR
RETURN
         
         */
        /*
         PROCEDURE draw_hse
PARAMETER r4, r3
PRIVATE i, inc, x1, x2, y1, y2,   delta, glyph
SET DECIMALS TO 0
FOR i = 1 TO 12
     inc = hse(i) - aa + 180
     inc = IIF(inc < 0, inc + u,   inc)
     inc = inc * PI() / 180.0 
     x1 = x0 + r4 * COS(inc)
     y1 = y0 - r4 * SIN(inc)
     x2 = x0 + r1 * COS(inc)
     y2 = y0 - r1 * SIN(inc)
     = drawline(x1,y1,x2,y2)
     IF i < 12
          delta = IIF(hse(i + 1) >   hse(i), (hse(i +   1) - hse(i)),   (hse(i + 1) + u -   hse(i)))
     ELSE
          delta = IIF(hse(1) >   hse(12),   (hse(1) -   hse(12)),   (hse(1) + u -   hse(12)))
     ENDIF
     inc = delta * 0.5  + hse(i) +   180 - aa
     inc = IIF(inc < 0, inc + 360,   inc)
     inc = inc * PI() / 180.0 
     glyph = ALLTRIM(STR(INT(i)))
     x1 = x0 + (r3 + r4) * 0.5  *   COS(inc)
     y1 = y0 - (r3 + r4) * 0.5  *   SIN(inc) + 7
     = font_say("arial_12",atx(x1,  0),aty(y1,0),glyph)
ENDFOR
SET DECIMALS TO 16
RETURN
         
         */
        /*
         PROCEDURE chart
PARAMETER prtscreen, aspt_dbf,   replot
PRIVATE r2a, r2b, r5, r6, r7
PRIVATE ir_color, it_color,   ip_color, il_color,   ib_color
PRIVATE ie_color1, ie_color2,   ie_color3, ie_color4,   ia_color1, ia_color2
PRIVATE itime, ijk
PRIVATE hdpi, vdpi, hpage, vpage,   vhandp
IF done_befor
     = view_show(vhand,1)
ELSE
     done_befor = .T.
ENDIF
vhand = view_open(57,0,628,404 +   16 * istatus,15,0,1,  IIF(vu_port,   'Birth Chart',   'Printing Birth Chart, please wait...' ))
= view_set(vhand)
= view_show(vhand,3)
= view_show(vhand,4)
= view_scale(vhand,0,0,571,420)
= INKEY(0.1 )
ib_color = 0
ie_color1 = 0
ie_color2 = 0
ie_color3 = 0
ie_color4 = 0
ir_color = 0
il_color = 4
it_color = 0
ip_color = 0
ia_color1 = 4
ia_color2 = 4
= colours(prtscreen,@ir_color,  @it_color,@ip_color,@il_color,  @ib_color,@ie_color1,@ie_color2,  @ie_color3,@ie_color4,  @ia_color1,@ia_color2)
= set_font("astro_20",0,2,1,1,2,  il_color)
= set_font("astro_20p",0,2,1,1,2,  ip_color)
= set_font("astro_09",0,3,1,0,2,  il_color)
= set_font("astro_9a1",0,3,1,1,2,  ia_color1)
= set_font("astro_9a2",0,3,1,1,2,  ia_color2)
= set_font("astro_12",0,2,1,1,2,  ip_color)
= set_font("astro_13",0,3,1,0,2,  il_color)
= set_font("astro_18",0,2,1,1,2,  il_color)
= set_font("arial_08",0,3,1,1,2,  il_color)
= set_font("arial_09",0,3,1,1,2,  il_color)
= set_font("arial_12",0,2,1,1,2,  il_color)
= set_font("arial_8r",0,2,1,0,2,  il_color)
= set_font("arial_9r",0,2,1,0,2,  il_color)
IF vu_port .OR. prtscreen
     x0 = 356 - 6 * istatus
     y0 = 202 + 8 * istatus
     r1 = 199 + 8 * istatus
     xx0 = x0 - r1 * (IIF(istatus =   1, 1.625 , 1.725 ))
     yy0 = y0 - r1 + 5
ELSE
     x0 = 413
     y0 = 208 + 8 * istatus
     r1 = 206 + 8 * istatus
     xx0 = 65
     yy0 = 8
ENDIF
pixelx = 015
pixely = (47.0/4)
r2a = r1 * (169.0/200)
r2b = r1 * (41.0/50)
r3 = r1 * (173.0/400)
r4 = r1 * (677.0/2000)
r5 = r1 - (r3 + r2b) * 0.5 
r7 = (r3 + r4) * 0.5 
r6 = (r1 + r2a) * 0.5 
= show_logo()
= circles(r2a,r2b,r3,r4,r5,r7,  ir_color,it_color,prtscreen)
= calibrate(r2b,r2a)
= draw_sign(it_color,ie_color1,  ie_color2,ie_color3,ie_color4,  r6,prtscreen)
= draw_hse(r4,r3)
= draw_star(r2b,r3,'natal')
= draw_aspt(.F.,r4,ia_color1,  ia_color2,'natal',aspt_dbf,  .F.)
= draw_aspt(.T.,r4,ia_color1,  ia_color2,'natal',aspt_dbf,  .F.)
= tables()
= signelem(ie_color1,ie_color2,  ie_color3,ie_color4,it_color,  prtscreen)
= show_name()
IF prtscreen
     = print_view(600,400,100,  100)
ENDIF
IF prtscreen
     = view_show(vhand,1)
ENDIF
RETURN
         */

        static void chart2(bool biodex, bool therm)
        {
            draw_star(r2b, r3, "transit");
            draw_aspt(false, r4, ia_color1, ia_color2, "transit", false, biodex);
        }

        static void bioindex(double angle1, double angle2, bool health, bool finance, bool intel, bool friend, bool love,
            int _intervals, int _trn_days, string _trn_date, string _trn_time, string _trn_ampm,
            string trn_zone, bool pause_trn, bool retrieve, string rfname, bool savefile, string sfname)
        {
            string bdstore1, bdstore2, bdstore3;
            int ip_color, il_color, ib_color, iu_color, ia_color1, ia_color2;
            double r2a, r2b, r5, r6, r7;
            trans1.Clear();
            trans2.Clear();

            il_color = 11;
            ib_color = 22;
            iu_color = 14;
            ia_color1 = 15;
            ia_color2 = 9;
            bdstore1 = "";
            bdstore2 = "";
            bdstore3 = "";
            //= set_font("arial_09",0,2,1,2,2,  il_color)
            //= set_font("astro_20p",0,2,1,1,2,  iu_color)
            //= set_font("astro_20",0,2,1,1,2,  ib_color)
            intervals = _intervals;
            trn_days = _trn_days;
            trn_date = _trn_date;
            trn_time = _trn_time;
            trn_ampm = _trn_ampm;

            if ((!retrieve) || string.IsNullOrEmpty(rfname))
            {

                trn_save(ref bdstore1, ref bdstore2, ref bdstore3);
                r2a = r1 * (169.0 / 200);
                r2b = r1 * (41.0 / 50);
                r3 = r1 * (173.0 / 400);
                r4 = r1 * (677.0 / 2000);
                transits();
            }
            //= close_svpw()
            dplot(angle1, angle2, health, finance, intel, friend, love);
            if ((!retrieve) || string.IsNullOrEmpty(rfname))
            {
                trn_call(ref bdstore1, ref bdstore2, ref bdstore3);
            }
        }
        static void match(int agefrom, int ageto, string gender, double angle1, double angle2)
        {
            string bdstore1, bdstore2, bdstore3;
            string bestmatch;

            //compat
            //SELECT subjects
            //SET ORDER TO subjects
            //= set_font("arial_09", 0, 2, 1, 1, 2, 11)
            //= set_font(small_9r, 0, 1, 1, 0, 2, 13)
            bdstore1 = "";
            bdstore2 = "";
            bdstore3 = "";
            trn_save(ref bdstore1, ref bdstore2, ref bdstore3);
            bestmatch = dbf_match();
            if (bestmatch == "NOBODY")
                Console.WriteLine("There is nobody in the database who is compatible to the current subject.");
            else
            {
                mplot(angle1, angle2, bestmatch);
                trn_call(ref bdstore1, ref bdstore2, ref bdstore3);
            }
        }
        

       

        static string dbf_match()
        {

            /*
    PRIVATE oldyear, thisyear,   yearsold, diff, value,   subplt
    PRIVATE nom, cur_rec, bestperson,   bestvalue, nobody
    SELECT subjects
    SEEK subject
    cur_rec = RECNO()
    thisyear = YEAR(DATE())
    GOTO TOP
    DO WHILE  .NOT. EOF()
         oldyear = YEAR(CTOD(date))
         yearsold = thisyear -   oldyear
         REPLACE age WITH yearsold
         SKIP
    ENDDO
    IF gender <> 'A'
         SET FILTER TO sex = gender .AND. age >= agefrom .AND. age <= ageto
    ELSE
         SET FILTER TO age >= agefrom .AND. age <= ageto
    ENDIF
    bestvalue = -100.00 
    GOTO TOP
    DO WHILE  .NOT. EOF()
         IF RECNO() <> cur_rec
              value = 0.0 
              FOR i = 1 TO 12
                   subplt = FIELD(i +   7)
                   diff=abs(&subplt-mnatal(i))
                   value = IIF(ABS(aspects(1,  1) - diff) <=   orbs(1,1),   value + 1,   value)
                   value = IIF(ABS(aspects(1,  10) -   diff) <=   orbs(1,10),   value + 1,   value)
                   value = IIF(ABS(aspects(2,  2) - diff) <=   orbs(2,2),   value - 1,   value)
                   value = IIF(ABS(aspects(2,  6) - diff) <=   orbs(2,6),   value - 1,   value)
              ENDFOR
              SELECT compat
              APPEND BLANK
              REPLACE point WITH   value
              nom = ALLTRIM(subjects.first) +   ' ' +   ALLTRIM(subjects.last)
              REPLACE name WITH nom
              IF value > bestvalue
                   bestvalue = value
                   bestperson = nom
              ENDIF
         ELSE
              SELECT compat
              APPEND BLANK
              REPLACE point WITH 0.0 
              REPLACE name WITH bd(7)
         ENDIF
         SELECT subjects
         SKIP
    ENDDO
    SELECT compat
    nobody = .T.
    GOTO TOP
    DO WHILE  .NOT. EOF()
         IF point > 0.0 
              nobody = .F.
              EXIT
         ELSE
              SKIP
         ENDIF
    ENDDO
    IF nobody
         bestperson = 'NOBODY'
    ENDIF
    RETURN (bestperson)
            */
            return "NOBODY";
        }

        static void mplot(double angle1, double angle2, string bestperson)
        {

        
        /* 
        

PRIVATE i, nrows, iend, view_hand
PRIVATE cl
DIMENSION cl( 2)
IF vu_port
    = view_show(vhand,1)
ENDIF
view_hand = view_open(0,0,628,404 +   16 * istatus,15,0,1,  'Compatibility Chart' )
= view_set(view_hand)
= view_show(view_hand,3)
= view_show(view_hand,4)
SELECT compat
nrows = initdata(2)
nrows = nrows - 1
IF RECCOUNT() > nrows
    iend = nrows
ELSE
    iend = RECCOUNT()
ENDIF
GOTO TOP
FOR i = 1 TO iend
    GOTO i
    = store_data(ALLTRIM(STR(RECNO())),  point)
ENDFOR
= set_graph(0,  'Most Compatible To ' + person +   ' Is ' + bestperson)
= set_graph(1,"arial_09")
= set_graph(2,"arial_09")
= set_graph(3,"arial_09")
= set_graph(4,0.0 ,0.0 ,80.0 ,  93.75 )
= set_graph(5,11)
= set_graph(6,9)
= set_graph(7,11)
= set_graph(8,11)
= set_graph(9,9)
= set_graph(10,14)
= set_graph(11,9)
= set_graph(12,0)
= set_graph(13,0)
= set_graph(14,2)
= set_graph(15,100)
= set_graph(16,1)
= set_graph(17,1)
= set_graph(20,0)
= set_column(1,0,3)
= set_column(1,1,1)
= set_column(1,2,2)
= set_column(1,3,1)
= set_column(1,4,1)
= set_column(1,5,19)
= set_axis(0,0,'SUBJECTS')
= set_axis(0,1,0)
= set_axis(0,2,1)
= set_axis(0,3,0)
= set_axis(0,4,0)
= set_axis(0,10,1)
= set_axis(0,12,1)
= set_axis(0,11,1)
= set_axis(1,0,'POINT')
= set_axis(1,1,0)
= set_axis(1,2,1)
= set_axis(1,3,0)
= set_axis(1,4,0)
= set_axis(1,11,1)
= set_axis(1,16,1)
= set_axis(1,20,0)
= set_3d(angle1,angle2,10000000)
= plot()
x = 512
y = 0
SELECT compat
iend = RECCOUNT()
FOR i = 1 TO iend
    GOTO i
    = font_say(small_9r,x,y + i *   8,ALLTRIM(STR(i)) + '  ' +   name)
ENDFOR
= set_brush(0)
WAIT WINDOW 'Click Mouse Once To Remove This Message & Once More To Return To Menu.'
WAIT ''
= view_show(view_hand,1)
*/

}






        static double[] cl = new double[6];
        static void dplot(double angle1, double angle2, bool health, bool finance, bool intel, bool friend, bool love)
        {

            int i, nrows, iend, view_hand;

            //DIMENSION cl( 5)
            //view_hand = view_open(0,0,628,404 +   16 * istatus,15,0,1,  'Astro-Biodex Graph' )
            //= view_set(view_hand)
            //= view_show(view_hand,3)
            //= view_show(view_hand,4)

            //SELECT trans2

            //nrows = initdata(6);
            //nrows = nrows - 1
            //IF RECCOUNT() > nrows
            //     iend = nrows
            //ELSE
            //     iend = RECCOUNT()
            //ENDIF

            //GOTO TOP

            for (i = 0; i < trans2.Count; i++)
            {
                TRN_HSE hse =  trans2[i];
                
                //store_data(date, ratio1, ratio2, ratio3, ratio4, ratio5)
            }

            
        }


        static string AddDays(string indate, int days)
        {
            DateTime dt = DateTime.ParseExact(indate, "MM/dd/yyyy", null);
            dt = dt.AddDays(days);
            return dt.ToString("MM/dd/yyyy");
        }

        static void set_data(int i)
        {
            string d;
            d = AddDays(trn_date, trn_days * (i - 1));
            bd[1] = d;
            bd[3] = "000:00";
            bd[8] = "A";
        }

        static void asp_erase()
        {

            double x1, y1, x2, y2;
            int i;
            //= set_pen(ib_color)
            for (i = 1; i <= masptotal; i++)
            {
                x1 = masplines[i, 1];
                y1 = masplines[i, 2];
                x2 = masplines[i, 3];
                y2 = masplines[i, 4];
                //= drawline(atx(x1, 0), aty(y1, 0), atx(x2, 0), aty(y2, 0))
            }

        }

        static double[] kstore = new double[14 + 1];
        static double[] kstore2 = new double[(14 + 1)];

        static int intervals, trn_days;
        static string trn_zone, trn_ampm, trn_date, trn_time;
        static void init_trdat()
        {
            intervals = 1;
            trn_days = 1;
            trn_time = "11:59:59";
            trn_ampm = "A";
            trn_zone = "+00:00";
        }


        static void trn_save(ref string bdstore1, ref string bdstore2, ref string bdstore3)
        {
            int i;
            for (i = 1; i <= 14; i++)
            {
                kstore[i] = k[7, i];
            }
            bdstore1 = bd[1];
            bdstore2 = bd[2];
            bdstore3 = bd[8];
        }
        static void trn_save2(ref string bdstore21, ref string bdstore22, ref string bdstore23)
        {
            int i;
            for (i = 1; i <= 14; i++)
            {
                kstore2[i] = k[7, i];
            }
            bdstore21 = bd[1];
            bdstore22 = bd[2];
            bdstore23 = bd[8];
        }
        static void trn_call(ref string bdstore1, ref string bdstore2, ref string bdstore3)
        {
            int i;
            for (i = 1; i <= 14; i++)
            {
                k[7, i] = kstore[i];
            }
            bd[(1)] = bdstore1;
            bd[(2)] = bdstore2;
            bd[(8)] = bdstore3;
        }

        static void trn_call2(ref string bdstore21, ref string bdstore22, ref string bdstore23)
        {
            int i;
            for (i = 1; i <= 14; i++)
            {
                k[7, i] = kstore2[i];
            }
            bd[(1)] = bdstore21;
            bd[(2)] = bdstore22;
            bd[(8)] = bdstore23;
        }

        static List<TRN_HSE> trans2 = new List<TRN_HSE>();

        static void trn_hse()
        {
            int j;
            double rr, ihse;
            string hse = "";
            string pl = "";
            //NUGGET

            trans2 = new List<TRN_HSE>();

            for (j = 1; j <= 10; j++)
            {
                rr = mtransit[j];
                ihse = pl_in_hse(rr);
                hse = $"{ihse:00}";
                //hse = IIF(LEN(hse) = 1, '0' +   hse, hse)
                pl = SUBSTR(planets[j], 1, 2);

                string index5 = pl + hse;
                string group = planets[j] + " IN HOUSE " + ihse;

                trans2.Add(new TRN_HSE() { rr = rr, ihse = ihse, group = group, hse = hse, index5 = index5, pl = pl });

                //Console.WriteLine($"TRN-HSE-{rr}/{ihse}/{hse}/{pl}/{index5}/{group}");
            }

        }



        static string gen_ex(int xplain_typ)
        {
            string ex = "";
            switch (xplain_typ)
            {
                case 1:
                    ex = profile(false);
                    break;
                case 2:
                    ex = xplain_plt(); break;
                case 3:
                    ex = xplain_pt(10); break;
                case 4:
                    ex = xplain_pt(4); break;
                case 5:
                    ex = xplain_pt(7); break;
                case 6:
                    ex = xplain_pt(1); break;
                case 7:
                    ex = xplain_trn(); break;
            }
            return ex;
        }
        static string xplain_plt()
        {
            string ex, plt;
            int li = 0;
            int i;
            ex = "";
            for (i = 1; i <= 10; i++)
            {
                plt = planets[i];
                xplainp(plt, i, ref ex, ref li);
            }

            return ex;
        }

        // Follow up all xplain in FP and track here
        static void xplainp(string plt, int iplt, ref string ex, ref int li)
        {

            string[] heading = AstroData.profile;
            ex = ex + heading[iplt];// explain

            STARS? star = dbstars.Find(a => { return string.Compare(a.planet, plt, true) == 0; });
            if (star != null)
            {
                if (AstroData.plt_sgn.ContainsKey(star.index1))
                {
                    ex += "\n" + AstroData.plt_sgn[star.index1];

                }
                if (AstroData.plt_hse.ContainsKey(star.index2))
                {
                    ex += "\n" + AstroData.plt_hse[star.index2];
                }

            }
            

        }


    
        static string xplain_trn()
        {
            string ex;

            ex = "";

            for (int i = 0; i < trans1.Count; i++)
            {
                if (AstroData.trn_plt.ContainsKey(trans1[i].index5))
                {
                    ex += "\n" + AstroData.trn_plt[trans1[i].index5]+"\n";
                }
            }
            ex += "\n\n";

            for (int i = 0; i < trans2.Count; i++)
            {
                if (AstroData.trn_hse.ContainsKey(trans2[i].index5))
                {
                    ex += "\n" + AstroData.trn_hse[trans2[i].index5]+"\n";
                }
            }

           
            return ex;
        }
        static string xplain_pt(int i)
        {

            string ex;

            ex = "";

            int idx = 0;

            switch (i)
            {
                case 10: idx = 11; break;
                case 1: idx = 14; break;
                case 7: idx = 13; break;

                case 4: idx = 12; break;
                default: break;
            }
            if (idx > 0)
            {
                ex = ex + AstroData.profile[idx];
            }
            HOUSE hse = dbhouse[i];
            if (AstroData.hse_sgn.ContainsKey(hse.index4))
            {
                ex += AstroData.hse_sgn[hse.index4];
            }

            return ex;
        }


        //--------

        static string profile(bool prtscreen)
        {
            double i;
            string ex;

            double[,] elems = new double[4, 5];
            do_elems(ref elems);
            cal_value(ref elems);
            do_hses(ref hse_pt);
            cal_skill(ref hse_pt);
            do_asps();
            ex = make_txt(prtscreen);
            ex = ex + CHR(13) + CHR(10) + "DETAILED PROFILE ANALYSIS" + CHR(13) + CHR(10);
            ex = ex + do_detail();

            //Console.WriteLine(ex);
            return ex;
        }
        static double[] hse_pt = new double[12 + 1];
        static double[] par = new double[(5) + 1];

        static void do_elems(ref double[,] elems)
        {
            int i, j;
            double isign, point;
            for (i = 1; i <= 3; i++)
            {
                for (j = 1; j <= 4; j++)
                {
                    elems[i, j] = 0;
                }
            }
            for (i = 1; i <= 10; i++)
            {
                isign = which_sign(k[7, i]);
                if (i == 1)
                {
                    point = 2.5;
                }
                else
                {
                    if (i == 10)
                        point = 2.25;
                    else
                        point = 1;

                }
                switch (isign)
                {
                    case 1:
                        elems[1, 1] = elems[1, 1] + point; break;
                    case 2:
                        elems[2, 2] = elems[2, 2] + point; break;
                    case 3:
                        elems[3, 3] = elems[3, 3] + point; break;
                    case 4:
                        elems[1, 4] = elems[1, 4] + point; break;
                    case 5:
                        elems[2, 1] = elems[2, 1] + point; break;
                    case 6:
                        elems[3, 2] = elems[3, 2] + point; break;
                    case 7:
                        elems[1, 3] = elems[1, 3] + point; break;
                    case 8:
                        elems[2, 4] = elems[2, 4] + point; break;
                    case 9:
                        elems[3, 1] = elems[3, 1] + point; break;
                    case 10:
                        elems[1, 2] = elems[1, 2] + point; break;
                    case 11:
                        elems[2, 3] = elems[2, 3] + point; break;
                    case 12:
                        elems[3, 4] = elems[3, 4] + point; break;
                    default: break;
                }
            }
        }
        static double fire, earth, air, water, cardinal, ffixed, mutable;
        static double warm, warming, cool, cooling, spring, summer, fall, winter;
        static void cal_value(ref double[,] elems)
        {

            fire = elems[1, 1] + elems[2, 1] + elems[3, 1];
            earth = elems[1, 2] + elems[2, 2] + elems[3, 2];
            air = elems[1, 3] + elems[2, 3] + elems[3, 3];
            water = elems[1, 4] + elems[2, 4] + elems[3, 4];
            cardinal = elems[1, 1] + elems[1, 2] + elems[1, 3] + elems[1, 4];
            ffixed = elems[2, 1] + elems[2, 2] + elems[2, 3] + elems[2, 4];
            mutable = elems[3, 1] + elems[3, 2] + elems[3, 3] + elems[3, 4];
            spring = elems[1, 1] + elems[2, 2] + elems[3, 3];
            summer = elems[1, 4] + elems[2, 1] + elems[3, 2];
            fall = elems[1, 3] + elems[2, 4] + elems[3, 1];
            winter = elems[1, 2] + elems[2, 3] + elems[3, 4];
            warm = spring + summer;
            cool = fall + winter;
            warming = winter + spring;
            cooling = summer + fall;
        }
        static void do_hses(ref double[] hse_pt)
        {
            int i, j, ihse;
            double point, rr;
            for (i = 1; i <= 12; i++)
                hse_pt[i] = 0;
            for (j = 1; j <= 10; j++)
            {
                ihse = 0;
                rr = mnatal[j];
                if (j == 1)
                {
                    point = 2.5;
                }
                else
                {
                    if (j == 10)
                        point = 2.25;
                    else
                        point = 1;
                }
                ihse = pl_in_hse(rr);
                hse_pt[ihse] = hse_pt[ihse] + point;
            }
        }
        static double respect1, respect2, prime, second, third, fourth;
        static double angular, succeed, cadent, lower, upper, rising, setting;
        static void cal_skill(ref double[] hse_pt)
        {
            int i;
            angular = 0;
            for (i = 1; i <= 4; i++)
            {
                angular = angular + hse_pt[1 + (i - 1) * 3];
            }
            succeed = 0;
            for (i = 1; i <= 4; i++)
            {
                succeed = succeed + hse_pt[2 + (i - 1) * 3];
            }
            cadent = 0;
            for (i = 1; i <= 4; i++)
            {
                cadent = cadent + hse_pt[3 + (i - 1) * 3];
            }
            lower = 0;
            for (i = 1; i <= 6; i++)
            {
                lower = lower + hse_pt[i];
            }
            upper = 0;
            for (i = 7; i <= 12; i++)
            {
                upper = upper + hse_pt[i];
            }
            rising = 0;
            for (i = 1; i <= 3; i++)
            {
                rising = rising + hse_pt[i] + hse_pt[i + 9];
            }
            setting = 0;
            for (i = 4; i <= 9; i++)
            {
                setting = setting + hse_pt[i];
            }
            prime = hse_pt[1] + hse_pt[2] + hse_pt[3];
            second = hse_pt[4] + hse_pt[5] + hse_pt[6];
            third = hse_pt[7] + hse_pt[8] + hse_pt[9];
            fourth = hse_pt[10] + hse_pt[11] + hse_pt[12];
            respect1 = prime + third;
            respect2 = fourth + second;
        }
        static double isoft, ihard, conjunct, sextile, square, trine, oppose;
        static void do_asps()
        {

            int i, ll, ii, j;
            double point, r, a;
            isoft = 0;
            ihard = 0;
            conjunct = 0;
            sextile = 0;
            square = 0;
            trine = 0;
            oppose = 0;
            for (i = 1; i <= 2; i++)
            {
                int sem = (10 - 3 * (i - 1));
                for (ll = 1; ll <= sem; ll++)
                {
                    for (ii = 1; ii <= 9; ii++)
                    {
                        for (j = (ii + 1); j <= 10; j++)
                        {
                            if (ii == 1)
                            {
                                point = 2.5;
                            }
                            else
                            {
                                if (j == 10)
                                {
                                    point = 2.25;
                                }
                                else
                                {
                                    point = 1;
                                }
                            }
                            r = mnatal[ii] - mnatal[j];
                            r = (r < 0.0 ? r + 360.0 : r);
                            a = ABS(r - aspects[i, ll]);
                            if (a <= orbs[i, ll])
                            {
                                if (i == 1)
                                {
                                    isoft = isoft + point;
                                    switch (ll)
                                    {

                                        case 1:
                                        case 7:
                                            conjunct = conjunct + point;
                                            break;
                                        case 3:
                                            sextile = sextile + point;
                                            break;
                                        case 4:
                                        case 6:
                                            trine = trine + point;
                                            break;
                                        default: break;
                                    }
                                }
                                else
                                {
                                    ihard = ihard + point;

                                    if (ll == 2)
                                        square = square + point;
                                    if (ll == 4)
                                        oppose = oppose + point;



                                }
                            }
                        }
                    }
                }
            }

        }
        static string detail(int istart, int ipar, ref double[] par)
        {

            int i, ibig, irec;
            double big;

            string ex;
            big = -1;
            ex = "";
            for (i = 1; i <= ipar; i++)
            {
                if (par[i] > big)
                {
                    big = par[i];
                    ibig = i;
                }
            }
            for (i = 1; i <= ipar; i++)
            {
                if (big == par[i])
                {
                    irec = istart + i;
                    //GOTO irec
                    if (irec < AstroData.profile.Length)
                    {
                        ex = ex + AstroData.profile[irec] + "\r\n";// $"-EX-{irec}=";
                    }
                    else
                    {
                        Console.WriteLine("-----------------ERROR - IREC ");
                    }
                }
            }
            return ex + "\r\n";
        }
        static string do_detail()
        {
            int ipointer, irec;
            string ex;

            ex = "";
            par[1] = warm;
            par[2] = cool;
            par[3] = warming;
            par[4] = cooling;
            ex = ex + detail(14, 4, ref par);
            par[1] = spring;
            par[2] = summer;
            par[3] = fall;
            par[4] = winter;
            ex = ex + detail(18, 4, ref par);
            par[1] = lower;
            par[2] = upper;
            par[3] = rising;
            par[4] = setting;
            ex = ex + detail(22, 4, ref par);
            par[1] = prime;
            par[2] = second;
            par[3] = third;
            par[4] = fourth;
            ex = ex + detail(26, 4, ref par);
            par[1] = fire;
            par[2] = earth;
            par[3] = air;
            par[4] = water;
            ex = ex + detail(30, 4, ref par);
            par[1] = cardinal;
            par[2] = ffixed;
            par[3] = mutable;
            ex = ex + detail(34, 3, ref par);
            par[1] = angular;
            par[2] = succeed;
            par[3] = cadent;
            ex = ex + detail(37, 3, ref par);
            par[1] = rising;
            par[2] = setting;
            ex = ex + detail(40, 2, ref par);
            par[1] = conjunct;
            par[2] = sextile;
            par[3] = square;
            par[4] = trine;
            par[5] = oppose;
            ex = ex + detail(42, 5, ref par);
            par[1] = isoft;
            par[2] = ihard;
            ex = ex + detail(47, 2, ref par);



            return (ex + "\r\n");

        }
        static double total = 0;
        static string make_txt(bool prtscreen)
        {
            string ex;
            ex = "";
            ex = ex + "PREDOMINANCE OF TRAITS\r\n\r\n" ;
            ex = ex + "VALUES\r\n\r\n" ;
            ex = ex + "Values are inner characteristics underlying all important choices, decisions, interests,";
            ex = ex + " preferences, and intentions.  You share certain values in common with your age group and generation.";
            ex = ex + "  Values also figure importantly in your compatibility with other people." + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "DISPOSITION" + CHR(13) + CHR(10);
            ex = ex + "Lively, enthusiastic, exuberant,   [fire     ]       " + (!prtscreen ? CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + scale(fire) + CHR(13) + CHR(10);
            ex = ex + "Practical, realistic, sensible,    [earth    ]       " + CHR(9) + CHR(9) + CHR(9) + CHR(9) + scale(earth) + CHR(13) + CHR(10);
            ex = ex + "Rational, communicative, clever,   [air      ]       " + CHR(9) + CHR(9) + CHR(9) + scale(air) + CHR(13) + CHR(10);
            ex = ex + "Sympathetic, sensitive, caring,    [water    ]       " + CHR(9) + CHR(9) + CHR(9) + scale(water) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "AFFINITY" + CHR(13) + CHR(10);
            ex = ex + "Initiative, business, political,   [cardinal ]       " + CHR(9) + CHR(9) + CHR(9) + CHR(9) + scale(cardinal) + CHR(13) + CHR(10);
            ex = ex + "Concentration, creative,technical, [fixed    ]       " + CHR(9) + CHR(9) + CHR(9) + scale(ffixed) + CHR(13) + CHR(10);
            ex = ex + "Flexibility, academic, cultural,   [mutable  ]       " + CHR(9) + CHR(9) + CHR(9) + scale(mutable) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "NATURE" + CHR(13) + CHR(10);
            ex = ex + "Artistic, sensual, absorbed                          " + (prtscreen ? CHR(9) + CHR(9) + CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + CHR(9) + scale(warm) + CHR(13) + CHR(10);
            ex = ex + "Scientific, idealistic, aloof                        " + (prtscreen ? CHR(9) + CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + CHR(9) + CHR(9) + scale(cool) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "Spontaneous, active, impulsive                       " + (prtscreen ? CHR(9) + CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + CHR(9) + scale(warming) + CHR(13) + CHR(10);
            ex = ex + "Reflective, contemplative, tactful                   " + (prtscreen ? CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + CHR(9) + scale(cooling) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "LIFESTYLE" + CHR(13) + CHR(10);
            ex = ex + "Experiential self-reliant values, involvement       " + CHR(9) + CHR(9) + CHR(9) + scale(spring) + CHR(13) + CHR(10);
            ex = ex + "Comprehender friendship values,harmony              " + (prtscreen ? CHR(9) : "") + CHR(9) + CHR(9) + scale(fall) + CHR(13) + CHR(10);
            ex = ex + "Belonger family values, tradition                   " + (prtscreen ? CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + CHR(9) + scale(summer) + CHR(13) + CHR(10);
            ex = ex + "Achiever conventional values, reputation            " + CHR(9) + CHR(9) + CHR(9) + scale(winter) + CHR(13) + CHR(10) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "SKILLS" + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "Skills are outward behavior and stress responses that maintain personal comfort zones and accommodate life circumstances.";
            ex = ex + "  Skills are your personal talents, abilities, vocations, callings, and affiliations.  They are not associated with any";
            ex = ex + " age group or generation.  Skills give your life meaning, purpose and prominence." + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "MANNER" + CHR(13) + CHR(10);
            ex = ex + "Vivid, attention-getting, intuitive, high profile    " + CHR(9) + CHR(9) + CHR(9) + scale(angular) + CHR(13) + CHR(10);
            ex = ex + "Brooding, purposeful, confiding, medium profile      " + CHR(9) + CHR(9) + scale(succeed) + CHR(13) + CHR(10);
            ex = ex + "Gentle, observant, knowledgeable, low profile        " + CHR(9) + CHR(9) + scale(cadent) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "ENVIRONMENT" + CHR(13) + CHR(10);
            ex = ex + "Expressive, domestic, casual, private                " + (prtscreen ? CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + scale(lower) + CHR(13) + CHR(10);
            ex = ex + "Controlled, official, formal, public                 " + (prtscreen ? CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + CHR(9) + scale(upper) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "Independent, competitive, separateness               " + (prtscreen ? CHR(9) + CHR(9) : "") + CHR(9) + CHR(9) + scale(rising) + CHR(13) + CHR(10);
            ex = ex + "Interactive, cooperative, closeness                  " + (prtscreen ? CHR(9) + CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + scale(setting) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "RESPECT" + CHR(13) + CHR(10);
            ex = ex + "Equality, instinct, natural, affable                 " + (prtscreen ? CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + CHR(9) + scale(respect1) + CHR(13) + CHR(10);
            ex = ex + "Hierarchy, authority, civil, polite                  " + (prtscreen ? CHR(9) + CHR(9) : "") + CHR(9) + CHR(9) + CHR(9) + CHR(9) + scale(respect2) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "ADAPTATION" + CHR(13) + CHR(10);
            ex = ex + "Performer assertive, faces challenge, entertains    " + CHR(9) + CHR(9) + scale(prime) + CHR(13) + CHR(10);
            ex = ex + "Provider protective, nurturing, gives support       " + (!prtscreen ? CHR(9) : "") + CHR(9) + CHR(9) + scale(second) + CHR(13) + CHR(10);
            ex = ex + "Coordinator detached, gets facts and evidence       " + CHR(9) + CHR(9) + scale(third) + CHR(13) + CHR(10);
            ex = ex + "Manager responsible, carries on with duties         " + (prtscreen ? CHR(9) : "") + CHR(9) + CHR(9) + scale(fourth) + CHR(13) + CHR(10) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "ATTITUDES" + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            ex = ex + "Attitudes test one primal urge against another.  Your attitudes reflect your opinions, beliefs, and outlook on life.";
            ex = ex + "  Attitudes give your life a rhythm of development and maturity.  Some attitudes exemplify issues within an age-group";
            ex = ex + " or generation.  However, certain attitudes are common to everyone at specific ages often associated with 'coming of";
            ex = ex + " age' and the quest for identity." + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            total = 12 / (conjunct + sextile + square + trine + oppose);
            ex = ex + "TEMPERAMENT" + CHR(13) + CHR(10);
            ex = ex + "Self-improvement, challenging initiative, resolve    " + CHR(9) + CHR(9) + scale(conjunct * total) + CHR(13) + CHR(10);
            ex = ex + "Self-assurance, confident of ideas, formulation      " + CHR(9) + CHR(9) + scale(sextile * total) + CHR(13) + CHR(10);
            ex = ex + "Self-discipline, drive to succeed, prove oneself     " + CHR(9) + CHR(9) + scale(square * total) + CHR(13) + CHR(10);
            ex = ex + "Self-respect, exuberant faith, honesty, pride        " + CHR(9) + CHR(9) + CHR(9) + scale(trine * total) + CHR(13) + CHR(10);
            ex = ex + "Self-restraint, demand for objectivity, compromise   " + (!prtscreen ? CHR(9) : "") + CHR(9) + scale(oppose * total) + CHR(13) + CHR(10) + CHR(13) + CHR(10);
            total = 12 / (isoft + ihard);


           
            ex = ex + "COMPOSURE" + CHR(13) + CHR(10);
            ex = ex + "Easygoing, assimilating, usually accepted            " + (prtscreen ? CHR(9) : "") + CHR(9) + CHR(9) + scale(isoft / total) + CHR(13) + CHR(10);
            ex = ex + "Tough-minded, confronting, usually misunderstood     " + CHR(9) + scale(ihard / total) + CHR(13) + CHR(10) + CHR(13) + CHR(10);

            return ex;
        }

        static string scale(dynamic pt)
        {
            string string1;
            double remain;
            remain = pt - (int)(pt);
            if (remain >= 0.5)
            {
                pt = (int)(pt) + 1;
                pt = (pt > 12 ? 12 : pt);
            }
            else
            {
                pt = (int)(pt);
                pt = (pt < 1 ? 1 : pt);
            }
            string1 = new string('*', pt);
            return string1;


        }


        public static void polar(double x, double y, ref double z, ref double a)
        {

            z = Math.Sqrt(x * x + y * y);


            if (x == 0.0)
            {
                a = Math.Atan(y / (x - 0.000000001));
            }
            else
            {

                a = Math.Atan(y / x);
                if (y == 0 && x > 0)
                    a = 0.0;
                else if (y == 0 && x < 0)
                    a = p;
                else if (x > 0 && y > 0)
                    a = a * 1;
                else if (x > 0 && y < 0)
                    a = 2 * p + a;
                else if (x < 0 && y > 0)
                    a = p + a;
                else if (x < 0 && y < 0)
                    a = p + a;

            }
        }
        static void rectang(double z, double a, ref double x, ref double y)
        {
            x = z * Math.Cos(a);
            y = z * Math.Sin(a);
        }
        static double VAL(string val)
        {
            return double.Parse(val);
        }

        static double julian(double y, double d, double h)
        {
            double c = (y + 4800) * 12 + h - 3;
            double jd = (int)(c / 48) + (int)((365 * c + 2 * (c - 12 * (int)(c / 12)) + 7) / 12) + d - 32083;
            if (jd > 2299170.0)
                jd = jd + (int)(c / 4800) - (int)(c / 1200) + 38;

            jd = jd - 2415020.0 - 0.5;
            return jd;
        }

        static string SUBSTR(string str, int start, int len)
        {
            return str.Substring(start - 1, len);
        }


        static double univer_t()
        {
            double c, b, d;

            c = VAL(SUBSTR(bd[4], 1, 3)) + VAL(SUBSTR(bd[4], 5, 2)) / 60;
            c = (SUBSTR(bd[4], 4, 1) == "E" ? -1 * c : c);
            m[11] = c;
            c = VAL(SUBSTR(bd[5], 1, 2)) + VAL(SUBSTR(bd[5], 4, 2)) / 60;
            c = (SUBSTR(bd[5], 3, 1) == "S" ? -1 * c : c);
            m[12] = c * r;
            d = VAL(SUBSTR(bd[3], 2, 2)) + VAL(SUBSTR(bd[3], 5, 2)) / 3600;
            d = (SUBSTR(bd[3], 1, 1) == "-" ? -1 * d : d);
            b = VAL(SUBSTR(bd[2], 1, 2)) + VAL(SUBSTR(bd[2], 4, 2)) / 60 + VAL(SUBSTR(bd[2], 7, 2)) / 3600;
            double ut = b + d;
            if (bd[8] != "A")
            {
                ut = ut + 12;
            }

            m[13] = ut;
            return ut;
        }


        static double TAN(double x)
        {
            return Math.Tan(x);
        }

        static double ABS(double x)
        {
            return Math.Abs(x);
        }
        static double COS(double x)
        {
            return Math.Cos(x);
        }

        static double SIN(double x)
        {
            return Math.Sin(x);
        }
        static double SQRT(double x)
        {
            return Math.Sqrt(x);
        }
        static double ATAN(double x)
        {
            return Math.Atan(x);
        }

        static void planet(string type)
        {
            for (int i = 1; i <= 9; i++)
            {
                c[i] = 0;
                f[i] = 0;
                w[i] = 0;
            }

            for (int i = 1; i <= 15; i++)
            {
                ll[i] = 0;
            }
            for (int i = 1; i <= 72; i++)
            {
                mooadata[i] = 0.0;
            }

            c[5] = 11;
            c[6] = 5;
            c[7] = 4;
            c[9] = 4;
            c[8] = 4;
            ll[1] = 0;
            ll[2] = 36;
            ll[3] = 72;
            ll[4] = 0;
            ll[5] = 18;
            ll[6] = 36;
            ll[7] = 0;
            ll[8] = 15;
            ll[9] = 30;
            ll[10] = 0;
            ll[11] = 15;
            ll[12] = 30;
            ll[13] = 0;
            ll[14] = 15;
            ll[15] = 30;
            ct = 0;
            cb = 3;
            dv = 6;
            f[1] = 1.0168;
            f[2] = 1.4835;
            f[3] = 1.745;
            f[4] = 2.6828;
            f[5] = 6.4716;
            f[6] = 11.106;
            f[7] = 21.126;
            f[8] = 31.397;
            f[9] = 50.36;
            w[1] = 0.033502;
            w[2] = 0.966901;
            w[3] = 1.49003;
            w[4] = 2.31814;
            w[5] = 2.53814;
            w[6] = 3.10158;
            w[7] = 3.81481;
            w[8] = 2.57568;
            w[9] = 21.6852;
            moon(type);
            stars(type);
        }
        static void moon(string type)
        {
            double x, w, d, xx, cc, ss, mt;
            double b, e, f, y;
            double fnt_hand;


            b = 0;
            e = 0;
            f = 0;
            y = 0;
            mt = t;
            t = t + 0.0000273785079;
            xx = moon2(ref b, ref e, ref f, ref y);
            t = mt;
            ss = moon2(ref b, ref e, ref f, ref y);
            cc = ss;
            xx = xx - ss;
            xx = (ABS(xx) > 20.0 ? 360.0 + xx : xx);
            w = 18461.5 * SIN(f) + 1010.0 * SIN(b + f) - 999.0 * SIN(f - b) - 624.0 * SIN(f - y) + 199.0 * SIN(f + y - b);
            w = w - 167.0 * SIN(b + f - y) + 117.0 * SIN(f + y) + 62.0 * SIN(2 * b + f) - 33.0 * SIN(f - y - b);
            d = (w - 32.0 * SIN(f - 2 * b) - 30.0 * SIN(e + f - y)) / s;
            x = 3422.6064 + 186.5398 * COS(b) + 34.3117 * COS(b - y) + 28.23 * COS(y) + 10.2 * COS(2 * b);
            x = 6378.14 / SIN(x / 3600.0 * r);
            x = 100.0 * ((406698.0 - x) / 50287.0);
            k[1, 10] = d;
            k[2, 10] = xx;
            k[6, 10] = x;
            k[7, 10] = cc;
        }
        static double moon2(ref double b, ref double e, ref double f, ref double y)
        {
            double a, cc, d;
            a = 973563.0 + 1732564379.0 * t - 4.0 * t * t;
            b = (a - (1203586.0 + 14648523.0 * t - 37.0 * t * t)) / s * r;
            d = 1262655.0 + 1602961611.0 * t - 5.0 * t * t;
            e = ((a - d) - (1012395.0 + 6189.0 * t)) / s * r;
            f = (a - ln / r * s) / s * r;
            cc = 22639.6 * SIN(b);
            d = d / s * r;
            y = 2 * d;
            cc = cc - 4586.4 * SIN(b - y) + 2369.9 * SIN(y) + 769.0 * SIN(2 * b) - 669.0 * SIN(e);
            cc = cc - 411.6 * SIN(2 * f) - 212.0 * SIN(2 * b - y) - 206.0 * SIN(b + e - y) + 192.0 * SIN(b + y);
            cc = cc - 165.0 * SIN(e - y) + 148.0 * SIN(b - e) - 125.0 * SIN(d) - 110.0 * SIN(b + e);
            cc = cc - 55.0 * SIN(2 * f - y) - 45.0 * SIN(b + 2 * f) + 40.0 * SIN(b - 2 * f) - 38.0 * SIN(b - 2 * y);
            cc = a + cc + 36.0 * SIN(3 * b) - 31.0 * SIN(2 * b - 2 * y) - 29.0 * SIN(b - e - y) - 24.0 * SIN(e + y);
            double ss = fnu(cc / s, u);
            return ss;

        }

        static void data(int i, ref double data1, ref double data2, ref double data3, ref double data4, ref double data5,
             ref double data6, ref double data7, ref double data8, ref double data9, ref double data10, ref double data11,
              ref double data12, ref double data13, ref double data14, ref double data15, ref double data16)
        {
            switch (i)
            {
                case 1:
                    data1 = 358.4758445;
                    data2 = 35999.04975;
                    data3 = -0.000150278;
                    data4 = 0.016751040;
                    data5 = -0.00004180;
                    data6 = -0.000000126;
                    data7 = 1.000000230;
                    data8 = 101.2208333;
                    data9 = 1.718175000;
                    data10 = 0.000452778;
                    data11 = 0.0;
                    data12 = 0.0;
                    data13 = 0.0;
                    data14 = 0.0;
                    data15 = 0.0;
                    data16 = 0.0;
                    break;
                case 2:
                    data1 = 102.2793806;
                    data2 = 149472.5152;
                    data3 = 0.000006389;
                    data4 = 0.205614210;
                    data5 = 0.000020460;
                    data6 = -0.00000003;
                    data7 = 0.387098400;
                    data8 = 28.75375278;
                    data9 = 0.370280556;
                    data10 = 0.000120833;
                    data11 = 47.14594444;
                    data12 = 1.185208333;
                    data13 = 0.000173889;
                    data14 = 7.002880556;
                    data15 = 0.001860833;
                    data16 = -0.000018333;
                    break;
                case 3:
                    data1 = 212.6032194;
                    data2 = 58517.80386;
                    data3 = 0.001286056;
                    data4 = 0.006820690;
                    data5 = -0.00004774;
                    data6 = 0.00000009099999;
                    data7 = 0.723331600;
                    data8 = 54.38418611;
                    data9 = 0.508186111;
                    data10 = -0.001386389;
                    data11 = 75.77964722;
                    data12 = 0.89985000;
                    data13 = 0.00041000;
                    data14 = 3.393630556;
                    data15 = 0.001005833;
                    data16 = -0.0000009719998;
                    break;
                case 4:
                    data1 = 319.5294250;
                    data2 = 19139.85850;
                    data3 = 0.000180806;
                    data4 = 0.093312910;
                    data5 = 0.00009206399;
                    data6 = -0.000000077;
                    data7 = 1.523691500;
                    data8 = 285.4317611;
                    data9 = 1.069766667;
                    data10 = 0.000131250;
                    data11 = 48.78644167;
                    data12 = 0.770991667;
                    data13 = -0.00001389;
                    data14 = 1.850333333;
                    data15 = -0.00067500;
                    data16 = 0.000012611;
                    break;
                case 5:
                    data1 = 225.4928125;
                    data2 = 3033.687936;
                    data3 = 0.0;
                    data4 = 0.048381440;
                    data5 = -0.00001550;
                    data6 = 0.0;
                    data7 = 5.202904930;
                    data8 = 273.3930152;
                    data9 = 1.338344640;
                    data10 = 0.0;
                    data11 = 99.41984827;
                    data12 = 1.05829152;
                    data13 = 0.0;
                    data14 = 1.3096585;
                    data15 = -0.00515613;
                    data16 = 0.0;
                    mooadata[1] = -0.001;
                    mooadata[2] = -0.0005;
                    mooadata[3] = 0.0045;
                    mooadata[4] = 0.0051;
                    mooadata[5] = 581.659;
                    mooadata[6] = -9.74;
                    mooadata[7] = -0.0005;
                    mooadata[8] = 2510.654;
                    mooadata[9] = -12.538;
                    mooadata[10] = -0.0026;
                    mooadata[11] = 1313.714;
                    mooadata[12] = -61.41;
                    mooadata[13] = 0.0013;
                    mooadata[14] = 2370.79;
                    mooadata[15] = -24.64;
                    mooadata[16] = -0.0013;
                    mooadata[17] = 3599.29;
                    mooadata[18] = 37.68;
                    mooadata[19] = -0.001;
                    mooadata[20] = 2574.69;
                    mooadata[21] = 31.43;
                    mooadata[22] = -0.00096;
                    mooadata[23] = 6708.18;
                    mooadata[24] = -114.49;
                    mooadata[25] = -0.0006;
                    mooadata[26] = 5499.43;
                    mooadata[27] = -74.97;
                    mooadata[28] = -0.0013;
                    mooadata[29] = 1419.04;
                    mooadata[30] = 54.22;
                    mooadata[31] = 0.0006;
                    mooadata[32] = 6339.27;
                    mooadata[33] = -109.01;
                    mooadata[34] = 0.0007;
                    mooadata[35] = 4824.47;
                    mooadata[36] = -50.85;
                    mooadata[37] = 0.002;
                    mooadata[38] = -0.0134;
                    mooadata[39] = 0.0127;
                    mooadata[40] = -0.0023;
                    mooadata[41] = 676.16;
                    mooadata[42] = 0.9329;
                    mooadata[43] = 0.0005;
                    mooadata[44] = 2361.355;
                    mooadata[45] = 174.953;
                    mooadata[46] = 0.0015;
                    mooadata[47] = 1427.462;
                    mooadata[48] = -188.836;
                    mooadata[49] = 0.0006;
                    mooadata[50] = 2110.129;
                    mooadata[51] = 153.64;
                    mooadata[52] = 0.0014;
                    mooadata[53] = 3606.806;
                    mooadata[54] = -57.67401;
                    mooadata[55] = -0.0017;
                    mooadata[56] = 2540.15;
                    mooadata[57] = 121.74;
                    mooadata[58] = -0.001;
                    mooadata[59] = 6704.78;
                    mooadata[60] = -22.25;
                    mooadata[61] = -0.0006;
                    mooadata[62] = 5480.16;
                    mooadata[63] = 24.51;
                    mooadata[64] = 0.001;
                    mooadata[65] = 1651.28;
                    mooadata[66] = -118.229;
                    mooadata[67] = 0.0006;
                    mooadata[68] = 6310.76;
                    mooadata[69] = -4.827;
                    mooadata[70] = 0.0007;
                    mooadata[71] = 4826.61;
                    mooadata[72] = 36.2451;
                    break;
                case 6:
                    data1 = 174.2152960;
                    data2 = 1223.507963;
                    data3 = 0.0;
                    data4 = 0.05422831;
                    data5 = -0.00020495;
                    data6 = 0.0;
                    data7 = 9.552517450;
                    data8 = 338.9116730;
                    data9 = -0.31667941;
                    data10 = 0.0;
                    data11 = 112.8261394;
                    data12 = 0.82587569;
                    data13 = 0.0;
                    data14 = 2.49080547;
                    data15 = -0.00466035;
                    data16 = 0.0;
                    mooadata[1] = -0.00090;
                    mooadata[2] = 0.003700;
                    mooadata[3] = 0.0;
                    mooadata[4] = 0.013400;
                    mooadata[5] = 1238.847;
                    mooadata[6] = -16.356;
                    mooadata[7] = -0.00426;
                    mooadata[8] = 3040.88;
                    mooadata[9] = -25.174;
                    mooadata[10] = 0.0064;
                    mooadata[11] = 1835.31;
                    mooadata[12] = 36.07;
                    mooadata[13] = -0.0153;
                    mooadata[14] = 610.78;
                    mooadata[15] = -44.21;
                    mooadata[16] = -0.0015;
                    mooadata[17] = 2480.529;
                    mooadata[18] = -69.39;
                    mooadata[19] = -0.0014;
                    mooadata[20] = 0.0026;
                    mooadata[21] = 0;
                    mooadata[22] = 0.0111;
                    mooadata[23] = 1242.164;
                    mooadata[24] = 78.263;
                    mooadata[25] = -0.0045;
                    mooadata[26] = 3034.955;
                    mooadata[27] = 62.76;
                    mooadata[28] = -0.0066;
                    mooadata[29] = 1829.23;
                    mooadata[30] = -51.48;
                    mooadata[31] = -0.0078;
                    mooadata[32] = 640.585;
                    mooadata[33] = 24.183;
                    mooadata[34] = -0.0016;
                    mooadata[35] = 2363.38;
                    mooadata[36] = -141.39;
                    mooadata[37] = 0.00006;
                    mooadata[38] = -0.0002;
                    mooadata[39] = 0.0;
                    mooadata[40] = -0.0005;
                    mooadata[41] = 1251.127;
                    mooadata[42] = 43.68;
                    mooadata[43] = 0.0005;
                    mooadata[44] = 662.81;
                    mooadata[45] = 13.69;
                    mooadata[46] = 0.0003;
                    mooadata[47] = 1824.67;
                    mooadata[48] = -71.108;
                    mooadata[49] = 0.0001;
                    mooadata[50] = 2997.125;
                    mooadata[51] = 78.21;
                    break;
                case 7:
                    data1 = 74.17574887;
                    data2 = 427.2742717;
                    data3 = 0.0;
                    data4 = 0.046816640;
                    data5 = 0.000418750;
                    data6 = 0.0;
                    data7 = 19.22150505;
                    data8 = 95.68630380;
                    data9 = 2.050825480;
                    data10 = 0.0;
                    data11 = 73.52220082;
                    data12 = 0.524155980;
                    data13 = 0.0;
                    data14 = 0.772566520;
                    data15 = 0.000128240;
                    data16 = 0.0;
                    mooadata[1] = -0.0021;
                    mooadata[2] = -0.0159;
                    mooadata[3] = 0.0;
                    mooadata[4] = 0.0299;
                    mooadata[5] = 422.31;
                    mooadata[6] = -17.649;
                    mooadata[7] = -0.0049;
                    mooadata[8] = 3035.09;
                    mooadata[9] = -31.34;
                    mooadata[10] = -0.0038;
                    mooadata[11] = 945.32;
                    mooadata[12] = 60.06;
                    mooadata[13] = -0.0023;
                    mooadata[14] = 1227.02;
                    mooadata[15] = -4.99;
                    mooadata[16] = 0.0134;
                    mooadata[17] = -0.0219;
                    mooadata[18] = 0.0;
                    mooadata[19] = 0.0317;
                    mooadata[20] = 404.32;
                    mooadata[21] = 81.87;
                    mooadata[22] = -0.005;
                    mooadata[23] = 3037.917;
                    mooadata[24] = 57.317;
                    mooadata[25] = 0.004;
                    mooadata[26] = 993.5281;
                    mooadata[27] = -54.39701;
                    mooadata[28] = -0.0018;
                    mooadata[29] = 1249.36;
                    mooadata[30] = 79.206;
                    mooadata[31] = -0.0003;
                    mooadata[32] = 0.0005;
                    mooadata[33] = 0.0;
                    mooadata[34] = 0.0005;
                    mooadata[35] = 352.53;
                    mooadata[36] = -54.98701;
                    mooadata[37] = 0.0001;
                    mooadata[38] = 3027.55;
                    mooadata[39] = 54.17;
                    mooadata[40] = -0.0001;
                    mooadata[41] = 1150.264;
                    mooadata[42] = -88.039;
                    break;
                case 8:
                    data1 = 30.13294370;
                    data2 = 240.4551595;
                    data3 = 0.0;
                    data4 = 0.009128050;
                    data5 = -0.00127185;
                    data6 = 0.0;
                    data7 = 30.11375930;
                    data8 = 284.1682550;
                    data9 = -21.6328615;
                    data10 = 0.0;
                    data11 = 130.6841531;
                    data12 = 1.100464920;
                    data13 = 0.0;
                    data14 = 1.779392810;
                    data15 = -0.00975088;
                    data16 = 0.0;
                    mooadata[1] = 0.1832;
                    mooadata[2] = -0.6718;
                    mooadata[3] = 0.2726;
                    mooadata[4] = -0.1923;
                    mooadata[5] = 175.65;
                    mooadata[6] = 31.829;
                    mooadata[7] = 0.0122;
                    mooadata[8] = 542.139;
                    mooadata[9] = 189.61;
                    mooadata[10] = 0.0027;
                    mooadata[11] = 1219.38;
                    mooadata[12] = 178.12;
                    mooadata[13] = -0.005000001;
                    mooadata[14] = 3035.57;
                    mooadata[15] = -31.29;
                    mooadata[16] = -0.1122;
                    mooadata[17] = 0.166;
                    mooadata[18] = -0.0544;
                    mooadata[19] = -0.005;
                    mooadata[20] = 3035.31;
                    mooadata[21] = 58.68;
                    mooadata[22] = 0.0961;
                    mooadata[23] = 177.067;
                    mooadata[24] = -68.83;
                    mooadata[25] = -0.0073;
                    mooadata[26] = 630.93;
                    mooadata[27] = 51.0;
                    mooadata[28] = -0.0025;
                    mooadata[29] = 1236.56;
                    mooadata[30] = 78.0;
                    mooadata[31] = 0.002;
                    mooadata[32] = -0.0119;
                    mooadata[33] = 0.0111;
                    mooadata[34] = 0.0001;
                    mooadata[35] = 3049.28;
                    mooadata[36] = 44.19;
                    mooadata[37] = -0.0002;
                    mooadata[38] = 893.9481;
                    mooadata[39] = 48.512;
                    mooadata[40] = 0.0001;
                    mooadata[41] = 1416.52;
                    mooadata[42] = -25.18;
                    break;
                case 9:
                    data1 = 229.7810007;
                    data2 = 145.1781092;
                    data3 = 0.0;
                    data4 = 0.247973760;
                    data5 = 0.002898750;
                    data6 = 0.0;
                    data7 = 39.53903455;
                    data8 = 113.5365760;
                    data9 = 0.208637610;
                    data10 = 0.0;
                    data11 = 108.944050;
                    data12 = 1.37395444;
                    data13 = 0.0;
                    data14 = 17.15140319;
                    data15 = -0.01611824;
                    data16 = 0.0;
                    mooadata[1] = -0.0426;
                    mooadata[2] = 0.073;
                    mooadata[3] = -0.029;
                    mooadata[4] = 0.0371;
                    mooadata[5] = 372.0;
                    mooadata[6] = -331.33;
                    mooadata[7] = -0.0049;
                    mooadata[8] = 3049.58;
                    mooadata[9] = -39.196;
                    mooadata[10] = -0.01;
                    mooadata[11] = 566.21;
                    mooadata[12] = 318.26;
                    mooadata[13] = 0.0003;
                    mooadata[14] = 1746.45;
                    mooadata[15] = -238.31;
                    mooadata[16] = -0.0603;
                    mooadata[17] = 0.5002;
                    mooadata[18] = -0.6127;
                    mooadata[19] = 0.049;
                    mooadata[20] = 273.974;
                    mooadata[21] = 89.97801;
                    mooadata[22] = -0.0049;
                    mooadata[23] = 3030.62;
                    mooadata[24] = 61.34;
                    mooadata[25] = 0.0027;
                    mooadata[26] = 1075.28;
                    mooadata[27] = -28.08;
                    mooadata[28] = -0.0007;
                    mooadata[29] = 1402.31;
                    mooadata[30] = 20.31;
                    mooadata[31] = 0.145;
                    mooadata[32] = -0.0928;
                    mooadata[33] = 0.1195;
                    mooadata[34] = 0.0117;
                    mooadata[35] = 302.64;
                    mooadata[36] = -77.31;
                    mooadata[37] = 0.002;
                    mooadata[38] = 528.14;
                    mooadata[39] = 48.56;
                    mooadata[40] = -0.0002;
                    mooadata[41] = 1000.37;
                    mooadata[42] = -46.117;
                    break;
                default: break;
            }

        }
        static void rotate(ref double x, ref double y, ref double f, ref double b, ref double kk, ref double g)
        {

            double j = 0, z = 0, a = 0;
            polar(x, y, ref z, ref a);
            a = a + f;
            rectang(z, a, ref x, ref y);
            j = x;
            polar(y, 0, ref z, ref a);
            a = a + kk;
            rectang(z, a, ref x, ref y);
            g = y;
            polar(j, x, ref z, ref a);
            a = a + b;
            a = (a < 0 ? a + 2.0 * p : a);
            rectang(z, a, ref x, ref y);

        }
        static double readdata(double x, double y, double hh)
        {
            double h = r * (x + y * t + hh * t * t);
            return h;
        }

        static void stars(string type)
        {

            int i, ia, ik, j;
            double x, y, z, a, b, e, ff, h, kk, x4, y4, g, xx, yy, zz;
            double x2, y2, z2, x3, y3, z3, f0, c1, cc, re;
            double data1, data2, data3, data4, data5, data6, data7, data8, data9;
            double data10, data11, data12, data13, data14, data15, data16;
            double d1, d2, d3;
            double fnt_hand, d;
            double m1 = 0;
            double x1 = 0, y1 = 0, z1 = 0;
            double e1 = 0;

            c1 = 0;

            x2 = 0;
            y2 = 0;
            z2 = 0;

            d1 = 0;
            d2 = 0;
            d3 = 0;


            data1 = 0;
            data2 = 0;
            data3 = 0;
            data4 = 0;
            data5 = 0;
            data6 = 0;
            data7 = 0;
            data8 = 0;
            data9 = 0;
            data10 = 0;
            data11 = 0;
            data12 = 0;
            data13 = 0;
            data14 = 0;
            data15 = 0;
            data16 = 0;

            for (i = 1; i <= 9; i++)
            {

                data(i, ref data1, ref data2, ref data3, ref data4, ref data5, ref data6, ref data7, ref data8, ref data9, ref data10, ref data11, ref data12, ref data13, ref data14, ref data15, ref data16);
                h = readdata(data1, data2, data3);
                b = fnu(h, q);
                h = readdata(data4, data5, data6);
                e = h / r;
                cc = b;
                for (ia = 1; ia <= 5; ia++)
                {
                    cc = b + e * SIN(cc);
                }
                d = data7;

                e1 = 0.01720209 / (Math.Pow(d, 1.5) * (1.0 - e * COS(cc)));
                ff = readdata(data8, data9, data10);
                x = d * e1;
                x4 = -x * SIN(cc);
                y4 = x * SQRT(1.0 - e * e) * COS(cc);
                b = readdata(data11, data12, data13);
                kk = readdata(data14, data15, data16);
                x = x4;
                y = y4;
                g = 0.0;
                rotate(ref x, ref y, ref ff, ref b, ref kk, ref g);
                x3 = x;
                y3 = y;
                z3 = g;


                if (i == 1)
                {
                    x2 = -x3;
                    z2 = -z3;
                    y2 = -y3;
                }
                else
                {
                    x4 = x3 + x2;
                    y4 = y3 + y2;
                }
                x = d * (COS(cc) - e);
                y = d * SIN(cc) * SQRT(1.0 - e * e);
                rotate(ref x, ref y, ref ff, ref b, ref kk, ref g);
                xx = x;
                yy = y;
                zz = g;
                if (i >= 5 && i <= 9)
                {
                    for (j = 1; j <= 3; j++)
                    {
                        if (i == 5 && j == 3)
                        {
                            c[3] = 0;

                        }
                        else
                        {
                            d1 = mooadata[1 + (int)ll[(i - 5) * 3 + j]];
                            d2 = mooadata[2 + (int)ll[(i - 5) * 3 + j]];
                            d3 = mooadata[3 + (int)ll[(i - 5) * 3 + j]];
                            h = readdata(d1, d2, d3);
                            a = 0;
                            c[i] = (j == 3 ? c[i] - 1 : c[i]);
                            for (ik = 1; ik <= c[i]; ik++)
                            {

                                b = mooadata[04 + 3 * (ik - 1) + (int)ll[(i - 5) * 3 + j]];
                                cc = mooadata[05 + 3 * (ik - 1) + (int)ll[(i - 5) * 3 + j]];
                                d = mooadata[06 + 3 * (ik - 1) + (int)ll[(i - 5) * 3 + j]];
                                a = a + r * b * COS((cc * t + d) * r);
                            }//endfor
                            c[j] = (h + a) / r;
                        }
                    }//endfor
                    xx = xx + c[2];
                    yy = yy + c[1];
                    zz = zz + c[3];
                }
                b = 0;
                x = xx;
                y = yy;
                z = zz;
                f0 = 0;
                sphere(x, y, ref z, ref b, ref cc, ref kk, i, f0);
                f0 = 1;

                //c1 = 0;
                //re = 0;
                if (i == 1)
                {
                    m1 = z;
                    c1 = cc;
                }
                re = ((Math.Pow(z, 0.5) + Math.Pow(m1, 0.5)) * (Math.Pow(m1, 0.5) * Math.Pow(z, 0.5))) / (Math.Pow(z, 1.5) + Math.Pow(m1, 1.5));
                re = re - COS(c1 * p / 180.0 - cc * p / 180.0);
                k[cb + 2, i] = (xx * y3 - yy * x3) / (xx * xx + yy * yy) / r;
                k[cb, i] = fnu(kk / r, u);
                k[cb + 1, i] = d;


                if (i == 1)
                {
                    x1 = xx;
                    y1 = yy;
                    z1 = zz;
                }
                else
                {
                    xx = xx - x1;
                    yy = yy - y1;
                    zz = zz - z1;
                }
                x = xx * xx + yy * yy;
                e = (xx * y4 - yy * x4) / x;
                x = SQRT(x + zz * zz);
                k[dv, i] = 100 * ((f[i] - x) / w[i]);
                b = 0.0057683 * SQRT(x) / r * e;
                if (i != 1)
                {
                    k[ct + 2, i] = ff / r;
                }
                else
                {
                    k[ct + 2, i] = k[ct + 2, i];
                }
                x = xx;
                y = yy;
                sphere(x, y, ref z, ref b, ref cc, ref kk, i, f0);
                k[7, i] = cc;

                //We have issues here

                k[ct + 1, i] = d;
            }
        }

        static void sphere(double x, double y, ref double z, ref double b, ref double cc, ref double kk, int i, double f0)
        {

            double a = 0;
            double zz = z;
            polar(x, y, ref z, ref a);
            kk = a;
            cc = fnu(a / r + nu - b, u);
            if (i == 1 && f0 > 0)
            {
                cc = fnu(cc + v, u);
            }
            y = zz;
            x = z;
            polar(x, y, ref z, ref a);
            a = (a > 0.35 ? a + q : a);
            rectang(z, a, ref x, ref y);
        }
        static double jd, ut, j2;
        static void calculate(string type, bool therm)
        {
            double c, d, h, y, b;

            h = VAL(SUBSTR(bd[1], 1, 2));
            d = VAL(SUBSTR(bd[1], 4, 2));
            y = VAL(SUBSTR(bd[1], 7, 4));

            jd = julian(y, d, h);
            ut = univer_t();
            j2 = jd + ut / 24;
            m[14] = j2;
            ra = ramc(j2, ut);
            planet(type);
            if (type == "natal")
            {
                midheaven();
                prt_planet();
                save_stars("natal");
                houses(ihouse);
                prt_house();
                save_house();
                into_dbf4();
                into_dbf5();

                into_dbf1();
                into_dbf2();
                into_dbf3();

                person = bd[7];

                printvadi();
            }
            else
            {
                save_stars("transit");
            }

        }



        static double ramc(double x, double m13)
        {
            double y;
            t = x / 36525.0;
            ra = fnu((6.64606556 + t * ((975112027.0 / 406288) + 0.0000258055555 * t) + m13) * 15, u);
            y = (((-361.1 * t + 895.4) * t - 693.7) * t + 239.25) * t - 7.396;
            t = x + y / 86400.0;
            t = t / 36525.0;
            ln = fnu(259.18328 - 1934.1419 * t, u) * r;
            ms = fnu(279.69668 + 36000.7689 * t, u) * r;
            ob = 84428.26 - t * (46.845 - t * (0.0059 + 0.00181 * t));
            ob = (ob + (9.21 + 0.00091 * t) * Math.Cos(ln) + (0.5522 - 0.00029 * t) * Math.Cos(2 * ms)) / s * r;
            m[8] = ob;
            nu = (-1 * (17.2327 + 0.01737 * t) * Math.Sin(ln) - 1.273 * Math.Sin(2 * ms) + 0.2088 * Math.Sin(2 * ln)) / 3600;
            x = -23.3439306 - (5025.64 + 1.11 * t) * t / s - nu;
            m[1] = (m[1] == 1 ? x : m[1]);
            m[9] = x;
            k[7, 17] = fnu(ln / r, u);
            k[1, 17] = 0;
            k[2, 17] = 0.05;
            ra = fnu(ra + nu * Math.Cos(ob) - m[11], u) * r;
            k[7, 13] = k[7, 17];
            return ra;

        }


        static double fnu(double x, double m)
        {
            double retx = 0;

            if (x >= 0.0)
                retx = x - (int)(x / m) * m;
            else
                retx = x - (int)((x / m) - 1) * m;

            return retx;
        }

        static void trnform(double l, double b, ref double g)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            double a = 0;
            rectang(1, b, ref x, ref y);
            double qq = y;
            rectang(x, l, ref x, ref y);
            g = x;
            polar(y, qq, ref z, ref a);
            a = a - ob;
            rectang(z, a, ref x, ref y);
            qq = ATAN(y / Math.Pow((1.0 - y * y), 0.5));
            polar(g, x, ref z, ref a);
            a = (a < 0.0 ? a + qq : a);
            g = a;
        }

        static double b = 0;
        static void midheaven()
        {
            double x = 0;
            double y = 0;
            double z = 0;
            double g = 0;
            double a = 0;
            rectang(1, ra, ref x, ref y);
            x = x * COS(-1 * ob);
            polar(x, y, ref z, ref a);
            mc = a / p * v;
            k[7, 12] = mc;
            b = m[12];
            trnform(ra, b, ref g);
            ra = (b < 0.0 ? ra + p : ra);
            aa = (g + p * 0.5) / p * v;
            aa = fnu(aa, u);
            k[7, 11] = aa;
            g = k[7, 10] - k[7, 1];
            g = (g < 0.0 ? g + u : g);
            pf = aa + g;
            pf = (pf >= u ? pf - u : pf);
            k[7, 14] = pf;
        }
        static void prt_planet()
        {
            double rr = 0;

            Console.WriteLine("PLANET      POSITION     SIGN      LONGITUDE");
            for (int j = 1; j <= 10; j++)
            {
                rr = k[7, j];
                say_planet(rr, j, 7 + j, false, "");
            }
            rr = k[7, 13];
            say_planet(r, 13, 18, false, "");
            Console.WriteLine("L.S.TIME");
            rr = ra / p * v / u * 24.0;
            string time = degminsec(rr);
            Console.WriteLine(time);
            rr = aa;
            say_planet(rr, 11, 20, false, "");
            rr = mc;
            say_planet(rr, 12, 21, false, "");
            rr = pf;
            say_planet(rr, 14, 22, false, "");
        }
        static int which_sign(double r)
        {
            int isign = (int)(r / 30.0);
            isign = (isign * 30 < r ? isign + 1 : isign);
            return (isign);
        }
        static string degminsec(double rd)
        {
            int id = (int)(rd);
            string dd = id.ToString();
            if (dd.Length == 1)
            {
                dd = '0' + dd;
            }
            string pos = dd;
            double rm = (rd - id) * 60.0;
            int im = (int)(rm);
            dd = im.ToString();
            if (dd.Length == 1)
            {
                dd = '0' + dd;
            }
            pos = pos + ":" + dd;
            double rs = (rm - im) * 60.0;
            int isx = (int)(rs);
            dd = isx.ToString();
            if (dd.Length == 1)
            {
                dd = '0' + dd;
            }
            pos = pos + ":" + dd;
            return pos;
        }
        static string CHR(int n)
        {
            return Char.ConvertFromUtf32(n);

        }
        static string say_planet(double rr, int j, int i, bool prtscreen, string ex)
        {
            int isign = which_sign(rr);
            string pos = degminsec(rr - (isign - 1) * 30.0);
            string p = planets[j];
            //rr = ALLTRIM(STR(rr, 6, 2));
            //rr = REPLICATE('0', 6 - LEN(rr)) + rr;
            if (!prtscreen)
            {
                Console.WriteLine($"{p} {pos} {signs[isign]} {rr:000.00}");
            }
            else
            {
                switch (i)
                {
                    case 1:
                    case 3:
                    case 4:
                    case 9:
                    case 10:
                    case 11:
                    case 14:
                        ex = p + CHR(9) + CHR(9) + CHR(9) + pos + CHR(9) + '(' + rr + ')' + CHR(9) + signs[isign] + CHR(13) + CHR(10);
                        break;
                    case 2:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        ex = p + CHR(9) + CHR(9) + pos + CHR(9) + '(' + rr + ')' + CHR(9) + signs[isign] + CHR(13) + CHR(10);
                        break;
                    case 12:
                    case 13:
                        ex = p + CHR(9) + pos + CHR(9) + '(' + rr + ')' + CHR(9) + signs[isign] + CHR(13) + CHR(10);
                        break;
                    default: break;
                }

                Console.WriteLine(ex);
            }


            return ex;
        }

        static void placidus()
        {
            double a = 0.0;
            hse[4] = (mc * p / v + p);
            hse[4] = ((hse[4] > q ? hse[4] - q : hse[4]));
            hse[1] = (aa * p / v);
            double b = ra + 30.0 * p / v;
            geometry(0, 3.0, b, ref a);
            hse[5] = (a + p);
            hse[5] = (hse[5] > q ? hse[5] - q : hse[5]);
            b = ra + 60.0 * p / v;
            geometry(0, 1.5, b, ref a);
            hse[6] = (a + p);
            hse[6] = (hse[6] > q ? hse[6] - q : hse[6]);
            b = ra + 120.0 * p / v;
            geometry(1, 1.5, b, ref a);
            hse[2] = a;
            b = ra + 150.0 * p / v;
            geometry(1, 3.0, b, ref a);
            hse[3] = a;
            last_six();
        }

        static void last_six()
        {

            for (int i = 1; i <= 6; i++)
            {
                hse[i] = (hse[i] < 0 ? hse[i] + 2 * p : hse[i]);
                hse[i] = (hse[i] * v / p);
                hse[i + 6] = (hse[i] + v);
                hse[i + 6] = ((hse[i + 6] > v ? fnu(hse[i + 6], u) : hse[i + 6]));
            }
        }
        static void geometry(double y, double g, double b, ref double a)
        {
            double ll = 0;



            if (y == 1)
            {
                ll = 1;
            }
            else
            {
                ll = -1;
            }

            for (int i = 1; i <= 10; i++)
            {
                double x = ll * SIN(b) * TAN(ob) * TAN(m[12]);
                x = ATAN(SQRT(1.0 - x * x) / x);
                x = (x < 0 ? x + p : x);
                double c = (y == 1 ? ra + p - (x / g) : ra + x / g);
                b = c;
            }
            a = ATAN(TAN(b) / COS(ob));
            a = (a < 0 ? a + p : a);
            a = (SIN(b) < 0.0 ? a + p : a);
        }




        static void save_stars(string xtype)
        {


            if (xtype == "natal")
            {
                for (int j = 1; j <= 10; j++)
                {
                    mnatal[j] = k[7, j];
                }
                mnatal[11] = aa;
                mnatal[12] = mc;
                mnatal[13] = k[7, 13];
                mnatal[14] = pf;
            }
            else
            {
                for (int j = 1; j <= 10; j++)
                {
                    mtransit[j] = k[7, j];
                }
            }
        }
        static void houses(int i)
        {


            switch (i)
            {
                case 1:

                    house = "PLACIDUS";
                    placidus();
                    break;
                default: break;
            }
        }



        static void say_house(double r, int j, int i, bool prtscreen, string ex)
        {
            int isign = which_sign(r);
            string pos = degminsec(r - (isign - 1) * 30.0);
            if (!prtscreen)
            {
                Console.WriteLine($"[{j}] [{pos}] [{signs[isign]}] [{r}]");
            }
            else
            {
                ex = (j.ToString()) + CHR(9) + CHR(9) + pos + CHR(9) + '(' + r + ')' + CHR(9) + signs[isign] + CHR(13) + CHR(10);
                Console.WriteLine(ex);
            }
        }

        static void prt_house()
        {
            Console.WriteLine("HOUSE SYSTEM:" + house);
            Console.WriteLine("HSE POSITION   SIGN     LONGITUDE");
            for (int j = 1; j <= 12; j++)
            {
                double r = hse[j];
                say_house(r, j, 7 + j, false, "");
            }
        }



        static void save_house()
        {
            for (int j = 1; j <= 12; j++)
            {
                mhouse[j] = hse[j];
            }

        }

        static void tables()
        {
            int i;
            double x1, x2, y1, y2; char glyph;
            x1 = xx0 + pixelx;
            double yy0 = y0 + r1 - 12 * (pixely + 5);
            x2 = 0;

            for (i = 1; i <= 11; i++)
            {
                x2 = xx0 + (i + 1) * pixelx;
                y1 = yy0 + (i - 1) * (pixely + 5);
                y2 = y1;
                Common.drawline(Common.atx(x1, 0), Common.aty(y1, 0), Common.atx(x2, 0), Common.aty(y2, 0));
            }
            y1 = yy0 + 11 * (pixely + 5);
            y2 = y1;
            Common.drawline(Common.atx(x1, 0), Common.aty(y1, 0), Common.atx(x2, 0), Common.aty(y2, 0));
            y2 = y0 + r1 - (pixely + 5);
            for (i = 1; i <= 11; i++)
            {
                x1 = xx0 + (i + 1) * pixelx;
                y1 = yy0 + (i - 1) * (pixely + 5);
                x2 = x1;
                Common.drawline(Common.atx(x1, 0), Common.aty(y1, 0), Common.atx(x2, 0), Common.aty(y2, 0));
            }
            x1 = xx0 + pixelx;
            x2 = x1;
            y1 = yy0;
            Common.drawline(Common.atx(x1, 0), Common.aty(y1, 0), Common.atx(x2, 0), Common.aty(y2, 0));
            y2 = y0 + r1;
            for (i = 1; i <= 11; i++)
            {
                switch (i)
                {
                    case 1:
                        glyph = '.'; break;
                    case 2:
                        glyph = 'M'; break;
                    case 3:
                        glyph = 'v'; break;
                    case 4:
                        glyph = 'm'; break;
                    case 5:
                        glyph = 'j'; break;
                    case 6:
                        glyph = 't'; break;
                    case 7:
                        glyph = 'u'; break;
                    case 8:
                        glyph = 'n'; break;
                    case 9:
                        glyph = 'P'; break;
                    case 10:
                        glyph = '/'; break;
                    case 11:
                        glyph = ':'; break;
                }
                x2 = xx0 + i * pixelx;
                //= font_say(IIF(i = 11, "astro_09", "astro_13"), x2, y2, glyph)
            }
            x2 = xx0;
            for (i = 1; i <= 11; i++)
            {
                switch (i)
                {
                    case 1:
                        glyph = 'M'; break;
                    case 2:
                        glyph = 'v'; break;
                    case 3:
                        glyph = 'm'; break;
                    case 4:
                        glyph = 'j'; break;
                    case 5:
                        glyph = 't'; break;
                    case 6:
                        glyph = 'u'; break;
                    case 7:
                        glyph = 'n'; break;
                    case 8:
                        glyph = 'P'; break;
                    case 9:
                        glyph = '/'; break;
                    case 10:
                        glyph = ':'; break;
                    case 11:
                        glyph = ' '; break;
                }
                y2 = yy0 + i * (pixely + 5);
                //= font_say(IIF(i >= 10,   "astro_09", "astro_13"),x2,y2,  glyph)
            }


        }


        static void signelem(int ie_color1, int ie_color2, int ie_color3, int ie_color4, int it_color, bool prtscreen)
        {

            double x1, x2, y1, y2;
            int i, j, isign;
            double[,] elems = new double[3 + 1, 4 + 1];
            for (i = 1; i <= 3; i++)
            {
                for (j = 1; j <= 4; j++)
                {
                    elems[i, j] = 0;
                }
            }
            yy0 = y0 - r1 + 5;
            x1 = xx0;
            x2 = x1 + (pixelx * 2) * 5;
            for (i = 1; i <= 5; i++)
            {
                y1 = yy0 + (i - 1) * (pixely + 5);
                y2 = y1;
                Common.drawline(Common.atx(x1, 0), Common.aty(y1, 0), Common.atx(x2, 0), Common.aty(y2, 0));
            }
            y1 = yy0;
            y2 = yy0 + (pixely + 5) * 4;
            for (i = 1; i <= 6; i++)
            {
                x1 = xx0 + (pixelx * 2) * (i - 1);
                x2 = x1;
                Common.drawline(Common.atx(x1, 0), Common.aty(y1, 0), Common.atx(x2, 0), Common.aty(y2, 0));
            }
            y1 = yy0 + (pixely + 5) * 1.5 + 7;
            x1 = xx0 + (pixelx * 2) * 0.5;
            //= font_say("arial_08",atx(x1,0),  aty(y1,0),'CAR')
            y1 = yy0 + (pixely + 5) * 2.5 + 7;
            //= font_say("arial_08",atx(x1,0),  aty(y1,0),'FIX')
            y1 = yy0 + (pixely + 5) * 3.5 + 7;
            //= font_say("arial_08",atx(x1,0),  aty(y1,0),'MUT')
            y1 = yy0 + (pixely + 5) * 0.5 + 7;
            x1 = xx0 + (pixelx * 2) * 1.5;
            //IF  .NOT. prtscreen
            //     = set_brush(1,ie_color1,2,  ie_color1)
            //     = fillarea(x1,y1,il_color,1)
            //ENDIF
            //= font_say("arial_08",x1,y1,'FIR')
            x1 = xx0 + (pixelx * 2) * 2.5;
            //IF  .NOT. prtscreen
            //     = set_brush(1,ie_color2,2,  ie_color2)
            //     = fillarea(x1,y1,il_color,1)
            //ENDIF
            //= font_say("arial_08",atx(x1,0),  aty(y1,0),'EAR')
            x1 = xx0 + (pixelx * 2) * 3.5;
            //IF  .NOT. prtscreen
            //     = set_brush(1,ie_color3,2,  ie_color3)
            //     = fillarea(x1,y1,il_color,1)
            //ENDIF
            //= font_say("arial_08",atx(x1,0),  aty(y1,0),'AIR')
            x1 = xx0 + (pixelx * 2) * 4.5;
            //IF  .NOT. prtscreen
            //     = set_brush(1,ie_color4,2,  ie_color4)
            //     = fillarea(x1,y1,il_color,1)
            //     = set_brush(0,0)
            //ENDIF
            //= font_say("arial_08",atx(x1,0),  aty(y1,0),'WAT')
            for (i = 1; i <= 10; i++)
            {
                isign = which_sign(k[7, i]);
                switch (isign)
                {
                    default: break;
                    case 1:
                        elems[1, 1] = elems[1, 1] + 1; break;
                    case 2:
                        elems[2, 2] = elems[2, 2] + 1; break;
                    case 3:
                        elems[3, 3] = elems[3, 3] + 1; break;
                    case 4:
                        elems[1, 4] = elems[1, 4] + 1; break;
                    case 5:
                        elems[2, 1] = elems[2, 1] + 1; break;
                    case 6:
                        elems[3, 2] = elems[3, 2] + 1; break;
                    case 7:
                        elems[1, 3] = elems[1, 3] + 1; break;
                    case 8:
                        elems[2, 4] = elems[2, 4] + 1; break;
                    case 9:
                        elems[3, 1] = elems[3, 1] + 1; break;
                    case 10:
                        elems[1, 2] = elems[1, 2] + 1; break;
                    case 11:
                        elems[2, 3] = elems[2, 3] + 1; break;
                    case 12:
                        elems[3, 4] = elems[3, 4] + 1; break;
                }
            }
            for (i = 1; i <= 3; i++)
            {
                for (j = 1; j <= 4; j++)
                {
                    x1 = xx0 + (pixelx * 2) * (j + 0.5);
                    y1 = yy0 + (pixely + 5) * (i + 0.5) + 7;
                    //= font_say("arial_09", atx(x1,0),aty(y1,0),  ALLTRIM(STR(elems(i, j))))
                }

            }
        }


        static double masptotal = 0;
        static void asp_lines(int i, int j, int n, int ll, int a, double r4, int ia_color1, int ia_color2, ref int kk, string type, bool aspt_dbf)
        {
            double da, x1, y1, x2, y2, color, r44;
            string asp, asxx;
            r44 = r4 - 2;
            if (type == "natal")
                da = (mnatal[n] + 180 - aa) * Math.PI / 180.0;
            else
                da = (mtransit[n] + 180 - aa) * Math.PI / 180.0;

            x1 = x0 + r44 * COS(da);
            y1 = y0 - r44 * SIN(da);
            da = (mnatal[(j)] + 180 - aa) * Math.PI / 180.0;
            x2 = x0 + r44 * COS(da);
            y2 = y0 - r44 * SIN(da);
            color = (i == 1 ? ia_color1 : ia_color2);
            //= set_pen(color);
            Common.drawline(Common.atx(x1, 0), Common.aty(y1, 0), Common.atx(x2, 0), Common.aty(y2, 0));
            //= set_pen(il_color);
            kk = kk + 1;
            masptotal = kk;
            masplines[kk, 1] = x1;
            masplines[kk, 2] = y1;
            masplines[kk, 3] = x2;
            masplines[kk, 4] = y2;
            if (type == "natal")
            {
                if (aspt_dbf)
                {
                    //SELECT aspts
                    // APPEND BLANK
                    bool h_or_s = (i == 1 ? true : false);
                    double aspect = aspects[i, ll];
                    double orb = a;
                    string planet1 = planets[j];
                    string planet2 = planets[n];
                    string group = (planets[j]) + '-' + (planets[n]);
                    string index3 = SUBSTR(planets[j], 1, 2) + SUBSTR(planets[n], 1, 2) + (i == 1 ? "-S" : "-H");

                    Console.WriteLine($"NAT-ASPT-DBF:{index3}/{group}/{planet1}/{planet2}/{orb}/{aspect}/{h_or_s}");


                }
            }
            else
            {

                bool h_or_s = (i == 1 ? true : false);
                double aspect = aspects[i, ll];
                double orb = a;
                string nplanet = planets[j];
                string tplanet = planets[n];
                string date = bd[1];
                string group = (planets[n]) + '-' + (planets[j]);
                switch (aspect)
                {
                    case 0:
                        asp = "000"; break;
                    case 360:
                        asp = "000"; break;
                    case 120:
                        asp = "120"; break;
                    case 240:
                        asp = "120"; break;
                    case 150:
                        asp = "030"; break;
                    case 30:
                        asp = "030"; break;
                    case 60:
                        asp = "060"; break;
                    case 45:
                        asp = "045"; break;
                    case 135:
                        asp = "045"; break;
                    case 90:
                        asp = "090"; break;
                    case 180:
                        asp = "180"; break;
                    case 210:
                        asp = "030"; break;
                    case 300:
                        asp = "060"; break;
                    case 330:
                        asp = "030"; break;
                    case 225:
                        asp = "045"; break;
                    case 270:
                        asp = "090"; break;
                    case 315:
                        asp = "045"; break;
                    default:
                        asxx = $"{aspect:000}";
                        asp = asxx;//IIF(LEN(asx) = 2, '0', '') + asx;
                        break;
                }
                string index5 = "      ";
                string smash = SUBSTR(index5, 1, 2);
                switch (smash)
                {
                    case "VE":
                        index5 = SUBSTR(planets[n], 1, 2) + SUBSTR(planets[j], 1, 2) + asp + "FI";
                        h_or_s = (i == 1 ? true : false);
                        aspect = aspects[i, ll];
                        orb = a;
                        nplanet = planets[(j)];
                        tplanet = planets[(n)];
                        date = bd[1];
                        group = (planets[n]) + '-' + (planets[j]);
                        index5 = SUBSTR(planets[n], 1, 2) + SUBSTR(planets[j], 1, 2) + asp + "RO";
                        break;
                    case "ME":
                        index5 = SUBSTR(planets[n], 1, 2) + SUBSTR(planets[j], 1, 2) + asp + "CO";
                        h_or_s = (i == 1 ? true : false);
                        aspect = aspects[i, ll];
                        orb = a;
                        nplanet = planets[(j)];
                        tplanet = planets[(n)];
                        date = bd[(1)];
                        group = (planets[(n)]) + "-" + (planets[(j)]);
                        index5 = SUBSTR(planets[(n)], 1, 2) + SUBSTR(planets[(j)], 1, 2) + asp + "WO";
                        break;
                    default:
                        index5 = SUBSTR(planets[(n)], 1, 2) + SUBSTR(planets[(j)], 1, 2) + asp;
                        break;
                }

                trans1.Add(new TRN_ASP() { index5 = index5, group = group, aspect = aspect, date = date, h_or_s = h_or_s, nplanet = nplanet, orb = orb, tplanet = tplanet });
                //Console.WriteLine($"ASPLINES:{index5}/{group}/{nplanet}/{tplanet}/{orb}/{aspect}/{h_or_s}/{date}");
            }
        }

        static List<TRN_ASP> trans1 = new List<TRN_ASP>();


        static void transits()
        {
            int i;
            //SELECT stars
            //do_it = IIF(RECNO() > 0, .T.,   .F.)
            mtrn_pos[1, 1] = 0.0;
            mtrn_pos[1, 2] = 0.0;

            asp_erase();
            
            Console.WriteLine("Career , Family, Relation, Aspiration, GENERAL");
            Console.WriteLine("Work   , Health, Romance , Fitness   ,        ");
            Console.WriteLine("Finance, Safety, Friends , Creative  ,        ");
            Console.WriteLine("       ,       ,         , Education ,        ");

            for (i = 1; i <= intervals; i++)
            {
                set_data(i);
                writ_date();
                calculate("transit", false);
                if (mtrn_pos[1, 1] != 0.0 || mtrn_pos[1, 2] != 0.0)
                {
                    plt_erase();
                }
                chart2(true, false);
            }
        }


        static void draw_star(double r2b, double r3, string type)
        {

            int i, inc;
            double x1, y1;
            char glyph;
            int j;
            double r8, a;
            bool yes_minus;
            double x2, y2, segment; int xshift, yshift; double font_shift;
            xshift = 6;
            yshift = 13;
            font_shift = 7.5;
            segment = (r2b - r3) / 6.0;
            x2 = 0; y2 = 0;
            for (i = 1; i <= 10; i++)
            {
                if (type == "natal")
                {
                    r8 = r2b - segment;
                }
                else
                {
                    r8 = r3 + segment;
                }
                a = (k[7, i] + v - aa) * Math.PI / 180.0;
                for (j = 1; j2 <= (i - 1); j++)
                {
                    if (ABS(k[7, i] - k[7, j]) <= font_shift)
                        if (type == "natal")
                            r8 = r8 - segment * 2;
                        else
                            r8 = r8 + segment * 2;


                }
                switch (i)
                {
                    case 1:
                        glyph = '.';
                        x2 = xshift - 2;
                        y2 = yshift; break;
                    case 2:
                        glyph = 'M';
                        x2 = xshift - 2;
                        y2 = yshift; break;
                    case 3:
                        glyph = 'v';
                        x2 = xshift - 2;
                        y2 = yshift; break;
                    case 4:
                        glyph = 'm';
                        x2 = xshift - 2;
                        y2 = yshift; break;
                    case 5:
                        glyph = 'j';
                        x2 = xshift - 2;
                        y2 = yshift; break;
                    case 6:
                        glyph = 't';
                        x2 = xshift - 2;
                        y2 = yshift; break;
                    case 7:
                        glyph = 'u';
                        x2 = xshift - 2;
                        y2 = yshift; break;
                    case 8:
                        glyph = 'n';
                        x2 = xshift - 2;
                        y2 = yshift; break;
                    case 9:
                        glyph = 'P';
                        x2 = xshift - 2;
                        y2 = yshift; break;
                    case 10:
                        glyph = '/';
                        x2 = xshift - 2;
                        y2 = yshift; break;
                }
                x1 = x0 + r8 * COS(a) + x2;
                y1 = y0 - r8 * SIN(a) + y2;
                //= set_font("astro_20p", 0, 2, 1, 1, 2, IIF(type = 'natal', 18, 21));
                // = font_say("astro_20p", atx(x1, 0), aty(y1, 0), glyph);
                if (type == "natal")
                {
                    mnat_pos[i, 1] = x1;
                    mnat_pos[i, 2] = y1;
                }
                else
                {
                    mtrn_pos[i, 1] = x1;
                    mtrn_pos[i, 2] = y1;
                }
            }
            if (type == "natal")
            {
                for (i = 13; i <= 14; i++)
                {
                    yes_minus = false;
                    r8 = r2b - segment;
                    a = (k[7, i] + v - aa) * Math.PI / 180.0;
                    for (j = 1; j <= 10; j++)
                    {
                        if (ABS(k[7, i] - k[7, j]) <= font_shift)
                        {
                            r8 = r8 - segment * 2;
                            yes_minus = true;
                        }
                    }
                    if ((i == 14) && (!yes_minus) && ((k[7, 13] - k[7, 14]) <= 5.0))
                    {
                        r8 = r8 - segment * 2;
                    }
                    if (i == 13)
                    {
                        glyph = '0';
                        x2 = xshift - 4;
                        y2 = yshift - 4;
                    }
                    else
                    {
                        glyph = 'E';
                        x2 = xshift - 4;
                        y2 = yshift - 4;
                    }
                    x1 = x0 + r8 * COS(a) + x2;
                    y1 = y0 - r8 * SIN(a) + y2;
                    //= font_say("astro_12", atx(x1, 0), aty(y1, 0), glyph);
                    mnat_pos[i, 1] = Common.atx(x1, 0);
                    mnat_pos[i, 2] = Common.aty(y1, 0);
                }
            }
        }

        static void asp_table(int i, int j, int n, int ll, int ia_color1, int ia_color2, ref int iorb)
        {


            string glyph;
            double x1, y1;
            yy0 = y0 + r1 - 12 * (pixely + 5);
            if (j > n)
            {
                x1 = xx0 + (n - 1) * pixelx + 1.5 * pixelx + 2;
                y1 = yy0 + (j - 2) * (pixely + 5) + 0.5 * (pixely + 5) + 6;
            }
            else
            {
                x1 = xx0 + (j - 1) * pixelx + 1.5 * pixelx + 2;
                y1 = yy0 + (n - 2) * (pixely + 5) + 0.5 * (pixely + 5) + 6;
            }
            if (i == 1)
            {
                ll = (ll == 10 ? 1 : ll);
                ll = (ll == 9 ? 2 : ll);
                ll = (ll == 5 ? 2 : ll);
                ll = (ll == 6 ? 2 : ll);
                ll = (ll == 8 ? 3 : ll);
                ll = (ll == 7 ? 4 : ll);
                glyph = (CHR(ll));
            }
            else
            {
                ll = (ll == 5 ? 1 : ll);
                ll = (ll == 6 ? 2 : ll);
                ll = (ll == 7 ? 1 : ll);
                glyph = (CHR(ll + 5));
            }
            //= font_say(IIF(i = 1, "astro_9a1",   "astro_9a2"),atx(x1,0),aty(y1,0),  glyph)
            iorb = iorb + 1;
            morb_pos[iorb, 1] = Common.atx(x1, 0);
            morb_pos[iorb, 2] = Common.aty(y1, 0);
            // NEED FIX
            string ss =  (i == 1 ? "S-" : "H-") + SUBSTR(planets[j], 1, 2) + SUBSTR(planets[n], 1, 2) + ((CHR(ll)));
            Console.WriteLine($"------------------Need fix:{ss}");
            // morb_asp[iorb] = (i == 1 ? "S-" : "H-") + SUBSTR(planets[j], 1, 2) + SUBSTR(planets[n], 1, 2) + ((CHR(ll)));
        }




        

        static void draw_aspt(bool table, double r4, int ia_color1, int ia_color2, string type, bool aspt_dbf, bool biodex)
        {



            int i, j, ll, kk, n, a, iorb;
            double r, m, nn;
            double sh1, sh2, sh3, sh4;
            if (type == "natal")
            {
                nn = 12;
                iorb = 0;
                kk = 0;
                for (i = 1; i <= 2; i++)
                {
                    for (j = 1; j <= 12; j++)
                    {
                        for (n = 1; n <= nn; n++)
                        {
                            if (j != n)
                            {
                                r = mnatal[n] - mnatal[j];
                                r = (r < 0.0 ? r + 360.0 : r);
                                m = 10 - (i - 1) * 3;
                                for (ll = 1; ll <= m; ll++)
                                {
                                    a = (int)ABS(r - aspects[i, ll]);
                                    if (a <= orbs[i, ll])
                                    {
                                        if (table)
                                            asp_table(i, j, n, ll, ia_color1, ia_color2, ref iorb);
                                        else
                                            asp_lines(i, j, n, ll, a, r4, ia_color1, ia_color2, ref kk, type, aspt_dbf);

                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (biodex)
                {
                    sh1 = 0;
                    sh2 = 0;
                    sh3 = 0;
                    sh4 = 0;
                    trn_aspect(ref sh1, ref sh2, ref sh3, ref sh4);
                    lunarcycle(ref sh1, ref sh2, ref sh3, ref sh4);
                    lunarsign(ref sh1, ref sh2, ref sh3, ref sh4);
                    //SELECT trans2
                    //APPEND BLANK
                    string date = bd[1];
                    double ratio1 = sh1;
                    double ratio2 = sh2;
                    double ratio3 = sh3;
                    double ratio4 = sh4;
                    double ratio5 = (sh1 + sh2 + sh3 + sh4);
                    Console.WriteLine($"BIODEX:{date}|{ratio1,5:###.#}|{ratio2,5:###.#}|{ratio3,5:###.#}|{ratio4,5:###.#}|{ratio5,5:###.#}");

                    /*
                     ratio1: Career/Work/Finance
                     ratio2: Family/Health/Safety
                     ratio3: Relation/Romance/Friends
                     ratio4: Aspiration/Fitness/Creative/Education
                     ratio5: GENERAL

                     */

                }
                else
                {
                    nn = 10;
                    kk = 0;
                    for (i = 1; i <= 2; i++)
                    {
                        for (j = 1; j <= 12; j++)
                        {
                            for (n = 1; n <= nn; n++)
                            {
                                r = mnatal[j] - mtransit[n];
                                r = (r < 0.0 ? r + 360.0 : r);
                                m = 10 - (i - 1) * 3;
                                for (ll = 1; ll <= m; ll++)
                                {
                                    a = (int)ABS(r - aspects[i, ll]);
                                    if (a <= trn_orbs[i, ll])
                                    {
                                        asp_lines(i, j, n, ll, a, r4, ia_color1, ia_color2, ref kk, type, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }





        static void plt_erase()
        {
            int i; char glyph; double x1, y1; int fnt_hand;
            for (i = 1; i <= 10; i++)
            {
                switch (i)
                {
                    case 1:
                        glyph = '.'; break;
                    case 2:
                        glyph = 'M'; break;
                    case 3:
                        glyph = 'v'; break;
                    case 4:
                        glyph = 'm'; break;
                    case 5:
                        glyph = 'j'; break;
                    case 6:
                        glyph = 't'; break;
                    case 7:
                        glyph = 'u'; break;
                    case 8:
                        glyph = 'n'; break;
                    case 9:
                        glyph = 'P'; break;
                    case 10:
                        glyph = '/'; break;
                    default: break;
                }
                x1 = mtrn_pos[i, 1];
                y1 = mtrn_pos[i, 2];
                // = font_say("astro_20", atx(x1, 0), aty(y1, 0), glyph)
            }
        }
        static void writ_date()
        {
            //        PRIVATE d
            //          = txt_erase()
            //Console.WriteLine(bd[1]);
            //= font_say("arial_09", atx(x0, 0), aty(y0, 0), d)
        }

        static void trn_aspect(ref double sh1, ref double sh2, ref double sh3, ref double sh4)
        {
            sh1 = soft_hard(1);
            sh2 = soft_hard(2);
            sh3 = soft_hard(3);
            sh4 = soft_hard(4);
        }
        static int soft_hard(int iclass)
        {
            int i, ii, sh, isoft, ihard;
            int[] inat = new int[3 + 1];
            //PRIVATE inat;

            isoft = 0;
            ihard = 0;
            switch (iclass)
            {
                case 1:
                    inat[1] = 6;
                    inat[2] = 3;
                    inat[3] = 2; break;
                case 2:
                    inat[1] = 10;
                    inat[2] = 8;
                    inat[3] = 9; break;
                case 3:
                    inat[1] = 3;
                    inat[2] = 7;
                    inat[3] = 2; break;
                case 4:
                    inat[1] = 4;
                    inat[2] = 1;
                    inat[3] = 5; break;
                default: break;
            }
            for (i = 1; i <= 3; i++)
            {
                ii = inat[i];
                check_orb(ii, 1, 1, ref isoft, ref ihard);
                check_orb(ii, 1, 7, ref isoft, ref ihard);
                check_orb(ii, 2, 2, ref isoft, ref ihard);
            }
            sh = isoft - ihard;
            return (sh);
        }

        static void check_orb(int ii, int i, int j, ref int isoft, ref int ihard)
        {
            double r, a;
            for (int jj = 1; jj <= 10; jj++)
            {
                r = mnatal[ii] - mtransit[jj];
                r = (r < 0.0 ? r + 360.0 : r);
                a = ABS(r - aspects[i, j]);
                if (a <= trn_orbs[i, j])
                {
                    if (i == 1)
                        isoft = isoft + 1;
                    else
                        ihard = ihard + 1;
                }
            }
        }

        static void lunarcycle(ref double sh1, ref double sh2, ref double sh3, ref double sh4)
        {
            double r; int isign;
            r = mtransit[10];
            isign = which_sign(r);
            switch (isign)
            {
                case 2:
                case 6:
                case 10:
                    sh1 = sh1 + 2;
                    sh2 = sh2 + 1;
                    sh3 = sh3 - 1.5;
                    sh4 = sh4 - 1.5; break;
                case 4:
                case 8:
                case 12:
                    sh1 = sh1 + 1;
                    sh2 = sh2 + 2;
                    sh3 = sh3 - 1.5;
                    sh4 = sh4 - 1.5; break;
                case 3:
                case 7:
                case 11:
                    sh1 = sh1 - 1.5;
                    sh2 = sh2 - 1.5;
                    sh3 = sh3 + 2;
                    sh4 = sh4 + 1; break;
                case 1:
                case 5:
                case 9:
                    sh1 = sh1 - 1.5;
                    sh2 = sh2 - 1.5;
                    sh3 = sh3 + 1;
                    sh4 = sh4 + 2; break;
            }
        }

        static void lunarsign(ref double sh1, ref double sh2, ref double sh3, ref double sh4)
        {
            double r, sunsign;
            r = mtransit[1];
            sunsign = which_sign(r);
            r = mtransit[10];
            r = (r > 360.0 ? r - 360.0 : r);
            if (r >= (sunsign - 1) * 30 && r <= sunsign * 30)
            {
                sh1 = sh1 + 2;
                sh2 = sh2 + 2;
                sh3 = sh3 + 2;
                sh4 = sh4 + 2;
            }

            else if (r >= (sunsign - 1 + 6) * 30 && r <= (sunsign + 6) * 30)
            {
                sh1 = sh1 + 1;
                sh2 = sh2 + 1;
                sh3 = sh3 + 1;
                sh4 = sh4 + 1;
            }
            else if (r >= (sunsign - 1 + 3) * 30 && r <= (sunsign + 3) * 30)
            {
                sh1 = sh1 - 1;
                sh2 = sh2 - 1;
                sh3 = sh3 - 1;
                sh4 = sh4 - 1;
            }
            else if (r >= (sunsign - 1 + 9) * 30 && r <= (sunsign + 9) * 30)
            {
                sh1 = sh1 - 1;
                sh2 = sh2 - 1;
                sh3 = sh3 - 1;
                sh4 = sh4 - 1;
            }
        }



        static List<HOUSE> dbhouse = new List<HOUSE>();
        static List<STARS> dbstars = new List<STARS>() { };
        static List<SUBJECTS> dbsubjects = new List<SUBJECTS>();
        static void printvadi()
        {
            Console.WriteLine("STARS--");
            for (int i = 1; i < dbstars.Count; i++)
            {
                Console.WriteLine(dbstars[i]);
            }
            Console.WriteLine("\r\nHOUSE---");
            for (int i = 1; i < dbhouse.Count; i++)
            {
                Console.WriteLine(dbhouse[i]);
            }
            //Console.WriteLine("\r\nSUBBOO---");
            //for (int i = 1; i < dbsubjects.Count; i++)
            //{
            //    Console.WriteLine(dbsubjects[i]);
            //}


        }
        static void into_dbf1()
        {

            dbstars.Clear();
            dbstars.Add(new STARS() { index1 = "SHOULD NOT USE" });

            for (int i = 1; i <= 14; i++)
            {

                string p = planets[i];
                double rr = mnatal[i];
                int isign = which_sign(rr);
                string pos = degminsec(rr - (isign - 1) * 30.0);
                //APPEND BLANK
                string index1 = SUBSTR(p, 1, 2) + '-' + SUBSTR(signs[isign], 1, 3);
                string group1 = p + " in " + signs[isign];
                string planet = p;
                string symbol = signs[isign];
                string location = pos;
                double measure = rr;
                //Console.WriteLine($"STARS:{index1}:{group1}:{planet}:{symbol}:{location}:{measure}");

                dbstars.Add(new STARS() { index1 = index1, group1 = group1, planet = planet, symbol = symbol, location = location, measure = measure });

            }

        }
        static void into_dbf2()
        {
            dbhouse = new List<HOUSE>();
            dbhouse.Add(new HOUSE() { index4 = "DONOTUSE" });
            //SELECT house
            for (int i = 1; i <= 12; i++)
            {
                double r = mhouse[i];
                int isign = which_sign(r);
                string pos = degminsec(r - (isign - 1) * 30.0);
                //APPEND BLANK
                string index4 = i.ToString() + '-' + SUBSTR(signs[isign], 1, 3);//ALLTRIM(STR(RECNO()))
                string group = "HOUSE " + i.ToString() + " in " + signs[isign];//ALLTRIM(STR(RECNO()))
                house = i.ToString();// ALLTRIM(STR(RECNO()));
                string symbol = signs[isign];
                string location = pos;
                double measure = r;

                dbhouse.Add(new HOUSE() { index4 = index4, group = group, house = house, symbol = symbol, location = location, measure = measure });
                Console.WriteLine($"HOUSE:{index4}:{group}:{house}:{symbol}:{location}:{measure}");

            }
        }

        static void into_dbf3()
        {

            //SELECT stars
            for (int j = 1; j <= 14; j++)
            {
                string p = planets[j];
                double rr = mnatal[j];
                int ihse = pl_in_hse(rr);

                string index2 = ihse.ToString() + '-' + SUBSTR(p, 1, 2);
                string group2 = p + " in HOUSE " + ihse.ToString();
                string house = ihse.ToString();

                dbstars[j].index2 = index2;
                dbstars[j].group2 = group2;
                dbstars[j].house = house;


                //Console.WriteLine($"DBF3:{index2}:{group2}:{house}");

            }
        }



        static void into_dbf4()
        {
            //SELECT subjects
            double plt1 = mnatal[1];
            double plt2 = mnatal[2];
            double plt3 = mnatal[3];
            double plt4 = mnatal[4];
            double plt5 = mnatal[5];
            double plt6 = mnatal[6];
            double plt7 = mnatal[7];
            double plt8 = mnatal[8];
            double plt9 = mnatal[9];
            double plt10 = mnatal[10];
            double asc = mnatal[11];
            double mh = mnatal[12];

            Console.WriteLine($"DBF4-Subject:{plt1}/{plt2}/{plt3}/{plt4}/{plt5}/{plt6}/{plt7}/{plt8}/{plt9}/{plt10}/{asc}/{mh}");
        }
        static void into_dbf5()
        {
            // SELECT subjects
            double hse1 = mhouse[1];
            double hse2 = mhouse[2];
            double hse3 = mhouse[3];
            double hse4 = mhouse[4];
            double hse5 = mhouse[5];
            double hse6 = mhouse[6];
            double hse7 = mhouse[7];
            double hse8 = mhouse[8];
            double hse9 = mhouse[9];
            double hse10 = mhouse[10];
            double hse11 = mhouse[11];
            double hse12 = mhouse[12];


            Console.WriteLine($"DBF5-Subject:{hse1}/{hse2}/{hse3}/{hse4}/{hse5}/{hse6}/{hse7}/{hse8}/{hse9}/{hse10}/{hse11}/{hse12}");

        }




        static int pl_in_hse(dynamic rr)
        {
            bool ifound = false;
            int ihse = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (i < 12)
                {
                    if (mhouse[i] < mhouse[i + 1])
                    {
                        if (rr > mhouse[i] && rr < mhouse[i + 1])
                        {
                            ihse = i;
                            ifound = true;
                            break;
                        }
                    }
                }
                else
                {
                    if (mhouse[12] < mhouse[1])
                    {
                        if (rr > mhouse[12] && rr < mhouse[1])
                        {
                            ihse = 12;
                            ifound = true;
                            break;
                        }
                    }
                }
            }

            if (!ifound)
            {
                for (int i = 1; i <= 12; i++)
                {
                    if (i < 12)
                    {
                        if (mhouse[i] > mhouse[i + 1])
                        {
                            ihse = i;
                            break;
                        }
                    }
                    else
                    {
                        if (mhouse[12] > mhouse[1])
                        {
                            ihse = 12;
                            break;
                        }
                    }
                }
            }
            return ihse;
        }

        public static void Init()
        {

            trans1 = new List<TRN_ASP>();
            trans2 = new List<TRN_HSE>();
            dbhouse = new List<HOUSE>();
            dbstars = new List<STARS>();



            for (int i = 1; i <= 18; i++)
            {
                m[i] = 0;
            }
            for (int i = 1; i <= 7; i++)
            {
                for (int j = 1; j <= 17; j++)
                {
                    k[i, j] = 0;
                }
            }

            for (int i = 1; i <= 12; i++)
            {
                hse[i] = 0;
            }
            for (int i = 1; i <= 8; i++)
                bd[i] = "";

            for (int i = 1; i <= 14; i++)
            {
                mnatal[i] = 0;
                for (int j = 1; j <= 2; j++)
                {
                    mnat_pos[i, j] = 0;
                }
            }
            for (int i = 1; i <= 12; i++)
                mhouse[i] = 0;

            for (int i = 1; i <= 10; i++)
            {
                mtransit[i] = 0;
                for (int j = 1; j <= 2; j++)
                    mtrn_pos[i, j] = 0;
            }

            for (int i = 1; i <= 72; i++)
            {
                morb_asp[i] = 0;
                for (int j = 1; j <= 2; j++)
                    morb_pos[i, j] = 0;
            }

            for (int i = 1; i <= 72; i++)
                for (int j = 1; j <= 4; j++)
                    masplines[i, j] = 0;



            aspects[1, 1] = 0.0;
            aspects[1, 2] = 30.0;
            aspects[1, 3] = 60.0;
            aspects[1, 4] = 120.0;
            aspects[1, 5] = 150.0;
            aspects[1, 6] = 210.0;
            aspects[1, 7] = 240.0;
            aspects[1, 8] = 300.0;
            aspects[1, 9] = 330.0;
            aspects[1, 10] = 360.0;
            aspects[2, 1] = 45.0;
            aspects[2, 2] = 90.0;
            aspects[2, 3] = 135.0;
            aspects[2, 4] = 180.0;
            aspects[2, 5] = 225.0;
            aspects[2, 6] = 270.0;
            aspects[2, 7] = 315.0;


            orbs[1, 1] = 5;
            orbs[1, 2] = 1;
            orbs[1, 3] = 1;
            orbs[1, 4] = 5;
            orbs[1, 5] = 1;
            orbs[1, 6] = 1;
            orbs[1, 7] = 5;
            orbs[1, 8] = 1;
            orbs[1, 9] = 1;
            orbs[1, 10] = 5;
            orbs[2, 1] = 1;
            orbs[2, 2] = 6;
            orbs[2, 3] = 1;
            orbs[2, 4] = 6;
            orbs[2, 5] = 1;
            orbs[2, 6] = 1;
            orbs[2, 7] = 6;

            trn_orbs[1, 1] = 2;
            trn_orbs[1, 2] = 0;
            trn_orbs[1, 3] = 0;
            trn_orbs[1, 4] = 2;
            trn_orbs[1, 5] = 0;
            trn_orbs[1, 6] = 0;
            trn_orbs[1, 7] = 2;
            trn_orbs[1, 8] = 0;
            trn_orbs[1, 9] = 0;
            trn_orbs[1, 10] = 2;
            trn_orbs[2, 1] = 0;
            trn_orbs[2, 2] = 3;
            trn_orbs[2, 3] = 0;
            trn_orbs[2, 4] = 3;
            trn_orbs[2, 5] = 0;
            trn_orbs[2, 6] = 0;
            trn_orbs[2, 7] = 3;




        }


    }
}
