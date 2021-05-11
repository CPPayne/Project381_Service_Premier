using System;
using System.Collections.Generic;
using System.Text;

namespace Project381_Service_Premier.BusinessLayer
{
    class WorkRequest
    {
        private int id;
        private string description;
        private string expertise_required;

        public WorkRequest(int id, string description, string expertise_required)
        {
            this.Id = id;
            this.Description = description;
            this.Expertise_required = expertise_required;
        }

        public int Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public string Expertise_required { get => expertise_required; set => expertise_required = value; }

        public override bool Equals(object obj)
        {
            return obj is WorkRequest request &&
                   id == request.id &&
                   description == request.description &&
                   expertise_required == request.expertise_required;
        }

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(id, description, expertise_required);
        //}
    }
}
