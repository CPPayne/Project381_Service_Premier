using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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



      private void btnAddServiceP_Click(object sender, EventArgs e)
      {

      }
      //click op die datagridview Service en display wat ookal geclick word in txt boxes
      private void dbgService_CellContentClick(object sender, DataGridViewCellEventArgs e)
      {
         if (e.RowIndex != -1)
         {
            DataGridViewRow dgvRow = dgvService.Rows[e.RowIndex];
            tbServiceType.Text = dgvRow.Cells[0].Value.ToString();
            tbServiceName.Text = dgvRow.Cells[1].Value.ToString();
            rtbServiceSpec.Text = dgvRow.Cells[2].Value.ToString();
         }
      }

      void ServiceGridFill()
      {
         SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog= servicePremierDB; Integrated Security = SSPI");

         string query = @"SELECT * FROM ServiceC";

         SqlDataAdapter sda = new SqlDataAdapter(query, conn);

         DataSet ds = new DataSet();

         sda.Fill(ds, "ServiceC");

         dgvService.DataSource = ds.Tables[0];
      }

      //click op die datagridview Package en display wat ookal geclick word in txt boxes
      private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
      {
         if (e.RowIndex != -1)
         {
            //DataGridViewRow dgvRow = dgvPackage.Rows[e.RowIndex];
            //txtPackageN.Text = dgvRow.Cells[1].Value.ToString();
            //txtPackageN.Text = dgvRow.Cells[2].Value.ToString();
         }
      }

      void PackageGridFill()
      {
         SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog= servicePremierDB; Integrated Security = SSPI");

         string query = @"SELECT * FROM PPackage";

         SqlDataAdapter sda = new SqlDataAdapter(query, conn);

         DataSet ds = new DataSet();

         sda.Fill(ds, "PPackage");

         //dgvPackage.DataSource = ds.Tables[0];
      }
   }
}
