using System;
using NUnit.Framework;

namespace AddPatient
{
    [TestFixture]
    public class AddPatientTest
    {
      
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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);
        }

        [Test]
        public void AddPatient_With_Invalid_FamilyName()
        {

            testPatient.FamilyName = "ОРоырв224*/-";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            client.AddPatient(guid, idLPU, testPatient);
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

       [Test]
        public void AddPatient_With_Invalid_BirthDate()
        {
            
            testPatient.FamilyName = "Ivanov";
            testPatient.GivenName = "Petr";
            testPatient.MiddleName = "Fedorovich";
            testPatient.BirthDate = new DateTime();
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);
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
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);
        }

        [Test]
        public void AddPatient_MissParametr_IdPatientMIS()
        {

            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1993, 05, 05);
            testPatient.Sex = 1;
      
            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);
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
            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient2, PixService.SourceType.Reg);
            Assert.AreEqual(testPatient2.GetHashCode(), testPatients[0].GetHashCode());
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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);
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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);
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

            client.AddPatient(guid, idLPU, testPatient);
           
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

            client.AddPatient(guid, idLPU, testPatient);

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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

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

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

           
        [Test]
        public void AddPatient_With_Job_DateEnd_LessThan_DateStart()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;

            PixService.JobDto job = new PixService.JobDto();
            job.CompanyName = "ssfs"; //Пожелание: проверка на "подлинность"
            job.Sphere = "wfw";//Пожелание: проверка на "подлинность"
            job.Position = "sfdsf";//Пожелание: проверка на "подлинность"
            job.DateStart = new DateTime(2017, 01, 21);
            job.DateEnd = new DateTime(2017, 01, 20);

            testPatient.Job = job;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Job_Invalid_OgrnCode()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;

            PixService.JobDto job = new PixService.JobDto();
            job.CompanyName = "ssfs"; //Пожелание: проверка на "подлинность"
            job.OgrnCode = "123344566";

            testPatient.Job = job;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }


        [Test]
        public void AddPatient_With_Contact_IdContactType_0()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;

       
            PixService.ContactDto contact = new PixService.ContactDto();
            contact.IdContactType = 0;
            contact.ContactValue = "мать";

            testPatient.Contacts = new PixService.ContactDto[1];

            testPatient.Contacts[0] = contact;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Contact_IdContactType_5()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;


            PixService.ContactDto contact = new PixService.ContactDto();
            contact.IdContactType = 5;
            contact.ContactValue = "мать";

            testPatient.Contacts = new PixService.ContactDto[1];
            testPatient.Contacts[0] = contact;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }


        [Test]
        public void AddPatient_With_Invalid_BirthPlace()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;

            PixService.BirthPlaceDto birthPlace = new PixService.BirthPlaceDto();
            birthPlace.Country = "rtetr";
            birthPlace.City = "Sfsfs";
            birthPlace.Region = "sfsdfs";

            testPatient.BirthPlace = birthPlace;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.Exception);

        }

        [Test]
        public void AddPatient_With_Valid_BirthPlace()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.BirthPlaceDto birthPlace = new PixService.BirthPlaceDto();
            birthPlace.Country = "Россия";
            birthPlace.City = "Санкт-Петербург";
            birthPlace.Region = "Северо-Западный регион";

            testPatient.BirthPlace = birthPlace;

            client.AddPatient(guid, idLPU, testPatient);

            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual("Россия", testPatients[0].BirthPlace.Country);
            Assert.AreEqual("Санкт-Петербург", testPatients[0].BirthPlace.City);
            Assert.AreEqual("Северо-Западный регион", testPatients[0].BirthPlace.Region);

        }

        [Test]
        public void AddPatient_With_Valid_Address()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            address.Street = "78000000000005400";
            address.PostalCode = 198216;
            address.Building = "34";
            address.Appartment = "654";
            address.City = "Санкт-Петербург";
            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            client.AddPatient(guid, idLPU, testPatient);

            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual(1, testPatients[0].Addresses[0].IdAddressType);
            Assert.AreEqual("ул. Автомобильная, 34-654", testPatients[0].Addresses[0].StringAddress);
            Assert.AreEqual("78000000000005400", testPatients[0].Addresses[0].Street);
            Assert.AreEqual(198216, testPatients[0].Addresses[0].PostalCode);
            Assert.AreEqual("34", testPatients[0].Addresses[0].Building);
            Assert.AreEqual("654", testPatients[0].Addresses[0].Appartment);
            Assert.AreEqual("Санкт-Петербург", testPatients[0].Addresses[0].City);

        }


        [Test]
        public void AddPatient_With_Address_IdAddressType_0()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;


            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 0;
            address.StringAddress = "ул. Автомобильная, 34-654";

            testPatient.Addresses = new PixService.AddressDto[1];
            testPatient.Addresses[0] = address;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);
        }

        [Test]
        public void AddPatient_With_Address_IdAddressType_10()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 10;
            address.StringAddress = "ул. Автомобильная, 34-654";
            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);
        }

        [Test]
        public void AddPatient_With_Address_Valid_StringAddress()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            client.AddPatient(guid, idLPU, testPatient);

            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual("ул. Автомобильная, 34-2-654", testPatients[0].Addresses[0].StringAddress);
        }

        [Test]
        public void AddPatient_With_Address_Valid_Street_Without_Spaces()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            address.Street = "78000000000005400";
            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            client.AddPatient(guid, idLPU, testPatient);

            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual("78000000000005400", testPatients[0].Addresses[0].Street);
        }

        [Test]
        public void AddPatient_With_Address_Valid_Street_With_Spaces()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            address.Street = "78 000 000 000 0054 00";
            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Address_Invalid_Street()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 1;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            address.Street = "78000000000005500";
            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Address_Valid_PostalCode()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 3;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            address.PostalCode = 198216;

            testPatient.Addresses = new PixService.AddressDto[1];
            testPatient.Addresses[0] = address;

            client.AddPatient(guid, idLPU, testPatient);

            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual(198216, testPatients[0].Addresses[0].PostalCode);
        }

        [Test]
        public void AddPatient_With_Address_Valid_PostalCode_Convert_ToInt()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 3;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            address.PostalCode = Convert.ToInt32("198216");
            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            client.AddPatient(guid, idLPU, testPatient);

            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual(198216, testPatients[0].Addresses[0].PostalCode);
        }


        [Test]
        public void AddPatient_With_Address_Invalid_PostalCode()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 3;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            address.PostalCode = 194216;
            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Address_Invalid_Building()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 3;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            address.Building = "32";
            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Address_Invalid_Apartment()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 3;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            address.Appartment = "321";

            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;
            client.AddPatient(guid, idLPU, testPatient);

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Address_Invalid_GeoData()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 3;

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "ул. Автомобильная, 34-654";
            address.GeoData = "sfdfsfsdfdf"; //Пожелание: маска GeoData

            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        
        [Test]
        public void AddPatient_With_Invalid_Address_City_and_IdLivingAreaType()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 2; //Село

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "Кирочная";
            address.City = "Санкт-Петербург";//Город
            testPatient.Addresses = new PixService.AddressDto[1];

            testPatient.Addresses[0] = address;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_2_Addresses()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;
            testPatient.IdLivingAreaType = 2; //Село

            PixService.AddressDto address = new PixService.AddressDto();
            address.IdAddressType = 1;
            address.StringAddress = "Кирочная";
            address.City = "Санкт-Петербург";//Город

            PixService.AddressDto address2 = new PixService.AddressDto();
            address2.IdAddressType = 2;
            address2.StringAddress = "Кирочная ул., д. 24-3-345";
            address2.City = "Санкт-Петербург";//Город

            testPatient.Addresses = new PixService.AddressDto[2];

            testPatient.Addresses[0] = address;
            testPatient.Addresses[1] = address2;

            client.AddPatient(guid, idLPU, testPatient);

            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual("Кирочная", testPatients[0].Addresses[0].StringAddress);
            Assert.AreEqual("Кирочная ул., д. 24-3-345", testPatients[0].Addresses[1].StringAddress);
     
        }

        //Document

        [Test]
        public void AddPatient_With_Document()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.DocumentDto doc = new PixService.DocumentDto();
            doc.IdDocumentType = 1;
            doc.DocS = "4445";
            doc.DocN = "444444";
            doc.ExpiredDate = new DateTime(2020, 05, 01);
            doc.IssuedDate = new DateTime(2010, 05, 01);
            doc.ProviderName = "ОМ45";
            doc.RegionCode = "78";

            testPatient.Documents = new PixService.DocumentDto[1];
            testPatient.Documents[0] = doc;

            client.AddPatient(guid, idLPU, testPatient);
            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual(1, testPatients[0].Documents[0].IdDocumentType);
            Assert.AreEqual("4445", testPatients[0].Documents[0].DocS);
            Assert.AreEqual("444444", testPatients[0].Documents[0].DocN);
            Assert.AreEqual(new DateTime(2020, 05, 01), testPatients[0].Documents[0].ExpiredDate);
            Assert.AreEqual(new DateTime(2010, 05, 01), testPatients[0].Documents[0].IssuedDate);
            Assert.AreEqual("ОМ45", testPatients[0].Documents[0].ProviderName);
            Assert.AreEqual("78", testPatients[0].Documents[0].RegionCode);

        }

        [Test]
        public void AddPatient_With_2_Documents()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.DocumentDto doc = new PixService.DocumentDto();
            doc.IdDocumentType = 1;
            doc.DocS = "4445";
            doc.DocN = "444444";
         

            PixService.DocumentDto doc2 = new PixService.DocumentDto();
            doc2.IdDocumentType = 230;
            doc.DocS = "4445";
            doc2.DocN = "44444355344";

            testPatient.Documents = new PixService.DocumentDto[2];

            testPatient.Documents[0] = doc;
            testPatient.Documents[1] = doc2;

            client.AddPatient(guid, idLPU, testPatient);

            PixService.PatientDto[] testPatients = client.GetPatient(guid, idLPU, testPatient, PixService.SourceType.Reg);

            Assert.AreEqual(1, testPatients[0].Documents[0].IdDocumentType);
            Assert.AreEqual("4445", testPatients[0].Documents[0].DocS);
            Assert.AreEqual("444444", testPatients[0].Documents[0].DocN);
            Assert.AreEqual(230, testPatients[0].Documents[1].IdDocumentType);
            Assert.AreEqual("4445", testPatients[0].Documents[1].DocS);
            Assert.AreEqual("44444355344", testPatients[0].Documents[1].DocN);

        }



        //IdProvider for not ПОЛИС
        // 226 Полис ОМС старого образца
        // 228 Полис ОМС единого образца
        // 240 Полис ДМС


        [Test]
        public void AddPatient_With_Document_DocN_not_Int_Without_Spaces()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.DocumentDto doc = new PixService.DocumentDto();
            doc.IdDocumentType = 1;
            doc.DocS = "4445";
            doc.DocN = "etretet";

            testPatient.Documents = new PixService.DocumentDto[1];
            testPatient.Documents[0] = doc;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Document_DocS_not_Int_Without_Spaces()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.DocumentDto doc = new PixService.DocumentDto();
            doc.IdDocumentType = 1;
            doc.DocS = "sfdfsf";
            doc.DocN = "345678";
            testPatient.Documents = new PixService.DocumentDto[1];
            testPatient.Documents[0] = doc;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }



        [Test]
        public void AddPatient_With_Document_DocS_with_Spaces()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.DocumentDto doc = new PixService.DocumentDto();
            doc.IdDocumentType = 1;
            doc.DocS = "44 45";
            doc.DocN = "444444";
            testPatient.Documents = new PixService.DocumentDto[1];
            testPatient.Documents[0] = doc;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Document_DocN_with_Spaces()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.DocumentDto doc = new PixService.DocumentDto();
            doc.IdDocumentType = 1;
            doc.DocS = "4445";
            doc.DocN = "444 444";
            testPatient.Documents = new PixService.DocumentDto[1];
            testPatient.Documents[0] = doc;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Document_ProviderName_with_Spaces()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.DocumentDto doc = new PixService.DocumentDto();
            doc.IdDocumentType = 1;
            doc.DocS = "4445";
            doc.DocN = "444444";
            doc.ProviderName = "ОМ 45";
            testPatient.Documents = new PixService.DocumentDto[1];
            testPatient.Documents[0] = doc;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Document_MissParameter_DocN()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.DocumentDto doc = new PixService.DocumentDto();
            doc.IdDocumentType = 1;
            testPatient.Documents = new PixService.DocumentDto[1];
            testPatient.Documents[0] = doc;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Document_IdDocumentType_0()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.DocumentDto doc = new PixService.DocumentDto();
            doc.IdDocumentType = 0;
            doc.DocS = "44455";// Не должны использоваться разделители (пробелы, тире и т.д.)
            doc.DocN = "gjg"; // Не должны использоваться разделители (пробелы, тире и т.д.)
            doc.ExpiredDate = new DateTime(2020, 01, 01);
            doc.IssuedDate = new DateTime(2010, 01, 01);
            doc.ProviderName = "sdfsdf";
            doc.RegionCode = "sdfsf";
            testPatient.Documents = new PixService.DocumentDto[1];
            testPatient.Documents[0] = doc;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }

        [Test]
        public void AddPatient_With_Document_ExpiredDate_LessThan_IssuedDate()
        {
            testPatient.FamilyName = "Иванов";
            testPatient.GivenName = "Петр";
            testPatient.MiddleName = "Федорович";
            testPatient.BirthDate = new DateTime(1992, 05, 05);
            testPatient.IdPatientMIS = "1234567";
            testPatient.Sex = 1;

            PixService.DocumentDto doc = new PixService.DocumentDto();
            doc.IdDocumentType = 1;
            doc.DocS = "4445";// Не должны использоваться разделители (пробелы, тире и т.д.)
            doc.DocN = "545455"; // Не должны использоваться разделители (пробелы, тире и т.д.)
            doc.ExpiredDate = new DateTime(2010, 01, 01);
            doc.IssuedDate = new DateTime(2020, 01, 01);
            testPatient.Documents = new PixService.DocumentDto[1];
            testPatient.Documents[0] = doc;

            Assert.That(() => client.AddPatient(guid, idLPU, testPatient), Throws.ArgumentException);

        }



    }
    }
