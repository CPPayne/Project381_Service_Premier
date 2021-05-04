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
        BindingSource source = new BindingSource();
        List<Service> allServices = new List<Service>();

        private void updateServiceDBGRID()
        {
            Service svc = new Service();

            allServices = svc.getAllServices();
            source.DataSource = allServices;
            dgvServices.DataSource = source;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateServiceDBGRID();
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
    }
}
