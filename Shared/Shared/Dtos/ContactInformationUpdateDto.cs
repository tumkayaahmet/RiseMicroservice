﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ContactInformationUpdateDto : BaseClass
    {
        public string Id { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string InformationContent { get; set; }
        public string PersonId { get; set; }

    }
}
