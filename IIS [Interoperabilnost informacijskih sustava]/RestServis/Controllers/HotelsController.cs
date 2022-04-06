using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestServis.Models;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Runtime.Serialization;
using Commons.Xml.Relaxng;

namespace RestServis.Controllers
{

    // http://localhost:5000/api/Hotels

    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private bool error = false;

        // http://localhost:5000/api/Hotels/validate-xsd
        [HttpPost("validate-xsd")]
        public bool XsdValidation(XmlElement hotels)
        {
            try
            {
                XmlDocument xmlDocument = hotels.OwnerDocument;
                xmlDocument.AppendChild(hotels);

                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add("http://schemas.datacontract.org/2004/07/RestServis.Models", Path.GetFullPath("hotels_shema.xsd"));
                XmlReader xmlReader = new XmlNodeReader(xmlDocument);
                XDocument document = XDocument.Load(xmlReader);

                document.Validate(schema, (o, e) => 
                {
                    Console.WriteLine("{0}", e.Message);
                    error = true;
                });

                //xmlDocument.Schemas.Add("http://schemas.datacontract.org/2004/07/RestServis.Models", "hotel_shema.xsd");
                //ValidationEventHandler validation = new ValidationEventHandler(XmlValidation);
                //xmlDocument.Validate(validation);

                if (!error)
                {
                    DataContractSerializer deserialization = new DataContractSerializer(typeof(HotelList));
                    MemoryStream xmlStream = new MemoryStream();
                    document.Save(xmlStream);
                    xmlStream.Position = 0;
                    HotelList hl = (HotelList)deserialization.ReadObject(xmlStream);

                    foreach (var item in hl.HotelDataList)
                    {
                        Startup.HotelList.HotelDataList.Add(item);
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
            var error = false;
            XmlReader xml = new XmlNodeReader(xmlDocument);
            XmlReader rng = XmlReader.Create(Path.GetFullPath("hotels_rng.rng"));
            using (var reader = new RelaxngValidatingReader(xml, rng))
            {
                reader.InvalidNodeFound += (source, message) =>
                {
                    Console.WriteLine("Error: " + message);
                    error = true;
                    return true;
                };

                XDocument doc = XDocument.Load(reader);
            }

            if (error)
            {
                return false;
            }

            else
            {
                DataContractSerializer deserialization = new DataContractSerializer(typeof(HotelList));
                MemoryStream xmlStream = new MemoryStream();
                xmlDocument.Save(xmlStream);
                xmlStream.Position = 0;
                HotelList hl = (HotelList)deserialization.ReadObject(xmlStream);

                foreach (var item in hl.HotelDataList)
                {
                    Startup.HotelList.HotelDataList.Add(item);
                }

                return true;
            }
        }

        private void XmlValidation(object sender, ValidationEventArgs e)
        {
            error = true;
        }
    }
}
