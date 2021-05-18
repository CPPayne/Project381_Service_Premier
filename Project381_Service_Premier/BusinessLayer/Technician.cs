using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Project381_Service_Premier.BusinessLayer;
using Project381_Service_Premier.DataAccessLayer;
using System.Windows.Forms;

namespace Project381_Service_Premier.BusinessLayer
{
    class Technician
    {
        private string name;
        private string surname;
        private string techID;
        private string username;
        private string password;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string TechID { get => techID; set => techID = value; }
        public string Username { get => username; set => username = value; }
        public string Password{ get => password; set => password = value; }

        public Technician(string name, string surname, string username, string password)
        {
            this.Name = name;
            this.Surname = surname;
            this.Username = username;
            this.Password = password;


        }

        public Technician()
        {

        }

        //public Technician(string name, string surname, string username, string password)
        //{
        //    this.name = name;
        //    this.surname = surname;
        //    this.username = username;
        //    this.password = password;

        //}

        public Technician getTechByUser(string username)
        {
            FileHandler fh = new FileHandler();
            return fh.getTechnicianByUsername(username);
        }


        public void addTechnicianToDB()
        {
            FileHandler dbaccess = new FileHandler();
            dbaccess.addTechnician(Name, Surname, username, password);
        }

        public bool technicianLogin(string username, string password)
        {
            FileHandler fh = new FileHandler();

            if (fh.checkTechnicianLogin(username, password))
            {
                Technician temp = new Technician();
                temp = fh.getTechnicianByUsername(username);
                this.techID = temp.techID;
                this.name = temp.name;
                this.surname = temp.surname;
                this.username = temp.username;
                this.password = temp.password;
                return true;
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password");
                return false;
            }
        }


        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
