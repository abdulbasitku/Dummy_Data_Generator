using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DataGenerator.Models
{
    [DataContract]
    public class AttributeModel
    {
        public string AttributeId { get; set; }

        [DataMember(Name = "AttributeName")]
        public string AttributeName { get; set; }

        [DataMember(Name = "IsSystemAttribute")]
        public bool IsSystemAttribute { get; set; }

        public int Order { get; set; }

        [DataMember(Name = "DataType")]
        public string DataType { get; set; }

        [DataMember(Name = "BogusLabel")]
        public string BogusLabel { get; set; }

        public string Description { get; set; }

        public string ToolTip { get; set; }

        public string Group { get; set; }

        [DataMember(Name = "Label")]
        public string Label { get; set; }

        [DataMember(Name = "Pattern")]
        public string Pattern { get; set; }

        [DataMember(Name = "Picklist")]
        public string[] Picklist { get; set; }

        [DataMember(Name = "Unique")]
        public bool Unique { get; set; }

        [DataMember(Name = "LookUpAttribute")]
        public LookUpAttribute LookUpAttribute { get; set; }

        public bool Inherited { get; set; }

        [DataMember(Name = "DefaultValue")]
        public object DefaultValue { get; set; }

        [DataMember(Name = "SetDefaultDate")]
        public bool SetDefaultDate { get; set; }

        public bool Required { get; set; }

        [DataMember(Name = "ReadOnly")]
        public bool ReadOnly { get; set; }

        [DataMember(Name = "Min")]
        public object Min { get; set; }

        [DataMember(Name = "Max")]
        public object Max { get; set; }

        [DataMember(Name = "MaxTime")]
        public DateTime MaxTime { get; set; }

        [DataMember(Name = "MinTime")]
        public DateTime MinTime { get; set; }
    }
}