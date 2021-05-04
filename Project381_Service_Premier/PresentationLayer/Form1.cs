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
        BindingSource sourcePackage = new BindingSource();
        BindingSource sourceServicePackage = new BindingSource();
        List<Service> allServices = new List<Service>();


        List<Service> servicesInPackage = new List<Service>();

        private void updateServiceDBGRID()
        {
            Service svc = new Service();

            allServices = svc.getAllServices();
            sourcePackage.DataSource = allServices;
            dgvServices.DataSource = sourcePackage;
        }

        private void updatePackageServiceDBGRID()
        {

            sourceServicePackage.DataSource = servicesInPackage;
            dgvPackageServices.DataSource = sourceServicePackage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateServiceDBGRID();
            updatePackageServiceDBGRID();
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
            double packageCost = double.Parse(txtPackageCost.Text);

            if (servicesInPackage.Count != 0)
            {
                Package newPackage = new Package(packageName, packageCost, servicesInPackage);
            }
            else {
                MessageBox.Show("No Services Added");
            }
            

        }

        private void btnAddServiceToPackage_Click(object sender, EventArgs e)
        {



            updatePackageServiceDBGRID();
        }
    }
}
