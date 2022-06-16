using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DataGenerator.Responses
{
    [DataContract]
    public class EntitiesResponse
    {
        [DataMember(Name = "Entities")]
        public List<EntityResponse> entities;
    }
}