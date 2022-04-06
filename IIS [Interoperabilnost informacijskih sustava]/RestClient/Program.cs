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

namespace RestClient
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

            Console.Clear();
            Console.WriteLine("Choose an option:\n");
            Console.WriteLine("1. XSD Validate");
            Console.WriteLine("2. RNG Validate");
            Console.WriteLine("3. Entity Filter");
            Console.WriteLine("4. Exit");
            Console.WriteLine("5. Exit");
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
                    Console.WriteLine("b. spa");
                    string response = Console.ReadLine();
                    if (response != "a" && response != "b")
                    {
                        Console.WriteLine("Wrong letter inserted, try again!");
                    }
                    if (response == "a")
                    {
                        callSoap("a");
                    }
                    else if (response == "b")
                    {
                        callSoap("b");
                    }
                    return true; ;
                case "4":
                    return false;
                case "5":
                    return false;
                case "6":
                    Task task = connectRestApiAsync();
                    return true;
                default:
                    return true;
            }
        }

        private static void createRequest(string path)
        {
            List<Hotels> hotels = new List<Hotels>();
            Hotels h = new Hotels();

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

                h.Name = name;
                h.PosName = posName;
                h.HcomLocale = hcomLocale;
                hotels.Add(h);

                Console.Write("Continue (y/n)?");
                continueOption = Console.ReadLine();

            } while (continueOption == "y");

            HotelList hotelList = new HotelList(hotels);

            DataContractSerializer serializer = new DataContractSerializer(typeof(HotelList));
            MemoryStream memoryStream = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(memoryStream);
            serializer.WriteObject(writer, hotelList);
            writer.Close();

            var dataString = Encoding.UTF8.GetString(memoryStream.ToArray());
            byte[] rowByte = Encoding.UTF8.GetBytes(dataString);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            request.Accept = "application/xml";
            request.Method = HttpMethod.Post.ToString();
            request.ContentType = "application/xml";
            Stream requestData = request.GetRequestStream();
            requestData.Write(rowByte, 0, rowByte.Length);
            requestData.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseData = response.GetResponseStream();
            DataContractSerializer deserializer = new DataContractSerializer(typeof(bool));

            bool successAdded = (bool)deserializer.ReadObject(responseData);

            if (successAdded)
            {
                Console.WriteLine("Validation was successful!");
            }

            else Console.WriteLine("Validation was successful");
        }
        private static void callSoap(string response)
        {
            HotelsServis.HotelsServisSoapClient service = new HotelsServis.HotelsServisSoapClient(HotelsServis.HotelsServisSoapClient.EndpointConfiguration.HotelsServisSoap);

            HotelList hl = new HotelList();
            List<Hotels> hotelsTemp = service.EntitySearchAsync(response).Result.Body.EntitySearchResult
                .Select(x => new Hotels
                {
                    Name = x.Name,
                    PosName = x.PosName,
                    HcomLocale = x.HcomLocale,
                })
                .ToList();

            foreach (var item in hotelsTemp)
            {
                Console.WriteLine("Name: " + item.Name + "PosName: " + item.PosName + "HcomLocale: " + item.HcomLocale);
            }
        }

        private static async Task connectRestApiAsync()
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
