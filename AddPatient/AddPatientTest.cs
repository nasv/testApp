using System;
using NUnit.Framework;

namespace AddPatient
{
    [TestFixture]
    public class AddPatientTest
    {

        [Test]
        public void AddPatient_WithLatinName()
        {
            string guid = "0310C7A6-BDF3-4124-B9D4-5FD5C72FA066";
            string idLPU = "1.2.643.5.1.13.3.25.78.118";

            PixService.PixServiceClient client = new PixService.PixServiceClient("BasicHttpBinding_IPixService");
            PixService.PatientDto testPatient= new PixService.PatientDto();
            testPatient.FamilyName = "Ivanov";
            testPatient.GivenName = "Petr";
            testPatient.MiddleName = "Fedorovich";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            client.AddPatient(guid, idLPU, testPatient);
            PixService.PatientDto[] testPatient2 = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);
            Assert.AreEqual("Ivanov", testPatient2[0].FamilyName);
            client.Close();
        }

        [Test]
        public void AddPatient_WithRusName()
        {
            string guid = "0310C7A6-BDF3-4124-B9D4-5FD5C72FA066";
            string idLPU = "1.2.643.5.1.13.3.25.78.118";

            PixService.PixServiceClient client = new PixService.PixServiceClient("BasicHttpBinding_IPixService");
            PixService.PatientDto testPatient = new PixService.PatientDto();
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            client.AddPatient(guid, idLPU, testPatient);
            PixService.PatientDto[] testPatient2 = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);
            Assert.AreEqual("Иванов", testPatient2[0].FamilyName);
            client.Close();
        }

        [Test]
        public void AddPatient_WrongDate()
        {
            string guid = "0310C7A6-BDF3-4124-B9D4-5FD5C72FA066";
            string idLPU = "1.2.643.5.1.13.3.25.78.118";

            PixService.PixServiceClient client = new PixService.PixServiceClient("BasicHttpBinding_IPixService");
            PixService.PatientDto testPatient = new PixService.PatientDto();
            testPatient.FamilyName = "Ivanov";
            testPatient.GivenName = "Petr";
            testPatient.MiddleName = "Fedorovich";
            testPatient.BirthDate = new DateTime(1000, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);
            client.Close();
        }

        [Test]
        public void AddPatient_MissParametr_IdPatientMIS()
        {
            string guid = "0310C7A6-BDF3-4124-B9D4-5FD5C72FA066";
            string idLPU = "1.2.643.5.1.13.3.25.78.118";

            PixService.PixServiceClient client = new PixService.PixServiceClient("BasicHttpBinding_IPixService");
            PixService.PatientDto testPatient = new PixService.PatientDto();
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Petr";
            testPatient.MiddleName = "Fedorovich";
            testPatient.BirthDate = new DateTime(1993, 05, 05);
            testPatient.Sex = 1;
      
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);
            client.Close();
        }
    }
}
