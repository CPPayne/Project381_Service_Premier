using System;
using System.Collections.Generic;
using System.Text;

namespace Project381_Service_Premier.businesslogic
{
   class Package
   {
      private string packageName;
      private int level;
      private double cost;
      private DateTime startDate;
      private int duration;
      private List<Service> Services;

      public Package(string packagename, int lvl, double Cost, DateTime StartDate, int Duration, List<Service> Services)
      {
         this.PackageName = packagename;
         this.Level = lvl;
         this.Cost = Cost;
         this.StartDate = StartDate;
         this.Duration = Duration;
         this.Services1 = Services;
      }             //ek hou van appels

      public string PackageName { get => packageName; set => packageName = value; }
      public int Level { get => level; set => level = value; }
      public double Cost { get => cost; set => cost = value; }
      public DateTime StartDate { get => startDate; set => startDate = value; }
      public int Duration { get => duration; set => duration = value; }
      internal List<Service> Services1 { get => Services; set => Services = value; }

      public override bool Equals(object obj)
      {
         return obj is Package package &&
                packageName == package.packageName &&
                level == package.level &&
                cost == package.cost &&
                startDate == package.startDate &&
                duration == package.duration &&
                isBusiness == package.isBusiness &&
                PackageName == package.PackageName &&
                Level == package.Level &&
                Cost == package.Cost &&
                StartDate == package.StartDate &&
                Duration == package.Duration &&
                IsBusiness == package.IsBusiness;
      }

      public override int GetHashCode()
      {
         HashCode hash = new HashCode();
         hash.Add(packageName);
         hash.Add(level);
         hash.Add(cost);
         hash.Add(startDate);
         hash.Add(duration);
         hash.Add(isBusiness);
         hash.Add(PackageName);
         hash.Add(Level);
         hash.Add(Cost);
         hash.Add(StartDate);
         hash.Add(Duration);
         hash.Add(IsBusiness);
         return hash.ToHashCode();
      }

      public override string ToString()
      {
         return base.ToString();
      }

      public void createPackage()
      {

      }

      public void createService()
      {

      }
   }
}
