using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domican_Stephen_Assignment1
{
    class Ward
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }

        static int wardCount = 0;
        public Ward(string name, int capacity)
        {
            Patients = new ObservableCollection<Patient>();
            Capacity = capacity;
            Name = name;
            wardCount++;
        }

        //Adds a patient to the hospital ward. Returns true bool if succesfull and false if at capacity
        public bool AddPatient(Patient patient)
        {
            if(Patients.Count+1 > Capacity)
            {
                return false;
            }
            
            Patients.Add(patient);
            return true;  
        }


        public override string ToString()
        {
            return String.Format($"{Name}    (Limit: {Capacity})");
        }


    }
}
