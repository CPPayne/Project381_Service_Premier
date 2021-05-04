using Project381_Service_Premier.DataAccessLayer;
using System.Collections.Generic;

namespace Project381_Service_Premier.BusinessLayer
{
   class Service
   {
      private string sType;
      private string sName;
      private string sSpecifications;

      public Service(string sType, string sName, string sSpecifications)
      {
         this.SType = sType;
         this.SName = sName;
         this.SSpecifications = sSpecifications;
      }

      public Service()
      {

      }
        
        public List<Service> getAllServices()
        {
            FileHandler fh = new FileHandler();
            return fh.getAllServices();
        }

        public void addServiceToDB()
      {
         FileHandler dbaccess = new FileHandler();
         dbaccess.addService(sType, sName, sSpecifications);
      }

      public string SType { get => sType; set => sType = value; }
      public string SName { get => sName; set => sName = value; }
      public string SSpecifications { get => sSpecifications; set => sSpecifications = value; }

      public override bool Equals(object obj)
      {
         return base.Equals(obj);
      }

      public override int GetHashCode()
      {
         return base.GetHashCode();
      }

        public string getID()
        {
            FileHandler fh = new FileHandler();

            return fh.getServiceID(this.sName);
        }

        public override string ToString()
      {
         return base.ToString();
      }


   }
}
