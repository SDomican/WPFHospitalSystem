using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Domican_Stephen_Assignment1
{

    

    public partial class MainWindow : Window
    {
        static ObservableCollection<Ward> hospitalWards = new ObservableCollection<Ward>();
        static int wardCount = hospitalWards.Count;
        public MainWindow()
        {
            InitializeComponent();
            

            Patient p1 = new Patient("Chico", new DateTime(1990,06,21),BloodType.A);
            Patient p2 = new Patient("Harpo", new DateTime(1943,03,19), BloodType.O);
            Patient p3 = new Patient("Groucho", new DateTime(1976,12,09), BloodType.B);

            Patient p4 = new Patient("Orwell", new DateTime(1984,02,03), BloodType.AB);

            Ward w1 = new Ward("Marx Brothers Ward", 5);
            Ward w2 = new Ward("Writer's Ward", 4);

            w1.AddPatient(p1);
            w1.AddPatient(p2);
            w1.AddPatient(p3);

            w2.AddPatient(p4);

            hospitalWards.Add(w1);
            hospitalWards.Add(w2);

            lsbxWards.ItemsSource = hospitalWards;

            //Displays count of hospital wards
            txblWards.Text = String.Format("Ward List ({0})",wardCount);

        }

        //Displays the patients in the patients listBox when a Ward is selected from the Ward listBox.
        private void lsbxWards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();

            Ward selectedWard = lsbxWards.SelectedItem as Ward;

            if(selectedWard != null)
            {
                foreach (Patient patient in selectedWard.Patients)
                {
                    patients.Add(patient);
                }
            }

            
            lsbxPatients.ItemsSource = patients;

        }

        //Changes BloodType image and the name above it when a patient is selected in the patient Listbox
        private void lsbxPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient selectedPatient = lsbxPatients.SelectedItem as Patient;

            //Checks the patient is not null, so an error is not produced
            if(selectedPatient != null)
            {
                //Changes image to appropriate blood-type picture.
                switch (selectedPatient.BloodType)
                {
                    case BloodType.A:
                        image.Source = new BitmapImage(new Uri("/images/BloodTypeA.png",UriKind.Relative));
                        break;

                    case BloodType.B:
                        image.Source = new BitmapImage(new Uri("/images/BloodTypeB.png", UriKind.Relative));
                        break;

                    case BloodType.AB:
                        image.Source = new BitmapImage(new Uri("/images/BloodTypeAB.png", UriKind.Relative));
                        break;

                    case BloodType.O:
                        image.Source = new BitmapImage(new Uri("/images/BloodTypeO.png", UriKind.Relative));
                        break;

                    default:
                        MessageBox.Show("Unknown Blood Type");
                        break;
                }
                //Updates patient name in the textblock above the image
                txblPatientName.Text = selectedPatient.Name;
                

            }

        }

        //Creates a patient object and adds it to the selected ward.
        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            //Booleans for which radio button is currently selected.
            bool aClicked = rbA.IsChecked.Equals(true);
            bool abClicked = rbAB.IsChecked.Equals(true);
            bool bClicked = rbB.IsChecked.Equals(true);
            bool oClicked = rbO.IsChecked.Equals(true);

            Ward selectedWard = lsbxWards.SelectedItem as Ward;
            string name = txbPatientName.Text;
            BloodType bloodType = BloodType.A;


            //Creates message for user if no (or an invalid) patient date of birth has been selected from the date picker.
            if (dtpDateOfBirth.SelectedDate == null || dtpDateOfBirth.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Please select a valid patient date of birth!");
                return;
            }

            DateTime patientDOB = dtpDateOfBirth.SelectedDate.Value;

            //Creates message for user if no blood type has been selected from the radio buttons.
            if (aClicked == false && bClicked == false && abClicked == false && oClicked == false)
            {
                MessageBox.Show("Please select a blood type!");
                return;
            }


            //Creates message for user if no ward is currently selected
            if(selectedWard == null)
            {
                MessageBox.Show("Please select a ward!");
                return;
            }

            //Creates message for user if no patient name is currently entered
            if (name.Equals(""))
            {
                MessageBox.Show("Please enter a patient name!");
                return;
            }

            //Creates message for user if no patient date of birth is currently entered
            if (patientDOB == null)
            {
                MessageBox.Show("Please enter a patient date of birth!");
                return;
            }

            //Sets BloodType enum to appropriate blood type depending on which radio button is checked.
            if (aClicked)
                bloodType = BloodType.A;
            else if (abClicked)
                bloodType = BloodType.AB;
            else if(bClicked)
                bloodType = BloodType.B;
            else if(oClicked)
                bloodType = BloodType.O;


            //Creates Patient object from the data provided by the user.
            Patient newPatient = new Patient(name, patientDOB, bloodType);


            //loops through hospital wards observable collection, finds one that matches the selected ward, and adds the new patient object to it (if there is capacity).
            foreach(Ward ward in hospitalWards)
            {
                if (ward.Name.Equals(selectedWard.Name))
                {
                    bool addPatientStatus = ward.AddPatient(newPatient);

                    if(addPatientStatus == false)
                    {
                        MessageBox.Show("Ward is at capacity!");
                    }
                }
            }

            //Updates patient listBox
            lsbxPatients.ItemsSource = selectedWard.Patients;

        }

        //Adds a new ward to the hospitalWord observable collection
        private void btnAddWard_Click(object sender, RoutedEventArgs e)
        {
            string wardName = txblWardName.Text;
            int newWardCapacity = (int)slider.Value;


            //Creates message for user if no ward name is currently entered
            if (wardName.Equals(""))
            {
                MessageBox.Show("Please enter a ward name!");
                return;
            }

            Ward newWard = new Ward(wardName, newWardCapacity);

            hospitalWards.Add(newWard);

            //Updates count of how many wards there are in the observable collection.
            wardCount = hospitalWards.Count;
            txblWards.Text = String.Format("Ward List ({0})", wardCount);

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(@"c:\temp\hospital.json"))
            {

                string json = JsonConvert.SerializeObject(hospitalWards, Formatting.Indented);

                sw.Write(json);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            using (StreamReader sr = new StreamReader(@"c:\temp\hospital.json"))
            {
                string json = sr.ReadToEnd();

                hospitalWards = JsonConvert.DeserializeObject<ObservableCollection<Ward>>(json);
            }

            lsbxWards.ItemsSource = hospitalWards;
        }
    }
}
