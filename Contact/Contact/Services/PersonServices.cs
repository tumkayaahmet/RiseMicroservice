using AutoMapper;
using Contact.Models;
using Contact.Settings;
using MongoDB.Driver;
using Shared.Dtos;
using Shared.Messages;
using System;
using System.Collections.Generic;

namespace Contact.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly IMongoCollection<Person> _personCollection;
        private readonly IMongoCollection<ContactInformation> _contactInformationCollection;
        private readonly IMapper _mapper;
        public PersonServices(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _personCollection = database.GetCollection<Person>(databaseSettings.PersonCollectionName);
            _contactInformationCollection = database.GetCollection<ContactInformation>(databaseSettings.ContactInfromationCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<PersonDto>>> GetAllAsync()
        {
            var persons = await _personCollection.Find(person => true).ToListAsync();
            var personCount = persons.Count();
            if (persons.Any())
            {
                foreach (var person in persons) 
                {
                    var contactInformation = await _contactInformationCollection.Find<ContactInformation>(x => x.PersonId == person.Id).ToListAsync();
                    List<ContactInformation> list = new List<ContactInformation>(_mapper.Map<List<ContactInformation>>(contactInformation));
                    person.ContactInformation = list;
                }
            }
            else
            {
                persons = new List<Person>();
            }
            return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(persons),ResponseMessages.DataCount + personCount, 200);
        }
        public async Task<Response<PersonDto>> CreateAsync(PersonCreateDto personCreateDto)
        {
            var newPerson = _mapper.Map<Person>(personCreateDto);

            await _personCollection.InsertOneAsync(newPerson);
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(newPerson),ResponseMessages.PersonAdded, 200);
        }
        public async Task<Response<PersonDto>> GetByIdAsync(string id)
        {
            var person = await _personCollection.Find<Person>(x => x.Id == id).FirstOrDefaultAsync();

            if (person == null)
            {
                return Response<PersonDto>.Fail(ResponseMessages.PersonNotFound, 404);
            }
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person),ResponseMessages.Success, 200);
        }
        public async Task<Response<NoContent>> UpdateAsync(PersonUpdateDto personUpdateDto)
        {
            var updatePerson = _mapper.Map<Person>(personUpdateDto);
            updatePerson.ModifyDate = DateTime.Now;
            var result = await _personCollection.FindOneAndReplaceAsync(x => x.Id == personUpdateDto.Id, updatePerson);

            if (result == null)
            {
                return Response<NoContent>.Fail(ResponseMessages.PersonNotFound, 404);
            }
            return Response<NoContent>.Success(ResponseMessages.PersonUpdated, 204);
        }
        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _personCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(ResponseMessages.PersonDeleted, 204);
            }
            else
            {
                return Response<NoContent>.Fail(ResponseMessages.PersonNotFound, 404);
            }
        }
 
    }
}
