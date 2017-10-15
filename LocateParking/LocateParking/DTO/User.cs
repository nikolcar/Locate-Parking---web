using System;
using System.Collections.Generic;

namespace LocateParking.DTO
{
    internal class User
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String nickname { get; set; }
        public String dateOfBirth { get; set; }

        public String longitude { get; set; }
        public String latitude { get; set; }

        public String points { get; set; }
        public List<String> friends { get; set; }

        public String gpsrefresh { get; set; }
        public String showfriends { get; set; }
        public String showplayers { get; set; }
        public String workback { get; set; }
    }
}