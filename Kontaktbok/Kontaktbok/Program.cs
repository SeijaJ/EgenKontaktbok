using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktbok
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository<Kontakter> KontaktRepo = new Repository<Kontakter>();
            Validering validering = new Validering();

            KontaktRepo.SparaKontakt(new Kontakter
            {
                Förnamn = "Anna",
                Efternamn = "Eriksson",
                Gatuadress = "Vägenbort 12",
                Postnummer = 12345,
                Postort = "Långtbortistan",
                HemNummer = "123456",
                JobbNummer = "070123456",
                KontaktTyp = Kontakt.Vänner
            });

            KontaktRepo.SparaKontakt(new Kontakter
            {
                Förnamn = "Lennart",
                Efternamn = "Hilding",
                Gatuadress = "Kastvägen 89",
                Postnummer = 98754,
                Postort = "Långtbortistan",
                HemNummer = "67890",
                JobbNummer = "07012386",
                KontaktTyp = Kontakt.Arbete
            });

            while (true)
            { 

                Console.WriteLine($"\t1. Skapa kontakt \t2. Lista kontakter \t3. Sök kontakt \n\t4. Avsluta");
                Console.Write("\n\tDitt val: ");
                ConsoleKeyInfo menyVal = Console.ReadKey();

                switch (menyVal.Key)
                {
                    case ConsoleKey.D1:
                        #region Skapa kontakt
                        Console.Write("\n\n\tFörnamn: ");
                        string kontaktFörnamn = Console.ReadLine();

                        Console.Write("\tEfternamn: ");
                        string kontaktEfternamn = Console.ReadLine();

                        Console.Write("\tGatuadress: ");
                        string kontaktGatuAdress = Console.ReadLine();

                        Console.Write("\tPostnummer: ");
                        int kontaktPostNummer;

                        while (!int.TryParse(Console.ReadLine(), out kontaktPostNummer))
                        {
                            Console.WriteLine("\tFelaktig inmatning. Försök igen!");
                            Console.Write("\tPostnummer: ");
                        }

                        Console.Write("\tPostort: ");
                        string kontaktPostort = Console.ReadLine();

                        Console.Write("\tHemtelefon:");
                        string kontaktHemTel = Console.ReadLine();

                        Console.Write("\tJobbtelefon:");
                        string kontaktJobbTel = Console.ReadLine();

                        Console.WriteLine("\n\tVilken typ av kontakt vill du skapa?");
                        Console.WriteLine("\t1. Vänner \t2. Familj \t3. Arbete \t4. Skola");
                        Console.Write("\n\tDitt val: ");

                        var kontaktTyp = Console.ReadKey();

                        Kontakt valdKontaktTyp = validering.SättTypAv(kontaktTyp);

                        KontaktRepo.SparaKontakt(new Kontakter 
                        { 
                            Förnamn = kontaktFörnamn, 
                            Efternamn = kontaktEfternamn,
                            Gatuadress = kontaktGatuAdress,
                            Postnummer = kontaktPostNummer,
                            Postort = kontaktPostort,
                            HemNummer = kontaktHemTel, 
                            JobbNummer = kontaktJobbTel, 
                            KontaktTyp = valdKontaktTyp
                        });

                        Console.WriteLine($"\n\n\tDin kontakt {kontaktFörnamn} {kontaktEfternamn} är nu sparad under {valdKontaktTyp}.");

                        ÅtergåTillMeny();

                        #endregion
                        break;
                    case ConsoleKey.D2:
                        #region Lista kontakter
                        foreach (var person in KontaktRepo)
                        {
                            Console.WriteLine($"\n\n\t{person.KontaktTyp}");
                            Console.WriteLine($"\tNamn: {person.Förnamn} {person.Efternamn}");
                            Console.WriteLine($"\tAdress: {person.Gatuadress}");
                            Console.WriteLine($"\tPostnummer: {person.Postnummer} Ort: {person.Postort}");
                        }
                        ÅtergåTillMeny();
                        #endregion
                        break;
                    case ConsoleKey.D3:
                        #region Sök kontakt

                        Console.WriteLine("\n\n\tVill du söka på: \n\t1. Förnamn 2. Efternamn 3. Hemnummer 4. Jobbnummer 5. Kontakttyp");
                        Console.Write("\n\tDitt val: ");
                        menyVal = Console.ReadKey();

                        string sökKontakt = String.Empty;
                        List<Kontakter> sökLista = new List<Kontakter>();

                        switch (menyVal.Key)
                        {
                            case ConsoleKey.D1:
                                Console.Write($"\n\n\tFörnamn: ");
                                sökKontakt = Console.ReadLine();
                                sökLista = KontaktRepo.HämtaKontakt(sökKontakt, "Förnamn");
                                break;
                            case ConsoleKey.D2:
                                Console.Write($"\n\n\tEfternamn: ");
                                sökKontakt = Console.ReadLine();
                                sökLista = KontaktRepo.HämtaKontakt(sökKontakt, "Efternamn");
                                break;
                            case ConsoleKey.D3:
                                Console.Write($"\n\n\tHemnummer: ");
                                sökKontakt = Console.ReadLine();
                                sökLista = KontaktRepo.HämtaKontakt(sökKontakt, "Hemnummer");
                                break;
                            case ConsoleKey.D4:
                                Console.Write($"\n\n\tJobbnummer: ");
                                sökKontakt = Console.ReadLine();
                                sökLista = KontaktRepo.HämtaKontakt(sökKontakt, "Jobbnummer");
                                break;
                            case ConsoleKey.D5:
                                Console.Write($"\n\n\tKontakttyp: ");
                                sökKontakt = Console.ReadLine();
                                sökLista = KontaktRepo.HämtaKontakt(sökKontakt, "Kontakttyp");
                                break;
                            default:
                                Console.WriteLine("Ogiltigt val.");
                                break;
                        }
                        foreach (var person in sökLista)
                        {
                            Console.WriteLine($"\n\t{person.KontaktTyp}");
                            Console.WriteLine($"\tNamn: {person.Förnamn} {person.Efternamn}");
                            Console.WriteLine($"\tAdress: {person.Gatuadress}");
                            Console.WriteLine($"\tPostnummer: {person.Postnummer} Ort: {person.Postort}");
                        }


                        Console.WriteLine("\n\n\tVill du 1. Ändra kontakt 2. Radera kontakt 3. Återgå till huvudmeny?");
                        Console.Write("\tDitt val: ");
                        menyVal = Console.ReadKey();

                        if (menyVal.Key == ConsoleKey.D1)
                            Console.WriteLine("\tÄndra kontakt någon gång men inte nu");
                        else if (menyVal.Key == ConsoleKey.D2)
                        {
                           
                            Console.WriteLine("------------sucessfully Removed---------------");

                        }



                        ÅtergåTillMeny();
                    #endregion
                    break;
                    case ConsoleKey.D4:
                        #region Radera kontakt


                        ÅtergåTillMeny();
                    #endregion
                        break;
                    case ConsoleKey.D5:
                        #region Avsluta

                    #endregion
                        break;
                    default:
                        break;

                }
            }
        }

        static void ÅtergåTillMeny()
        {
            Console.Write("\n\n\tTryck på valfri tangent för att återgå till föregående meny: ");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
