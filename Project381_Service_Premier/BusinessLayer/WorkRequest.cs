using System;
using System.Collections.Generic;
using System.Text;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{
    class WorkRequest
    {
        private string workRequestID;
        private string problemType;
        private string description;
        private string callID;
        private string clientID;
        private DateTime dateCreated;

        public WorkRequest(string problemType, string description, string callID, string clientID, DateTime dateCreated)
        {
            this.problemType = problemType;
            this.description = description;
            this.callID = callID;
            this.clientID = clientID;
            this.dateCreated = dateCreated;
            
        }

        public WorkRequest(string workRequestID,string problemType, string description, string callID, string clientID, DateTime dateCreated)
        {
            this.workRequestID = workRequestID;
            this.problemType = problemType;
            this.description = description;
            this.callID = callID;
            this.clientID = clientID;
            this.dateCreated = dateCreated;

        }

        public WorkRequest()
        {
        }

        public string ProblemType { get => problemType; set => problemType = value; }
        public string Description { get => description; set => description = value; }
        public string CallID { get => callID; set => callID = value; }
        public string ClientID { get => clientID; set => clientID = value; }
        public DateTime DateCreated { get => dateCreated; set => dateCreated = value; }
        public string WorkRequestID { get => workRequestID; set => workRequestID = value; }

        public void addWorkRequestToDB()
        {
            FileHandler fh = new FileHandler();
            fh.addWorkRequestToDB(this.workRequestID, this.problemType, this.description, this.callID, this.clientID, this.dateCreated);
        }

        public List<WorkRequest> getWorkRequestForClient(string clientID)
        {
            FileHandler fh = new FileHandler();
            return fh.getWorkRequestsForClient(clientID);
        }

        public void GenerateWorkRequestID()
        {
            int max = 100000;
            int min = 1;
            string completeID;
            DateTime date = DateTime.Now;
            Random rndLetter = new Random();
            FileHandler fh = new FileHandler();

            string FirstLetter = "W";

            while (true)
            {
                completeID = "";


                completeID = pad_an_int(rndLetter.Next(min, max), 5);

                completeID = FirstLetter + completeID;

                if (!fh.checkIfWorkRequestIDExist(completeID))
                {
                    this.workRequestID = completeID;
                    break;
                }
            }



        }

        public override string ToString()
        {
            return this.workRequestID;
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
    }
}
