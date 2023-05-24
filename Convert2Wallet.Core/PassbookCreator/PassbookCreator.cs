using Microsoft.AspNetCore.Mvc;
using Passbook.Generator;
using System.Security.Cryptography.X509Certificates;

namespace Convert2Wallet.Core
{
    public static class PassbookCreator
    {
        // Die Variable passbook beinhaltet die Felder, die in der GUI eingegeben wurden
        public static FileContentResult GeneratePass(Passbook passbook)
        {
            // PassGenerator wird zum Generieren des Passbooks benötigt
            PassGenerator generator = new PassGenerator();

            // PassGeneratorRequest bietet die Möglichkeit 
            // die benötigten Felder in den Passbook einzufügen.
            PassGeneratorRequest passGenReq = new PassGeneratorRequest();

            // Vom Passbook geforderte Felder
            passGenReq.PassTypeIdentifier = "pass.lukasbochis.convert2wallet"; // PassTypeID des Zertifikates
            passGenReq.TeamIdentifier = "<Team ID>"; // TeamID des Apple Developer Accounts
            passGenReq.SerialNumber = "12345678"; // Seriennummer des Passbooks
            passGenReq.Description = "Convert2Wallet"; // Beschreibung
            passGenReq.OrganizationName = "Lukas Bochis"; // Name der Organisation

            // Geschtaltungs möglichkeiten des Passbooks
            passGenReq.BackgroundColor = "rgb(255,255,255)";
            passGenReq.LabelColor = "rgb(0,0,0)";
            passGenReq.ForegroundColor = "rgb(0,0,0)";
            passGenReq.Style = passbook.PassStyle;
            passGenReq.TransitType = TransitType.PKTransitTypeGeneric;

            // Zertifikate
            passGenReq.AppleWWDRCACertificate = 
                new X509Certificate2("<Pfad zum Apple WWDR Zertifikat>");
            passGenReq.PassbookCertificate = 
                new X509Certificate2("<Pfad zum Passbook Zertifikat>", "<Passwort des Zertifikats>");

            // Passspezifische Informationen, die über die GUI eingegeben wurden
            passGenReq.LogoText = passbook.LogoText;            
            passGenReq.AddPrimaryField(passbook.PrimaryField);

            foreach (var secField in passbook.SecondaryFields)
                passGenReq.AddSecondaryField(secField);

            foreach (var auxField in passbook.AuxFields)
                passGenReq.AddSecondaryField(auxField);

            passGenReq.AddBackField(passbook.BackField);


            // Generieren des Passes als Byte Array
            byte[] generatedPass = generator.Generate(passGenReq);

            // Übermittlung des Passes als ByteArray
            return new FileContentResult(generatedPass, "application/vnd.apple.pkpasses")
            {
                FileDownloadName = passbook.FileName
            };
        }
    }
}
