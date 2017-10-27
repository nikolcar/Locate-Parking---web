using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocateParking.DTO
{

    public class MyHub : Hub
    {
        private List<string> statistics = new List<string>();

        public void AddRows()
        {
            FirebaseClient firebase = FirebaseSingleton.getInstance().getFirebaseClient();

            var observable = firebase
              .Child("statistic")
              .OrderByKey()
              .AsObservable<Statistic>()
              .Subscribe(s =>
              {
                  if (!statistics.Contains(s.Key))
                  {
                      statistics.Add(s.Key);
                      Clients.Caller.Add(Models.HomeStatisticModels.createHomeDTO(firebase,
                    s.Object.userId, s.Object.parkingId, s.Object.dateTime));
                  }
              }); 
        }
    }
}