using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Shared.Dtos;

namespace Report.Models
{
    public class ReportDetail : BaseClass
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportStatus { get; set; }
        public string ReportPath { get; set; }
    }
}
 
