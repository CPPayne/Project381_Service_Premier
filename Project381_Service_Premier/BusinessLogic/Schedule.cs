using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
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
   }
}
