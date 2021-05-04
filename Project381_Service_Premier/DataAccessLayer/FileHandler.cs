using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using Project381_Service_Premier.BusinessLayer;

namespace Project381_Service_Premier.DataAccessLayer
{
    class FileHandler
    {
        public FileHandler()
        {
        }


        //Set connection string
        string connect = "Data Source=.; Initial Catalog= servicePremierDB; Integrated Security= SSPI";
        //SqlConnection conn;
        //SqlConnection conn2; //Declare SqlConnection object
        //SqlCommand command;     //Declare SqlCommand object
        //SqlDataReader reader;   //Declare SqlDataReader object
        //SqlDataReader reader2;   //Declare SqlDataReader object
        //Declare Student object
        //Service objStudent = new Student();

        //Register method
        public void addService(string sType, string sName, string sSpecifications)
        {
            SqlConnection conn;
            SqlCommand command;
            string query = @"INSERT INTO ServiceC VALUES ( '" + sType + "', '" + sName + "', '" + sSpecifications + "' )";

            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);
            

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Service added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Details of new service not saved: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public void addPackage(string pName, Decimal pCost, List<Service> packageServices)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command;
            SqlConnection conn2 = new SqlConnection(connect); ;
            SqlCommand command2;

            int packageID;
            int serviceID;
            string query = @"INSERT INTO PPackage VALUES ( '" + pName + "', '" + pCost + "' )";

            

            conn.Open();

            command = new SqlCommand(query, conn);




            try
            {
                command.ExecuteNonQuery();
                packageID = int.Parse(getPackageID(pName));
                foreach (Service service in packageServices)
                {
                    serviceID = int.Parse(getServiceID(service.SName));
                    string query2 = @"INSERT INTO Service_Packages VALUES ( '" + packageID + "', '" + serviceID + "' )";
                   
                    
                    conn2.Open();
                    command2 = new SqlCommand(query2, conn2);
                    command2.ExecuteNonQuery();
                    conn2.Close();
                }
                MessageBox.Show("Added");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Details of new service not saved: " + ex.Message);
            }
            finally
            {
                conn.Close();
              

            }
        }

        //public void addclient(string cid, string cname, string csurname, string caddress, string cnumber, bool isbusiness, package package)
        //{
        //    string packageid;
        //    string serviceid;
        //    string query = @"insert into client values ( '"+ cid + "', '" + cname + "', '" + csurname + "', '" + caddress + "', '" + cnumber + "', '" + isbusiness +  "' )";

        //    conn = new sqlconnection(connect);

        //    conn.open();

        //    command = new sqlcommand(query, conn);




        //    try
        //    {
        //        command.executenonquery();
        //        packageid = getpackageid(pname);
        //        foreach (service service in packageservices)
        //        {
        //            serviceid = getserviceid(service.sname);
        //            string query2 = @"insert into service_packages values ( '" + packageid + "', '" + serviceid + "' )";

        //            sqlcommand command2 = new sqlcommand(query2, conn);
        //            command2.executenonquery();
        //        }

        //    }
        //    catch (exception ex)
        //    {
        //        messagebox.show("details of new service not saved: " + ex.message);
        //    }
        //    finally
        //    {
        //        conn.close();
        //    }
        //}

        //Delete method
        //public void Delete(int sID)
        //{
        //    string query = @"DELETE FROM Students WHERE StudentID = ('" + sID + "')";

        //    conn = new SqlConnection(connect);

        //    conn.Open();

        //    command = new SqlCommand(query, conn);

        //    try
        //    {
        //        command.ExecuteNonQuery();
        //        MessageBox.Show("Deleted the details of student with Student Number: " + sID);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        //public bool Search(int sID)
        //{
        //    //query select all collumns provided stdID = value

        //    string query = @"SELECT * FROM Students WHERE StudentID = ('" + sID + "')";

        //    //connect
        //    conn = new SqlConnection(connect);

        //    conn.Open();

        //    command = new SqlCommand(query, conn);
        //    List<Student> myStudent = new List<Student>();

        //    //run query command
        //    try
        //    {
        //        //read data SqlDataReader
        //        reader = command.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            //Store each collumn value in student class variables/field
        //            objStudent.StudentID = int.Parse(reader[0].ToString());
        //            objStudent.StudentName = reader[1].ToString();
        //            objStudent.StudentSurname = reader[2].ToString();
        //            objStudent.CourseID = reader[3].ToString();

        //            //Add field values to a Student type list
        //            myStudent.Add(new Student(objStudent.StudentID, objStudent.StudentName, objStudent.StudentSurname, objStudent.CourseID));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return myStudent;
        //}
        
        //Search method
        //public List<Student> Search(int sID)
        //{
        //    //query select all collumns provided stdID = value

        //    string query = @"SELECT * FROM Students WHERE StudentID = ('" + sID + "')";

        //    //connect
        //    conn = new SqlConnection(connect);

        //    conn.Open();

        //    command = new SqlCommand(query, conn);
        //    List<Student> myStudent = new List<Student>();

        //    //run query command
        //    try
        //    {
        //        //read data SqlDataReader
        //        reader = command.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            //Store each collumn value in student class variables/field
        //            objStudent.StudentID = int.Parse(reader[0].ToString());
        //            objStudent.StudentName = reader[1].ToString();
        //            objStudent.StudentSurname = reader[2].ToString();
        //            objStudent.CourseID = reader[3].ToString();

        //            //Add field values to a Student type list
        //            myStudent.Add(new Student(objStudent.StudentID, objStudent.StudentName, objStudent.StudentSurname, objStudent.CourseID));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return myStudent;
        //}

        public List<Service> getAllServices()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command;
            SqlDataReader reader;

            string query = @"SELECT * FROM ServiceC";

            Service objService = new Service();
            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);
            List<Service> allServices = new List<Service>();

            
            try
            {

                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    objService.SName = reader.GetValue(1).ToString();
                    objService.SType = reader.GetValue(2).ToString();
                    objService.SSpecifications = reader.GetValue(3).ToString();

                    allServices.Add(new Service(objService.SName, objService.SType, objService.SSpecifications));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return allServices;
        }

        public List<Package> getAllPackages()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command;
            SqlDataReader reader;

            string query = @"SELECT * FROM PPackage";

            Package objPackage = new Package();

            conn.Open();

            command = new SqlCommand(query, conn);
            List<Package> allPackages = new List<Package>();


            try
            {

                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string packID = reader.GetValue(0).ToString();
                    objPackage.PackageName = reader.GetValue(1).ToString();
                    objPackage.Cost = Decimal.Parse(reader.GetValue(2).ToString());
                    //Maybe convert string to int if error.
                    objPackage.Services = getServicesForPackage(packID);



                    allPackages.Add(new Package(objPackage.PackageName, objPackage.Cost, objPackage.Services));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return allPackages;
        }

        public List<Service> getServicesForPackage(string packageID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command;
            SqlDataReader reader;

            string query = @"SELECT * FROM ServiceC WHERE ServiceID IN (SELECT ServiceID FROM Service_Packages WHERE PackageID = '" + packageID + "')";

            Service objService = new Service();

            conn.Open();

            command = new SqlCommand(query, conn);
            List<Service> servicesForPack = new List<Service>();


            try
            {

                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    objService.SName = reader.GetValue(1).ToString();
                    objService.SType = reader.GetValue(2).ToString();
                    objService.SSpecifications = reader.GetValue(3).ToString();

                    servicesForPack.Add(new Service(objService.SName, objService.SType, objService.SSpecifications));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return servicesForPack;
        }

        public string getPackageID(string name)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command;
            SqlDataReader reader;

            string query = @"SELECT * FROM pPackage WHERE PackageName = ('" + name + "')";

            //connect
            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);

            string packageID = "";
            //run query command
            try
            {
                //read data SqlDataReader
                reader = command.ExecuteReader();
                if (reader.Read())
                {

                    packageID = reader[0].ToString();



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            //Return the student list
            return packageID;
        }


        public string getServiceID(string name)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command;
            SqlDataReader reader;

            string query = @"SELECT * FROM ServiceC WHERE ServiceName = ('" + name + "')";

            //connect
            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);

            string ServiceID = "";
            //run query command
            try
            {
                //read data SqlDataReader
                reader = command.ExecuteReader();
                if (reader.Read())
                {

                    ServiceID = reader[0].ToString();



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            //Return the student list
            return ServiceID;
        }

        //Update method
        public void Update(int sID, string sName, string sSurnane, string cID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command;
            SqlDataReader reader;
            string query = @"UPDATE Students SET StudentID = ('" + sID + "'), FirstName = ('" + sName + "'), " +
                "LastName = ('" + sSurnane + "'), CourseID = ('" + cID + "') WHERE StudentID = ('" + sID + "')";

            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Details updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //public List<Call> GetClientCallHistory()
        //{

        //}

        public void GetClientWorkHistory() { }

        public void GetTechnicianWorkHistory() { }

        public void GetCallLog() { }

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
        //Ek hou van aartappels
    }
}
