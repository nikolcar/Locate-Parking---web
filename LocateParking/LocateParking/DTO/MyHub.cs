using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocateParking.Models;
using LocateParking.Controllers;

namespace LocateParking.DTO
{

    public class MyHub : Hub
    {
        private List<string> statistics = new List<string>();
        
        public void AddRows()
        {
            //statistics.Add("-Ky4Ys353CaI5bKk9eYm");
            FirebaseClient firebase = FirebaseSingleton.getInstance().getFirebaseClient();

            var observable = firebase
              .Child("statistic")
              .OrderByKey()
              .AsObservable<Statistic>()
              .Subscribe(async s =>
              {
                  //if (!HomeController.model.statisticList.Where(x=>x.statisticId == s.Key).Any())
                  if(!statistics.Contains(s.Key))
                  {
                      statistics.Add(s.Key);
                      HomeDTO row = await HomeStatisticModels.createHomeDTO(firebase, s.Object.userId, s.Object.parkingId, s.Object.dateTime, s.Key);
                      Clients.Caller.Add(row);
                      Clients.Caller.Sort();
                      //HomeController.model.statisticList.Add(row);  
                  }
              });
        }
    }
}