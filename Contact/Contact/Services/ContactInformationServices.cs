﻿using AutoMapper;
using Contact.Models;
using Contact.Settings;
using MongoDB.Driver;
using Shared.Dtos;
using Shared.Messages;

namespace Contact.Services
{
    public class ContactInformationServices : IContactInformationServices
    {
        private readonly IMongoCollection<ContactInformation> _contactInformationCollection;
        private readonly IMapper _mapper;
        public ContactInformationServices(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _contactInformationCollection = database.GetCollection<ContactInformation>(databaseSettings.ContactInfromationCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<ContactInformationDto>>> GetAllByPersonIdAsync(string personId)
        {
            var contactInformation = await _contactInformationCollection.Find<ContactInformation>(x =>  x.PersonId == personId).ToListAsync();
            var ContactInformationCount = contactInformation.Count();
            return Response<List<ContactInformationDto>>.Success(_mapper.Map<List<ContactInformationDto>>(contactInformation),ResponseMessages.DataCount + ContactInformationCount, 200);

        }
        public async Task<Response<ContactInformationDto>> CreateAsync(ContactInformationCreateDto contactInformationCreateDto)
        {
            var newContactInformation = _mapper.Map<ContactInformation>(contactInformationCreateDto);
            newContactInformation.CreateDate = DateTime.Now;
            await _contactInformationCollection.InsertOneAsync(newContactInformation);
            return Response<ContactInformationDto>.Success(_mapper.Map<ContactInformationDto>(newContactInformation),ResponseMessages.ContactInfrormationAdded, 200);
        }
        public async Task<Response<NoContent>> UpdateAsync(ContactInformationUpdateDto contactInformationUpdateDto)
        {
            var updateContactInformation = _mapper.Map<ContactInformation>(contactInformationUpdateDto);
            updateContactInformation.ModifyDate = DateTime.Now;
            var result = await _contactInformationCollection.FindOneAndReplaceAsync(x => x.Id == contactInformationUpdateDto.Id, updateContactInformation);

            if (result == null)
            {
                return Response<NoContent>.Fail(ResponseMessages.ContactInfrormationNotFound, 404);
            }
            return Response<NoContent>.Success(ResponseMessages.ContactInfrormationUpdated, 204);
        }
        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _contactInformationCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(ResponseMessages.ContactInfrormationDeleted, 204);
            }
            else
            {
                return Response<NoContent>.Fail(ResponseMessages.ContactInfrormationNotFound, 404);
            }
        }

    }
}
