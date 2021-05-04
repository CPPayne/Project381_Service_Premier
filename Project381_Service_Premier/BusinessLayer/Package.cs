using System;
using System.Collections.Generic;
using System.Text;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{
   class Package
   {
       private string packageName;
       private Decimal cost;
       private List<Service> services;

        public string PackageName { get => packageName; set => packageName = value; }
        public Decimal Cost { get => cost; set => cost = value; }
        internal List<Service> Services { get => services; set => services = value; }

        public Package(string packageName, Decimal cost, List<Service> services)
        {
            this.PackageName = packageName;
            this.Cost = cost;
            Services = services;
        }
        public Package()
        {

        }

        public override string ToString()
      {
         return base.ToString();
      }

        public void addPackageToDB()
        {
            FileHandler fh = new FileHandler();
            fh.addPackage(this.packageName, this.cost, this.Services);
        }


        public List<Package> getAllPackages()
        {
            FileHandler fh = new FileHandler();
            return fh.getAllPackages();
        }

        public void createService()
      {

      }

    }
}
