using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Sockets;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            //Console.Clear();
            Console.WriteLine("Choose an option:\n");
            Console.WriteLine("1. XSD Validate");
            Console.WriteLine("2. RNG Validate");
            Console.WriteLine("3. Entity Filter");
            Console.WriteLine("4. JAXB");
            Console.WriteLine("5. XML-RPC");
            Console.WriteLine("6. RestApi_Connection");
            Console.Write("\r\nSelect an option: "); 
            
            string xsdValidation = "http://localhost:5000/api/Hotels/validate-xsd/";
            string rngValidation = "http://localhost:5000/api/Hotels/validate-rng/";

            switch (Console.ReadLine())
            {
                case "1":
                    createRequest(xsdValidation);
                    return true;
                case "2":
                    createRequest(rngValidation);
                    return true;
                case "3":
                    Console.WriteLine("Choose hotels location native language:");
                    Console.WriteLine("a. eng");
                    Console.WriteLine("b. cro");
                    string response = Console.ReadLine();
                    if (response != "a" && response != "b")
                    {
                        Console.WriteLine("Wrong letter inserted, try again!");
                    }
                    if (response == "a")
                    {
                        callSoap("eng");
                    }
                    else if (response == "b")
                    {
                        callSoap("cro");
                    }
                    return true;
                case "4":
                    callJava();
                    return true;
                case "5":
                    callXmlRpc();
                    return true;
                case "6":
                    connectRestApi();
                    return true;
                default:
                    return true;
            }
        }

        private static void callXmlRpc()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(@"C:\Users\nivan\Desktop\Algebra\Projects\NevenIvanisic_IIS.21\ClientApplication\ClientApplication\tempData.xml");
            xmlDocument.DocumentElement.ChildNodes[0].InnerText = "Dhmz.GetTemperature";
            xmlDocument.DocumentElement.ChildNodes[1].ChildNodes[0].ChildNodes[0].InnerText = "Zadar";
            MemoryStream xmlStream = new MemoryStream();
            xmlDocument.Save(xmlStream);

            byte[] data = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(xmlStream.ToArray()));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:1132");
            request.Method = "POST";
            request.Accept = "application/xml";
            Stream requestData = request.GetRequestStream();
            requestData.Write(data, 0, data.Length);
            requestData.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataResponse = response.GetResponseStream();

            XmlDocument documentResponse = new XmlDocument();
            documentResponse.Load(dataResponse);
            Console.WriteLine("Temperature in Zadar is: " + documentResponse.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText + " degrees.");
        }

        private static void callJava()
        {
            TcpClient client = new TcpClient("localhost", 1133);
            string path = @"C:\Users\nivan\Desktop\Algebra\Projects\NevenIvanisic_IIS.21\hotels_soap.xml";
            byte[] buffer;
            buffer = Encoding.UTF8.GetBytes(path + "\n");

            NetworkStream stream = client.GetStream();
            stream.Write(buffer, 0, path.Length + 1);

            buffer = new byte[100];
            stream.Read(buffer, 0, 100);
            string response = Encoding.UTF8.GetString(buffer);
            Console.WriteLine(response);
        }

        private static void callSoap(string response)
        {
            HotelService.HotelServisSoapClient servis = new HotelService.HotelServisSoapClient(HotelService.HotelServisSoapClient.EndpointConfiguration.HotelServisSoap);
            Hotels hotels = new Hotels();
            List<HotelData> hotelData = servis.EntitySearchAsync(response).Result.Body.EntitySearchResult
            .Select(h => new HotelData
            {
                Name = h.Name,
                PosName = h.PosName,
                HcomLocale = h.HcomLocale
            }).ToList();



            foreach (var item in hotelData)
            {
                Console.WriteLine(" Name: " + item.Name + " \n" + " Pos_name: " + item.PosName + "\n" + " HComLocale: " + item.HcomLocale);
            }
        }

        private static void createRequest(string path)
        {
            List<HotelData> hotelData = new List<HotelData>();
            HotelData hd = new HotelData();

            string name;
            string posName;
            string hcomLocale; 
            string continueOption;

            do
            {
                Console.Write("Insert the name of hotel location - country: ");
                name = Console.ReadLine();

                Console.Write("Insert the name of hotel location - position: ");
                posName = Console.ReadLine();

                Console.Write("Insert the name of hotel location - location: ");
                hcomLocale = Console.ReadLine();

                hd.Name = name;
                hd.PosName = posName;
                hd.HcomLocale = hcomLocale;
                hotelData.Add(hd);

                Console.Write("Continue (y/n)?");
                continueOption = Console.ReadLine();

            } while (continueOption == "y");

            Hotels hotels = new Hotels(hotelData);

            DataContractSerializer serializer = new DataContractSerializer(typeof(Hotels));
            MemoryStream data = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(data);
            serializer.WriteObject(writer, hotels);
            writer.Close();

            var dataString = Encoding.UTF8.GetString(data.ToArray());
            byte[] rowByte = Encoding.UTF8.GetBytes(dataString);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            request.Accept = "application/xml";
            request.Method = HttpMethod.Post.ToString();
            request.ContentType = "application/xml";
            Stream dataRequested = request.GetRequestStream();
            dataRequested.Write(rowByte, 0, rowByte.Length);
            dataRequested.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataResponse = response.GetResponseStream();
            DataContractSerializer deserialization = new DataContractSerializer(typeof(bool));
            bool successAdded = (bool)deserialization.ReadObject(dataResponse);

            if (successAdded)
            {
                Console.WriteLine("Validation was successful!");
            }

            else Console.WriteLine("Validation wasn't successful");
        }

        private static async void connectRestApi()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://hotels4.p.rapidapi.com/get-meta-data"),
                    Headers =
                    {
                        { "x-rapidapi-host", "hotels4.p.rapidapi.com" },
                        { "x-rapidapi-key", "020ad4b83cmsh09b5252dfaed2cfp1dde5djsnb927dd17746d" },
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("We had a problem: " + ex.Message.ToString());
            }
        }
    }
}
