using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LocateParking.DTO
{
    public class FirebaseSingleton
    {
        private static FirebaseSingleton mInstance = null;

        private FirebaseClient mFirebase;

        private static object syncLock = new object();

        private FirebaseSingleton()
        {
            var auth = "idQEArqBs5FRR4z3Zf4eWFTeb3v12uEaG2Z2X12D";
            mFirebase = new FirebaseClient(
              "https://locateparking-180910.firebaseio.com/",
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(auth)
              });
        }

        public static FirebaseSingleton getInstance()
        {
            if(mInstance == null)
            {
                lock (syncLock)
                {
                    if (mInstance == null)
                    {
                        mInstance = new FirebaseSingleton();
                    }
                }
            }
            return mInstance;
        }

        public FirebaseClient getFirebaseClient()
        {
            return this.mFirebase;
        }
    }
}