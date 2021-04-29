using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project381_Service_Premier.BusinessLogic
{
    class BusinessClient : Client
    {
       

        public string BusinessID { get => businessID; set => businessID = value; }

        public override bool Equals(object obj)
        {
            return obj is BusinessClient client &&
            BusinessID == client.BusinessID;
        }

        public override int GetHashCode()
        {
            return 1905410614 + EqualityComparer<string>.Default.GetHashCode(BusinessID);
        }

        public BusinessClient(string businessID)
        {
            this.BusinessID = businessID;
        }

        public BusinessClient()
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + ": " + value.ToString();
        }


    }

}
