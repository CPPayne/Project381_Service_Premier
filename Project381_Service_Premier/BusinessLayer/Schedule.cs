using System;
using System.Collections.Generic;
using System.Text;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{
    class Schedule
    {
        private DateTime date;
        private string client;
        private string technician;
        private string scheduleID;
        private int buffer;
        private int workRequestID;

        public Schedule(DateTime dateStart, string client, string technician, string scheduleID, int buffer, int workRequestID)
        {

            this.date = dateStart;
            this.Client = client;
            this.Technician = technician;
            this.ScheduleID = scheduleID;
            this.Buffer = buffer;
            this.WorkRequestID = workRequestID;
        }

        public DateTime Date { get => date; set => date = value; }
        internal string Client { get => client; set => client = value; }
        internal string Technician { get => technician; set => technician = value; }
        public string ScheduleID { get => scheduleID; set => scheduleID = value; }
        public int Buffer { get => buffer; set => buffer = value; }
        public int WorkRequestID { get => workRequestID; set => workRequestID = value; }

        public Schedule() { }

        public void addScheduleToDB()
        {

        }


        //private void SortSchedules(List<Schedule> allSchedules)
        //{
        //    FileHandler fh = new FileHandler();

        //    for (int i = 0; i < 50; i++)
        //    {
        //        List<Schedule> tempSchedules = new List<Schedule>();
        //        foreach (Schedule schedule in allSchedules)
        //        {
        //            if (schedule.date.Day == i)
        //            {
        //                tempSchedules.Add(schedule);
        //                allSchedules.Remove(schedule);
        //            }
        //        }
        //        tempSchedules.Sort((x, y) => x.Buffer.CompareTo(y.Buffer));

        //        while (tempSchedules.Count > 6)
        //        {
        //            tempSchedules[tempSchedules.Count - 1].date.AddDays(1);
        //            tempSchedules[tempSchedules.Count - 1].Buffer--;
        //            //verify buffer > 0
        //            fh.IncrementDayDecrementBufferInDB(tempSchedules[tempSchedules.Count - 1]);
        //        }
        //        allSchedules.AddRange(tempSchedules);
        //    }
        //    allSchedules.Sort((x, y) => x.Day.CompareTo(y.Day));
        //}


        public void GenerateClientID()
        {
            int max = 10000;
            int min = 1;
            string completeID;
            DateTime date = DateTime.Now;
            Random rndLetter = new Random();
            FileHandler fh = new FileHandler();

            string FirstLetter = date.Month == 1 ? "Ja" : date.Month == 2 ? "F" : date.Month == 3 ? "M" : date.Month == 4 ? "A" :
                date.Month == 5 ? "M" : date.Month == 6 ? "Ju" : date.Month == 7 ? "Jl" : date.Month == 8 ? "Au" : date.Month == 9 ? "S" :
                date.Month == 10 ? "O" : date.Month == 11 ? "N" : date.Month == 12 ? "D" : "Not a Month";

            while (true)
            {
                completeID = "";


                completeID = pad_an_int(rndLetter.Next(min, max), 5);

                completeID = FirstLetter + completeID;

                if (!fh.checkIfScheduleIdExists(completeID))
                {
                    this.scheduleID = completeID;
                    break;
                }
            }



        }

        private static string pad_an_int(int N, int P)
        {

            string s = "{0:";
            for (int i = 0; i < P; i++)
            {
                s += "0";
            }
            s += "}";


            string value = string.Format(s, N);


            return value;
        }



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
