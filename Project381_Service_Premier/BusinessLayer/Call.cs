using System;
using System.Collections.Generic;
using System.Text;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{
    class Call
    {
        private DateTime callDate;
        private string callDuration;
        private string clientID;




        public Call()
        {

        }

        public Call(DateTime callDate, string callDuration, string clientID)
        {
            this.callDate = callDate;
            this.callDuration = callDuration;
            this.clientID = clientID;
        }

        public DateTime CallDate { get => callDate; set => callDate = value; }
        public string CallDuration { get => callDuration; set => callDuration = value; }
        public string ClientID { get => clientID; set => clientID = value; }

        public void addCallToDB()
        {
            FileHandler fh = new FileHandler();
            fh.addCallToDB(this.clientID, this.callDate, this.callDuration);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
