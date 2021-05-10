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

         string query = @"INSERT INTO Client (ClientID,ClientName ,ClientSurname,ClientAdress,PhoneNumber,BusinessBoolean,ClientUsername,ClientPassword) VALUES ( '" + cId + "', '" + cName + "', '" + cSurname + "', '" + cAddress + "', '" + cNumber + "', '" + isBusiness + "', '" + username + "', '" + password + "' )";

         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);




            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Client Registered, Please log in!" );

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


            conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);


            try
            {
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

        public void addContractToDB(DateTime contractStart, string ClientID, string packageID, string ContractLevel)
        {
            string query = @"INSERT INTO ContractC  VALUES ( '" + contractStart+ "', '" + ClientID + "', '" + packageID + "', '" + ContractLevel  + "' )";

            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);




            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Contract Created");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Contract Creation Failed! " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void addPackage(string pName, Decimal pCost, List<Service> packageServices)
        {

            int serviceID;
            int packageID;
            string query = @"INSERT INTO PPackage VALUES ( '" + pName + "', '" + pCost + "' )";

         conn = new SqlConnection(connect);
         conn2 = new SqlConnection(connect);

            conn.Open();


            command = new SqlCommand(query, conn);


            try
            {
                command.ExecuteNonQuery();
                packageID = int.Parse(getPackageID(pName));

                //addPackageServicesToDB(packageServices, packageID);
               
                foreach (Service service in packageServices)
                {
                    conn2.Open();
                    serviceID = int.Parse(getServiceID(service.SName));
                    string query2 = @"INSERT INTO Service_Packages VALUES ( '" + packageID + "', '" + serviceID + "' )";

                    command2 = new SqlCommand(query2, conn2);
                    command2.ExecuteNonQuery();
                    conn2.Close();

                }
                conn2.Close();
                conn.Close();
                MessageBox.Show("Package Added");

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

        public void addPackageServicesToDB(List<Service> packageServices, int packageID)
        {
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

         string query = @"SELECT * FROM PPackage WHERE PackageName = ('" + name + "')";


         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);

            string packageID = "";
            try
            {
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
            return packageID;
        }

        public Package getPackageByID(string id)
        {

            string query = @"SELECT * FROM PPackage WHERE PackageID = ('" + id + "')";


            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);

            Package objPackage = new Package();

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                {

                    objPackage.PackageName = reader[1].ToString();
                    objPackage.Cost = Decimal.Parse(reader[2].ToString());


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("getPackageByID: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return objPackage;
        }

        public string getServiceID(string name)
        {


         string query = @"SELECT * FROM ServiceC WHERE ServiceName = ('" + name + "')";

            conn3 = new SqlConnection(connect);

         conn3.Open();

         command3 = new SqlCommand(query, conn3);

            string ServiceID = "";

            try
            {

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

            return ServiceID;
        }

        public void Update(int sID, string sName, string sSurnane, string cID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command;
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
         string query = @"SELECT * FROM Client WHERE ClientUsername = ('" + username + "') AND ClientPassword = ('" + password + "')";


            conn = new SqlConnection(connect);

         conn.Open();

            command = new SqlCommand(query, conn);
            bool clientFound = false;

            try
            {

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

            return clientFound;
        }

        public Client getClient(string username, string password)
      {
         string query = @"SELECT * FROM Client WHERE ClientUsername = ('" + username + "') AND ClientPassword = ('" + password + "')";


            conn = new SqlConnection(connect);

         conn.Open();

            command = new SqlCommand(query, conn);
            Client loggedClient = new Client();

            try
            {

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

            return loggedClient;
        }

        public List<Contract> getContractsForClient(string clientId)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command;
            SqlDataReader reader;

            string query = @"SELECT * FROM ContractC WHERE ClientID  = ('" + clientId + "')";

            Contract objContract = new Contract();

            conn.Open();

            command = new SqlCommand(query, conn);
            List<Contract> contractsForClient = new List<Contract>();


            try
            {

                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    objContract.StartDate = Convert.ToDateTime(reader.GetValue(1).ToString());
                    string packageID = reader.GetValue(3).ToString();
                    objContract.ContractLevel = reader.GetValue(4).ToString();
                    objContract.cPackage = getPackageByID(packageID);
                    contractsForClient.Add(new Contract(objContract.ContractLevel,objContract.StartDate,objContract.cPackage));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("getContractsForClient: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return contractsForClient;
        }
        public Decimal getPackageCostByName(string packageName)
        {
            string query = @"SELECT * FROM PPackage WHERE PackageName = ('" + packageName + "')";


            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);

            Decimal cost = 0;

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                {

                    cost = Convert.ToDecimal(reader.GetValue(2).ToString());


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("getPackageByID: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return cost;
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



