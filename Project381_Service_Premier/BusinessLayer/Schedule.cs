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

      public TimeSpan Duration { get => duration; set => duration = value; }
      public DateTime AppointmentStart { get => appointmentStart; set => appointmentStart = value; }
      public Client Client { get => client; set => client = value; }
      public Technician Technician { get => technician; set => technician = value; }
      public int Day { get => day; set => day = value; }
      public int Buffer { get => buffer; set => buffer = value; }

      public Schedule(TimeSpan duration, DateTime appointmentStart, Client client, Technician technician)
      {
         this.duration = duration;
         this.appointmentStart = appointmentStart;
         this.client = client;
         this.technician = technician;
      }

      public Schedule() { }

      public void CalcualteAppointmentTime()
      {

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


      private int day;
      private int buffer;

      private void SortSchedules()
      {
         FileHandler fh = new FileHandler();
         List<Schedule> AllSchedules = new List<Schedule>();
         for (int i = 0; i < 50; i++)
         {
            List<Schedule> tempSchedules = new List<Schedule>();
            foreach (Schedule schedule in AllSchedules)
            {
               if (schedule.day == i)
               {
                  tempSchedules.Add(schedule);
                  AllSchedules.Remove(schedule);
               }
            }
            tempSchedules.Sort((x, y) => x.buffer.CompareTo(y.buffer));

            while (tempSchedules.Count > 6)
            {
               tempSchedules[tempSchedules.Count - 1].day++;
               tempSchedules[tempSchedules.Count - 1].buffer--;
               //verify buffer > 0
               fh.IncrementDayDecrementBuffer();
            }
            AllSchedules.AddRange(tempSchedules);
         }
         AllSchedules.Sort((x, y) => x.day.CompareTo(y.day));
      }

   }
}
