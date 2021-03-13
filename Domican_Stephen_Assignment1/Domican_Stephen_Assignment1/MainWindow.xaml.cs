using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<Ward> hospitalWards = new ObservableCollection<Ward>();

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

        }

        //Displays the patients in the patients listBox when a Ward is selected from the Ward listBox.
        private void lsbxWards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();

            Ward selectedWard = lsbxWards.SelectedItem as Ward;

            foreach(Patient patient in selectedWard.Patients)
            {
                patients.Add(patient);
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
    }
}
