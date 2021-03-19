using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Wian_Se_Gedeelte_classes_
{
    class WorkRequest
    {
        public int id { get; set; };
        public string discription { get; set; };
        public string expertise_requered { get; set; };

        public void Equals(int id, string discription, string expertise_requered)
        {
            return id;
            return discription;
            return expertise_requered;
        }

        public override int GetHashCode()
        {
            return HashCode.Equals(id, discription, expertise_requered);
        }


    }
}
