using AutoMapper;
using Design2WorkroomApi.Data;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Enums;
using Design2WorkroomApi.Helpers;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinifyAPI;

namespace Design2WorkroomApi.Controllers
{
    //[Route("api/seeder")]
    //[ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ILogger<SeedController> _logger;
        private readonly IMapper _mapper;
        private readonly IDesignerRepository _designerRepo;
        private readonly IWorkroomRepository _workroomRepo;
        private readonly IClientRepository _clientRepo;
        private readonly AppUserHelper _appUserHelper;
        private static Random _randomizer;

        public SeedController(ILogger<SeedController> logger,
            IMapper mapper,
            IDesignerRepository designerRepo,
            IWorkroomRepository workroomRepo,
            IClientRepository clientRepo,
            AppUserHelper appUserHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _designerRepo = designerRepo;
            _workroomRepo = workroomRepo;
            _clientRepo = clientRepo;
            _appUserHelper = appUserHelper;
            _randomizer = new Random();
        }


        [HttpGet("MockDesigners")]
        public async Task<IActionResult> SeedMockDesigners()
        {

            var dataSeeder = new SeedMockDataStore();
            dataSeeder.GenerateDummyDesigners();

            // seed designers
            foreach (var record in dataSeeder.FakeDesignersDataStore)
            {
                var designer = new DesignerModel(record.Email, "")
                {
                    AppUserRole = AppUserRole.Designer,
                    CreatedAt = DateTime.UtcNow,
                    Profile = new ProfileModel(record.Email, record.FirstName, record.LastName, record.Phone, record.PostalCode)
                };

                await _designerRepo.CreateDesignerAsync(designer);
            }

            return Ok(dataSeeder.FakeDesignersDataStore);
        }

        [HttpGet("MockClients")]
        public async Task<IActionResult> SeedMockClients()
        {
            var dataSeeder = new SeedMockDataStore();
            dataSeeder.GenerateDummyClients();

            // seed designers
            foreach (var record in dataSeeder.FakeClientsDataStore)
            {
                var client = new ClientModel(record.Email, "")
                {
                    AppUserRole = AppUserRole.Client,
                    CreatedAt = DateTime.UtcNow,
                    Profile = new ProfileModel(record.Email, record.FirstName, record.LastName, record.Phone, record.PostalCode)
                };

                await _clientRepo.CreateClientAsync(client);
            }

            return Ok(dataSeeder.FakeClientsDataStore);
        }

        [HttpGet("MockWorkrooms")]
        public async Task<IActionResult> SeedMockWorkrooms()
        {
            var dataSeeder = new SeedMockDataStore();
            dataSeeder.GenerateDummyWorkrooms();

            // seed designers
            foreach (var record in dataSeeder.FakeWorkroomsDataStore)
            {
                var workroom = new WorkroomModel(record.Email, "")
                {
                    AppUserRole = AppUserRole.Workroom,
                    CreatedAt = DateTime.UtcNow,
                    Profile = new ProfileModel(record.Email, record.FirstName, record.LastName, record.Phone, record.PostalCode, record.WorkroomName, record.ContactName) };

                await _workroomRepo.CreateWorkroomAsync(workroom);
            }

            return Ok(dataSeeder.FakeWorkroomsDataStore);
        }


        [HttpGet("MockDesignerClients")]
        public async Task<IActionResult> SeedDesignerClients()
        {
            var designersResult = await _designerRepo.GetAllDesignersAsync();
            var clientsResult = await _clientRepo.GetAllClientsAsync();

            foreach (var client in clientsResult.Clients) 
            {
                var designerSelected = designersResult.Designers?.ElementAt(_randomizer.Next(designersResult.Designers.Count()));
                await _designerRepo.AddClientToDesignerAsync(designerSelected.Id, client.Id);
            }

            return Ok();
        }


        [HttpGet("MockDesignerWorkrooms")]
        public async Task<IActionResult> SeedDesignerWorkrooms()
        {
            var designersResult = await _designerRepo.GetAllDesignersAsync();
            var workroomsResult = await _workroomRepo.GetAllWorkroomsAsync();

            foreach (var workroom in workroomsResult.Workrooms)
            {
                var designerSelected = designersResult.Designers?.ElementAt(_randomizer.Next(designersResult.Designers.Count()));
                await _designerRepo.AddWorkroomToDesignerAsync(designerSelected.Id, workroom.Id);
            }

            return Ok();
        }

        [HttpGet("ActiveStatus")]
        public async Task<IActionResult> SeedActiveStatus()
        {
            var designersResult = await _designerRepo.GetAllDesignersAsync();

            if (designersResult.IsSuccess)
            {
                foreach (var designer in designersResult.Designers)
                {
                    Guid designerId = designer.Id;

                    var result1 = await _clientRepo.GetClientsByDesignerIdAsync(designerId);

                    foreach (var designerClient in result1.Clients)
                    {
                        bool randomBool = Faker.Number.Bool();
                        await _designerRepo.UpdateClientActiveStatusAsync(designerId, designerClient.Id, randomBool);
                    }

                    var result2 = await _workroomRepo.GetWorkroomsByDesignerIdAsync(designerId);

                    foreach (var designerworkroom in result2.Workrooms)
                    {
                        bool randomBool = Faker.Number.Bool();
                        await _designerRepo.UpdateWorkroomActiveStatusAsync(designerId, designerworkroom.Id, randomBool);
                    }
                }

                return Ok();
            }

            return BadRequest();

        }

        [HttpGet("ClientAddresses")]
        public async Task<IActionResult> SeedAddresses()
        {

            var result = await _clientRepo.GetAllClientsAsync();

            if (result.IsSuccess)
            {
                foreach (var client in result.Clients)
                {
                    client.Profile.CountryCode = "US";
                    client.Profile.PostalCode = Faker.Address.USZipCode();
                    client.Profile.State = Faker.Address.StateAbbreviation();
                    client.Profile.StreetAddress1 = $"{ Faker.Number.RandomNumber(100,4999) } { Faker.Address.StreetName() }";
                    client.Profile.City = Faker.Address.USCity();

                    await _clientRepo.UpdateClientAsync(client);
                }
                
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("WorkroomAddresses")]
        public async Task<IActionResult> SeedWorkroomAddresses()
        {

            var result = await _workroomRepo.GetAllWorkroomsAsync();

            if (result.IsSuccess)
            {
                foreach (var workroom in result.Workrooms)
                {
                    workroom.Profile.CountryCode = "US";
                    workroom.Profile.PostalCode = Faker.Address.USZipCode();
                    workroom.Profile.State = Faker.Address.StateAbbreviation();
                    workroom.Profile.StreetAddress1 = $"{ Faker.Number.RandomNumber(100, 4999) } { Faker.Address.StreetName() }";
                    workroom.Profile.City = Faker.Address.USCity();

                    await _workroomRepo.UpdateWorkroomAsync(workroom);
                }

                return Ok();
            }

            return BadRequest();
        }

    }
}
