using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Linq;
using Windows.Storage.Pickers;
using Windows.Storage;
//using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using System.Collections.ObjectModel;

namespace zxdemo
{
    sealed partial class MainPage : Page
    {
        private MobileServiceCollection<Patient, Patient> patients;
        private IMobileServiceTable<Patient> patientTable = App.MobileService.GetTable<Patient>();
        //private IMobileServiceSyncTable<Patient> patientTable = App.MobileService.GetSyncTable<Patient>(); // offline sync



        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
        }


        public StringTable MLInput { get; set; }
        public string  MLResult {get; set;}

        public MainPage()
        {
            this.InitializeComponent();

            ButtonUpdateImage.Click += new RoutedEventHandler(ButtonUpdateImage_Click);
        }

        private async void ButtonUpdateImage_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous returned file name, if it exists, between iterations of this scenario
            OutputTextBlock.Text = "";

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                OutputTextBlock.Text = "Photo: " + file.Name;

                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    BitmapImage bitmapImage = new BitmapImage();

                    bitmapImage.SetSource(fileStream);
                    PatientPhoto.Source = bitmapImage;

                    //save to Azure Storage

                    await App.container.CreateIfNotExistsAsync();
                    var blob = App.container.GetBlockBlobReference(file.Name);
                    await blob.DeleteIfExistsAsync();
                    await blob.UploadFromFileAsync(file);

                    //update patient record imageuri
                    imageuri.Text = file.Name;
                    await UpdatePatient();

                }



            }
            else
            {
                OutputTextBlock.Text = "Operation cancelled.";
            }
        }

        private async Task InsertPatient()
        {
            patients = await patientTable
            .Where(p => p.Patient_num == (patient_num.Text ?? "000"))
            .ToCollectionAsync();

            if (patients.Count > 0)
            {
                msg.Text = "Patient record already exisit.";
                return;
            }
            //New Patient Record

            Patient patient = new Patient();

            patient.Id = Guid.NewGuid().ToString();

            patient.Patient_num = patient_num.Text;
            patient.Encounter_id = encounter_id.Text;
            patient.First_name = first_name.Text;
            patient.Last_name = last_name.Text;
            patient.Address = address.Text;


            patient.Admission_source_id = admission_source_id.Text;


            patient.Admission_type_id = admission_type_id.SelectedIndex.ToString();
            //patient.Admission_type_id = admission_type_id.SelectedValue.ToString();
            patient.DiabetesMed = diabetesMed.Text;
            patient.Diag_1 = diag_1.Text;
            patient.Discharge_disposition_id = discharge_disposition_id.Text;
            patient.Gender = gender.Text;
            patient.Insulin = insulin.Text;
            patient.Metformin = metformin.Text;
            patient.Num_age = num_age.Text;
            patient.Discharge_time = discharge_time.Text;
            patient.Date_of_birth = date_of_birth.Text;

            patient.Num_lab_procedures = num_lab_procedures.Text;
            patient.Num_procedures = num_procedures.Text;
            patient.Number_diagnoses = number_diagnoses.Text;
            patient.Number_emergency = number_emergency.Text;
            patient.Number_inpatient = number_inpatient.Text;
            patient.Number_outpatient = number_outpatient.Text;
            patient.Pioglitazone = pioglitazone.Text;
            patient.Rosiglitazone = rosiglitazone.Text;
            patient.Time_in_hospital = time_in_hospital.Text;
            patient.Bmi = bmi.Text;
            patient.Weight  = weight.Text;
            patient.Height  = height.Text;

            patient.Zipcode = zipcode.Text;
            patient.Pct_calories_from_carbs = pct_calories_from_carbs.Text;
            patient.Daily_minutes_walking = daily_Minutes_walking.Text;
            patient.Sd_glucose = sd_glucose.Text;
            patient.Readmitted = readmitted.Text;
            patient.Imageuri = imageuri.Text;

            // This code inserts a new PatientRecord into the database. When the operation completes
            // and Mobile App backend has assigned an Id, the item is added to the CollectionView.
            await patientTable.InsertAsync(patient);
            patients.Add(patient);

            msg.Text = "Patient record added.";
            //await SyncAsync(); // offline sync
        }

        private async Task QueryPatients()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the PatientRecords table.
                // The query excludes completed PatientRecords.
                patients = await patientTable
                    .Where(p => p.Patient_num == (TextInput.Text?? "000"))
                    .ToCollectionAsync();
                //.Where(patient => patient.Complete == false)
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
                msg.Text = e.Message;
            }

            if (exception != null)
            {
                //await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
                msg.Text = "Error loading database record.";
            }
            else
            {
                //ListItems.ItemsSource = patients;
                //this.ButtonSave.IsEnabled = true;


                await DisplayPatient();


            }
        }

        private async Task UpdatePatient()
        {
            //update patient record

            Patient patient = new Patient();

            patient.Id = id.Text;

            patient.Patient_num = patient_num.Text;
            patient.Encounter_id = encounter_id.Text;
            patient.First_name = first_name.Text;
            patient.Last_name = last_name.Text;
            patient.Address = address.Text;

            patient.Admission_source_id = admission_source_id.Text;
            patient.Admission_type_id = admission_type_id.SelectedIndex.ToString();
            patient.DiabetesMed = diabetesMed.Text;
            patient.Diag_1 = diag_1.Text;
            patient.Discharge_disposition_id = discharge_disposition_id.Text;
            patient.Gender = gender.Text;
            patient.Insulin = insulin.Text;
            patient.Metformin = metformin.Text;
            patient.Num_age = num_age.Text;

            patient.Discharge_time = discharge_time.Text;
            patient.Date_of_birth = date_of_birth.Text;




            patient.Num_lab_procedures = num_lab_procedures.Text;
            patient.Num_procedures = num_procedures.Text;
            patient.Number_diagnoses = number_diagnoses.Text;
            patient.Number_emergency = number_emergency.Text;
            patient.Number_inpatient = number_inpatient.Text;
            patient.Number_outpatient = number_outpatient.Text;
            patient.Pioglitazone = pioglitazone.Text;
            patient.Rosiglitazone = rosiglitazone.Text;
            patient.Time_in_hospital = time_in_hospital.Text;
            patient.Bmi = bmi.Text;

            patient.Weight = weight.Text;
            patient.Height = height.Text;

            patient.Zipcode = zipcode.Text;
            patient.Pct_calories_from_carbs = pct_calories_from_carbs.Text;
            patient.Daily_minutes_walking = daily_Minutes_walking.Text;
            patient.Sd_glucose = sd_glucose.Text;
            patient.Readmitted = readmitted.Text;
            patient.Imageuri = imageuri.Text;

            
            MobileServiceInvalidOperationException exception = null;
            try
            {
                await patientTable.UpdateAsync(patient);
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }
            //ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);
        }

        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            await UpdatePatient();

            //await UpdatePatientImage(imageuri.Text);

            //await SyncAsync(); // offline sync

            msg.Text = "Patient info updated";
        }

        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            await InsertPatient();

            //await UpdatePatientImage(imageuri.Text);
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            
            if (TextInput.Text.Equals(string.Empty))
            {

                TextInput.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
                msg.Text = "Please enter patient number.";
                return;

            }

            //Notes.Visibility = Visibility.Visible;
            //Notes_Label.Visibility = Visibility.Visible;
            Prediction.Visibility = Visibility.Visible;
            Prediction_Label.Visibility = Visibility.Visible;
            //Notes.Text = "";
            Prediction.Text = "";
            admission_type_id.ItemsSource = null;
            msg.Text = "";
            PatientPhoto.Source = null;
            OutputTextBlock.Text = "";

            TextInput.Background = new SolidColorBrush(Windows.UI.Colors.White);


            await QueryPatients();


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //await InitLocalStoreAsync(); // offline sync
            //await RefreshPatients();

            //PopulateCombobox();
        }

        private void ButtonPowerBI_Click(object sender, RoutedEventArgs e)
        {
            //https://msdn.microsoft.com/en-us/windows/uwp/controls-and-patterns/web-view
            this.Frame.Navigate(typeof(PowerBIView));

        }

        private void PopulateCombobox()
        {
            ObservableCollection<string> mylist = new ObservableCollection<string>();

            mylist.Add("Normal Visit");
            mylist.Add("Emergency");
            

            //admission_type_id.SelectedValue.ToString()
            admission_type_id.ItemsSource = mylist;
            admission_type_id.SelectedIndex = 0;
        }

        private async void ButtonPredict_Click(object sender, RoutedEventArgs e)
        {
            Prediction.Text = "";

            MLInput =
            new StringTable()
            {
                ColumnNames = new string[] {
                    "admission_source_id", "admission_type_id", "diabetesMed", "diag_132",
                    "discharge_disposition_id", "gender", "insulin", "metformin", "num_age",
                    "num_lab_procedures", "num_procedures", "number_diagnoses", "number_emergency",
                    "number_inpatient", "number_outpatient", "pioglitazone", "rosiglitazone",
                    "time_in_hospital", "bmi", "zipcode", "pct_calories_from_carbs",
                    "minutes_walking", "sd_glucose", "readmitted"
                },
                //Values = new string[,] { { "7", "1", "Yes", "428", "1", "Male", "Down", "Steady", "79", "2", "0", "5", "0", "0", "0", "No", "No", "3", "29.56", "11370", "45.8508140044172", "193", "2.0758852287561", "NO" }, }
                Values = new string[,] { {
                        //"7", "1", "Yes", "528", "1", "Male", "Down", "Steady", "80", 
                        //"2", "0", "5", "0", "0", "0", "No", "No", "3", "29.56", "21370",
                        //"45.8508140044172", "153", "2.0758852287561", "NO"

                    admission_source_id.Text,
                    admission_type_id.SelectedIndex.ToString(),
                    diabetesMed.Text,
                    diag_1.Text,

                    discharge_disposition_id.Text,
                    gender.Text,
                    insulin.Text,
                    metformin.Text,
                    num_age.Text,

                    num_lab_procedures.Text,
                    num_procedures.Text,
                    number_diagnoses.Text,
                    number_emergency.Text,

                    number_inpatient.Text,
                    number_outpatient.Text,
                    pioglitazone.Text,
                    rosiglitazone.Text,

                    time_in_hospital.Text,
                    bmi.Text,
                    zipcode.Text,
                    pct_calories_from_carbs.Text,

                    daily_Minutes_walking.Text,
                    sd_glucose.Text,
                    readmitted.Text

        }, }
            };

            // Calling Azure ML web service
            InvokeRequestResponseService().Wait();

            if (MLResult.Substring(0,5)!="Error")
            {
                // Parsing ML web service response - prediction result
                var mlresult = MLResult.Replace("[[", "[").Replace("]]", "]");
                JObject ss = JObject.Parse(mlresult);
                string sstype = (string)ss["Results"]["output1"]["type"]; //table
                JArray ssColumnNames = (JArray)ss["Results"]["output1"]["value"]["ColumnNames"];
                JArray ssColumnTypes = (JArray)ss["Results"]["output1"]["value"]["ColumnTypes"];
                JArray ssValues = (JArray)ss["Results"]["output1"]["value"]["Values"];

                Prediction.Text = ssColumnNames[0].ToString() + ": " + ssValues[0].ToString() + "\n" + ssColumnNames[1].ToString() + ": " + ssValues[1].ToString() + "\n" + ssColumnNames[2].ToString() + ": " + ssValues[2].ToString();
            }

            else
                Prediction.Text = MLResult;

        }

        // Calling Azure Machine Learning web service

        private async Task InvokeRequestResponseService()
        {
           
                using (var client = new HttpClient())
            {
                var scoreRequest = new
                {   Inputs = new Dictionary<string, StringTable>() {
                        {"input1", MLInput},
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {}
                };

                const string apiKey = "xxx"; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("xxx"); // Replace this with the API for the web service

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    MLResult = result;
                }
                else
                {
                     string responseContent = await response.Content.ReadAsStringAsync();
                    MLResult = "Error:" + responseContent;
                }
            }
        }


        private async Task DisplayPatient()
        {
            try
            {
                if (patients.Count == 0)
                {
                    SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                    TextInput.Background = redBrush;
                    msg.Text = "No patient found.";
                    return;
                }

                PopulateCombobox();

                if (patients[0].Admission_type_id == "1")
                {
                    admission_type_id.SelectedIndex = 1;
                }
                //get patient record

                id.Text = patients[0].Id;

                patient_num.Text = patients[0].Patient_num;


                encounter_id.Text= patients[0].Encounter_id;
                first_name.Text = patients[0].First_name ;
                last_name.Text= patients[0].Last_name ;
                address.Text = patients[0].Address ;

                admission_source_id.Text = patients[0].Admission_source_id;



                //admission_type_id.SelectedValue = patient.Admission_type_id;

                diabetesMed.Text = patients[0].DiabetesMed;
                diag_1.Text = patients[0].Diag_1;
                discharge_disposition_id.Text = patients[0].Discharge_disposition_id;
                gender.Text = patients[0].Gender;
                insulin.Text = patients[0].Insulin;
                metformin.Text = patients[0].Metformin;
                num_age.Text = patients[0].Num_age;

                discharge_time.Text = patients[0].Discharge_time ;
                date_of_birth.Text = patients[0].Date_of_birth ;

                num_lab_procedures.Text = patients[0].Num_lab_procedures;
                num_procedures.Text = patients[0].Num_procedures;
                number_diagnoses.Text = patients[0].Number_diagnoses;
                number_emergency.Text = patients[0].Number_emergency;
                number_inpatient.Text = patients[0].Number_inpatient;
                number_outpatient.Text = patients[0].Number_outpatient;
                pioglitazone.Text = patients[0].Pioglitazone;
                rosiglitazone.Text = patients[0].Rosiglitazone;
                time_in_hospital.Text = patients[0].Time_in_hospital;
                bmi.Text = patients[0].Bmi;
                weight.Text = patients[0].Weight ;
                height.Text = patients[0].Height ;
                zipcode.Text = patients[0].Zipcode;
                pct_calories_from_carbs.Text = patients[0].Pct_calories_from_carbs;
                daily_Minutes_walking.Text = patients[0].Daily_minutes_walking;
                sd_glucose.Text = patients[0].Sd_glucose;
                readmitted.Text = patients[0].Readmitted;
                if (patients[0].Imageuri != null)
                {
                    imageuri.Text = patients[0].Imageuri;


                    //display image

                    var blob = App.container.GetBlockBlobReference(imageuri.Text);
                    StorageFile file;

                    Windows.Storage.StorageFolder temporaryFolder = ApplicationData.Current.TemporaryFolder;
                    file = await temporaryFolder.CreateFileAsync(imageuri.Text,
                       CreationCollisionOption.ReplaceExisting);

                    // Save blob contents to a file.
                    using (var fileStream = System.IO.File.OpenWrite(file.Path))
                    {
                        await blob.DownloadToStreamAsync(fileStream);

                        PatientPhoto.Source = new BitmapImage(new Uri(file.Path));
                    }

                }

            }
            catch (Exception ex)
            {
                    OutputTextBlock.Text = (ex.Message + "\n");
            }
           

        }

        #region Offline sync

        //private async Task InitLocalStoreAsync()
        //{
        //    if (!App.MobileService.SyncContext.IsInitialized)
        //    {
        //        var store = new MobileServiceSQLiteStore("localstore.db");
        //        store.DefineTable<PatientRecord>();
        //        await App.MobileService.SyncContext.InitializeAsync(store);
        //    }
        //
        //    await SyncAsync();
        //}

        //private async Task SyncAsync()
        //{
        //    await App.MobileService.SyncContext.PushAsync();
        //    await patientTable.PullAsync("patientRecords", patientTable.CreateQuery());
        //}

        #endregion
    }
}

