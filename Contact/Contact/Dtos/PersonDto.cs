﻿using Contact.Models;

namespace Contact.Dtos
{
    public class PersonDto : PropertyClass
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
    }
}
