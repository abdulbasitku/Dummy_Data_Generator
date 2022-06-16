using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DataGenerator.Models
{
    [DataContract]
    public class Attributes
    {
        [DataMember(Name = "Attributes")]
        public List<AttributeModel> attributes;
    }
}