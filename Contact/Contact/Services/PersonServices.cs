using AutoMapper;
using Contact.Dtos;
using Contact.Models;
using Contact.Settings;
using MongoDB.Driver;
using Shared.Dtos;
using Shared.Messages;
using System.Diagnostics.Eventing.Reader;

namespace Contact.Services
{
    internal class PersonServices : IPersonServices
    {
        private readonly IMongoCollection<Person> _personCollection;
        private readonly IMongoCollection<ContactInformation> _contactInformationCollection;

        private readonly IMapper _mapper;

        public PersonServices(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _personCollection = database.GetCollection<Person>(databaseSettings.PersonCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<PersonDto>>> GetAllAsync()
        {
            var persons = await _personCollection.Find(person => true).ToListAsync();

            if (persons.Any())
            {
                foreach (var person in persons)
                {
                    person.contactInformation = await _contactInformationCollection.Find<ContactInformation>(x => x.PersonId == person.Id).FirstAsync();
                }
            }
            else
            {
                persons = new List<Person>();
            }
            return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(persons), 200);
        }

        public async Task<Response<PersonDto>> CreatePersonAsync(Person person)
        {
            await _personCollection.InsertOneAsync(person);
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person),ResponseMessages.PersonAdded, 200);
        }

        public async Task<Response<PersonDto>> GetByIdAsync(string id)
        {
            var person = await _personCollection.Find<Person>(x => x.Id == id).FirstOrDefaultAsync();

            if (person == null)
            {
                return Response<PersonDto>.Fail("Person not found", 404);
            }
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person), 200);
        }




    }
}
