
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataGenerator.Responses
{
    [DataContract]
    public class EntityResponse
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "records")]
        public Dictionary<int, RecordResponse> Records { get; set; }
    }
}