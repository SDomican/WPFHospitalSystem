using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domican_Stephen_Assignment1
{
    public enum BloodType { A, B, AB, O }
    class Patient
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public int Age { get; set; }

        public Patient(string name, DateTime dateOfBirth, BloodType bloodType)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            BloodType = bloodType;
            Age = CalculateAge(dateOfBirth);
        }

        public override string ToString()
        {
            return String.Format($"{Name}\t  ({Age} years)\t\t  Type: {BloodType}");
        }

        //Calculates patient's age using their date of birth
        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age -= 1;

            return age;
        }
    }
}
