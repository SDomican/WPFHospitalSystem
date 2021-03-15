using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domican_Stephen_Assignment1
{
    class Ward
    {
        public List<Patient> Patients { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }

        static int wardCount = 0;
        public Ward(string name, int capacity)
        {
            Patients = new List<Patient>();
            Capacity = capacity;
            Name = name;
            wardCount++;
        }

        //Adds a patient to the hospital ward.
        public void AddPatient(Patient patient)
        {
            if(Patients.Count+1 > Capacity)
            {
                return;
            }
            
            Patients.Add(patient);
            
        }


        public override string ToString()
        {
            return String.Format($"{Name}    (Limit: {Capacity})");
        }


    }
}
