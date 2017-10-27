
using Firebase.Database;
using Firebase.Database.Query;
using LocateParking.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LocateParking.Models
{
    public class HomeStatisticModels
    {
    //    public List<HomeDTO> statisticList { get; set; }

    //    public static async System.Threading.Tasks.Task<HomeStatisticModels> Load(string sort = null)
    //    {
    //        var model = new HomeStatisticModels();
    //    model.statisticList = new List<HomeDTO>();

    //        FirebaseClient firebase = FirebaseSingleton.getInstance().getFirebaseClient();

    //    var statistics = await firebase
    //      .Child("statistic")
    //      .OrderByKey()
    //      .OnceAsync<Statistic>();

    //        foreach (var s in statistics)
    //        { 
    //            model.statisticList.Add(await createHomeDTO(firebase, s.Object.userId,
    //                s.Object.parkingId, s.Object.dateTime, "statistic " + s.Key));
    //        }

    //var observable = firebase
    //  .Child("statistic")
    //  .OrderByKey()
    //  .AsObservable<Statistic>()
    //  .Subscribe(async s => model.statisticList.Add(await createHomeDTO(firebase,
    //        s.Object.userId, s.Object.parkingId, s.Object.dateTime, "observer " + s.Key)));

    //        return model;
    //    }

        public static HomeDTO createHomeDTO(FirebaseClient firebase, String uId, String pId, String date)
        {
            var parking = firebase.Child("parkings").OrderByKey().StartAt(pId).LimitToFirst(1).OnceAsync<Parking>();
            Parking p = parking.Result.First().Object;

            var user = firebase.Child("users").OrderByKey().StartAt(uId).LimitToFirst(1).OnceAsync<DTO.User>();
            DTO.User u = user.Result.First().Object;

            //Debug.WriteLine("CREATE HOME DTO "+ messege);

            return new HomeDTO
            {
                dateTime = DateTime.Parse(date),
                userId = uId,
                userName = u.firstName + " " + u.lastName + "\n" + u.nickname,
                parkingId = pId,
                parkingName = p.name,
                parkingType = p.secret == "true" ? "Private" : "Public"
            };
        }
    }

}
