using Microsoft.AspNetCore.Mvc;
using Passbook.Generator;
using Passbook.Generator.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Convert2Wallet.Core
{
    public class PassbookCreator
    {
        public static FileContentResult GeneratePass(Passbook passbook)
        {
            PassGenerator generator = new PassGenerator();

            PassGeneratorRequest passGenReq = new PassGeneratorRequest();
            passGenReq.PassTypeIdentifier = "pass.lukasbochis.convert2wallet";
            passGenReq.TeamIdentifier = "Convert2Wallet";
            passGenReq.SerialNumber = "12345678";
            passGenReq.Description = "Convert2Wallet";
            passGenReq.OrganizationName = "Lukas Bochis";
            passGenReq.LogoText = passbook.LogoText;

            passGenReq.BackgroundColor = "rgb(255,255,255)";
            passGenReq.LabelColor = "rgb(0,0,0)";
            passGenReq.ForegroundColor = "rgb(0,0,0)";

            passGenReq.AppleWWDRCACertificate = new X509Certificate2("D:\\Schule\\Diplomarbeit - Convert2Wallet\\Convert2Wallet\\Convert2Wallet\\Convert2Wallet.Core\\bin\\Debug\\net5.0\\AppleWWDRCAG4.cer");
            passGenReq.PassbookCertificate = new X509Certificate2("D:\\Schule\\Diplomarbeit - Convert2Wallet\\Convert2Wallet\\Convert2Wallet\\Convert2Wallet.Core\\bin\\Debug\\net5.0\\PassCertificate.pfx", "c2wcert");


            passGenReq.Style = passbook.PassStyle;
            passGenReq.AddPrimaryField(passbook.PrimaryField);

            foreach (var secField in passbook.SecondaryFields)
                passGenReq.AddSecondaryField(secField);

            foreach (var auxField in passbook.AuxFields)
                passGenReq.AddSecondaryField(auxField);

            passGenReq.AddBackField(passbook.BackField);

            passGenReq.TransitType = TransitType.PKTransitTypeGeneric;

            byte[] generatedPass = generator.Generate(passGenReq);

            return new FileContentResult(generatedPass, "application/vnd.apple.pkpasses")
            {
                FileDownloadName = passbook.FileName
            };
        }
    }
}
