using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/RestService.Models")]
    public class Hotels
    {
        [DataMember(Order = 0)]
        public List<HotelData> HotelDataList { get; set; }
        public Hotels()
        {

        }

        public Hotels(List<HotelData> hotelDataList)
        {
            HotelDataList = hotelDataList;
        }
    }

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/RestService.Models")]
    public class HotelData
    {
        [DataMember(Order = 0)]
        public string Name { get; set; }

        [DataMember(Order = 1)]
        public string PosName { get; set; }

        [DataMember(Order = 2)]
        public string HcomLocale { get; set; }

        public HotelData(string name, string posName, string hcomLocale)
        {
            Name = name;
            PosName = posName;
            HcomLocale = hcomLocale;
        }

        public HotelData()
        {

        }
    }
}
