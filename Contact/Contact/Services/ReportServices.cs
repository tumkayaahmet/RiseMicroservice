using AutoMapper;
using Contact.Models;
using Contact.Settings;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RabbitMQ.Client;
using Shared.Dtos;
using Shared.Messages;

namespace Contact.Services
{
    public class ReportServices : IReportServices
    {
        private readonly IMongoCollection<Person> _personCollection;
        private readonly IMongoCollection<ContactInformation> _contactInformationCollection;
        private readonly IMapper _mapper;
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public ReportServices(IMapper mapper, IDatabaseSettings databaseSettings, RabbitMQPublisher rabbitMQPublisher)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _personCollection = database.GetCollection<Person>(databaseSettings.PersonCollectionName);
            _contactInformationCollection = database.GetCollection<ContactInformation>(databaseSettings.ContactInfromationCollectionName);
            _mapper = mapper;
            _rabbitMQPublisher = rabbitMQPublisher;

        }
        public async Task<Response<NoContent>> GetLocationStatisticsReportAsync(string reportDetailId)
        {
            var persons = await _personCollection.Find(person => true).ToListAsync();

            if (persons.Any())
            {
                foreach (var person in persons)
                {
                    var contactInformation = await _contactInformationCollection.Find<ContactInformation>(x => x.PersonId == person.Id).ToListAsync();
                    List<ContactInformation> list = new List<ContactInformation>(_mapper.Map<List<ContactInformation>>(contactInformation));
                    person.ContactInformation = list;
                }

                var groupedPersons = persons.SelectMany(person => person.ContactInformation)
                                           .GroupBy(contact => contact.Location)
                                           .Select(group => new { Location = group.Key, Count = group.Count() })
                                           .ToList();
                var reportData = new List<ReportDto>();
                foreach (var item in groupedPersons)
                {
                    reportData.Add(new ReportDto
                    {
                        ReportDetailId= reportDetailId,
                        Location = item.Location,
                        PersonCount = item.Count
                    }); ;
                }
                _rabbitMQPublisher.Publish(reportData);

            }

            return Response<NoContent>.Success(ResponseMessages.Success, 200);

        }
    }
}
