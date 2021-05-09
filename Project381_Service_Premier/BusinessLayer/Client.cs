using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project381_Service_Premier.BusinessLayer;
using Project381_Service_Premier.DataAccessLayer;
namespace Project381_Service_Premier.BusinessLayer
{
   //namespace Project381_Service_Premier.businesslogic
   class Client
   {

      private string name;
      private string surname;
      private string phoneNum;
      private string address;
      private string clientID;
      private bool isBusiness;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Address { get => address; set => address = value; }
        public string ClientID { get => clientID; set => clientID = value; }
        public bool IsBusiness { get => isBusiness; set => isBusiness = value; }

        public Client(string name, string surname, string phoneNum, string address, bool isBusiness)
        {
            this.Name = name;
            this.Surname = surname;
            this.PhoneNum = phoneNum;
            this.Address = address;
            this.IsBusiness = isBusiness;
        }

        public Client get_client_by_phone(string phoneNum)
      {
         return null;
      }

      public void GenerateClientID()
      {
          int max = 10000000;
          int min = 1;
          string completeID;
          Random rndLetter = new Random();
          FileHandler fh = new FileHandler();

            while (true)
            {
                completeID = "";
                char randomChar = (char)rndLetter.Next('A', 'E');

                completeID = pad_an_int(rndLetter.Next(min, max), 8);

                completeID = randomChar + completeID;

                if (!fh.checkIfClientIdExists(completeID))
                {
                    this.ClientID = completeID;
                    break;
                }
            }
          

          
      }

        private static string pad_an_int(int N, int P)
      {

        string s = "{0:";
        for (int i = 0; i < P; i++)
        {
            s += "0";
        }
         s += "}";


         string value = string.Format(s, N);


         return value;
      }

        public void addClientToDB()
        {
            FileHandler dbaccess = new FileHandler();
            dbaccess.addclient(ClientID, Name, Surname, Address, PhoneNum, IsBusiness);
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
