using DataGenerator.Models;
using DataGenerator.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataGenerator.DataGenerator
{
    public abstract class DataGenerate
    {
        private Entities entities;
        private Random random = new Random();

        public DataGenerate(Entities entities) {
            this.entities = entities;
        }

        public virtual EntitiesResponse Generate()
        {
            Dictionary<string, Entity> entityDict = new Dictionary<string, Entity>();
            foreach (Entity entity in entities.entities) {
                entityDict.Add(entity.Name, entity);
            }
            EntitiesResponse entitiesResponse = new EntitiesResponse();
            entitiesResponse.entities = new List<EntityResponse>();
            Dictionary<string, EntityResponse> entityResponseDict = new Dictionary<string, EntityResponse>();
            foreach (KeyValuePair<string, Entity> keyValue in entityDict)
            {
                EntityResponse entityResponse = new EntityResponse();
                entityResponse.Name = keyValue.Key;
                entityResponse.Records = new Dictionary<int, RecordResponse>();
                for (int i = 0; i < keyValue.Value.NumberOfRecords; i++)
                {
                    Dictionary<string, object> record = new Dictionary<string, object>();
                    foreach (AttributeModel attribute in keyValue.Value.Attributes)
                    {
                        if(attribute.IsSystemAttribute)
                        {
                            continue;
                        }
                        object value = null;
                        if ((attribute.DataType != "Date" || attribute.DataType != "DateTime") && attribute.DefaultValue != null)
                        {
                            value = attribute.DefaultValue;
                        }
                        else {
                            if (attribute.DataType != "Entity Lookup") {
                                value = GetRandomData(attribute, i);
                            }
                        }
                        if (attribute.DataType != "Entity Lookup")
                        {
                            record.Add(attribute.AttributeName, value);
                        }
                    }
                    entityResponse.Records.Add(i + 1, new RecordResponse() { Data = record });
                }
                entityResponseDict.Add(keyValue.Key, entityResponse);
                entitiesResponse.entities.Add(entityResponse);
            }

            foreach (EntityResponse entityResponse in entitiesResponse.entities)
            {
                Entity entity = entityDict[entityResponse.Name];
                List<AttributeModel> attributes = entity.Attributes;
                Dictionary<string, LookUpAttribute> lookUpAttributeDict = new Dictionary<string, LookUpAttribute>(); 
                foreach (AttributeModel attribute in attributes)
                {
                    if (attribute.DataType == "Entity Lookup")
                    {
                        lookUpAttributeDict.Add(attribute.AttributeName, attribute.LookUpAttribute);
                       

                    }
                }
                if (lookUpAttributeDict.Count > 0)
                {
                    int index = 0;
                    foreach (KeyValuePair<int, RecordResponse> record in entityResponse.Records)
                    {
                        record.Value.ReferenceRecords = new List<ReferenceRecord>();
                        foreach (KeyValuePair<string, LookUpAttribute> lookUpAttribute in lookUpAttributeDict)
                        {
                            EntityResponse lookUpEntityResponse = entityResponseDict[lookUpAttribute.Value.Entity];
                            ReferenceRecord referenceRecord = GetReferenceRecords(entity.SubEntityRef, lookUpEntityResponse, index, lookUpAttribute.Value.AttributeName);
                            record.Value.ReferenceRecords.Add(referenceRecord);
                        }
                        index++;
                    }
                }
            }
            return entitiesResponse;
        }

        private ReferenceRecord GetReferenceRecords(List<Dictionary<string, object>> subEntityRefList, EntityResponse entityResponse, int index, string attributeName)
        {

            string cardinality = null;
            int n = 0;
            foreach (Dictionary<string, object> subEntityRef in subEntityRefList)
            {
                bool ShowReferenceAttribute = (bool)subEntityRef["ShowReferenceAttribute"];
                if (!ShowReferenceAttribute) {
                    continue;
                }
                string subEntityRefName = (string)subEntityRef["Name"];
                string RelationAttribute = (string)subEntityRef["RelationAttribute"];

                if (subEntityRefName == entityResponse.Name && RelationAttribute == attributeName)
                {
                    cardinality = (string)subEntityRef["Cardinality"];
                    n =  Convert.ToInt32(subEntityRef["LinkedDummyRecord"]);
                }
            }
            ReferenceRecord referenceRecord = new ReferenceRecord();
            referenceRecord.SubEntity = entityResponse.Name;
            referenceRecord.RelationName = attributeName;
            referenceRecord.ReferenceRecords = new List<int>();
           
            if (cardinality == "1-1")
            {
                if (index < entityResponse.Records.Count)
                {
                    referenceRecord.ReferenceRecords.Add(index);
                }
            }
            if (cardinality == "1-*" || cardinality == "*-*")
            {
                int i = 0;
                while (i < n)
                {
                    int recordIndex = index * n + i;
                    if (recordIndex < entityResponse.Records.Count)
                    {
                        referenceRecord.ReferenceRecords.Add(recordIndex);
                    }
                    else {
                        break;
                    }
                    i++;
                }

            }

            return referenceRecord;
        }

        protected abstract object GetRandomData(AttributeModel attribute, int index);
    }
}