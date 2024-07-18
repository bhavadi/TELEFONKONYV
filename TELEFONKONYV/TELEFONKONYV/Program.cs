using System;
using System.IO;

namespace TELEFONKONYV
{
    public class Szemely
    {
        public string Nev { get; set; }
        public string Cim { get; set; }
        public string AtyaNeve { get; set; }
        public string AnyaNeve { get; set; }
        public long MobilSzam { get; set; }
        public string Nem { get; set; }
        public string Email { get; set; }
        public string AllampolgarsagiSzam { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Kezdes();
        }

        static void Kezdes()
        {
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("\t\t**********ÜDVÖZÖLJÜK A TELEFONKÖNYVBEN*************");
            Console.WriteLine("\n\n\t\t\t MENÜ\t\t\n\n");
            Console.WriteLine("\t1. Új hozzáadása \t2. Lista \t3. Kilépés");
            Console.WriteLine("\t4. Módosítás \t5. Keresés\t6. Törlés");

            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    RekordHozzaadas();
                    break;
                case '2':
                    RekordLista();
                    break;
                case '3':
                    Environment.Exit(0);
                    break;
                case '4':
                    RekordModositas();
                    break;
                case '5':
                    RekordKereses();
                    break;
                case '6':
                    RekordTorles();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nCsak 1-től 6-ig adjon meg értéket");
                    Console.WriteLine("\nNyomjon meg bármelyik billentyűt");
                    Console.ReadKey();
                    Menu();
                    break;
            }
        }

        static void RekordHozzaadas()
        {
            Console.Clear();
            Szemely p = new Szemely();

            Console.WriteLine("\nAdja meg a nevet: ");
            p.Nev = Console.ReadLine();
            Console.WriteLine("\nAdja meg a címet: ");
            p.Cim = Console.ReadLine();
            Console.WriteLine("\nAdja meg az apa nevét: ");
            p.AtyaNeve = Console.ReadLine();
            Console.WriteLine("\nAdja meg az anya nevét: ");
            p.AnyaNeve = Console.ReadLine();
            Console.WriteLine("\nAdja meg a telefonszámot: ");
            p.MobilSzam = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("\nAdja meg a nemet: ");
            p.Nem = Console.ReadLine();
            Console.WriteLine("\nAdja meg az e-mail címet: ");
            p.Email = Console.ReadLine();
            Console.WriteLine("\nAdja meg az állampolgársági számot: ");
            p.AllampolgarsagiSzam = Console.ReadLine();

            using (FileStream fs = new FileStream("project.dat", FileMode.Append, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(p.Nev);
                writer.Write(p.Cim);
                writer.Write(p.AtyaNeve);
                writer.Write(p.AnyaNeve);
                writer.Write(p.MobilSzam);
                writer.Write(p.Nem);
                writer.Write(p.Email);
                writer.Write(p.AllampolgarsagiSzam);
            }

            Console.WriteLine("\nRekord mentve");
            Console.WriteLine("\n\nNyomjon meg bármelyik billentyűt");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        static void RekordLista()
        {
            Console.Clear();
            try
            {
                using (FileStream fs = new FileStream("project.dat", FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    while (fs.Position < fs.Length)
                    {
                        Szemely p = new Szemely
                        {
                            Nev = reader.ReadString(),
                            Cim = reader.ReadString(),
                            AtyaNeve = reader.ReadString(),
                            AnyaNeve = reader.ReadString(),
                            MobilSzam = reader.ReadInt64(),
                            Nem = reader.ReadString(),
                            Email = reader.ReadString(),
                            AllampolgarsagiSzam = reader.ReadString()
                        };

                        Console.WriteLine($"\n\n\nAZ ÖN REKORDJA\n\n");
                        Console.WriteLine($"\nNév: {p.Nev}\nCím: {p.Cim}\nApa neve: {p.AtyaNeve}\nAnya neve: {p.AnyaNeve}\nMobil szám: {p.MobilSzam}\nNem: {p.Nem}\nE-mail: {p.Email}\nÁllampolgársági szám: {p.AllampolgarsagiSzam}");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\nHiba a fájl megnyitásakor a listázásban.");
                Environment.Exit(1);
            }

            Console.WriteLine("\nNyomjon meg bármelyik billentyűt");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        static void RekordKereses()
        {
            Console.Clear();
            Console.WriteLine("\nAdja meg a keresett személy nevét\n");
            string nev = Console.ReadLine();

            try
            {
                using (FileStream fs = new FileStream("project.dat", FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    while (fs.Position < fs.Length)
                    {
                        Szemely p = new Szemely
                        {
                            Nev = reader.ReadString(),
                            Cim = reader.ReadString(),
                            AtyaNeve = reader.ReadString(),
                            AnyaNeve = reader.ReadString(),
                            MobilSzam = reader.ReadInt64(),
                            Nem = reader.ReadString(),
                            Email = reader.ReadString(),
                            AllampolgarsagiSzam = reader.ReadString()
                        };

                        if (p.Nev.Equals(nev, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"\n\tRészletes információk {nev}-ról/ről");
                            Console.WriteLine($"\nNév: {p.Nev}\nCím: {p.Cim}\nApa neve: {p.AtyaNeve}\nAnya neve: {p.AnyaNeve}\nMobil szám: {p.MobilSzam}\nNem: {p.Nem}\nE-mail: {p.Email}\nÁllampolgársági szám: {p.AllampolgarsagiSzam}");
                        }
                        else
                        {
                            Console.WriteLine("Fájl nem található");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\nHiba a fájl megnyitásakor\a\a\a\a");
                Environment.Exit(1);
            }

            Console.WriteLine("\nNyomjon meg bármelyik billentyűt");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        static void RekordTorles()
        {
            Console.Clear();
            Console.WriteLine("Adja meg a kapcsolat nevét:");
            string nev = Console.ReadLine();

            bool flag = false;
            try
            {
                using (FileStream fs = new FileStream("project.dat", FileMode.Open, FileAccess.Read))
                using (FileStream ft = new FileStream("temp.dat", FileMode.Create, FileAccess.Write))
                using (BinaryReader reader = new BinaryReader(fs))
                using (BinaryWriter writer = new BinaryWriter(ft))
                {
                    while (fs.Position < fs.Length)
                    {
                        Szemely p = new Szemely
                        {
                            Nev = reader.ReadString(),
                            Cim = reader.ReadString(),
                            AtyaNeve = reader.ReadString(),
                            AnyaNeve = reader.ReadString(),
                            MobilSzam = reader.ReadInt64(),
                            Nem = reader.ReadString(),
                            Email = reader.ReadString(),
                            AllampolgarsagiSzam = reader.ReadString()
                        };

                        if (!p.Nev.Equals(nev, StringComparison.OrdinalIgnoreCase))
                        {
                            writer.Write(p.Nev);
                            writer.Write(p.Cim);
                            writer.Write(p.AtyaNeve);
                            writer.Write(p.AnyaNeve);
                            writer.Write(p.MobilSzam);
                            writer.Write(p.Nem);
                            writer.Write(p.Email);
                            writer.Write(p.AllampolgarsagiSzam);
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }

                if (!flag)
                {
                    Console.WriteLine("NINCS TÖRLENDŐ KAPCSOLATI REKORD.");
                    File.Delete("temp.dat");
                }
                else
                {
                    File.Delete("project.dat");
                    File.Move("temp.dat", "project.dat");
                    Console.WriteLine("REKORD SIKERESEN TÖRÖLVE.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("A KAPCSOLATI ADATOK MÉG NINCSENEK HOZZÁADVA.");
            }

            Console.WriteLine("\nNyomjon meg bármelyik billentyűt");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        static void RekordModositas()
        {
            Console.Clear();
            Console.WriteLine("\nAdja meg a módosítani kívánt kapcsolat nevét:\n");
            string nev = Console.ReadLine();

            bool flag = false;
            try
            {
                using (FileStream fs = new FileStream("project.dat", FileMode.Open, FileAccess.ReadWrite))
                using (BinaryReader reader = new BinaryReader(fs))
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    while (fs.Position < fs.Length)
                    {
                        long position = fs.Position;

                        Szemely p = new Szemely
                        {
                            Nev = reader.ReadString(),
                            Cim = reader.ReadString(),
                            AtyaNeve = reader.ReadString(),
                            AnyaNeve = reader.ReadString(),
                            MobilSzam = reader.ReadInt64(),
                            Nem = reader.ReadString(),
                            Email = reader.ReadString(),
                            AllampolgarsagiSzam = reader.ReadString()
                        };

                        if (p.Nev.Equals(nev, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("\nAdja meg a nevet:");
                            p.Nev = Console.ReadLine();
                            Console.WriteLine("\nAdja meg a címet:");
                            p.Cim = Console.ReadLine();
                            Console.WriteLine("\nAdja meg az apa nevét:");
                            p.AtyaNeve = Console.ReadLine();
                            Console.WriteLine("\nAdja meg az anya nevét:");
                            p.AnyaNeve = Console.ReadLine();
                            Console.WriteLine("\nAdja meg a telefonszámot:");
                            p.MobilSzam = Convert.ToInt64(Console.ReadLine());
                            Console.WriteLine("\nAdja meg a nemet:");
                            p.Nem = Console.ReadLine();
                            Console.WriteLine("\nAdja meg az e-mail címet:");
                            p.Email = Console.ReadLine();
                            Console.WriteLine("\nAdja meg az állampolgársági számot:");
                            p.AllampolgarsagiSzam = Console.ReadLine();

                            fs.Seek(position, SeekOrigin.Begin);

                            writer.Write(p.Nev);
                            writer.Write(p.Cim);
                            writer.Write(p.AtyaNeve);
                            writer.Write(p.AnyaNeve);
                            writer.Write(p.MobilSzam);
                            writer.Write(p.Nem);
                            writer.Write(p.Email);
                            writer.Write(p.AllampolgarsagiSzam);

                            flag = true;
                            break;
                        }
                    }
                }

                if (flag)
                {
                    Console.WriteLine("\nAz adatai módosítva lettek");
                }
                else
                {
                    Console.WriteLine("\nAz adat nem található");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("A KAPCSOLATI ADATOK MÉG NINCSENEK HOZZÁADVA.");
            }

            Console.WriteLine("\nNyomjon meg bármelyik billentyűt");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }
    }
}
