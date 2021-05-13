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
      string connect = "Data Source=.;Initial Catalog=servicePremierDB;Integrated Security=True";
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
            MessageBox.Show("Client Registered, Please log in!");

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

      public void addWorkRequestToDB(string workrequestID, string problemType, string problemDescription, string callID, string clientID)
      {
         string query = @"INSERT INTO WorkRequest (WorkRequestID,ProblemType, Descriptions, CallID,ClientID) VALUES ( '" + workrequestID + "', '" + problemType + "', '" + problemDescription + "', '" + callID + "','" + clientID + "' )";

         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);


         try
         {
            command.ExecuteNonQuery();
            MessageBox.Show("WorkRequest added added!");
         }
         catch (Exception ex)
         {
            MessageBox.Show("WorkRequest not saved: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }

      }


      public bool checkIfScheduleIdExists(string ScheduleID)
      {

         bool ScheduleIDExist = false;
         string query = @"SELECT * FROM Schedule WHERE ScheduleID = ('" + ScheduleID + "')";


         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);


         try
         {
            reader = command.ExecuteReader();
            if (reader.Read())
            {
               ScheduleIDExist = true;
            }

         }
         catch (Exception ex)
         {
            MessageBox.Show("Error checkIfScheduleIdExists: " + ex.Message);
         }
         finally
         {

            conn.Close();

         }
         return ScheduleIDExist;
      }

      public bool checkIfCallIDExist(string callID)
      {

         bool callIDExist = false;
         string query = @"SELECT * FROM Calls WHERE CallID = ('" + callID + "')";


         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);


         try
         {
            reader = command.ExecuteReader();
            if (reader.Read())
            {
               callIDExist = true;
            }

         }
         catch (Exception ex)
         {
            MessageBox.Show("Error checkIfCallIDExist: " + ex.Message);
         }
         finally
         {

            conn.Close();

         }
         return callIDExist;
      }

      public bool checkIfWorkRequestIDExist(string workRequestID)
      {

         bool workRequestIDExist = false;
         string query = @"SELECT * FROM WorkRequest WHERE WorkRequestID = ('" + workRequestID + "')";


         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);


         try
         {
            reader = command.ExecuteReader();
            if (reader.Read())
            {
               workRequestIDExist = true;
            }

         }
         catch (Exception ex)
         {
            MessageBox.Show("Error checkIfWorkRequestIDExist: " + ex.Message);
         }
         finally
         {

            conn.Close();

         }
         return workRequestIDExist;
      }

      public List<string> getListOfAllPhoneNumbers()
      {

         string query = @"SELECT PhoneNumber FROM Client";

         string phoneNumber;

         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);
         List<string> allPhoneNumbers = new List<string>();


         try
         {

            reader = command.ExecuteReader();
            while (reader.Read())
            {

               phoneNumber = reader.GetValue(0).ToString();
               allPhoneNumbers.Add(phoneNumber);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show("Error getListOfAllPhoneNumbers: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
         return allPhoneNumbers;
      }
      public void addService(string sName, string sType, string sSpecifications)
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

      public void addCallToDB(string callID, string clientID, DateTime date, string callDuration)
      {

         string query = @"INSERT INTO Calls (CallID,CallDate,callDuration ,ClientID) VALUES ( '" + callID + "', '" + date + "', '" + callDuration + "', '" + clientID + "' )";

         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);




         try
         {
            command.ExecuteNonQuery();


         }
         catch (Exception ex)
         {
            MessageBox.Show("Call not saved in DB " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
      }
      public Client getClientByNum(string phoneNum)
      {
         SqlConnection conn = new SqlConnection(connect);
         SqlCommand command;
         SqlDataReader reader;

         string query = @"SELECT * FROM Client WHERE PhoneNumber = ('" + phoneNum + "')";

         Client objClient = new Client();
         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);


         try
         {

            reader = command.ExecuteReader();
            while (reader.Read())
            {
               objClient.ClientID = reader.GetValue(0).ToString();
               objClient.Name = reader.GetValue(1).ToString();
               objClient.Surname = reader.GetValue(2).ToString();
               objClient.Address = reader.GetValue(3).ToString();
               objClient.PhoneNum = reader.GetValue(4).ToString();
               objClient.IsBusiness = Convert.ToBoolean(reader.GetValue(5).ToString());


            }
         }
         catch (Exception ex)
         {
            MessageBox.Show("Error getClientByNum: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
         return objClient;
      }

      public void addContractToDB(DateTime contractStart, string ClientID, string packageID, string ContractLevel)
      {
         string query = @"INSERT INTO ContractC  VALUES ( '" + contractStart + "', '" + contractStart + "', '" + ClientID + "', '" + packageID + "', '" + ContractLevel + "' )";

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
            MessageBox.Show("Error getAllServices: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
         return allServices;
      }

      public List<Call> getAllClientCallHistory(string clientID)
      {

         string query = @"SELECT * FROM Calls WHERE ClientID = ('" + clientID + "')";

         Call objCall = new Call();
         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);
         List<Call> allCall = new List<Call>();


         try
         {

            reader = command.ExecuteReader();
            while (reader.Read())
            {

               objCall.CallID = reader.GetValue(0).ToString();
               objCall.CallDate = Convert.ToDateTime(reader.GetValue(1));
               objCall.CallDuration = reader.GetValue(2).ToString();
               objCall.ClientID = reader.GetValue(3).ToString();

               allCall.Add(new Call(objCall.CallID, objCall.CallDate, objCall.CallDuration, objCall.ClientID));
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show("Error getAllClientCallHistory: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
         return allCall;
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
            MessageBox.Show("Error getAllPackages: " + ex.Message);
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
            MessageBox.Show("Error getServicesForPackage: " + ex.Message);
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
            MessageBox.Show("Error getSinglePackageServices: " + ex.Message);
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
            MessageBox.Show("Error getPackageID: " + ex.Message);
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
               objPackage.Services = getServicesForPackage(id);

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
            MessageBox.Show("Error getServiceID: " + ex.Message);
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
            MessageBox.Show("Error Update: " + ex.Message);
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
            MessageBox.Show("Error checkLogin: " + ex.Message);
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
            MessageBox.Show("Error getClient: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }

         return loggedClient;
      }


      public string getContractLevel(string clientID, string serviceType)
      {
         string query = @"SELECT ContractLevel FROM ContractC WHERE ClientID = ('" + clientID + "') AND PackageID IN (SELECT PackageID FROM Service_Packages WHERE ServiceID IN (SELECT ServiceID FROM ServiceC WHERE ServiceType = ('" + serviceType + "')))";

         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);
         string contractLevel = "";

         try
         {

            reader = command.ExecuteReader();
            if (reader.Read())
            {

               contractLevel = reader.GetValue(0).ToString();

            }

         }
         catch (Exception ex)
         {
            MessageBox.Show("Error getContractLevel: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }

         return contractLevel;
      }

      public List<string> getTypesOfSerivesAvailable(string packageID)
      {


         string query = @"SELECT DISTINCT ServiceType FROM ServiceC WHERE ServiceID IN (SELECT ServiceID FROM Service_Packages WHERE PackageID = '" + packageID + "')";

         string serviceType;
         conn = new SqlConnection(connect);
         conn.Open();

         command = new SqlCommand(query, conn);
         List<string> listOfServiceTypes = new List<string>();


         try
         {

            reader = command.ExecuteReader();
            while (reader.Read())
            {

               serviceType = reader.GetValue(0).ToString();
               listOfServiceTypes.Add(serviceType);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show("Error getTypesOfSerivesAvailable: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
         return listOfServiceTypes;
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

               int contractID = int.Parse(reader.GetValue(0).ToString());
               objContract.StartDate = Convert.ToDateTime(reader.GetValue(1).ToString());
               string packageID = reader.GetValue(3).ToString();
               objContract.ContractLevel = reader.GetValue(4).ToString();
               objContract.cPackage = getPackageByID(packageID);
               contractsForClient.Add(new Contract(contractID, objContract.ContractLevel, objContract.StartDate, objContract.cPackage));

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

      public void IncrementDayDecrementBufferInDB(Schedule scheduleToUpdate)
      {
         string query = @"UPDATE Schedule SET ScheduleDate = ('" + scheduleToUpdate.Date + "'), sBuffer = ('" + scheduleToUpdate.Buffer + "') WHERE ScheduleID = ('" + scheduleToUpdate.ScheduleID + "')";
         conn = new SqlConnection(connect);
         conn.Open();
         command = new SqlCommand(query, conn);

         try
         {
            command.ExecuteNonQuery();
         }
         catch (Exception ex)
         {
            MessageBox.Show("Error IncrementDayDecrementBufferInDB: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
      }

      public List<Service> getSchedulesFromToday()
      {
         SqlConnection conn = new SqlConnection(connect);
         SqlCommand command;
         SqlDataReader reader;

         string query = @"SELECT * FROM Schedule WHERE ScheduleDate >= ";

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
            MessageBox.Show("Error getSchedulesFromToday: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
         return allServices;
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

      public List<Schedule> getAllSchedules()
      {
         string query = @"SELECT * FROM Schedule";

         Schedule objSchedule = new Schedule();
         conn = new SqlConnection(connect);

         conn.Open();

         command = new SqlCommand(query, conn);
         List<Schedule> AllSchedules = new List<Schedule>();


         try
         {
            reader = command.ExecuteReader();
            while (reader.Read())
            {
               objSchedule.ScheduleID = reader.GetValue(0).ToString();
               objSchedule.Date = reader.GetDateTime(1);
               objSchedule.WorkRequestID = reader.GetValue(2).ToString();
               objSchedule.Technician = int.Parse(reader.GetValue(3).ToString());
               objSchedule.Buffer = int.Parse(reader.GetValue(4).ToString());
               objSchedule.Client = getClientIDInWorkRequest(objSchedule.WorkRequestID);

               AllSchedules.Add(objSchedule);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show("Error getAllSchedules: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
         return AllSchedules;
      }
      public string getClientIDInWorkRequest(string workrequestID)
      {
         string query = @"SELECT ClientID FROM WorkRequest WHERE WorkRequestID = ('" + workrequestID + "')";
         conn2 = new SqlConnection(connect);
         conn2.Open();
         command2 = new SqlCommand(query, conn2);
         string clientID = "";
         try
         {
            reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
               clientID = reader2.GetValue(0).ToString();
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show("Error getClientIDInWorkRequest: " + ex.Message);
         }
         finally
         {
            conn2.Close();
         }
         return clientID;
      }
      public string getTechnicianID()
      {
         string query = @"SELECT * FROM Technician";
         conn = new SqlConnection(connect);
         conn.Open();
         command = new SqlCommand(query, conn);

         List<String> allTechnicians = new List<String>();
         var random = new Random();
         string technicianID = "";
         try
         {
            reader = command.ExecuteReader();
            while (reader.Read())
            {
               allTechnicians.Add(reader.GetValue(0).ToString());
            }
            technicianID = allTechnicians[random.Next(allTechnicians.Count)];
         }
         catch (Exception ex)
         {
            MessageBox.Show("Error getTechnicianID: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
         return technicianID;
      }
      public void addScheduleToDB(Schedule schedule)
      {
         string query = @"INSERT INTO Schedule (ScheduleID,ScheduleDate,WorkRequestID,technicianID,sBuffer) VALUES ( '" + schedule.ScheduleID + "', '" + schedule.Date + "', '" + schedule.WorkRequestID + "', '" + schedule.Technician + "', '" + schedule.Buffer + "')";
         conn = new SqlConnection(connect);
         conn.Open();
         command = new SqlCommand(query, conn);

         try
         {
            command.ExecuteNonQuery();
            MessageBox.Show("Work request scheduled!");
         }
         catch (Exception ex)
         {
            MessageBox.Show("details of new work request not scheduled: " + ex.Message);
         }
         finally
         {
            conn.Close();
         }
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



