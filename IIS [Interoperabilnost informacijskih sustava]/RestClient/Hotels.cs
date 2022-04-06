using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RestClient
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/RestServis.Models")]
    public class HotelList
    {
        [DataMember(Order = 0)]
        public List<Hotels> HotelDataList { get; set; }

        public HotelList()
        {

        }
        public HotelList(List<Hotels> hotels)
        {
            HotelDataList = hotels;
        }
    }

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/RestServis.Models")]
    public class Hotels
    {
        [DataMember(Order = 0)]
        public string Name { get; set; }

        [DataMember(Order = 1)]
        public string PosName { get; set; }

        [DataMember(Order = 2)]
        public string HcomLocale { get; set; }

        public Hotels()
        {

        }

        public Hotels(string name, string posName, string hcomLocale)
        {
            Name = name;
            PosName = posName;
            HcomLocale = hcomLocale;
        }
    }
}
