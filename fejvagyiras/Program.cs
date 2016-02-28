using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fejvagyiras
{
    class Program
    {
        static int _n;

        static void Main()
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Felatat4();
            Feladat5();
            Feladat6();
            Feladat7();
            Console.ReadLine();
        }

        static bool Random()
        {
            Random r = new Random();

            var state = (r.Next(0, 2) == 0);
            return state;
        }

        private static void Feladat1()
        {
            Console.WriteLine("1.Feladat");
            Console.WriteLine(Random() ? "Fej" : "Iras");
            Console.WriteLine();
        }

        private static void Feladat2()
        {
            Console.Write("Adon meg egy tippet! (F/I) !: ");
            var tipp = Console.ReadLine();
            var eredmeny = Random() ? "F" : "I";

            Console.WriteLine("2.Feladat");
            Console.WriteLine("A tipp " + tipp + ", a dobas eredmenye: " + eredmeny);
            Console.WriteLine((tipp == eredmeny) ? "On eltalalta!" : "On nem talalta el!");
            Console.WriteLine();
        }

        private static void Feladat3()
        {
            var r = new StreamReader("kiserlet.txt");

            while (!r.EndOfStream)
            {
                r.ReadLine();
                _n++;
            }

            r.Close();
            Console.WriteLine("3.Feladat");
            Console.WriteLine(_n + " dobasbol all.");
            Console.WriteLine();
        }

        private static void Felatat4()
        {
            int fejek = 0;
            var r = new StreamReader("kiserlet.txt");
            while (!r.EndOfStream)
            {
                var x = r.ReadLine();
                if (x == "F") fejek++;
            }

            r.Close();

            var fejgyakorisag = ((fejek/(float) _n)*100);

            Console.WriteLine("4.Feladat");
            Console.WriteLine("A kiserlet soran a fej relativ gyakorisaga " + fejgyakorisag.ToString("F2") + "% volt.");
            Console.WriteLine();
        }


        private static void Feladat5()
        {
            Console.WriteLine("5. feladat");
            double dbFf = 0;
            StreamReader sr = new StreamReader("kiserlet.txt");

            string x = String.Concat(sr.ReadLine(), sr.ReadLine(), sr.ReadLine());
            if (x == "FFI") dbFf++;
            x = String.Concat(" ", x);

            while (sr.Peek() > -1)
            {
                x = String.Concat(x.Substring(1, 3), sr.ReadLine());
                if (x == "IFFI") dbFf++;
            }
            sr.Close();

            if (x.Substring(1, 3) == "IFF") dbFf++;
            Console.WriteLine("A kiserlet soran {0} alkalommal dobtak pontosan ket fejet egymas után.", dbFf);
        }

        private static void Feladat6()
        {
            List<Fsor> listaFsor = new List<Fsor>();
            Console.WriteLine();
            Console.WriteLine("6. feladat");
            StreamReader sr = new StreamReader("kiserlet.txt");

            int i = 0;
            int hossz = 0;

            while (!sr.EndOfStream)
            {
                var x = sr.ReadLine();
                i++;
                if (x == "F")
                {
                    while (x == "F")
                    {
                        hossz++;
                        x = sr.ReadLine();
                    }

                    listaFsor.Add(new Fsor(i, hossz));
                    i += hossz;
                    hossz = 0;
                }
            }

            var maxhossz = listaFsor.Max(fsor => fsor.Hossz);
            int index = 0;

            foreach (Fsor fsor in listaFsor)
            {
                if (fsor.Hossz == maxhossz)
                    index = fsor.Index;
            }

            Console.WriteLine("A leghosszabb F-bol allo sorozat hossza: " + listaFsor.Max(fsor => fsor.Hossz));
            Console.WriteLine("Helye pedig: " + index);
        }

        static void Feladat7()
        {
            Console.WriteLine();
            Console.WriteLine("7. feladat");
            List<string> dobasList = new List<string>();
            Random r = new Random();
            StreamWriter writer = new StreamWriter("dobasok.txt");


            int fejes = 0;
            int irasos = 0;


            for (int i = 0; i < 1000; i++)
            {
                string temp = "";

                for (int k = 0; k < 4; k++)
                {
                    int x = r.Next(0, 2);
                    if (x == 0)
                        temp += "F";
                    else
                        temp += "I";
                }

                dobasList.Add(temp);

                if (dobasList[i][0] == 'F' && dobasList[i][1] == 'F' && dobasList[i][2] == 'F')
                    if (dobasList[i][3] == 'F')
                        fejes++;
                    else
                        irasos++;
            }

            Console.WriteLine("Kiiratas fajlba....");
            writer.WriteLine(fejes + " esetben fej, " + irasos + " esetben iras koveti az FFF sorozatot.");

            for (int i = 0; i < 1000; i++)
            {
                writer.Write(dobasList[i] + " ");
            }

            writer.Close();
        }
    }

    class Fsor
    {
        public int Index;
        public int Hossz;

        public Fsor(int i, int h)
        {
            Index = i;
            Hossz = h;
        }
    }
}