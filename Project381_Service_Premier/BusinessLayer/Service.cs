using Project381_Service_Premier.DataAccessLayer;

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

      public override string ToString()
      {
         return base.ToString();
      }


   }
}
