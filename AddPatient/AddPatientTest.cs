using System;
using NUnit.Framework;

namespace AddPatient
{
    [TestFixture]
    public class AddPatientTest
    {
        /*
         * temporaly use of GUID and IdLPU as strings not as GUID as it is said in Docs
         * TBD:
         * change string type to guid except for special tests 
         * */
        [TestFixtureSetUp]
        public void AddPatient_SetUp()
        {

        }

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

        [Test]
        public void AddPatient_UpdateFamilyName()
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
            PixService.PatientDto[] testPatient11 = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);
            Assert.AreEqual("Иванов", testPatient11[0].FamilyName);

            testPatient.FamilyName = "Петров";
            client.AddPatient(guid, idLPU, testPatient);
            PixService.PatientDto[] testPatient2 = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);
            Assert.AreEqual("Петров", testPatient2[0].FamilyName);
            client.Close();
        }
        [Test]
        public void AddPatient_UpdateGivenName()
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
            testPatient.GivenName = "Иван";
            client.AddPatient(guid, idLPU, testPatient);
            PixService.PatientDto[] testPatient2 = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);
            Assert.AreEqual("Иван", testPatient2[0].GivenName);
            client.Close();
        }
        [Test]
        public void AddPatient_UpdateAllData()
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
            PixService.PatientDto testPatient_2 = new PixService.PatientDto();

            testPatient_2.FamilyName = "Петров";
            testPatient_2.GivenName = "Иван";
            testPatient_2.MiddleName = "Данилович";
            testPatient_2.BirthDate = new DateTime(1987, 05, 05);
            testPatient_2.IdPatientMIS = "1234567";
            testPatient_2.Sex = 1;

            client.AddPatient(guid, idLPU, testPatient_2);
            PixService.PatientDto[] testPatient2 = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);
            Assert.That(testPatient_2.Equals(testPatient2[0]));

            client.Close();
        }

        //Проверить, что пациент с идентичными данным не добавляется

        [Test]
        public void AddPatient_DoNotAddTheSamePatient()
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
            PixService.PatientDto testPatient_2 = new PixService.PatientDto();

            testPatient_2.FamilyName = "Иванов";
            testPatient_2.GivenName = "Петр";
            testPatient_2.MiddleName = "Федорович";
            testPatient_2.BirthDate = new DateTime(1992, 05, 05);
            testPatient_2.IdPatientMIS = "1234567";
            testPatient_2.Sex = 1;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient_2), Throws.Exception);

            client.Close();
        }

        [Test]
        public void AddPatient_With()
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
            testPatient.IdBloodType = 1;
            testPatient.IdLivingAreaType = 2;
            testPatient.SocialGroup = 1;
            testPatient.SocialStatus = "2@";

            PixService.PrivilegeDto priv = new PixService.PrivilegeDto();
            priv.DateStart = new DateTime(2017, 01, 21);
            priv.DateEnd = new DateTime(2017, 01, 20);
            //не происходит проверки даты
            priv.IdDisabilityType = 1;
            priv.IdPrivilegeType = 1;
            //Доступны не указанные в описании поля, не думаю, что это нужно(можно) автоматизировать
            priv.DisabilityDegree = "32874879";

            PixService.JobDto job = new PixService.JobDto();
            job.CompanyName = "ssfs";
            job.OgrnCode = "aasfasdasd222";
            job.CompanyName = "sfdfs";
            job.Sphere = "wfw";
            job.Position = "sfdsf";
            job.DateStart = new DateTime(2017, 01, 21);
            job.DateEnd = new DateTime(2017, 01, 20);
            //все поля доступны, лишних полей нет

            PixService.ContactDto contact = new PixService.ContactDto();
            contact.IdContactType = 1;
            contact.ContactValue = "fssf";
            //+

            PixService.BirthPlaceDto birthPlace = new PixService.BirthPlaceDto();
            birthPlace.Country = "rtetr";
            birthPlace.City = "Sfsfs";
            birthPlace.Region = "sfsdfs";
            //+

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "wrwerw";
            address.Street = "fsfsf";
            address.Building = "fsf";
            address.City = "sfsf";
            address.Appartment = "sfsf";
            address.PostalCode = 12342;
            address.GeoData = "fsffs";
            //
            PixService.DocumentDto doc = new PixService.DocumentDto();




            client.AddPatient(guid, idLPU, testPatient);
            PixService.PatientDto[] testPatient2 = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);
            Assert.AreEqual("Иванов", testPatient2[0].FamilyName);
            client.Close();
        }

    }
    }
