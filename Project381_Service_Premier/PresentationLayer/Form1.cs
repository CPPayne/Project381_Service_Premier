using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project381_Service_Premier.BusinessLogic;


namespace Project381_Service_Premier.PresentationLayer
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void button1_Click(object sender, EventArgs e)
      {

      }

      private void label6_Click(object sender, EventArgs e)
      {

      }

      private void textBox4_TextChanged(object sender, EventArgs e)
      {

      }

      private void tabPage9_Click(object sender, EventArgs e)
      {

      }

      private void tabPage12_Click(object sender, EventArgs e)
      {

      }

      private void button10_Click(object sender, EventArgs e)
      {

      }

      private void button8_Click(object sender, EventArgs e)
      {
            string serviceName = tbServiceName.Text;
            string serviceType = tbServiceType.Text;
            string serviceSpec = rtbServiceSpec.Text;

            Service nService = new Service(serviceType, serviceName, serviceSpec);
            nService.addServiceToDB();

      }

      private void radioButton2_CheckedChanged(object sender, EventArgs e)
      {

      }

      private void rbtnBusiness_CheckedChanged(object sender, EventArgs e)
      {
         //tbpIndividual.Hide();
         //tbpBusiness.Show();
      }

      private void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
      {
         //tbpBusiness.Hide();
         //tbpIndividual.Show();
      }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dbgService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
