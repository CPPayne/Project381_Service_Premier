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

        BindingSource sourceServicePackage = new BindingSource();
        List<Service> allServices = new List<Service>();
        List<Package> allPackages = new List<Package>();

        List<Service> servicesInPackage = new List<Service>();

        string sType;
        string sName;
        string sSpec;

        private void updateServiceDBGRID()
        {
            Service svc = new Service();

            allServices = svc.getAllServices();
            sourceAllService.DataSource = allServices;
            dgvServices.DataSource = sourceAllService;
        }
        private void updatePakcageDBGRID()
        {
            Package pgs = new Package();

            allPackages = pgs.getAllPackages();
            sourceAllPackages.DataSource = allPackages;
            dgvPackages.DataSource = sourceAllPackages;
        }

        private void updatePackageServiceDBGRID()
        {

            sourceServicePackage.DataSource = servicesInPackage;
            dgvPackageServices.DataSource = sourceServicePackage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateServiceDBGRID();
            updatePakcageDBGRID();
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

            Service newService = new Service(serviceType, serviceName, serviceDescription);
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
            
            string packageName = txtPackageName.Text;
            Decimal packageCost = Convert.ToDecimal(txtPackageCost.Text);

            if (servicesInPackage.Count != 0)
            {
                Package newPackage = new Package(packageName, packageCost, servicesInPackage);
                newPackage.addPackageToDB();
            }
            else {
                MessageBox.Show("No Services Added");
            }
            

        }

        private void btnAddServiceToPackage_Click(object sender, EventArgs e)
        {
            Service newService = new Service(sType, sName, sSpec);
            servicesInPackage.Add(newService);
            foreach(Service serv in servicesInPackage)
            {
                MessageBox.Show(serv.SType);
            }

            //sourceServicePackage.DataSource = servicesInPackage;
            //dgvPackageServices.DataSource = sourceServicePackage;
            //updatePackageServiceDBGRID();
        }

        private void dgvServices_SelectionChanged(object sender, EventArgs e)
        {
            Service selectedService = (Service)sourceAllService.Current;
            sType = selectedService.SType;
            sName = selectedService.SName;
            sSpec = selectedService.SSpecifications;
        }

        private void btnDeleteServices_Click(object sender, EventArgs e)
        {
            Service NS = new Service(sType, sName, sSpec);
            MessageBox.Show(NS.getID());
        }
    }
}
