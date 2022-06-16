using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DataGenerator.Responses
{
    [DataContract]
    public class RecordResponse
    {
        [DataMember(Name = "data")]
        public Dictionary<string, object> Data { get; set; }

        [DataMember(Name = "reference", EmitDefaultValue = false)]
        public List<ReferenceRecord> ReferenceRecords { get; set; }
    }
}