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

        const string guid = "0310C7A6-BDF3-4124-B9D4-5FD5C72FA066";
        const string idLPU = "1.2.643.5.1.13.3.25.78.118";
        PixService.PixServiceClient client;
        PixService.PatientDto testPatient;

        [TestFixtureSetUp]
        public void Init()
        {

            client = new PixService.PixServiceClient("BasicHttpBinding_IPixService");
            testPatient = new PixService.PatientDto();
        }


        [TestFixtureTearDown]
        public void Dispose()
        {
            client.Close();
        }

        [Test]
        public void AddPatient_With_LatinName()
        {

            
            testPatient.FamilyName = "Ivanov";
            testPatient.GivenName = "Petr";
            testPatient.MiddleName = "Fedorovich";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            client.AddPatient(guid, idLPU, testPatient);
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);

        }

        [Test]
        public void AddPatient_With_RusName()
        {

            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            client.AddPatient(guid, idLPU, testPatient);
            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);
            Assert.AreEqual("Иванов", testPatients[0].FamilyName);
        }

        [Test]
        public void AddPatient_With_Eng_RusNames()
        {

            testPatient.FamilyName = "Ivanov";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);
        }

        [Test]
        public void AddPatient_With_Wrong_Rus_Name()
        {

            testPatient.FamilyName = "ОРоырв224*/-";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            client.AddPatient(guid, idLPU, testPatient);
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);

        }

       [Test]
        public void AddPatient_With_WrongDate()
        {
            
            testPatient.FamilyName = "Ivanov";
            testPatient.GivenName = "Petr";
            testPatient.MiddleName = "Fedorovich";
            testPatient.BirthDate = new DateTime();
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);
        }

        [Test]
        public void AddPatient_With_Date_Greater_Than_Today()
        {

            testPatient.FamilyName = "Ivanov";
            testPatient.GivenName = "Petr";
            testPatient.MiddleName = "Fedorovich";
                  
            testPatient.BirthDate = DateTime.Now.AddDays(1);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);
        }

        [Test]
        public void AddPatient_MissParametr_IdPatientMIS()
        {

            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1993, 05, 05);
            testPatient.Sex = 1;
      
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);
        }



        [Test]
        public void AddPatient_Update_FamilyName()
        {

            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            client.AddPatient(guid, idLPU, testPatient);

            testPatient.FamilyName = "Петров";
            client.AddPatient(guid, idLPU, testPatient);
            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual("Петров", testPatients[0].FamilyName);
        }

        [Test]
        public void AddPatient_Update_GivenName()
        {

            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            client.AddPatient(guid, idLPU, testPatient);

            testPatient.GivenName = "Иван";
            client.AddPatient(guid, idLPU, testPatient);
            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual("Иван", testPatients[0].GivenName);
        }

        [Test]
        public void AddPatient_Update_All_Data()
        {

            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            client.AddPatient(guid, idLPU, testPatient);

            PixService.PatientDto testPatient2 = new PixService.PatientDto();
            testPatient2.FamilyName = "Петров";
            testPatient2.GivenName = "Иван";
            testPatient2.MiddleName = "Данилович";
            testPatient2.BirthDate = new DateTime(1987, 05, 05);
            testPatient2.IdPatientMIS = "1234567";
            testPatient2.Sex = 1;

            client.AddPatient(guid, idLPU, testPatient2);
            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);
            Assert.That(testPatient2.Equals(testPatients[0]));
            //Equals ?
        }

        [Test]
        public void AddPatient_Do_Not_Add_The_Same_Patient()
        {

            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            client.AddPatient(guid, idLPU, testPatient);
            PixService.PatientDto testPatient2 = new PixService.PatientDto();

            testPatient2.FamilyName = "Иванов";
            testPatient2.GivenName = "Петр";
            testPatient2.MiddleName = "Федорович";
            testPatient2.BirthDate = new DateTime(1992, 05, 05);
            testPatient2.IdPatientMIS = "1234567";
            testPatient2.Sex = 1;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient2), Throws.Exception);

        }

        [Test]
        public void AddPatient_With_All_Parameters()
        {

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
            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);
            Assert.AreEqual("Иванов", testPatients[0].FamilyName);
        }

        [Test]
        public void AddPatient_With_Privilege_DateEnd_LessThan_DateStart()
        {

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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);
        }

        [Test]
        public void AddPatient_With_SocialGroup_0()
        {

            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdBloodType = 1;
            testPatient.IdLivingAreaType = 2;
            testPatient.SocialGroup = 0;
            testPatient.SocialStatus = "2@";

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);
        }

        [Test]
        public void AddPatient_With_SocialGroup_5()
        {
    
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdBloodType = 1;
            testPatient.IdLivingAreaType = 2;
            testPatient.SocialGroup = 5;
            testPatient.SocialStatus = "2@";
           
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);
        }

        [Test]
        public void AddPatient_Check_SocialGroup()
        {
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

            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual(1, testPatients[0].SocialGroup);

        }

        [Test]
        public void AddPatient_Check_SocialStatus()
        {
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

            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual("2@", testPatients[0].SocialStatus);

        }

        [Test]
        public void AddPatient_With_Sex_0()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 0;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);

        }

        [Test]
        public void AddPatient_With_Sex_4()
        {

            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 4;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);

        }

        [Test]
        public void AddPatient_With_IdBloodType_0()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdBloodType = 0;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);

        }

        [Test]
        public void AddPatient_With_IdBloodType_9()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdBloodType = 9;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);

        }

        [Test]
        public void AddPatient_With_IdLivingAreaType_0()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 0;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);

        }

        [Test]
        public void AddPatient_With_IdLivingAreaType_3()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 3;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);

        }
    }
    }
