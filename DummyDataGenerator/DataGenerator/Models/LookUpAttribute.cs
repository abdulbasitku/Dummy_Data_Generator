using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DataGenerator.Models
{
    [DataContract]
    public class LookUpAttribute
    {
        [DataMember(Name = "Entity")]
        public string Entity { get; set; }

        [DataMember(Name = "AttributeName")]
        public string AttributeName { get; set; }
    }
}