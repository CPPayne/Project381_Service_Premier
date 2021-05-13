using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Project381_Service_Premier.BusinessLayer;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier
{
   public partial class Form1 : Form
   {

      private System.Timers.Timer t;
      private string _CallDuration;
      int h, m, s;

      public Form1()
      {
         InitializeComponent();
      }



      BindingSource sourceAllService = new BindingSource();
      BindingSource sourceAllPackages = new BindingSource();
      BindingSource sourceClientContracts = new BindingSource();
      BindingSource selectedPackageServicesToAdd = new BindingSource();

      BindingSource sourceServicePackage = new BindingSource();

      BindingSource sourceClientCallHistory = new BindingSource();

      List<Service> allServices = new List<Service>();
      List<Package> allPackages = new List<Package>();
      List<Contract> clientContr = new List<Contract>();
      List<Call> clientCallsHistory = new List<Call>();

      List<Service> servicesInPackage = new List<Service>();

      List<Contract> loggedClientContracts = new List<Contract>();

      Client loggedInClient = new Client();

      //=================================Calling Client and Simulation===================================================================
      string simulationNumber = "";
      Client clientCalling = new Client();
      List<string> listOfAllClientNumSimulation = new List<string>();
      List<Contract> callingClientContracts = new List<Contract>();
      List<string> availableServiceTypeForCallingContract = new List<string>();
      Call clientCALL = new Call();
      //=================================================================================================================================
      string pName;
      Decimal pCost;
      List<Service> servicesOfChosenPackages = new List<Service>();

      private void updateServiceDBGRID()
      {
         Service svc = new Service();

         allServices = svc.getAllServices();
         sourceAllService.DataSource = allServices;
         dgvServices.DataSource = sourceAllService;
      }


      private void updateServiceInPackage(List<Service> services)
      {
         selectedPackageServicesToAdd.DataSource = services;
         dgvServicesInPackage.DataSource = selectedPackageServicesToAdd;


      }

      private void updateDBGRIDContracts(string clientID)
      {
         Contract cnrt = new Contract();

         clientContr = cnrt.getContractsForClient(clientID);
         sourceClientContracts.DataSource = clientContr;
         dgvContracts.DataSource = sourceClientContracts;
      }
      private void updateServiceDBGRIDpS()
      {
         sourceAllService.DataSource = servicesOfChosenPackages;
         dgvServices.DataSource = sourceAllService;
      }

      private void updatePakcageDBGRID()
      {
         Package pgs = new Package();

         allPackages = pgs.getAllPackages();
         sourceAllPackages.DataSource = allPackages;
         dgvPackages.DataSource = sourceAllPackages;
      }

      private void updatePackageServiceDBGRID(List<Service> servicesInPackage)
      {

         sourceServicePackage.DataSource = servicesInPackage;
         dgvPackageServices.DataSource = sourceServicePackage;
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         updateServiceDBGRID();
         updatePakcageDBGRID();

         tabControl1.SelectedTab = tpMainMenu;

         cmbPackages.DataSource = allPackages;

         Client simulateNumClient = new Client();
         listOfAllClientNumSimulation = simulateNumClient.getListOfAllPhoneNumbers();

         t = new System.Timers.Timer();
         t.Interval = 1000; //1s
         t.Elapsed += OnTimeEvent;
      }

      private void OnTimeEvent(object sender, ElapsedEventArgs e)
      {
         Invoke(new Action(() =>
         {
            s += 1;

            if (s == 60)
            {
               s = 0;
               m += 1;
            }
            if (m == 60)
            {
               m = 0;
               h += 1;
            }
            txtCallDuration.Text = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
         }));
      }

      private void btnAnswerCall_Click(object sender, EventArgs e)
      {
         if (simulationNumber != "")
         {
            availableServiceTypeForCallingContract.Clear();
            txtCallDuration.ForeColor = Color.Green;
            t.Start();

            Contract getConctracts = new Contract();
            clientCalling = clientCalling.getClientByNumber(simulationNumber);
            callingClientContracts = getConctracts.getContractsForClient(clientCalling.ClientID);

            clientCALL.GenerateCallID();
            clientCALL.CallDate = DateTime.Now;
            clientCALL.ClientID = clientCalling.ClientID;



            clientCallsHistory = clientCALL.getClientCallHis(clientCalling.ClientID);
            sourceClientCallHistory.DataSource = clientCallsHistory;
            dgvCallCentrePack.DataSource = sourceClientCallHistory;
            sourceClientCallHistory.ResetBindings(false);

            txtName.Text = clientCalling.Name;
            txtSurname.Text = clientCalling.Surname;
            txtAddress.Text = clientCalling.Address;
            txtNumber.Text = clientCalling.PhoneNum;
            cbBusiness.Checked = clientCalling.IsBusiness;

            foreach (Contract contr in callingClientContracts)
            {
               //lsbConPackage.Items.Add(contr.cPackage);
               contr.getTypeOfServicesForContract();
               foreach (string item in contr.TypesOfServicesForContract)
               {
                  if (!availableServiceTypeForCallingContract.Contains(item))
                  {
                     availableServiceTypeForCallingContract.AddRange(contr.TypesOfServicesForContract);
                  }
               }

            }
            cbTypeOfProblems.DataSource = availableServiceTypeForCallingContract;
            txtCallDuration.ForeColor = Color.Black;
            txtCallDuration.Text = "00:00:00";


         }
         else
         {
            MessageBox.Show("No call to answer");
         }


      }

      private void btnDeletePackage_Click(object sender, EventArgs e)
      {

      }

      private void btnCompleteService_Click(object sender, EventArgs e)
      {
         string serviceType = txtAddServiceType.Text;
         string serviceName = txtAddServiceName.Text;
         string serviceDescription = rtbServiceSpecification.Text;

         Service newService = new Service(serviceName, serviceType, serviceDescription);
         newService.addServiceToDB();
         updateServiceDBGRID();

         txtAddServiceType.Clear();
         txtAddServiceName.Clear();
         rtbServiceSpecification.Clear();

      }

      private void btnViewAllServices_Click(object sender, EventArgs e)
      {
         updateServiceDBGRID();
      }

      private void btnCompletePackage_Click(object sender, EventArgs e)
      {

         string packageName = txtAddPName.Text;
         Decimal packageCost = Convert.ToDecimal(txtPackageCost.Text);

         if (servicesInPackage.Count != 0)
         {
            Package newPackage = new Package(packageName, packageCost, servicesInPackage);
            newPackage.addPackageToDB();


            servicesInPackage = new List<Service>();
            txtAddPName.Clear();
            txtPackageCost.Clear();
            updatePakcageDBGRID();

         }
         else
         {
            MessageBox.Show("No Services Added");
         }


      }

      private void updatedgvCallCentrePack()
      {
         Package pgs = new Package();

         //allPackages = pgs.getAllPackages();
         // sourceAllPackages.DataSource = allPa;
         //dgvPackages.DataSource = sourceAllPackages;
      }

      private void btnAddServiceToPackage_Click(object sender, EventArgs e)
      {
         Service selectedService = (Service)sourceAllService.Current;


         Service newService = new Service(selectedService.SName, selectedService.SType, selectedService.SSpecifications);

         servicesInPackage.Add(newService);

         sourceServicePackage.DataSource = servicesInPackage;
         dgvPackageServices.DataSource = sourceServicePackage;

         sourceServicePackage.ResetBindings(false);
         pName = null;
         pCost = 0;
         servicesOfChosenPackages.Clear();

      }

      private void dgvServices_SelectionChanged(object sender, EventArgs e)
      {

      }

      private void btnDeleteServices_Click(object sender, EventArgs e)
      {
         List<Service> dispServ = new List<Service>();
         FileHandler fh = new FileHandler();

         //MessageBox.Show(fh.Search("test"));
         MessageBox.Show(fh.getServiceID("Telephone maintenance"));
      }

      private void dgvPackages_SelectionChanged(object sender, EventArgs e)
      {
         Package selectedPackage = (Package)sourceAllPackages.Current;
         pName = selectedPackage.PackageName;
         pCost = selectedPackage.Cost;
         selectedPackage.getPackageServices();
         servicesOfChosenPackages = selectedPackage.Services;

         updateServiceDBGRIDpS();
      }

      private void btnRegister_Click(object sender, EventArgs e)
      {
         //string[] _registerClientDetails = { "username", "password", "name", "surname", "address", "Phone Number" };

         //Boolean _isEmpty = false;

         string username = txtClientUsername.Text;
         string password = txtClientPassword.Text;
         string name = txtClientName.Text;
         string surname = txtClientSurname.Text;
         string address = txtClientAddress.Text;
         string phoneNumber = txtPhoneNumber.Text;
         bool isBusiness = cbxBusiness.Checked;





         Client client = new Client(name, surname, phoneNumber, address, isBusiness, username, password);
         client.GenerateClientID();
         client.addClientToDB();
         //MessageBox.Show(client.ClientID);


         txtClientName.Clear();
         txtClientSurname.Clear();
         txtClientAddress.Clear();
         txtPhoneNumber.Clear();
         txtClientUsername.Clear();
         txtClientPassword.Clear();

      }

      private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
      {

      }

      private void btnMenuCLogin_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpLogin;
      }

      private void textBox3_TextChanged(object sender, EventArgs e)
      {

      }

      private void label48_Click(object sender, EventArgs e)
      {

      }

      private void btnMenuCRegister_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpRegister;
      }

      private void button1_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpMainMenu;
      }

      private void button2_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpMainMenu;
      }

      private void btnLogin_Click(object sender, EventArgs e)
      {
         string usernameInput = txtLoginUsername.Text;
         string passwordInput = txtLoginPassword.Text;

         bool logged = loggedInClient.login(usernameInput, passwordInput);

         if (logged)
         {

            txtloggedClientID.Text = loggedInClient.ClientID;
            txtLoggedName.Text = loggedInClient.Name;
            txtLoggedSurname.Text = loggedInClient.Surname;
            txtLoggedNumber.Text = loggedInClient.PhoneNum;
            updateDBGRIDContracts(loggedInClient.ClientID);




            if (loggedInClient.IsBusiness)
            {
               rb4.Enabled = true;
               rb5.Enabled = true;
               rb4.Visible = true;
               rb5.Visible = true;
            }
            else
            {
               rb4.Enabled = false;
               rb5.Enabled = false;
               rb4.Visible = false;
               rb5.Visible = false;
            }

            txtLoginPassword.Clear();
            txtLoginUsername.Clear();
            tabControl1.SelectedTab = tpClientLoggedMenu;
         }
      }

      private void btnMtoSchedule_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpSchedule;

      }

      private void btnLogout_Click(object sender, EventArgs e)
      {
         loggedInClient = new Client();
         txtloggedClientID.Clear();
         txtLoggedName.Clear();
         txtLoggedSurname.Clear();
         txtLoggedNumber.Clear();
         tabControl1.SelectedTab = tpMainMenu;
      }

      private void cmbPackages_SelectedIndexChanged(object sender, EventArgs e)
      {

         Package selectedPackageToAdd = new Package();
         selectedPackageToAdd.PackageName = cmbPackages.Text;
         selectedPackageToAdd.setPackageCost();
         selectedPackageToAdd.getPackageServices();
         updateServiceInPackage(selectedPackageToAdd.Services);
      }

      private void btnCreateContract_Click(object sender, EventArgs e)
      {
         string packageName = cmbPackages.Text;
         string contractLevel = rb1.Checked ? "1" : (rb2.Checked ? "2" : (rb3.Checked ? "3" : (rb4.Checked ? "4" : "5")));
         DateTime startDate = DTPClientContract.Value.Date;

         Contract newContract = new Contract();
         Package tempPack = new Package();
         string packageID = tempPack.getPackageID(packageName);

         newContract.ContractLevel = contractLevel;
         newContract.StartDate = startDate;

         newContract.addContractToDB(loggedInClient.ClientID, packageID);

         updateDBGRIDContracts(loggedInClient.ClientID);
      }
      Random rndNum = new Random();

      private void btnLogDetails_Click(object sender, EventArgs e)
      {

         string typeOfProblem = cbTypeOfProblems.Text;
         string description = rtbDescription.Text;
         WorkRequest newWorkrequest = new WorkRequest(typeOfProblem, description, clientCALL.CallID, clientCalling.ClientID, DateTime.Now);
         newWorkrequest.GenerateWorkRequestID();
         newWorkrequest.addWorkRequestToDB();
         Schedule schedule = new Schedule();
         schedule.addWorkRequestToSchedule(newWorkrequest);
         schedule.SortSchedules();
      }

      private void btnClientToContract_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpClientContract;
      }

      private void btnClientMTClientF_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpFeedback;
      }

      private void btnClientMTMainM_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpMainMenu;
      }

      private void btnFeedback_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpFeedback;
      }

      private void btnClienFTMainM_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpClientLoggedMenu;
      }

      private void btnNextClient_Click(object sender, EventArgs e)
      {

      }

      private void btnClientCClientM_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpClientLoggedMenu;
      }

      private void btnMainMTCallC_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpCallCentre;
      }

      private void btnMainMTPackM_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpPackageManagement;
      }

      private void btnMainMenuCmain_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpMainMenu;
      }

      private void btnMainMenuCCentre_Click(object sender, EventArgs e)
      {
         tabControl1.SelectedTab = tpMainMenu;
      }

        private void btnGenerateSchedule_Click(object sender, EventArgs e)
        {
            Schedule sch = new Schedule();


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FileHandler fh = new FileHandler();
            List<Schedule> allSchedules = fh.getAllSchedules();
            for (int i = 0; i < 50; i++)
            {
                //allSchedules = fh.getAllSchedules();
                List<Schedule> tempSchedules = new List<Schedule>();
                foreach (Schedule schedule in allSchedules)
                {
                    if (schedule.Date.Day == i)
                    {
                        tempSchedules.Add(schedule);
                        //allSchedules.Remove(schedule);
                        //MessageBox.Show($"For day {i} the date is {schedule.Date} and the buffer is {schedule.Buffer}");

                    }
                }


                ////List<Schedule> orderedTempList = tempSchedules.OrderBy(x => x.buffer).ToList();
                ////List<Order> SortedList = objListOrder.OrderByDescending(o => o.OrderDate).ToList();
                List<Schedule> orderedTempList = tempSchedules.OrderBy(o => o.Buffer).ToList();

                foreach (Schedule s in orderedTempList)
                {
                    MessageBox.Show(s.Buffer.ToString());
                }


                //while (orderedTempList.Count > 6)
                //{
                //    orderedTempList[orderedTempList.Count - 1].Date.AddDays(1);
                //    if (orderedTempList[orderedTempList.Count - 1].Buffer > 0)
                //    {
                //        orderedTempList[orderedTempList.Count - 1].Buffer--;
                //    }

                //    fh.IncrementDayDecrementBufferInDB(orderedTempList[orderedTempList.Count - 1]);
                //}
                ////allSchedules.AddRange(tempSchedules);
            }
            //allSchedules.Sort((x, y) => x.date.Day.CompareTo(y.date.Day));
        }

        private void btnSimCall_Click(object sender, EventArgs e)
      {

         int randomNum = rndNum.Next(0, listOfAllClientNumSimulation.Count);

         simulationNumber = listOfAllClientNumSimulation[randomNum];

         lblCaller.Text = "CALL INCOMING: " + simulationNumber;
      }

      private void btnEndCall_Click(object sender, EventArgs e)
      {
         txtCallDuration.ForeColor = Color.Red;
         t.Stop();
         _CallDuration = txtCallDuration.Text;

         MessageBox.Show("The call has ended.");
         txtCallDuration.ForeColor = Color.Black;


         s = 0;
         m = 0;
         h = 0;



         clientCALL.CallDuration = _CallDuration;

         clientCALL.addCallToDB();

      }
   }
}
