using System;
using System.Collections.Generic;
using System.Text;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{
   class Schedule
   {
        private DateTime date;
        private Client client;
        private Technician technician;
        private int scheduleID;
        private int day;
        private int buffer;


        public Schedule(TimeSpan duration, DateTime appointmentStart, Client client, Technician technician, int scheduleID, int day, int buffer)
        {

            this.date = appointmentStart;
            this.Client = client;
            this.Technician = technician;
            this.ScheduleID = scheduleID;
            this.Buffer = buffer;
        }

        public DateTime Date { get => date; set => date = value; }
        internal Client Client { get => client; set => client = value; }
        internal Technician Technician { get => technician; set => technician = value; }
        public int ScheduleID { get => scheduleID; set => scheduleID = value; }
        public int Day { get => day; set => day = value; }
        public int Buffer { get => buffer; set => buffer = value; }

        public Schedule() { }



        public void CalcualteAppointmentTime()
      {

      }


        private void SortSchedules(List<Schedule> allSchedules)
        {
            FileHandler fh = new FileHandler();
         
            for (int i = 0; i < 50; i++)
            {
                List<Schedule> tempSchedules = new List<Schedule>();
                foreach (Schedule schedule in allSchedules)
                {
                    if (schedule.Day == i)
                    {
                        tempSchedules.Add(schedule);
                        allSchedules.Remove(schedule);
                    }
                }
                tempSchedules.Sort((x, y) => x.Buffer.CompareTo(y.Buffer));

                while (tempSchedules.Count > 6)
                {
                    tempSchedules[tempSchedules.Count - 1].date.AddDays(1);
                    tempSchedules[tempSchedules.Count - 1].Buffer--;
                    //verify buffer > 0
                    fh.IncrementDayDecrementBufferInDB(tempSchedules[tempSchedules.Count - 1]);
                }
                allSchedules.AddRange(tempSchedules);
            }
            allSchedules.Sort((x, y) => x.Day.CompareTo(y.Day));
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
