using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace EvidenceZamestnancu
{
    public class SpravaZamestnancu
    {


        private string jmeno;
        private string prijmeni;
        private string pozice;


        //encrypt



        //LOCAL
        //  string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=""D:\Visual Studio Projects\Databaze\EvidenceZamestnancu.mdf"";Initial Catalog=EvidenceZamestnancu;Integrated Security=True";

        //WEB
        string connectionString = @"Server=db.dw177.webglobe.com;Port=3306;Database=aplikace_janpluhar;Uid=aplikace_admin;Pwd=Aplikace123;";


        //Ano ja vim, encrypt jeste uplne neovladam :P

        public void OtestujPripojeni()
        {
            //pripojeni do DB
            using (MySqlConnection pripojeni = new MySqlConnection(connectionString))
            {
                pripojeni.Open();
                if (pripojeni.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("USPESNE Pripojeno k DB");
                else
                    Console.WriteLine("Chyba pripojeni k DB");

                pripojeni.Close();
                if (pripojeni.State != System.Data.ConnectionState.Open)
                    Console.WriteLine("USPESNE Odpojeno od DB");
                else
                    Console.WriteLine("Neuspesne odpojeni od DB");

                Console.WriteLine("");
                Console.WriteLine("Pokracujte stisknutim libovolne klavesy");
                Console.ReadKey();
                Console.Clear();
            }
        }


        //funkce aplikace
        public void PridejZamestnance()
        {
            //pripojeni do DB
            using (MySqlConnection pripojeni = new MySqlConnection(connectionString))
            {
                //funkce

                do
                {
                    Console.Clear();
                    Console.Write("Zadej jmeno zamestnance: ");
                    jmeno = Console.ReadLine();
                } while (jmeno == "");

                do
                {
                    Console.Clear();
                    Console.Write("Zadej prijmeni zamestnance: ");
                    prijmeni = Console.ReadLine();
                } while (prijmeni == "");

                do
                {
                    Console.Clear();
                    Console.Write("Zadej pozici zamestnance: ");
                    pozice = Console.ReadLine();
                } while (pozice == "");
                Console.Clear();
                int plat;
                do
                {

                    Console.WriteLine("Zadejte plat ve formátu celého čísla:");
                    if (!int.TryParse(Console.ReadLine(), out plat))
                    {
                        Console.Clear();
                        Console.WriteLine("Zadaný plat není celé číslo. Zkuste to znovu.");
                    }
                } while (plat == 0);


                //pridani do databaze
                string dotaz = "INSERT INTO Zamestnanci (Name, Surname, Position, Salary) VALUES (@jmeno, @prijmeni, @pozice, @plat)";
                using (MySqlCommand sqlDotaz = new MySqlCommand(dotaz, pripojeni))
                {
                    try
                    {

                        pripojeni.Open();
                        sqlDotaz.Parameters.AddWithValue("@jmeno", jmeno);
                        sqlDotaz.Parameters.AddWithValue("@prijmeni", prijmeni);
                        sqlDotaz.Parameters.AddWithValue("@pozice", pozice);
                        sqlDotaz.Parameters.AddWithValue("@plat", plat);

                        int radku = sqlDotaz.ExecuteNonQuery();
                        Console.Clear();
                        Console.WriteLine($"Zamestnanec pridan do DB, Bylo pridano {radku} radku.");
                        pripojeni.Close();
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("Chyba pri pridavani zamestnance do DB: " + ex.Message);
                        Console.WriteLine("");
                        pripojeni.Close();
                        Console.WriteLine("Pokracujte stisknutim libovolne klavesy");
                        Console.ReadKey();
                    }

                }


            }
        }

        public void VypisZamestnance()
        {
            using (MySqlConnection pripojeni = new MySqlConnection(connectionString))
            {
                string dotaz = "SELECT Id, Name, Surname, Position, Salary FROM Zamestnanci";
                MySqlCommand sqlDotaz = new MySqlCommand(dotaz, pripojeni);
                pripojeni.Open();
                MySqlDataReader data = sqlDotaz.ExecuteReader();

                Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                Console.WriteLine($"| ID   | Jmeno            | Prijmeni       | Pozice                | Plat         |");
                Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");

                while (data.Read())
                {
                    Console.WriteLine($"| {data["Id"],-4} | {data["Name"],-16} | {data["Surname"],-14} | {data["Position"],-21} | {data["Salary"],-12} |");
                }

                Console.WriteLine($"+------+------------------+----------------+---www.janpluhar.net---+--------------+");
                pripojeni.Close();

            }


        }

        public void UpravJmenoZamestnance()
        {
            using (MySqlConnection pripojeni = new MySqlConnection(connectionString))
            {
                //Vypis vsech zamestnancu
                VypisZamestnance();


                int idZamestnance;
                do
                {

                    Console.WriteLine("Zadej ID zamestnance:");
                    if (!int.TryParse(Console.ReadLine(), out idZamestnance))
                    {
                        Console.Clear();
                        VypisZamestnance();
                        Console.WriteLine("Zadane ID není platne. Zkuste to znovu.");
                    }
                } while (idZamestnance == 0);


                //Zobrazeni vybraneho zamestnance pomoci ID
                string dotaz1 = "SELECT Id, Name, Surname, Position, Salary FROM Zamestnanci WHERE Id = @idZamestnance";
                MySqlCommand sqlDotaz1 = new MySqlCommand(dotaz1, pripojeni);
                sqlDotaz1.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                pripojeni.Open();
                MySqlDataReader data1 = sqlDotaz1.ExecuteReader();
                Console.Clear();
                Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                Console.WriteLine($"| ID   | Jmeno            | Prijmeni       | Pozice                | Plat         |");
                Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                while (data1.Read())
                {
                    Console.WriteLine($"| {data1["Id"],-4} | {data1["Name"],-16} | {data1["Surname"],-14} | {data1["Position"],-21} | {data1["Salary"],-12} |");
                }
                Console.WriteLine($"+------+------------------+----------------+---www.janpluhar.net---+--------------+");
                pripojeni.Close();
                Console.WriteLine("");
                Console.WriteLine("EDITACE");
                do
                {

                    Console.Write("Zadej nove jmeno zamestnance: ");
                    jmeno = Console.ReadLine();
                } while (jmeno == "");

                do
                {

                    Console.Write("Zadej nove prijmeni zamestnance: ");
                    prijmeni = Console.ReadLine();
                } while (prijmeni == "");


                //UPRAVA JMENA
                string dotaz = "UPDATE Zamestnanci SET Name = @jmeno, Surname = @prijmeni WHERE Id = @idZamestnance";
                using (MySqlCommand sqlDotaz = new MySqlCommand(dotaz, pripojeni))
                {

                    try
                    {
                        pripojeni.Open();
                        sqlDotaz.Parameters.AddWithValue("@jmeno", jmeno);
                        sqlDotaz.Parameters.AddWithValue("@prijmeni", prijmeni);
                        sqlDotaz.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                        int radku = sqlDotaz.ExecuteNonQuery();

                        //Zobrazeni zmeny
                        string dotaz2 = "SELECT Id, Name, Surname, Position, Salary FROM Zamestnanci WHERE Id = @idZamestnance";
                        MySqlCommand sqlDotaz2 = new MySqlCommand(dotaz2, pripojeni);
                        sqlDotaz2.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                        MySqlDataReader data = sqlDotaz2.ExecuteReader();
                        Console.Clear();
                        Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                        Console.WriteLine($"| ID   | Jmeno  <<ZMENENO | Prijmeni <<ZME | Pozice                | Plat         |");
                        Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");

                        while (data.Read())
                        {
                            Console.WriteLine($"| {data["Id"],-4} | {data["Name"],-16} | {data["Surname"],-14} | {data["Position"],-21} | {data["Salary"],-12} |");
                        }

                        Console.WriteLine($"+------+------------------+----------------+---www.janpluhar.net---+--------------+");
                        Console.WriteLine("Zamestnanec byl uspesne editovan.");
                        Console.WriteLine($"Bylo zmeneno {radku} radku");
                        Console.WriteLine("");
                        Console.WriteLine("Pokracujte stisknutim libovolne klavesy");


                        pripojeni.Close();
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("Chyba pri upravovani jmena zamestnance v DB: " + ex.Message);
                        Console.WriteLine("");
                        pripojeni.Close();
                        Console.WriteLine("Pokracujte stisknutim libovolne klavesy");
                        Console.ReadKey();
                    }
                }

            }
        }



        public void UpravPoziciZamestnance()
        {
            using (MySqlConnection pripojeni = new MySqlConnection(connectionString))
            {
                //Vypis vsech zamestnancu
                VypisZamestnance();


                int idZamestnance;
                do
                {

                    Console.WriteLine("Zadej ID zamestnance:");
                    if (!int.TryParse(Console.ReadLine(), out idZamestnance))
                    {

                        Console.WriteLine("Zadane ID není platne. Zkuste to znovu.");
                    }
                } while (idZamestnance == 0);


                //Zobrazeni vybraneho zamestnance pomoci ID
                string dotaz1 = "SELECT Id, Name, Surname, Position, Salary FROM Zamestnanci WHERE Id = @idZamestnance";
                MySqlCommand sqlDotaz1 = new MySqlCommand(dotaz1, pripojeni);
                sqlDotaz1.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                pripojeni.Open();
                MySqlDataReader data1 = sqlDotaz1.ExecuteReader();
                Console.Clear();
                Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                Console.WriteLine($"| ID   | Jmeno            | Prijmeni       | Pozice                | Plat         |");
                Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                while (data1.Read())
                {
                    Console.WriteLine($"| {data1["Id"],-4} | {data1["Name"],-16} | {data1["Surname"],-14} | {data1["Position"],-21} | {data1["Salary"],-12} |");
                }
                Console.WriteLine($"+------+------------------+----------------+---www.janpluhar.net---+--------------+");
                pripojeni.Close();
                Console.WriteLine("");
                Console.WriteLine("EDITACE");
                do
                {

                    Console.Write("Zadej novou pozici zamestnance: ");
                    pozice = Console.ReadLine();
                } while (pozice == "");



                //UPRAVA POZICE
                string dotaz = "UPDATE Zamestnanci SET Position = @pozice WHERE Id = @idZamestnance";
                using (MySqlCommand sqlDotaz = new MySqlCommand(dotaz, pripojeni))
                {

                    try
                    {
                        pripojeni.Open();
                        sqlDotaz.Parameters.AddWithValue("@pozice", pozice);
                        sqlDotaz.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                        int radku = sqlDotaz.ExecuteNonQuery();

                        //Zobrazeni zmeny
                        string dotaz2 = "SELECT Id, Name, Surname, Position, Salary FROM Zamestnanci WHERE Id = @idZamestnance";
                        MySqlCommand sqlDotaz2 = new MySqlCommand(dotaz2, pripojeni);
                        sqlDotaz2.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                        MySqlDataReader data = sqlDotaz2.ExecuteReader();
                        Console.Clear();
                        Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                        Console.WriteLine($"| ID   | Jmeno            | Prijmeni       | Pozice  <<<<ZMENENO   | Plat         |");
                        Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");

                        while (data.Read())
                        {
                            Console.WriteLine($"| {data["Id"],-4} | {data["Name"],-16} | {data["Surname"],-14} | {data["Position"],-21} | {data["Salary"],-12} |");
                        }

                        Console.WriteLine($"+------+------------------+----------------+---www.janpluhar.net---+--------------+");
                        Console.WriteLine("Zamestnanec byl uspesne editovan.");
                        Console.WriteLine($"Bylo zmeneno {radku} radku");
                        Console.WriteLine("");
                        Console.WriteLine("Pokracujte stisknutim libovolne klavesy");


                        pripojeni.Close();
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("Chyba pri upravovani jmena zamestnance v DB: " + ex.Message);
                        Console.WriteLine("");
                        pripojeni.Close();
                        Console.WriteLine("Pokracujte stisknutim libovolne klavesy");
                        Console.ReadKey();
                    }
                }

            }
        }


        public void UpravPlatZamestnance()
        {
            using (MySqlConnection pripojeni = new MySqlConnection(connectionString))
            {
                //Vypis vsech zamestnancu
                VypisZamestnance();


                int idZamestnance;
                do
                {

                    Console.WriteLine("Zadej ID zamestnance:");
                    if (!int.TryParse(Console.ReadLine(), out idZamestnance))
                    {

                        Console.WriteLine("Zadane ID není platne. Zkuste to znovu.");
                    }
                } while (idZamestnance == 0);


                //Zobrazeni vybraneho zamestnance pomoci ID
                string dotaz1 = "SELECT Id, Name, Surname, Position, Salary FROM Zamestnanci WHERE Id = @idZamestnance";
                MySqlCommand sqlDotaz1 = new MySqlCommand(dotaz1, pripojeni);
                sqlDotaz1.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                pripojeni.Open();
                MySqlDataReader data1 = sqlDotaz1.ExecuteReader();
                Console.Clear();
                Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                Console.WriteLine($"| ID   | Jmeno            | Prijmeni       | Pozice                | Plat         |");
                Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                while (data1.Read())
                {
                    Console.WriteLine($"| {data1["Id"],-4} | {data1["Name"],-16} | {data1["Surname"],-14} | {data1["Position"],-21} | {data1["Salary"],-12} |");
                }
                Console.WriteLine($"+------+------------------+----------------+---www.janpluhar.net---+--------------+");
                pripojeni.Close();
                Console.WriteLine("");
                Console.WriteLine("EDITACE");
                Console.Write("Zadejte novy Platovy vymer ve formátu celého čísla: ");
                int plat;
                do
                {
                    if (!int.TryParse(Console.ReadLine(), out plat))
                    {
                        Console.WriteLine("Zadaný plat není celé číslo. Zkuste to znovu.");
                    }
                } while (plat == 0);



                //UPRAVA PLATU
                string dotaz = "UPDATE Zamestnanci SET Salary = @plat WHERE Id = @idZamestnance";
                using (MySqlCommand sqlDotaz = new MySqlCommand(dotaz, pripojeni))
                {

                    try
                    {
                        pripojeni.Open();
                        sqlDotaz.Parameters.AddWithValue("@plat", plat);
                        sqlDotaz.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                        int radku = sqlDotaz.ExecuteNonQuery();


                        //Zobrazeni zmeny
                        string dotaz2 = "SELECT Id, Name, Surname, Position, Salary FROM Zamestnanci WHERE Id = @idZamestnance";
                        MySqlCommand sqlDotaz2 = new MySqlCommand(dotaz2, pripojeni);
                        sqlDotaz2.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                        MySqlDataReader data = sqlDotaz2.ExecuteReader();
                        Console.Clear();
                        Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                        Console.WriteLine($"| ID   | Jmeno            | Prijmeni       | Pozice                | Plat <<ZMENE |");
                        Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");

                        while (data.Read())
                        {
                            Console.WriteLine($"| {data["Id"],-4} | {data["Name"],-16} | {data["Surname"],-14} | {data["Position"],-21} | {data["Salary"],-12} |");
                        }

                        Console.WriteLine($"+------+------------------+----------------+---www.janpluhar.net---+--------------+");
                        Console.WriteLine("Zamestnanec byl uspesne editovan.");
                        Console.WriteLine($"Bylo zmeneno {radku} radku");
                        Console.WriteLine("");
                        Console.WriteLine("Pokracujte stisknutim libovolne klavesy");


                        pripojeni.Close();
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("Chyba pri upravovani platu zamestnance v DB: " + ex.Message);
                        Console.WriteLine("");
                        pripojeni.Close();
                        Console.WriteLine("Pokracujte stisknutim libovolne klavesy");
                        Console.ReadKey();
                    }
                }

            }
        }



        public void VymazZamestnance()
        {
            using (MySqlConnection pripojeni = new MySqlConnection(connectionString))
            {
                //Vypis vsech zamestnancu
                VypisZamestnance();


                int idZamestnance;
                do
                {

                    Console.WriteLine("Zadej ID zamestnance:");
                    if (!int.TryParse(Console.ReadLine(), out idZamestnance))
                    {
                        Console.Clear();
                        Console.WriteLine("Zadane ID není platne. Zkuste to znovu.");
                    }
                } while (idZamestnance == 0);


                //Zobrazeni vybraneho zamestnance pomoci ID
                string dotaz1 = "SELECT Id, Name, Surname, Position, Salary FROM Zamestnanci WHERE Id = @idZamestnance";
                MySqlCommand sqlDotaz1 = new MySqlCommand(dotaz1, pripojeni);
                sqlDotaz1.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                pripojeni.Open();
                MySqlDataReader data1 = sqlDotaz1.ExecuteReader();
                Console.Clear();
                Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                Console.WriteLine($"| ID   | Jmeno            | Prijmeni       | Pozice                | Plat         |");
                Console.WriteLine($"+------+------------------+----------------+-----------------------+--------------+");
                while (data1.Read())
                {
                    Console.WriteLine($"| {data1["Id"],-4} | {data1["Name"],-16} | {data1["Surname"],-14} | {data1["Position"],-21} | {data1["Salary"],-12} |");
                }
                Console.WriteLine($"+------+------------------+----------------+---www.janpluhar.net---+--------------+");
                pripojeni.Close();
                Console.WriteLine("");
                Console.WriteLine("EDITACE");
                Console.Write("Opravdu si prejete zamestnance vymazat z databaze? ");
                Console.WriteLine("Tato operace je nevratna!");


                bool vymazat = false;

                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("Zadejte ANO pro potvrzeni nebo NE pro zruseni operace:");
                    string volba = Console.ReadLine().ToUpper();
                    if (volba == "ANO")
                    {
                        //VYMAZANI ZAMESTNANCE
                        string dotaz = "DELETE FROM Zamestnanci WHERE Id = @idZamestnance";
                        using (MySqlCommand sqlDotaz = new MySqlCommand(dotaz, pripojeni))
                        {
                            try
                            {
                                pripojeni.Open();
                                sqlDotaz.Parameters.AddWithValue("@idZamestnance", idZamestnance);
                                int radku = sqlDotaz.ExecuteNonQuery();
                                Console.Clear();
                                Console.WriteLine("Zamestnanec byl uspesne vymazan z DB.");
                                Console.WriteLine($"Bylo smazano {radku} radku");
                                Console.WriteLine("");
                                Console.WriteLine("Pokracujte stisknutim libovolne klavesy");
                                Console.ReadKey();
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine("Chyba pri mazani zamestnance z DB: " + ex.Message);
                                Console.WriteLine("");
                                pripojeni.Close();
                                Console.WriteLine("Pokracujte stisknutim libovolne klavesy");
                                Console.ReadKey();
                            }
                        }


                        vymazat = true;
                        return;
                    }
                    else if (volba == "NE")
                    {
                        vymazat = false;
                        Console.Clear();
                        Console.WriteLine("Operace byla zrusena uzivatelem.");
                        Console.WriteLine("");
                        Console.WriteLine("Pokracujte stisknutim libovolne klavesy");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Zadali jste neplatnou volbu. Zkuste to znovu.");
                    }
                } while (vymazat == false);









            }
        }



    }
}
