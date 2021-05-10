using System;
using System.Collections.Generic;
using System.Text;
using Project381_Service_Premier.DataAccessLayer;

namespace Project381_Service_Premier.BusinessLayer
{
   class Schedule
   {
      private TimeSpan duration;
      private DateTime appointmentStart;
      private Client client;
      private Technician technician;
      private int scheduleID;
      private int day;
      private int buffer;

        public TimeSpan Duration { get => duration; set => duration = value; }
        public DateTime AppointmentStart { get => appointmentStart; set => appointmentStart = value; }
        internal Client Client { get => client; set => client = value; }
        internal Technician Technician { get => technician; set => technician = value; }
        public int ScheduleID { get => scheduleID; set => scheduleID = value; }
        public int Day { get => day; set => day = value; }
        public int Buffer { get => buffer; set => buffer = value; }

        public Schedule() { }

        public Schedule(TimeSpan duration, DateTime appointmentStart, Client client, Technician technician, int scheduleID, int day, int buffer)
        {
            this.Duration = duration;
            this.AppointmentStart = appointmentStart;
            this.Client = client;
            this.Technician = technician;
            this.ScheduleID = scheduleID;
            this.Day = day;
            this.Buffer = buffer;
        }

        public void CalcualteAppointmentTime()
      {
         
      }


        private void SortSchedules()
        {
            FileHandler fh = new FileHandler();
            List<Schedule> AllSchedules = new List<Schedule>();
            for (int i = 0; i < 50; i++)
            {
                List<Schedule> tempSchedules = new List<Schedule>();
                foreach (Schedule schedule in AllSchedules)
                {
                    if (schedule.Day == i)
                    {
                        tempSchedules.Add(schedule);
                        AllSchedules.Remove(schedule);
                    }
                }
                tempSchedules.Sort((x, y) => x.Buffer.CompareTo(y.Buffer));

                while (tempSchedules.Count > 6)
                {
                    tempSchedules[tempSchedules.Count - 1].Day++;
                    tempSchedules[tempSchedules.Count - 1].Buffer--;
                    //verify buffer > 0
                    fh.IncrementDayDecrementBuffer();
                }
                AllSchedules.AddRange(tempSchedules);
            }
            AllSchedules.Sort((x, y) => x.Day.CompareTo(y.Day));
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
