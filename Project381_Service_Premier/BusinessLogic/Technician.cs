using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Wian_Se_Gedeelte_classes_
{
    class Technician
    {
        private string expertise;

        public Technician(string expertise)
        {
            this.Expertise = expertise;
        }

        public string Expertise { get => expertise; set => expertise = value; }

        public override bool Equals(object obj)
        {
            return obj is Technician technician &&
                   Expertise == technician.Expertise;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Expertise);
        }
    }
}
