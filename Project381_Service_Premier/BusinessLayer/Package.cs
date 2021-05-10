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

      public string getPackageID(string packageName)
        {
            FileHandler fh = new FileHandler();
            return fh.getPackageID(packageName);
        }

        public string getPackageID()
        {
            FileHandler fh = new FileHandler();
            return fh.getPackageID(this.packageName);
        }

        public override string ToString()
      {
            return this.packageName;
      }

      public void addPackageToDB()
      {
         FileHandler fh = new FileHandler();
         fh.addPackage(this.packageName, this.cost, this.Services);
      }

        public void getPackageServices()
        {
            FileHandler fh = new FileHandler();
            this.services=fh.getServicesForPackage(fh.getPackageID(this.packageName));
        }
        public void setPackageCost()
        {
            FileHandler fh = new FileHandler();
            this.cost = fh.getPackageCostByName(this.packageName);
        }

        public List<Package> getAllPackages()
        {
            FileHandler fh = new FileHandler();
            return fh.getAllPackages();
        }

       


        public override bool Equals(object obj)
        {
            return obj is Package package &&
                   packageName == package.packageName &&
                   cost == package.cost &&
                   EqualityComparer<List<Service>>.Default.Equals(services, package.services) &&
                   PackageName == package.PackageName &&
                   Cost == package.Cost &&
                   EqualityComparer<List<Service>>.Default.Equals(Services, package.Services);
        }

        public override int GetHashCode()
        {
            int hashCode = 597446354;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(packageName);
            hashCode = hashCode * -1521134295 + cost.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Service>>.Default.GetHashCode(services);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PackageName);
            hashCode = hashCode * -1521134295 + Cost.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Service>>.Default.GetHashCode(Services);
            return hashCode;
        }
    }
}
