using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocateParking.DTO
{
    public class HomeDTO
    {
        public String statisticId { get; set; }
        public DateTime dateTime { get; set; }
        public String userId { get; set; }
        public String userName { get; set; }
        public String parkingId { get; set; }
        public String parkingName { get; set; }
        public String parkingType { get; set; }
    }
}