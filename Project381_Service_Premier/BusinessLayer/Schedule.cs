﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Project381_Service_Premier.DataAccessLayer;
using System.Linq;

namespace Project381_Service_Premier.BusinessLayer
{
   class Schedule 
   {
      private DateTime date;
      private string clientID;
      private int technicianID;
      private string scheduleID;
      private int buffer;
      private string workRequestID;

      public Schedule(DateTime dateStart, string client, int technician, string scheduleID, int buffer, string workRequestID)
      {

         this.date = dateStart;
         this.Client = client;
         this.Technician = technician;
         this.ScheduleID = scheduleID;
         this.Buffer = buffer;
         this.WorkRequestID = workRequestID;
      }


      public DateTime Date { get => date; set => date = value; }
      internal string Client { get => clientID; set => clientID = value; }
      internal int Technician { get => technicianID; set => technicianID = value; }
      public string ScheduleID { get => scheduleID; set => scheduleID = value; }
      public int Buffer { get => buffer; set => buffer = value; }
      public string WorkRequestID { get => workRequestID; set => workRequestID = value; }

      public Schedule() { }

      public void addWorkRequestToSchedule(WorkRequest workrequest)
      {
         this.date = workrequest.DateCreated;
         this.clientID = workrequest.ClientID;
         GenerateScheduleID();
         this.buffer = calculateBuffer(workrequest.ProblemType);
         this.workRequestID = workrequest.WorkRequestID;
         this.technicianID = assignTechnician();

         addScheduleToDB(this);
      }

      public int assignTechnician()
      {
         FileHandler fh = new FileHandler();
         return int.Parse(fh.getTechnicianID());
      }

      public void addScheduleToDB(Schedule schedule)
      {
         FileHandler fh = new FileHandler();
         fh.addScheduleToDB(schedule);
      }

      public int calculateBuffer(string serviceType)
      {
         FileHandler fh = new FileHandler();
         string contractLevel = fh.getContractLevel(clientID, serviceType);
         /*level 1 = 7
          * level 2 = 5
          * level 3 = 3
          * level 4 = 2
          * level 5 = 1
          */
         int buffer = contractLevel == "1" ? 7 : (contractLevel == "2" ? 5 : (contractLevel == "3" ? 3 : (contractLevel == "4" ? 2 : 1)));

         return buffer;
      }


      public void SortSchedules()
      {
         FileHandler fh = new FileHandler();
         List<Schedule> allSchedules = fh.getAllSchedules();
         for (int i = 0; i < 50; i++)
         {
            //allSchedules = fh.getAllSchedules();
            List<Schedule> tempSchedules = new List<Schedule>();
            foreach (Schedule schedule in allSchedules)
            {
               if (schedule.date.Day == i)
               {
                  tempSchedules.Add(schedule);
                  //allSchedules.Remove(schedule);
               }
            }
            List<Schedule> orderedTempList = tempSchedules.OrderBy(x => x.buffer).ToList();

            while (tempSchedules.Count > 6)
            {
               tempSchedules[tempSchedules.Count - 1].date.AddDays(1);
               if (tempSchedules[tempSchedules.Count - 1].Buffer > 0)
               {
                  tempSchedules[tempSchedules.Count - 1].Buffer--;
               }

               fh.IncrementDayDecrementBufferInDB(tempSchedules[tempSchedules.Count - 1]);
            }
            //allSchedules.AddRange(tempSchedules);
         }
         allSchedules.Sort((x, y) => x.date.Day.CompareTo(y.date.Day));
      }


      public void GenerateScheduleID()
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
