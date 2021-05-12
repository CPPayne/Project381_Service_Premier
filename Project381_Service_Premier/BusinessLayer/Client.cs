using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project381_Service_Premier.BusinessLayer;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{

    class Client
    {

        private string name;
        private string surname;
        private string phoneNum;
        private string address;
        private string clientID;
        private bool isBusiness;
        private string username;
        private string password;

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

        public Client()
        {

        }

        public Client(string name, string surname, string phoneNum, string address, bool isBusiness, string username, string password)
        {
            this.name = name;
            this.surname = surname;
            this.phoneNum = phoneNum;
            this.address = address;
            this.isBusiness = isBusiness;
            this.username = username;
            this.password = password;

        }

        public Client getClientByNumber(string phoneNum)
        {
            FileHandler fh = new FileHandler();
            return fh.getClientByNum(phoneNum);
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
            dbaccess.addclient(ClientID, Name, Surname, Address, PhoneNum, IsBusiness, username, password);
        }

        public bool login(string username, string password)
        {
            FileHandler fh = new FileHandler();

            if (fh.checkLogin(username, password))
            {
                Client temp = new Client();
                temp = fh.getClient(username, password);
                this.ClientID = temp.ClientID;
                this.name = temp.name;
                this.surname = temp.surname;
                this.address = temp.address;
                this.phoneNum = temp.phoneNum;
                this.isBusiness = temp.isBusiness;
                return true;
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password");
                return false;
            }
        }


        public List<string> getListOfAllPhoneNumbers()
        {
            FileHandler fh = new FileHandler();
            return fh.getListOfAllPhoneNumbers();
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
