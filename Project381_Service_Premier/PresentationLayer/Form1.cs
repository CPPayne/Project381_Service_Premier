using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project381_Service_Premier.BusinessLayer;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        BindingSource sourceAllService = new BindingSource();
        BindingSource sourceAllPackages = new BindingSource();
        BindingSource sourceClientContracts = new BindingSource();
        BindingSource selectedPackageServicesToAdd = new BindingSource();

        BindingSource sourceServicePackage = new BindingSource();
        List<Service> allServices = new List<Service>();
        List<Package> allPackages = new List<Package>();
        List<Contract> clientContr = new List<Contract>();

      List<Service> servicesInPackage = new List<Service>();

        List<Contract> loggedClientContracts = new List<Contract>();

        Client loggedInClient = new Client();


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
        }

      private void btnAnswerCall_Click(object sender, EventArgs e)
      {

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
            txtAddPName.Clear();
            txtPackageCost.Clear();
            updatePakcageDBGRID();
         }
         else
         {
            MessageBox.Show("No Services Added");
         }


      }

      private void btnAddServiceToPackage_Click(object sender, EventArgs e)
      {
         Service selectedService = (Service)sourceAllService.Current;


         Service newService = new Service(selectedService.SName, selectedService.SType, selectedService.SSpecifications);
         //MessageBox.Show(newService.SType);
         servicesInPackage.Add(newService);
         //foreach(Service serv in servicesInPackage)
         //{
         //   MessageBox.Show(serv.SName);
         //}


         //updatePackageServiceDBGRID(servicesInPackage);
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
                tabControl1.SelectedTab = tpClientContract;
                txtloggedClientID.Text = loggedInClient.ClientID;
                txtLoggedName.Text = loggedInClient.Name;
                txtLoggedSurname.Text = loggedInClient.Surname;
                txtLoggedNumber.Text = loggedInClient.PhoneNum;
                updateDBGRIDContracts(loggedInClient.ClientID);
            }
        }

      private void btnMtoSchedule_Click(object sender, EventArgs e)
      {


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
    }
}
