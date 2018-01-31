using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test
{
    public class ApplicationUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Prop1 { get; set; }
        public int Prop2 { get; set; }
        public string Prop3 { get; set; }
        public string Prop4 { get; set; }
        public string Prop5 { get; set; }
        public string Prop6 { get; set; }
        public string Prop7 { get; set; }
        public string Prop8 { get; set; }
        public string Prop9 { get; set; } = Guid.NewGuid().ToString();
        public string Prop10 { get; set; }
        public bool Prop11 { get; set; }
        public bool Prop12 { get; set; }
        public string Prop13 { get; set; }
        public DateTimeOffset? Prop14 { get; set; }
        public bool Prop15 { get; set; }
        public int Prop16 { get; set; }
        public DateTime? Prop17 { get; set; }
    }
}
