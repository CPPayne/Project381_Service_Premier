using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using Project381_Service_Premier.BusinessLogic;

namespace Project381_Service_Premier.DataAccess
{
    class FileHandler
    {
        public FileHandler()
        {
        }


        //Set connection string
        string connect = "Data Source=.; Initial Catalog= premierServiceDB; Integrated Security= SSPI";
        SqlConnection conn;     //Declare SqlConnection object
        SqlCommand command;     //Declare SqlCommand object
        SqlDataReader reader;   //Declare SqlDataReader object

        //Declare Student object
        //Service objStudent = new Student();

        //Register method
        public void addService(string sType, string sName, string sSpecifications)
        {
            string query = @"INSERT INTO ServiceC VALUES ( '" + sType + "', '" + sName + "', '" + sSpecifications + "' )";

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
        public void addPackage(string packageID, string pName, double pCost, List<Service> packageServices)
        {
            string query = @"INSERT INTO pPackage VALUES ( '" + packageID + "', '" + pName + "','" + pCost + "' )";

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

        //Delete method
        public void Delete(int sID)
        {
            string query = @"DELETE FROM Students WHERE StudentID = ('" + sID + "')";

            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Deleted the details of student with Student Number: " + sID);
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

        //Search method
        public List<Student> Search(int sID)
        {
            //query select all collumns provided stdID = value

            string query = @"SELECT * FROM Students WHERE StudentID = ('" + sID + "')";

            //connect
            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);
            List<Student> myStudent = new List<Student>();

            //run query command
            try
            {
                //read data SqlDataReader
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    //Store each collumn value in student class variables/field
                    objStudent.StudentID = int.Parse(reader[0].ToString());
                    objStudent.StudentName = reader[1].ToString();
                    objStudent.StudentSurname = reader[2].ToString();
                    objStudent.CourseID = reader[3].ToString();

                    //Add field values to a Student type list
                    myStudent.Add(new Student(objStudent.StudentID, objStudent.StudentName, objStudent.StudentSurname, objStudent.CourseID));
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

            return myStudent;
        }

        public string getPackageID(string name)
        {


            string query = @"SELECT * FROM pPackage WHERE PackageName = ('" + name + "')";

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
            conn = new SqlConnection(connect);

            conn.Open();

            command = new SqlCommand(query, conn);

            string ServiceID = "";
            //run query command
            try
            {
                //read data SqlDataReader
                reader = command.ExecuteReader();
                if (reader.Read())
                {

                    ServiceID = reader[0].ToString();



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
            return ServiceID;
        }

        //Update method
        public void Update(int sID, string sName, string sSurnane, string cID)
        {
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

        public List<Call> GetClientCallHistory()
        {

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
