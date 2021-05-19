using System;
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
        private List<string> typesOfServicesForContract;

        private int contractID;

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

        public Contract(int contractID,string contractLevel, DateTime startDate, Package package)
        {

            this.ContractID = contractID;
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
        public List<string> TypesOfServicesForContract { get => typesOfServicesForContract; }
        public int ContractID { get => contractID; set => contractID = value; }

        public List<Contract> getContractsForClient(string clientId)
        {
            FileHandler fh = new FileHandler();
            return fh.getContractsForClient(clientId);
        }

        public void getTypeOfServicesForContract()
        {
            FileHandler fh = new FileHandler();
            typesOfServicesForContract = fh.getTypesOfSerivesAvailable(this.package.getPackageID());
            
        }

        public void addContractToDB(string clientID, string packageID)
        {
            FileHandler fh = new FileHandler();
            fh.addContractToDB(this.startDate, clientID, packageID, this.contractLevel);
        }

        public string getPackNameFromCont(int conID)
        {
            FileHandler fh = new FileHandler();
            return fh.getPackageNameByContract(conID);
        }

        public override string ToString()
        {

            return this.package.PackageName + " " + this.ContractLevel + " " + this.startDate;
        }

        public List<Service> getServicesFromCon(int conID)
        {
            FileHandler fh = new FileHandler();
            return fh.getServFromCon(conID);
        }
    }
}
