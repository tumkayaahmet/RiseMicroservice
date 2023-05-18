﻿using Shared.Dtos;

namespace Report.Models
{
    public class ContactInformation : BaseClass
    { 
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string InformationContent { get; set; } 
        public string PersonId { get; set; }


    }
}
