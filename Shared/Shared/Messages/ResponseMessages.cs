﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public static class ResponseMessages
    {
        public const string PersonNotFound = "Person not found";
        public const string PersonAdded = "Person added";
        public const string PersonUpdated = "Person updated";
        public const string PersonDeleted = "Person deleted";

        public const string ContactInfrormationNotFound = "Contact information not found";
        public const string ContactInfrormationAdded = "Contact information added";
        public const string ContactInfrormationUpdated = "Contact information updated";
        public const string ContactInfrormationDeleted = "Contact information deleted";

        public const string ReportDetailPreparing = "Report is Preparing";
        public const string ReportDetailCompleted = "Report is Completed";
        public const string ReportDetailNotFound = "Related report not found";

        public const string DataCount = "Data Count : ";
        public const string Success = "Success";

    }

}
