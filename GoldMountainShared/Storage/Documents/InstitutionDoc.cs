﻿using System;
using System.Collections.Generic;
using GoldMountainShared.Dto;
using GoldMountainShared.Storage.Converters;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace GoldMountainShared.Storage.Documents
{
    public class InstitutionDoc
    {
        [BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId InternalId { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public IEnumerable<String> Credentials { get; set; }
        public Boolean IsSupported { get; set; }
        public InstitutionType Type { get; set; }

        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
