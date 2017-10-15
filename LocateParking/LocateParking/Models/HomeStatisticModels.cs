using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using LocateParking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LocateParking.Models
{
    public class HomeStatisticModels
    {
        public List<HomeDTO> statisticList { get; set; }

        public static async System.Threading.Tasks.Task<HomeStatisticModels> Load(string sort = null)
        {
            var model = new HomeStatisticModels();
            model.statisticList = new List<HomeDTO>();

            FirebaseClient firebase = FirebaseSingleton.getInstance().getFirebaseClient();

            var statistics = await firebase
              .Child("statistic")
              .OrderByKey()
              .OnceAsync<Statistic>();

            foreach (var s in statistics)
            {
                var parking = await firebase.Child("parkings").OrderByKey().StartAt(s.Object.parkingId).LimitToFirst(1).OnceAsync<Parking>();
                Parking p = parking.First().Object;

                var user = await firebase.Child("users").OrderByKey().StartAt(s.Object.userId).LimitToFirst(1).OnceAsync<DTO.User>();
                DTO.User u = user.First().Object;

                model.statisticList.Add(new HomeDTO
                {
                    dateTime = DateTime.Parse(s.Object.dateTime),
                    userId = s.Object.userId,
                    userName = u.firstName + " " + u.lastName + "\n" + u.nickname,
                    parkingId = s.Object.parkingId,
                    parkingName = p.name,
                    parkingType = p.secret == "true" ? "Private" : "Public"
                });                
            }

            return model;

        //    var statistics = firebase
        //      .Child("statistic")
        //      .OrderByKey()
        //      .AsObservable<Statistic>()
        //      .Subscribe(async d => model.statisticList.Add(await createHomeDTO(firebase, d.Object.userId, d.Object.parkingId, d.Object.dateTime)));

        //    return model;
        //}

        //private static async Task<HomeDTO> createHomeDTO(FirebaseClient firebase, String uId, String pId, String date)
        //{
        //    var parking = await firebase.Child("parkings").OrderByKey().StartAt(pId).LimitToFirst(1).OnceAsync<Parking>();
        //    Parking p = parking.First().Object;

        //    var user = await firebase.Child("users").OrderByKey().StartAt(uId).LimitToFirst(1).OnceAsync<DTO.User>();
        //    DTO.User u = user.First().Object;

        //    return new HomeDTO
        //    {
        //        dateTime = DateTime.Parse(date),
        //        userId = uId,
        //        userName = u.firstName + " " + u.lastName + "\n" + u.nickname,
        //        parkingId = pId,
        //        parkingName = p.name,
        //        parkingType = p.secret == "true" ? "Private" : "Public"
        //    };
        //}
    }

    }
}