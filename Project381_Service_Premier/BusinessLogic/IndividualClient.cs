using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace michal_project_classes
{
    class IndividualClient : Client
    {
        private string surname;

        public string Surname { get => surname; set => surname = value; }
        public string Id { get => id; set => id = value; }

        public IndividualClient(string surname, string id)
        {
            this.surname = surname;
            this.id = id;
        }

        public IndividualClient()
        {
            
        }

        public override bool Equals(object obj)
        {
            return obj is IndividualClient client &&
                   surname == client.surname &&
                   id == client.id;
        }

        public override int GetHashCode()
        {
            int hashCode = 1117570774;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(id);
            return hashCode;
        }

        public override string ToString()
        {
            return base.ToString() + ": " + value.ToString();
        }
    }
}
