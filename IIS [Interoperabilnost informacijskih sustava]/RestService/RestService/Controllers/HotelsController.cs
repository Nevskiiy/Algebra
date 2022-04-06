using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestService.Models;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Xml.Linq;
using System.Runtime.Serialization;
using Commons.Xml.Relaxng;

namespace RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private bool error = false;

        [HttpPost("validate-xsd")]
        public bool XsdValidation(XmlElement hotels)
        {
            try
            {
                XmlDocument xmlDocument = hotels.OwnerDocument;
                xmlDocument.AppendChild(hotels);

                XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
                xmlSchemaSet.Add("http://schemas.datacontract.org/2004/07/RestService.Models", Path.GetFullPath("hotels_shema.xsd"));
                XmlReader xmlReader = new XmlNodeReader(xmlDocument);
                XDocument xDocument = XDocument.Load(xmlReader);

                xDocument.Validate(xmlSchemaSet, (o, e) => 
                {
                    Console.WriteLine("{0}", e.Message);
                    error = true;
                });

                if (!error)
                {
                    DataContractSerializer deserialization = new DataContractSerializer(typeof(Hotels));
                    MemoryStream xmlStream = new MemoryStream();
                    xDocument.Save(xmlStream);
                    xmlStream.Position = 0;
                    Hotels h = (Hotels)deserialization.ReadObject(xmlStream);

                    foreach (var item in h.HotelDataList)
                    {
                        Startup.Hotels.HotelDataList.Add(item);
                    }

                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }

        [HttpPost("validate-rng")]
        public bool RngValidation(XmlElement hotels)
        {
            XmlDocument xmlDocument = hotels.OwnerDocument;
            xmlDocument.AppendChild(hotels);
            var errors = false;
            XmlReader xmlReader = new XmlNodeReader(xmlDocument);
            XmlReader rngReader = XmlReader.Create(Path.GetFullPath("hotels_rng.rng"));
            using (var reader = new RelaxngValidatingReader(xmlReader, rngReader))
            {
                reader.InvalidNodeFound += (source, message) =>
                {
                    Console.WriteLine("Error: " + message);
                    errors = true;
                    return true;
                };

                XDocument doc = XDocument.Load(reader);
            }

            if (errors)
            {
                return false;
            }

            else
            {
                DataContractSerializer deserialization = new DataContractSerializer(typeof(Hotels));
                MemoryStream xmlStream = new MemoryStream();
                xmlDocument.Save(xmlStream);
                xmlStream.Position = 0;
                Hotels h = (Hotels)deserialization.ReadObject(xmlStream);

                foreach (var item in h.HotelDataList)
                {
                    Startup.Hotels.HotelDataList.Add(item);
                }

                return true;
            }
        }
    }
}
