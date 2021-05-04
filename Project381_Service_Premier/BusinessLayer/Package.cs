using System;
using System.Collections.Generic;
using System.Text;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{
   class Package
   {
       private string packageName;
       private double cost;
       private List<Service> Services;

        public string PackageName { get => packageName; set => packageName = value; }
        public double Cost { get => cost; set => cost = value; }
        internal List<Service> Services1 { get => Services; set => Services = value; }

        public Package(string packageName, double cost, List<Service> services)
        {
            this.PackageName = packageName;
            this.Cost = cost;
            Services1 = services;
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

      public void createService()
      {

      }

    }
}
