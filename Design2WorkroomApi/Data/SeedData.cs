using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;

namespace Design2WorkroomApi.Data
{
    public class SeedMockDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string WorkroomName { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string PostalCode { get; set; }
    }

    public class SeedMockDataStore
    {
        public List<SeedMockDataModel> FakeDesignersDataStore { get; set; }

        public List<SeedMockDataModel> FakeClientsDataStore { get; set; }

        public List<SeedMockDataModel> FakeWorkroomsDataStore { get; set; }


        public void GenerateDummyDesigners()
        {
            int totalRecords = 5;
            FakeDesignersDataStore = new List<SeedMockDataModel>();

            for (int i = 0; i < totalRecords; i++)
            {
                string firstName = Faker.Name.FirstName();
                string lastName = Faker.Name.LastName();
                var record = new SeedMockDataModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = Faker.User.Email(),
                    WorkroomName = $"{Faker.Name.LastName()} Workroom, LLC",
                    Phone = Faker.Phone.GetShortPhoneNumber(),
                    ContactName = $"{firstName} {lastName}",
                    PostalCode = GetRandomPostalCode()
                };

                FakeDesignersDataStore.Add(record);
            }
        }

        public void GenerateDummyClients()
        {
            int totalRecords = 100;
            FakeClientsDataStore = new List<SeedMockDataModel>();

            for (int i = 0; i < totalRecords; i++)
            {
                string firstName = Faker.Name.FirstName();
                string lastName = Faker.Name.LastName();
                var record = new SeedMockDataModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = Faker.User.Email(),
                    WorkroomName = $"{Faker.Name.LastName()} Workroom, LLC",
                    Phone = Faker.Phone.GetShortPhoneNumber(),
                    ContactName = $"{firstName} {lastName}",
                    PostalCode = GetRandomPostalCode()
                };

                FakeClientsDataStore.Add(record);
            }
        }

        public void GenerateDummyWorkrooms()
        {
            int totalRecords = 50;
            FakeWorkroomsDataStore = new List<SeedMockDataModel>();

            for (int i = 0; i < totalRecords; i++)
            {
                string firstName = Faker.Name.FirstName();
                string lastName = Faker.Name.LastName();
                var record = new SeedMockDataModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = Faker.User.Email(),
                    WorkroomName = $"{Faker.Name.LastName()} Workroom, LLC",
                    Phone = Faker.Phone.GetShortPhoneNumber(),
                    ContactName = $"{firstName} {lastName}",
                    PostalCode = GetRandomPostalCode()
                };

                FakeWorkroomsDataStore.Add(record);
            }
        }

        public string GetRandomPostalCode()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                sb.Append(Number.RandomNumber(10));
            }

            return sb.ToString();
        }
    }
}
