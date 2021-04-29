using System;
using System.Collections.Generic;
using System.Text;

namespace Project381_Service_Premier.BusinessLogic
{
   class Call
   {
      private DateTime startTime;
      private DateTime endTime;
      private Client client;
      private String callDetails;

      public DateTime StartTime { get => startTime; set => startTime = value; }
      public DateTime EndTime { get => endTime; set => endTime = value; }
      public Client Client { get => client; set => client = value; }
      public string CallDetails { get => callDetails; set => callDetails = value; }

      public Call(DateTime startTime, DateTime endTime, Client client, string callDetails)
      {
         this.startTime = startTime;
         this.endTime = endTime;
         this.client = client;
         this.callDetails = callDetails;
      }

      public Call()
      {

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
