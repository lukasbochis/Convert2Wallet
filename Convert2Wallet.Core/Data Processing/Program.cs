using System;
using System.Collections.Generic;
using Passbook.Generator;
using Passbook.Generator.Fields;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Linq;

namespace Convert2Wallet.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string name = "Lukas Bochis";
            DateTime date = DateTime.Now;

            string[] header = { "tag;value" }; // Kopfzeile für das 
            string[] dates = DateCreator.CreateDates(500);
            string[] names = ReadNames("OGDEXT_VORNAMEN_1.csv");
            string[] words = ReadEssay_SplitIntoWords("Beispielaufsatz.txt");

            string[] total = ((header.Concat(dates)).Concat(names)).Concat(words).ToArray();
            int totalCount = total.Count();

            File.WriteAllLines("dataset.csv", total, Encoding.UTF8);

            Console.WriteLine($"{totalCount} written to dataset.csv");
        }



        public static string[] ReadEssay_SplitIntoWords(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);

            LinkedList<string> wordsList = new LinkedList<string>();
            string tag = "wort;";
            char[] delimiters = { '.', ' ', ',', '!', '?', '\"', '»', '«', ':', '|', '(', ')' };

            foreach (string line in lines)
            {
                string[] separatedLine = line.Split(delimiters);

                foreach (string word in separatedLine)
                    wordsList.AddLast(tag + word);
            }


            return wordsList.ToArray();
        }


        public static string[] ReadNames(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);

            LinkedList<string> namesList = new LinkedList<string>();
            string tag = "name;";

            for (int i = 0; i < lines.Length; i++)
            {
                // Da über 500.000 Namen vorhanden sind, werden diese auf 1000 reduziert.
                if (i % 500 == 0)
                {
                    string[] separatedLine = lines[i].Split(";");
                    namesList.AddLast(tag + separatedLine[3]);
                }                
            }

            return namesList.ToArray();
        }


        public static FileContentResult GeneratePass(/*Dictionary<string, object> passData*/)
        {
            PassGenerator generator = new PassGenerator();

            PassGeneratorRequest passGenReq = new PassGeneratorRequest();
            passGenReq.PassTypeIdentifier = "pass.lukasbochis.convert2wallet";
            passGenReq.TeamIdentifier = "Convert2Wallet";
            passGenReq.SerialNumber = "12345678";
            passGenReq.Description = "Convert2Wallet";
            passGenReq.OrganizationName = "Lukas Bochis";
            passGenReq.LogoText = "My Pass";

            passGenReq.BackgroundColor = "rgb(200,255,200)";
            passGenReq.LabelColor = "rgb(0,255,0)";
            passGenReq.ForegroundColor = "rgb(0,0,0)";

            passGenReq.Images.Add(PassbookImage.Icon, System.IO.File.ReadAllBytes("ticketicon.png"));
            passGenReq.Images.Add(PassbookImage.Icon2X, System.IO.File.ReadAllBytes("ticketicon.png"));

            passGenReq.AppleWWDRCACertificate = new X509Certificate2("AppleWWDRCAG4.cer");
            passGenReq.PassbookCertificate = new X509Certificate2("PassCertificate.pfx", "c2wcert");

            passGenReq.Style = PassStyle.Generic;
            passGenReq.AddPrimaryField(new StandardField("name", "name", "Lukas Bochis"));
            passGenReq.AddSecondaryField(new StandardField("date", "date", DateTime.Now.ToShortDateString()));
            passGenReq.AddBackField(new StandardField("backfield", "backfield", "backfield"));
            passGenReq.AddAuxiliaryField(new StandardField("auxfield", "auxfield", "auxfield"));

            passGenReq.AddHeaderField(new StandardField("headerfield", "headerfield", "headerfield"));
            passGenReq.AddPrimaryField(new StandardField("prim", "Prim", "Prim"));
            passGenReq.AddSecondaryField(new StandardField("second", "second", "second"));
            passGenReq.AddSecondaryField(new StandardField("second2", "second2", "second2"));
            passGenReq.AddLocation(48.2684195, 14.2495645, "HTL Leonding");

            passGenReq.TransitType = TransitType.PKTransitTypeGeneric;

            byte[] generatedPass = generator.Generate(passGenReq);

            return new FileContentResult(generatedPass, "application/vnd.apple.pkpass")
            {
                FileDownloadName = "ticket.pkpass"
            };


            /*// Add the required pass data to the dictionary
            passData["formatVersion"] = 1;
            passData["passTypeIdentifier"] = "pass.com.example";WindowsCryptographicException: Key does not exist.
            passData["serialNumber"] = "123456";

            // Create a PKPass object with the pass data
            Pass pass = new PKPass(passData);

            // Get the pass as a byte array
            return pass.GetPass();*/
        }
    }
}
