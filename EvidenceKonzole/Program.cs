using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace EvidenceZamestnancu
{
    class Program
    {


        public static void Main(string[] args)
        {
            bool konec = false;
            SpravaZamestnancu spravaZamestnancu = new SpravaZamestnancu();
            while (!konec)
            {
                Console.Clear();
                Console.WriteLine("" +
                    "janpluhar.net - Evidence zamestnancu");



                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("|---------------FUNKCE--------------|");
                Console.WriteLine("");
                Console.WriteLine("1. Pridat zamestnance do databaze");
                Console.WriteLine("2. Zobrazit zamestnance z databaze");
                Console.WriteLine("");
                Console.WriteLine("|---------------UPRAVY--------------|");
                Console.WriteLine("");
                Console.WriteLine("3. Upravit Jmeno + Prijmeni zamestnance v databazi");
                Console.WriteLine("4. Upravit Pozici zamestnance v databazi");
                Console.WriteLine("5. Upravit Plat zamestnance v databazi");
                Console.WriteLine("");
                Console.WriteLine("|-------------NASTAVENI-------------|");
                Console.WriteLine("");
                Console.WriteLine("7. Smazat zamestnance z databaze");
                Console.WriteLine("9. Otestuj pripojeni s databazi");
                Console.WriteLine("0. Ukoncit aplikaci");
                Console.WriteLine("");
                Console.WriteLine("|-----------------------------------|");


                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Zadejte cislo akce: ");

                //POTREBA UDELAT OSETRENI VSTUPU !!!!
                if (!int.TryParse(Console.ReadLine(), out int volba))
                {
                    continue;
                }





                switch (volba)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Pridani zamestnance do databaze");
                        spravaZamestnancu.PridejZamestnance();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Zobrazeni zamestnancu z databaze");
                        spravaZamestnancu.VypisZamestnance();
                        Console.WriteLine("");
                        Console.WriteLine("Pokracujte stisknutim libovolne klavesy");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Uprav Jmeno + Prijmeni zamestnance v databazi");
                        spravaZamestnancu.UpravJmenoZamestnance();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Uprav Pozici zamestnance v databazi");
                        spravaZamestnancu.UpravPoziciZamestnance();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Uprav Plat zamestnance v databazi");
                        spravaZamestnancu.UpravPlatZamestnance();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Mazani zamestnance z databaze");
                        spravaZamestnancu.VymazZamestnance();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Ukonceni aplikace");
                        konec = true;
                        break;
                    case 9:
                        Console.Clear();
                        Console.WriteLine("Otestovani pripojeni k databazi");
                        spravaZamestnancu.OtestujPripojeni();
                        break;
                    default:

                        Console.WriteLine("Neplatna volba");
                        break;
                }


            }

        }
    }
}