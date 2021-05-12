using System;
using System.Collections.Generic;
using System.Text;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{
    class WorkRequest
    {
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

        public WorkRequest()
        {
        }

        public string ProblemType { get => problemType; set => problemType = value; }
        public string Description { get => description; set => description = value; }
        public string CallID { get => callID; set => callID = value; }
        public string ClientID { get => clientID; set => clientID = value; }
        public DateTime DateCreated { get => dateCreated; set => dateCreated = value; }



        public void addWorkRequestToDB()
        {
            FileHandler fh = new FileHandler();
            fh.addWorkRequestToDB(this.problemType, this.description, this.callID, this.clientID);
        }
    }
}
