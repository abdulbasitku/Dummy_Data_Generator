using DataGenerator.DataGenerator;
using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataGenerator.Controllers
{
    public class DataGeneratorController : ApiController
    {
        [HttpPost]
        public object Generate([FromBody] Entities entities)
        {
            BogusDataGenerate bogusDataGenerate = new BogusDataGenerate(entities);
            return bogusDataGenerate.Generate();
        }
    }
}
