using RandomDataGenerator.FieldOptions;
using DataGenerator.Models;
using RandomDataGenerator.Randomizers;
using System;
using System.Globalization;
using Bogus;
using System.Collections.Generic;
using Bogus.Healthcare.DataSets;
using Bogus.Locations.DataSets;
using Bogus.Hollywood.DataSets;
using Bogus.Premium;

namespace DataGenerator.DataGenerator
{
    public class BogusDataGenerate : DataGenerate
    {
        Faker faker = new Faker();
        Human human = new Human();
        Location location = new Location();
        Movies movies = new Movies();
        Icd10 icd10 = new Icd10();

        public BogusDataGenerate(Entities entities) : base(entities)
        {
           
        }
        private string GetString(AttributeModel attribute, int index)
        {
            if (attribute.Max != null && Convert.ToInt32(attribute.Max) == 1)
            {
                return faker.Lorem.Letter();
            }
            string number = Convert.ToString(index + 1);

            if (attribute.Min == null && attribute.Max != null)
            {
                if (number.Length >= Convert.ToInt32(attribute.Max))
                {
                    return faker.Lorem.Letter();
                }
                if (number.Length + attribute.Label.Length < Convert.ToInt32(attribute.Max))
                {
                    return attribute.Label + number;
                }
                else
                {
                    return attribute.Label.Substring(0, Convert.ToInt32(attribute.Max) - number.Length) + number;
                }

            }
            if (attribute.Max == null && attribute.Min != null)
            {
                if (attribute.Label.Length + number.Length < Convert.ToInt32(attribute.Min))
                {
                    int len = Convert.ToInt32(attribute.Min) - (attribute.Label.Length + number.Length);
                    return attribute.Label + faker.Random.String2(len) + number;
                }
                return attribute.Label + number;

            }
            if (attribute.Max != null && attribute.Min != null)
            {
                if (number.Length >= Convert.ToInt32(attribute.Max))
                {
                    return faker.Lorem.Letter();
                }
                string word = attribute.Label + number;
                if (attribute.Label.Length + number.Length < Convert.ToInt32(attribute.Min))
                {
                    int len = Convert.ToInt32(attribute.Min) - (attribute.Label.Length + number.Length);
                    word = attribute.Label + faker.Random.String2(len) + number;
                }
                if (word.Length < Convert.ToInt32(attribute.Max))
                {
                    return word;
                }
                word = attribute.Label + faker.Random.String2(Convert.ToInt32(attribute.Max));
                return word.Substring(0, word.Length - number.Length) + number;

            }
            return attribute.Label + (index + 1);
        }
        protected override object GetRandomData(AttributeModel attribute, int index)
        {


            object data = GetFromLabel(faker, attribute.BogusLabel);
            if (data != null)
            {
                return data;
            }

            string dataType = attribute.DataType;

            if (dataType == "String")
            {
                if (attribute.Pattern != null && attribute.Pattern != string.Empty)
                {
                    var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = attribute.Pattern });
                    return randomizerTextRegex.Generate();
                }
                return GetString(attribute, index);
            }

            if (dataType == "Integer")
            {
                if (attribute.Min != null && attribute.Max != null)
                {
                    return faker.Random.Number(Convert.ToInt32(attribute.Min), Convert.ToInt32(attribute.Max));
                }
                return faker.Random.Number();
            }

            if (dataType == "URL")
            {
                return faker.Internet.Url();
            }

            if (dataType == "Multiline Text")
            {
                return faker.Lorem.Lines();
            }

            if (dataType == "Email")
            {
                return faker.Internet.Email();
            }

            if (dataType == "Tags")
            {
                return faker.Lorem.Word();
            }

            if (dataType == "Picklist String")
            {
                return faker.PickRandom(attribute.Picklist);
            }

            if (dataType == "Date")
            {
                DateTime date = faker.Date.Between(new DateTime(2000, 1, 1), new DateTime(2023, 12, 31));
                SetDefaultDate(attribute, date);
            }

            if (dataType == "DateTime")
            {
                DateTime dateTime = faker.Date.Between(attribute.MinTime, attribute.MaxTime);
                SetDefaultDate(attribute, dateTime);

            }

            if (dataType == "Time")
            {
                string min = Convert.ToString(attribute.Min);
                string max = Convert.ToString(attribute.Max);
                DateTime minTime = DateTime.ParseExact("1/1/2000 " + min, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime maxTime = DateTime.ParseExact("1/1/2000 " + max, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime dateTime = faker.Date.Between(minTime, maxTime);
                return dateTime.ToString("h:mm:ss tt", CultureInfo.InvariantCulture);
            }

            if (dataType == "Boolean")
            {
                return faker.Random.Bool();
            }

            return null;
        }

        private DateTime SetDefaultDate(AttributeModel attribute, DateTime value)
        {
            if (!attribute.SetDefaultDate)
            {
                return value;
            }

            Dictionary<string, object> dict = (Dictionary<string, object>)attribute.DefaultValue;
            int number = (int)dict["Number"];

            if (dict["Sign"].ToString() == "-")
            {
                number = -1 * number;
            }
            string type = dict["Type"].ToString();
            if (type == "Days")
            {
                return value.AddDays(number);
            }
            if (type == "Weeks")
            {
                return value.AddDays(number * 7);
            }
            if (type == "Months")
            {
                return value.AddMonths(number);
            }
            if (type == "Years")
            {
                return value.AddYears(number);
            }
            return value;
        }

        private object GetFromLabel(Faker faker, string label)
        {
            if (label == "" || label == null)
            {
                return null;
            }
            if ("ZipCode" == label)
            {
                return faker.Address.ZipCode();
            }
            if ("Weekday" == label)
            {
                return faker.Date.Weekday();
            }
            if ("Vin" == label)
            {
                return faker.Vehicle.Vin();
            }
            if ("Lines" == label)
            {
                return faker.Lorem.Lines();
            }
            if ("Type" == label)
            {
                return faker.Vehicle.Type();
            }
            if ("UserNameUnicode" == label)
            {
                return faker.Internet.UserNameUnicode();
            }
            if ("UserName" == label)
            {
                return faker.Internet.UserName();
            }
            if ("UrlWithPath" == label)
            {
                return faker.Internet.UrlWithPath();
            }
            if ("UrlRootedPath" == label)
            {
                return faker.Internet.UrlRootedPath();
            }
            if ("Url" == label)
            {
                return faker.Internet.Url();
            }
            if ("TransactionType" == label)
            {
                return faker.Finance.TransactionType();
            }
            if ("Suffix" == label)
            {
                return faker.Name.Suffix();
            }
            if ("StreetSuffix" == label)
            {
                return faker.Address.StreetSuffix();
            }
            if ("StreetName" == label)
            {
                return faker.Address.StreetName();
            }
            if ("StreetAddress" == label)
            {
                return faker.Address.StreetAddress();
            }
            if ("StateAbbr" == label)
            {
                return faker.Address.StateAbbr();
            }
            if ("State" == label)
            {
                return faker.Address.State();
            }
            if ("SecondaryAddress" == label)
            {
                return faker.Address.SecondaryAddress();
            }
            if ("RoutingNumber" == label)
            {
                return faker.Finance.RoutingNumber();
            }
            if ("Review" == label)
            {
                return faker.Rant.Review();
            }
            if ("ProductName" == label)
            {
                return faker.Commerce.ProductName();
            }
            if ("ProductMaterial" == label)
            {
                return faker.Commerce.ProductMaterial();
            }
            if ("ProductAdjective" == label)
            {
                return faker.Commerce.ProductAdjective();
            }
            if ("Product" == label)
            {
                return faker.Commerce.Product();
            }
            if ("Price" == label)
            {
                return faker.Commerce.Price();
            }
            if ("Prefix" == label)
            {
                return faker.Name.Prefix();
            }
            if ("PhoneNumber" == label)
            {
                return faker.Phone.PhoneNumber();
            }
            if ("OrdinalDirection" == label)
            {
                return faker.Address.OrdinalDirection();
            }
            if ("Number" == label)
            {
                return faker.Random.Number();
            }
            if ("Nature" == label)
            {
                return faker.Image.Nature();
            }
            if ("Month" == label)
            {
                return faker.Date.Month();
            }
            if ("Model" == label)
            {
                return faker.Vehicle.Model();
            }
            if ("Manufacturer" == label)
            {
                return faker.Vehicle.Manufacturer();
            }
            if ("Longitude" == label)
            {
                return faker.Address.Longitude();
            }
            if ("Latitude" == label)
            {
                return faker.Address.Latitude();
            }
            if ("LastName" == label)
            {
                return faker.Name.LastName();
            }
            if ("JobType" == label)
            {
                return faker.Name.JobType();
            }
            if ("JobTitle" == label)
            {
                return faker.Name.JobTitle();
            }
            if ("JobDescriptor" == label)
            {
                return faker.Name.JobDescriptor();
            }
            if ("JobArea" == label)
            {
                return faker.Name.JobArea();
            }
            if ("Ipv6EndPoint" == label)
            {
                return faker.Internet.Ipv6EndPoint();
            }
            if ("Ipv6Address" == label)
            {
                return faker.Internet.Ipv6Address();
            }
            if ("Ipv6" == label)
            {
                return faker.Internet.Ipv6();
            }
            if ("IpEndPoint" == label)
            {
                return faker.Internet.IpEndPoint();
            }
            if ("IpAddress" == label)
            {
                return faker.Internet.IpAddress();
            }
            if ("Iban" == label)
            {
                return faker.Finance.Iban();
            }
            if ("FullName" == label)
            {
                return faker.Name.FullName();
            }
            if ("FullAddress" == label)
            {
                return faker.Address.FullAddress();
            }
            if ("Fuel" == label)
            {
                return faker.Vehicle.Fuel();
            }
            if ("FirstName" == label)
            {
                return faker.Name.FirstName();
            }
            if ("ExampleEmail" == label)
            {
                return faker.Internet.ExampleEmail();
            }
            if ("EthereumAddress" == label)
            {
                return faker.Finance.EthereumAddress();
            }
            if ("Email" == label)
            {
                return faker.Internet.Email();
            }
            if ("DomainWord" == label)
            {
                return faker.Internet.DomainWord();
            }
            if ("DomainSuffix" == label)
            {
                return faker.Internet.DomainSuffix();
            }
            if ("DomainName" == label)
            {
                return faker.Internet.DomainName();
            }
            if ("Direction" == label)
            {
                return faker.Address.Direction();
            }
            if ("Digits" == label)
            {
                return faker.Random.Digits(1);
            }
            if ("Department" == label)
            {
                return faker.Commerce.Department();
            }
            if ("Currency" == label)
            {
                return faker.Finance.Currency();
            }
            if ("CreditCardNumber" == label)
            {
                return faker.Finance.CreditCardNumber();
            }
            if ("CreditCardCvv" == label)
            {
                return faker.Finance.CreditCardCvv();
            }
            if ("County" == label)
            {
                return faker.Address.County();
            }
            if ("CountryCode" == label)
            {
                return faker.Address.CountryCode();
            }
            if ("CompanySuffix" == label)
            {
                return faker.Company.CompanySuffix();
            }
            if ("CompanyName" == label)
            {
                return faker.Company.CompanyName();
            }
            if ("Color" == label)
            {
                return faker.Internet.Color();
            }
            if ("CitySuffix" == label)
            {
                return faker.Address.CitySuffix();
            }
            if ("CityPrefix" == label)
            {
                return faker.Address.CityPrefix();
            }
            if ("City" == label)
            {
                return faker.Address.City();
            }
            if ("Categories" == label)
            {
                return faker.Commerce.Categories(1);
            }
            if ("CardinalDirection" == label)
            {
                return faker.Address.CardinalDirection();
            }
            if ("BuildingNumber" == label)
            {
                return faker.Address.BuildingNumber();
            }
            if ("BitcoinAddress " == label)
            {
                return faker.Finance.BitcoinAddress();
            }
            if ("Bic" == label)
            {
                return faker.Finance.Bic();
            }
            if ("AccountName" == label)
            {
                return faker.Finance.AccountName();
            }
            if ("Account" == label)
            {
                return faker.Finance.Account();
            }
            if ("Abbreviation" == label)
            {
                return faker.Hacker.Abbreviation();
            }
            if (label == "BodySystem")
            {
                return human.BodySystem();
            }
            if (label == "BodyRegion")
            {
                return human.BodyRegion();
            }
            if (label == "BodyPartInternal")
            {
                return human.BodyPartInternal();
            }
            if (label == "BodyPartExternal")
            {
                return human.BodySystem();
            }
            if (label == "BloodType")
            {
                return human.BloodType();
            }
            if (label == "AreaCircle")
            {
                return location.AreaCircle(1, 1, 100);
            }
            if (label == "Altitude")
            {
                return location.Altitude();
            }
            if (label == "ActorName")
            {
                return movies.ActorName();
            }
            if (label == "ProcedureShortDescription")
            {
                return icd10.ProcedureShortDescription();
            }
            if (label == "ProcedureLongDescription")
            {
                return icd10.ProcedureLongDescription();
            }
            if (label == "ProcedureEntry")
            {
                return icd10.ProcedureEntry();
            }
            if (label == "ProcedureCode")
            {
                return icd10.ProcedureCode();
            }
            if (label == "DiagnosisShortDescription")
            {
                return icd10.DiagnosisShortDescription();
            }
            if (label == "DiagnosisLongDescription")
            {
                return icd10.DiagnosisLongDescription();
            }
            if (label == "DiagnosisEntry")
            {
                return icd10.DiagnosisEntry();
            }
            if (label == "DiagnosisCode")
            {
                return icd10.DiagnosisCode();
            }
            return null;
        }

    }
}