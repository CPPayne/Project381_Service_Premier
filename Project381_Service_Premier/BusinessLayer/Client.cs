using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project381_Service_Premier.BusinessLayer;
namespace Project381_Service_Premier.BusinessLayer
{
   //namespace Project381_Service_Premier.businesslogic
   class Client
   {

      private string name;
      private string phoneNum;
      private string address;
      static Client AllClients;
      private Contract contract;
      private String ClientID;

      public Client(string name, string phoneNum, string address, Contract contract, string clientID)
      {
         this.name = name;
         this.phoneNum = phoneNum;
         this.address = address;
         this.contract = contract;
         ClientID = clientID;
      }

      public string Name { get => name; set => name = value; }
      public string PhoneNum { get => phoneNum; set => phoneNum = value; }
      public string Address { get => address; set => address = value; }
      internal static Client AllClients1 { get => AllClients; set => AllClients = value; }
      // public Contract Contract { get => contract; set => contract = value; }

      public Client get_client_by_phone(string phoneNum)
      {
         return null;
      }

      public static string GenerateClientID()
      {
          int max = 10000000;
          int min = 1;
          string completeID;
          Random rndLetter = new Random();

          char randomChar = (char)rndLetter.Next('A', 'E');
          //'a' and 'z' are interpreted as ints for parameters for Next()

          completeID = pad_an_int(rndLetter.Next(min, max), 8);

          completeID = randomChar + completeID;

          return completeID;
      }

        private static string pad_an_int(int N, int P)
      {
        // string used in Format() method
        string s = "{0:";
        for (int i = 0; i < P; i++)
        {
            s += "0";
        }
         s += "}";

        // use of string.Format() method
         string value = string.Format(s, N);

        // return output
         return value;
      }

        public override string ToString()
      {
         return base.ToString();
      }

      public override bool Equals(object obj)
      {
         return base.Equals(obj);
      }

      public override int GetHashCode()
      {
         return base.GetHashCode();
      }
   }
}
