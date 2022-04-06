using SoapServis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace SoapServis
{
    /// <summary>
    /// Summary description for HotelServis
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class HotelServis : System.Web.Services.WebService
    {
        public HotelServis()
        {

        }

        Hotels h = new Hotels();
        List<HotelData> hotelData = new List<HotelData>();
        HotelData h1 = new HotelData("CHAD", "HCOM_AFR", "afr");
        HotelData h2 = new HotelData("BIH", "HCOM_CRO", "cro");
        HotelData h3 = new HotelData("WALES", "HCOM_ENG", "eng");
        HotelData h4 = new HotelData("SCOTLAND", "HCOM_SCO", "eng");

        [WebMethod]
        public List<HotelData> EntitySearch(string input)
        {
            hotelData.Add(h1);
            hotelData.Add(h2);
            hotelData.Add(h3);
            hotelData.Add(h4);

            h.HotelDataList = hotelData;

            DataContractSerializer serializer = new DataContractSerializer(typeof(Hotels));
            string filePath = @"C:\Users\nivan\Desktop\Algebra\Projects\NevenIvanisic_IIS.21\hotels_soap.xml";

            using (FileStream writer = new FileStream(filePath, FileMode.Create))
            {
                serializer.WriteObject(writer, h);
                writer.Close();
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);
            XmlNode root = xmlDocument.DocumentElement;

            XmlNamespaceManager xnm = new XmlNamespaceManager(xmlDocument.NameTable);
            xnm.AddNamespace("ns", "http://schemas.datacontract.org/2004/07/SoapServis.Models");

            XmlNodeList xnl = root.SelectNodes("/ns:Hotels/ns:HotelDataList/ns:HotelData[ns:HcomLocale='" + input+"']", xnm);

            List<HotelData> result = new List<HotelData>();
            for (int i = 0; i < xnl.Count; i++)
            {
                var currentNodeXml = "<HotelData xmlns=\"http://schemas.datacontract.org/2004/07/SoapServis.Models\">" + xnl[i].InnerXml + "</HotelData>";
                Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(currentNodeXml));

                DataContractSerializer ds = new DataContractSerializer(typeof(HotelData));
                result.Add((HotelData)ds.ReadObject(stream));
            }

            return result;
        }
    }
}