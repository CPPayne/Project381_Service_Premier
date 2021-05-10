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
        SqlConnection conn;
        SqlConnection conn2;
        SqlConnection conn3;

        SqlCommand command;
        SqlCommand command2;
        SqlCommand command3;

        SqlDataReader reader;   
        SqlDataReader reader2;
        SqlDataReader reader3;

        public void addclient(string cId, string cName, string cSurname, string cAddress, string cNumber, bool isBusiness, string username, string password)
        {

            string query = @"INSERT INTO Client (ClientID,ClientName ,ClientSurname,ClientAdress,PhoneNumber,BusinessBoolean,ClientUsername,ClientPassword) VALUES ( '" + cId + "', '" + cName + "', '" + cSurname + "', '" + cAddress + "', '" + cNumber + "', '" + isBusiness + "', '" + username+ "', '" +password+ "' )";

         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);




         try
         {
            command.ExecuteNonQuery();
            MessageBox.Show("Details of new client saved:");

         }
         catch (Exception ex)
         {
            MessageBox.Show("details of new client not saved: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
      }

      public bool checkIfClientIdExists(string clientID)
      {

         bool clientExist = false;
         string query = @"SELECT * FROM Client WHERE ClientID = ('" + clientID + "')";

         //connect
         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);

         //run query command
         try
         {
            //read data SqlDataReader
            reader = command.ExecuteReader();
            if (reader.Read())
            {
               clientExist = true;
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
         return clientExist;
      }

        public void addService(string sName,string sType, string sSpecifications)
        {
            //SqlConnection conn;
           // SqlCommand command;
            string query = @"INSERT INTO ServiceC (ServiceName, ServiceType, ServiceSpecification) VALUES ( '" + sName + "', '" + sType + "', '" + sSpecifications + "' )";

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
            //SqlConnection conn = new SqlConnection(connect);
            //SqlCommand command;
            //SqlConnection conn2 = new SqlConnection(connect); ;
            //SqlCommand command2;
            int serviceID;
            int packageID;
            string query = @"INSERT INTO PPackage VALUES ( '" + pName + "', '" + pCost + "' )";

         conn = new SqlConnection(connect);
         conn2 = new SqlConnection(connect);

         conn.Open();
         conn2.Open();

         command = new SqlCommand(query, conn);


            try
            {
                command.ExecuteNonQuery();
                packageID = int.Parse(getPackageID(pName));
                foreach (Service service in packageServices)
                {
                    MessageBox.Show(getServiceID(service.SName));
                }

                //MessageBox.Show(Convert.ToString(packageID));
                //addPackageServicesToDB(packageServices, packageID);
                //conn2.Open();
                //foreach (Service service in packageServices)
                //{
                //    serviceID = int.Parse(getServiceID(service.SName));
                //    MessageBox.Show(Convert.ToString(serviceID));
                //    //string query2 = @"INSERT INTO Service_Packages () VALUES ( '" + packageID + "', '" + serviceID + "' )";

                //    string query2 = @"SELECT * FROM ServiceC";
                    
                //    command2 = new SqlCommand(query2, conn2);
                //    command2.ExecuteNonQuery();
                    
                //}
                //conn2.Close();
                //conn.Close();
                MessageBox.Show("Package Added");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Details of new service not saved: " + ex.Message);
            }
            finally
            {
                conn.Close();
                //conn2.Close();

         }
      }

      public void addPackageServicesToDB(List<Service> packageServices, int packageID)
      {
         //SqlConnection conn;
         //SqlCommand command;

         int serviceID;
         conn2 = new SqlConnection(connect);


         try
         {
            foreach (Service service in packageServices)
            {
               serviceID = int.Parse(getServiceID(service.SName));
               string query = @"INSERT INTO Service_Packages VALUES ( '" + packageID + "', '" + serviceID + "' )";
               conn2.Open();
               command2 = new SqlCommand(query, conn2);


               command.ExecuteNonQuery();
               MessageBox.Show("Services to package Added");
               conn2.Close();
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show("Details of new service not saved: " + ex.Message);
         }
         finally
         {
            conn2.Close();
         }
      }

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

      public List<Service> getSinglePackageServices(string packageName)
      {
         SqlConnection conn = new SqlConnection(connect);
         SqlCommand command;
         SqlDataReader reader;

         string query = @"SELECT * FROM PPackage WHERE PackageName = '" + packageName + "')";

         Package objPackage = new Package();

         conn.Open();

         command = new SqlCommand(query, conn);



         try
         {

            reader = command.ExecuteReader();
            if (reader.Read())
            {

               string packID = reader.GetValue(0).ToString();
               objPackage.PackageName = reader.GetValue(1).ToString();
               objPackage.Cost = Decimal.Parse(reader.GetValue(2).ToString());

               objPackage.Services = getServicesForPackage(packID);

               foreach (Service srv in objPackage.Services)
               {
                  MessageBox.Show(srv.SName);
               }


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
         return objPackage.Services;
      }

      public string getPackageID(string name)
      {
         //SqlConnection conn = new SqlConnection(connect);
         //SqlCommand command;
         //SqlDataReader reader;

         string query = @"SELECT * FROM PPackage WHERE PackageName = ('" + name + "')";

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


         string query = @"SELECT * FROM ServiceC WHERE ServiceName = ('" + name + "')";

            //connect
            conn3 = new SqlConnection(connect);

            conn3.Open();

            command3 = new SqlCommand(query, conn3);

            string ServiceID = "";
            //run query command
            try
            {
                //read data SqlDataReader
                reader3 = command3.ExecuteReader();
                if (reader3.Read())
                {
                    ServiceID = reader3[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn3.Close();
            }
            //Return the student list
            return ServiceID;
        }

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

        public bool checkLogin(string username, string password)
        {
            string query = @"SELECT * FROM Client WHERE ClientUsername = ('" + username + "') AND ClientPassword = ('"+password+"')";

            //connect
            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);
            bool clientFound = false;
            //run query command
            try
            {
                //read data SqlDataReader
                reader = command.ExecuteReader();
                if (reader.Read())
                {

                    clientFound = true;
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
            return clientFound;
        }

        public Client getClient(string username, string password)
        {
            string query = @"SELECT * FROM Client WHERE ClientUsername = ('" + username + "') AND ClientPassword = ('" + password + "')";

            //connect
            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);
            Client loggedClient = new Client();
            //run query command
            try
            {
                //read data SqlDataReader
                reader = command.ExecuteReader();
                if (reader.Read())
                {

                    loggedClient.ClientID = reader[0].ToString();
                    loggedClient.Name = reader[1].ToString();
                    loggedClient.Surname = reader[2].ToString();
                    loggedClient.Address = reader[3].ToString();
                    loggedClient.PhoneNum = reader[4].ToString();
                    loggedClient.IsBusiness = Convert.ToBoolean(reader[5].ToString());

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
            return loggedClient;
        }

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