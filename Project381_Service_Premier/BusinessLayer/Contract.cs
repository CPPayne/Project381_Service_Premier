﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{
    class Contract
    {

        private string contractLevel;
        private DateTime startDate;
        private Package package;
        private Client client;

        public Contract(string contractLevel, DateTime startDate, Package package, Client client)
        {

            this.ContractLevel = contractLevel;
            this.StartDate = startDate;
            this.package = package;
            this.client = client;
        }
        public Contract(string contractLevel, DateTime startDate, Package package)
        {

            this.ContractLevel = contractLevel;
            this.StartDate = startDate;
            this.package = package;
        }
        public Contract()
        {
        }


        public string ContractLevel { get => contractLevel; set => contractLevel = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        internal Package cPackage { get => package; set => package = value; }
        internal Client cClient { get => client; set => client = value; }

        public List<Contract> getContractsForClient(string clientId)
        {
            FileHandler fh = new FileHandler();
            return fh.getContractsForClient(clientId);
        }

        public void addContractToDB(string clientID, string packageID)
        {
            FileHandler fh = new FileHandler();
            fh.addContractToDB(this.startDate, clientID, packageID, this.contractLevel);
        }
    }
}
