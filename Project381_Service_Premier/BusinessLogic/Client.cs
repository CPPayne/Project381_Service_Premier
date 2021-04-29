using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project381_Service_Premier.BusinessLogic
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

        public Client(string name, string phoneNum, string address, Contract contract)
        {
            Name = name;
            PhoneNum = phoneNum;
            Address = address;
            Contract = contract;
            Name = name;
            PhoneNum = phoneNum;
            Address = address;
            Contract = contract;
        }

        public string Name { get => name; set => name = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Address { get => address; set => address = value; }
        internal static Client AllClients1 { get => AllClients; set => AllClients = value; }
        public Contract Contract { get => contract; set => contract = value; }

        public Client get_client_by_phone(string phoneNum)
        {
            return null;
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
