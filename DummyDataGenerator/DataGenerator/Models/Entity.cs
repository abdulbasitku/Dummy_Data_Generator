using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DataGenerator.Models
{
    [DataContract]
    public class Entity
    {
        [DataMember(Name = "Entity")]
        public string Name;

        [DataMember(Name = "DummyRecord")]
        public int NumberOfRecords;

        [DataMember(Name = "Attributes")]
        public List<AttributeModel> Attributes;

        [DataMember(Name = "SubEntityRef")]
        public List<Dictionary<string, object>> SubEntityRef;
    }
}