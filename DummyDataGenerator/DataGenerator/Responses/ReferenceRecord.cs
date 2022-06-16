using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DataGenerator.Responses
{
    [DataContract]
    public class ReferenceRecord
    {
        [DataMember(Name = "subEntity")]
        public string SubEntity { get; set; }

        [DataMember(Name = "relationName")]
        public string RelationName { get; set; }

        [DataMember(Name = "referenceRecords", EmitDefaultValue = false)]
        public List<int> ReferenceRecords { get; set; }
    }
}