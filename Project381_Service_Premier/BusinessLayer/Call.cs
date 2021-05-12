using System;
using System.Collections.Generic;
using System.Text;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{
    class Call
    {
        private string callID;
        private DateTime callDate;
        private string callDuration;
        private string clientID;

        public string CallID { get => callID; set => callID = value; }
        public DateTime CallDate { get => callDate; set => callDate = value; }
        public string CallDuration { get => callDuration; set => callDuration = value; }
        public string ClientID { get => clientID; set => clientID = value; }

        public Call()
        {
            
        }

        public Call(string callID, DateTime callDate, string callDuration, string clientID)
        {
            this.callID = callID;
            this.callDate = callDate;
            this.callDuration = callDuration;
            this.clientID = clientID;
        }

        public void addCallToDB()
        {
            FileHandler fh = new FileHandler();
            fh.addCallToDB(this.callID, this.clientID, this.callDate, this.callDuration);
        }

        //public int getCallID()
        //{

        //}

        public void GenerateCallID()
        {
            int max = 100000;
            int min = 1;
            string completeID;
            DateTime date = DateTime.Now;
            Random rndLetter = new Random();
            FileHandler fh = new FileHandler();

            string FirstLetter = date.Month == 1 ? "Ja" : date.Month == 2 ? "F" : date.Month == 3 ? "M" : date.Month == 4 ? "A" :
                date.Month == 5 ? "M" : date.Month == 6 ? "Ju" : date.Month == 7 ? "Jl" : date.Month == 8 ? "Au" : date.Month == 9 ? "S" :
                date.Month == 10 ? "O" : date.Month == 11 ? "N" : date.Month == 12 ? "D" : "Not a Month";

            while (true)
            {
                completeID = "";


                completeID = pad_an_int(rndLetter.Next(min, max), 5);

                completeID = FirstLetter + completeID;

                if (!fh.checkIfCallIDExist(completeID))
                {
                    this.callID = completeID;
                    break;
                }
            }



        }

        public List<Call> getClientCallHis(string clientID)
        {
            FileHandler fh = new FileHandler();

            return fh.getAllClientCallHistory(clientID);
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
