﻿using Contact.Dtos;
using Contact.Models;
using Shared.Dtos;

namespace Contact.Services
{
    internal interface IPersonServices
    {
        Task<Response<List<PersonDto>>> GetAllAsync();
        Task<Response<PersonDto>> CreatePersonAsync(Person person);
        Task<Response<PersonDto>> GetByIdAsync(string id);

    }
}