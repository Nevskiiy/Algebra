using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SoapServis.Models
{
    [DataContract]
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

    [DataContract]
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