using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using zxdemoService.DataObjects;
using zxdemoService.Models;
using Owin;
using Microsoft.Azure.Mobile.Server.Tables.Config;

namespace zxdemoService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            //https://azure.microsoft.com/en-us/documentation/articles/app-service-mobile-dotnet-backend-how-to-use-server-sdk/
            new MobileAppConfiguration()
                        .UseDefaultConfiguration()
                .ApplyTo(config);

            //.AddMobileAppHomeController()             // from the Home package
            //.MapApiControllers()
            //.AddTables(                               // from the Tables package
            //            new MobileAppTableConfiguration()
            //            .MapTableControllers()
            //            .AddEntityFramework()             // from the Entity package
            //        )

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new zxdemoInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<zxdemoContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    }

    public class zxdemoInitializer : CreateDatabaseIfNotExists<zxdemoContext>
    {
        protected override void Seed(zxdemoContext context)
        {
            List<Patient> patients = new List<Patient>
            {
                //new Patient {
                //     Id = Guid.NewGuid().ToString(),
                //     Text = "First item",
                //     Complete = false,
                //     PatientID = "11111",
                //     Age ="30",
                //     Gender ="Male",
                //     AdmissionType ="Emergency",
                //     DaysInHospital ="3",
                //     TimesInHospital ="3",
                //     Medication1 ="metformin",
                //     Medication2 ="glipizide",
                //     Medication3 ="golbutamide",
                //     AdmissionSource ="Emergency Room",
                //     Race ="American",
                //     Weight ="160",
                //     Height ="5.9",
                //     BMI ="32",
                //     Symptom1 ="Circulatory",
                //     Symptom2 ="Diabetes",
                //     Symptom3 ="Injury",
                //     BloodPressure ="120/90",
                //     GlucoseLevel ="3422",
                //     Pulse ="90",
                //     CaloriesFromCarb =".10",
                //     DischargeDisposition ="Discharged to home",
                //     Notes ="Doctor's note here",
                //     LikelihoodofReadmission ="30%",
                //     PatientImageUri="storageurl"
                //},
                //new Patient {
                //     Id = Guid.NewGuid().ToString(),
                //     Text = "Second item",
                //     Complete = false,
                //     PatientID = "22222",
                //     Age ="50",
                //     Gender ="Female",
                //     AdmissionType ="Emergency",
                //     DaysInHospital ="3",
                //     TimesInHospital ="3",
                //     Medication1 ="metformin",
                //     Medication2 ="glipizide",
                //     Medication3 ="golbutamide",
                //     AdmissionSource ="Emergency Room",
                //     Race ="Asian",
                //     Weight ="160",
                //     Height ="5.9",
                //     BMI ="32",
                //     Symptom1 ="Circulatory",
                //     Symptom2 ="Diabetes",
                //     Symptom3 ="Injury",
                //     BloodPressure ="120/90",
                //     GlucoseLevel ="3422",
                //     Pulse ="80",
                //     CaloriesFromCarb =".10",
                //     DischargeDisposition ="Discharged to home",
                //     Notes ="Doctor's note here",
                //     LikelihoodofReadmission ="70%",
                //     PatientImageUri="storageurl"
                //},

                //ColumnNames = new string[] { "admission_source_id", "admission_type_id", "diabetesMed", "diag_132", "discharge_disposition_id", "gender", "insulin", "metformin", "num_age", "num_lab_procedures", "num_procedures", "number_diagnoses", "number_emergency", "number_inpatient", "number_outpatient", "pioglitazone", "rosiglitazone", "time_in_hospital", "bmi", "zipcode", "pct_calories_from_carbs", "Daily_Minutes_walking", "sd_glucose", "readmitted" },

                //Values = new string[,] {  {"7", "1", "Yes", "428", "1", "Male", "Down", "Steady", "79", "2", "0", "5", "0", "0", "0", "No", "No", "3", "29.56", "11370", "45.8508140044172", "193", "2.0758852287561", "NO"}, {"7", "1", "Yes", "other", "1", "Male", "Steady", "No", "35", "38", "2", "9", "2", "9", "1", "No", "No", "3", "28.54", "21220", "56.276041315377", "54", "5.1133777716085", "<30"}, {"7", "1", "Yes", "786", "1", "Male", "No", "No", "50", "48", "0", "5", "0", "0", "0", "No", "No", "1", "28.92", "3110", "51.0927327469283", "88", "3.74782312625926", ">30"}, {"7", "1", "Yes", "276", "4", "Female", "Steady", "No", "50", "56", "0", "9", "0", "1", "0", "No", "No", "4", "38.66", "22027", "61.7604689066691", "43", "5.10369369379208", "<30"}, {"1", "2", "Yes", "276", "1", "Female", "Steady", "No", "87", "9", "0", "9", "0", "0", "0", "No", "No", "4", "22.85", "53105", "47.1724618878215", "148", "0", "NO"}, {"7", "1", "Yes", "428", "1", "Female", "Steady", "Steady", "65", "26", "3", "9", "0", "1", "0", "No", "No", "5", "28.14", "48827", "55.2926956709948", "178", "2.2019238199683", ">30"}, {"7", "2", "Yes", "other", "1", "Male", "Steady", "No", "39", "47", "4", "9", "0", "0", "0", "No", "No", "3", "30.29", "2139", "61.2441934602423", "100", "4.21885496107487", "<30"}, {"17", "5", "Yes", "250.7", "1", "Male", "Down", "No", "53", "18", "2", "8", "2", "1", "0", "No", "No", "3", "34.4", "93063", "56.5615327142705", "58", "5.48283740092985", "<30"}, {"7", "1", "Yes", "250.6", "1", "Male", "No", "No", "60", "32", "1", "7", "0", "0", "0", "No", "No", "4", "27.24", "62694", "41.9209257327604", "99", "3.24774631082266", "NO"}, {"7", "1", "Yes", "other", "1", "Female", "Steady", "No", "63", "64", "4", "9", "0", "0", "0", "No", "No", "3", "15.05", "45640", "37.9504644960098", "240", "2.66693267813295", "NO"},  }

                //{ "7", "1", "Yes", "428", "1", "Male", "Down", "Steady", "79", "2", "0", "5", "0", "0", "0", "No", "No", "3", "29.56", "11370", "45.8508140044172", "193", "2.0758852287561", "NO" },
                //{ "7", "1", "Yes", "other", "1", "Male", "Steady", "No", "35", "38", "2", "9", "2", "9", "1", "No", "No", "3", "28.54", "21220", "56.276041315377", "54", "5.1133777716085", "<30"},
                //{ "7", "1", "Yes", "786", "1", "Male", "No", "No", "50", "48", "0", "5", "0", "0", "0", "No", "No", "1", "28.92", "3110", "51.0927327469283", "88", "3.74782312625926", ">30"},
                //{ "7", "1", "Yes", "276", "4", "Female", "Steady", "No", "50", "56", "0", "9", "0", "1", "0", "No", "No", "4", "38.66", "22027", "61.7604689066691", "43", "5.10369369379208", "<30"},
                //{ "1", "2", "Yes", "276", "1", "Female", "Steady", "No", "87", "9", "0", "9", "0", "0", "0", "No", "No", "4", "22.85", "53105", "47.1724618878215", "148", "0", "NO"},
                //{ "7", "1", "Yes", "428", "1", "Female", "Steady", "Steady", "65", "26", "3", "9", "0", "1", "0", "No", "No", "5", "28.14", "48827", "55.2926956709948", "178", "2.2019238199683", ">30"},
                //{ "7", "2", "Yes", "other", "1", "Male", "Steady", "No", "39", "47", "4", "9", "0", "0", "0", "No", "No", "3", "30.29", "2139", "61.2441934602423", "100", "4.21885496107487", "<30"},
                //{ "17", "5", "Yes", "250.7", "1", "Male", "Down", "No", "53", "18", "2", "8", "2", "1", "0", "No", "No", "3", "34.4", "93063", "56.5615327142705", "58", "5.48283740092985", "<30"},
                //{ "7", "1", "Yes", "250.6", "1", "Male", "No", "No", "60", "32", "1", "7", "0", "0", "0", "No", "No", "4", "27.24", "62694", "41.9209257327604", "99", "3.24774631082266", "NO"},
                //{ "7", "1", "Yes", "other", "1", "Female", "Steady", "No", "63", "64", "4", "9", "0", "0", "0", "No", "No", "3", "15.05", "45640", "37.9504644960098", "240", "2.66693267813295", "NO"},

                //admission_source_id=                 "7",	 admission_type_id= "1",	 diabetesMed= "Yes",	 diag_132= "428",	 discharge_disposition_id= "1",	 gender= "Male",	 insulin= "Down",	 metformin= "Steady",	 num_age= "79",	 num_lab_procedures= "2",	 num_procedures= "0",	 number_diagnoses= "5",	 number_emergency= "0",	 number_inpatient= "0",	 number_outpatient= "0",	 pioglitazone= "No",	 rosiglitazone= "No",	 time_in_hospital= "3",	 bmi= "29.56",	 zipcode= "11370",	 pct_calories_from_carbs= "45.8508140044172",	 Daily_Minutes_walking= "193",	 sd_glucose= "2.0758852287561",	 readmitted= "NO"           ,
                //admission_source_id=                "7",	 admission_type_id= "1",	 diabetesMed= "Yes",	 diag_132= "other",	 discharge_disposition_id= "1",	 gender= "Male",	 insulin= "Steady",	 metformin= "No",	 num_age= "35",	 num_lab_procedures= "38",	 num_procedures= "2",	 number_diagnoses= "9",	 number_emergency= "2",	 number_inpatient= "9",	 number_outpatient= "1",	 pioglitazone= "No",	 rosiglitazone= "No",	 time_in_hospital= "3",	 bmi= "28.54",	 zipcode= "21220",	 pct_calories_from_carbs= "56.276041315377",	 Daily_Minutes_walking= "54",	 sd_glucose= "5.1133777716085",	 readmitted= "<30",
                //admission_source_id=                "7",	 admission_type_id= "1",	 diabetesMed= "Yes",	 diag_132= "786",	 discharge_disposition_id= "1",	 gender= "Male",	 insulin= "No",	 metformin= "No",	 num_age= "50",	 num_lab_procedures= "48",	 num_procedures= "0",	 number_diagnoses= "5",	 number_emergency= "0",	 number_inpatient= "0",	 number_outpatient= "0",	 pioglitazone= "No",	 rosiglitazone= "No",	 time_in_hospital= "1",	 bmi= "28.92",	 zipcode= "3110",	 pct_calories_from_carbs= "51.0927327469283",	 Daily_Minutes_walking= "88",	 sd_glucose= "3.74782312625926",	 readmitted= ">30",
                //admission_source_id=               "7",	 admission_type_id= "1",	 diabetesMed= "Yes",	 diag_132= "276",	 discharge_disposition_id= "4",	 gender= "Female",	 insulin= "Steady",	 metformin= "No",	 num_age= "50",	 num_lab_procedures= "56",	 num_procedures= "0",	 number_diagnoses= "9",	 number_emergency= "0",	 number_inpatient= "1",	 number_outpatient= "0",	 pioglitazone= "No",	 rosiglitazone= "No",	 time_in_hospital= "4",	 bmi= "38.66",	 zipcode= "22027",	 pct_calories_from_carbs= "61.7604689066691",	 Daily_Minutes_walking= "43",	 sd_glucose= "5.10369369379208",	 readmitted= "<30",
                //admission_source_id=                "1",	 admission_type_id= "2",	 diabetesMed= "Yes",	 diag_132= "276",	 discharge_disposition_id= "1",	 gender= "Female",	 insulin= "Steady",	 metformin= "No",	 num_age= "87",	 num_lab_procedures= "9",	 num_procedures= "0",	 number_diagnoses= "9",	 number_emergency= "0",	 number_inpatient= "0",	 number_outpatient= "0",	 pioglitazone= "No",	 rosiglitazone= "No",	 time_in_hospital= "4",	 bmi= "22.85",	 zipcode= "53105",	 pct_calories_from_carbs= "47.1724618878215",	 Daily_Minutes_walking= "148",	 sd_glucose= "0",	 readmitted= "NO",
                //admission_source_id=                "7",	 admission_type_id= "1",	 diabetesMed= "Yes",	 diag_132= "428",	 discharge_disposition_id= "1",	 gender= "Female",	 insulin= "Steady",	 metformin= "Steady",	 num_age= "65",	 num_lab_procedures= "26",	 num_procedures= "3",	 number_diagnoses= "9",	 number_emergency= "0",	 number_inpatient= "1",	 number_outpatient= "0",	 pioglitazone= "No",	 rosiglitazone= "No",	 time_in_hospital= "5",	 bmi= "28.14",	 zipcode= "48827",	 pct_calories_from_carbs= "55.2926956709948",	 Daily_Minutes_walking= "178",	 sd_glucose= "2.2019238199683",	 readmitted= ">30",
                //admission_source_id=                "7",	 admission_type_id= "2",	 diabetesMed= "Yes",	 diag_132= "other",	 discharge_disposition_id= "1",	 gender= "Male",	 insulin= "Steady",	 metformin= "No",	 num_age= "39",	 num_lab_procedures= "47",	 num_procedures= "4",	 number_diagnoses= "9",	 number_emergency= "0",	 number_inpatient= "0",	 number_outpatient= "0",	 pioglitazone= "No",	 rosiglitazone= "No",	 time_in_hospital= "3",	 bmi= "30.29",	 zipcode= "2139",	 pct_calories_from_carbs= "61.2441934602423",	 Daily_Minutes_walking= "100",	 sd_glucose= "4.21885496107487",	 readmitted= "<30",
                //admission_source_id=                "17",	 admission_type_id= "5",	 diabetesMed= "Yes",	 diag_132= "250.7",	 discharge_disposition_id= "1",	 gender= "Male",	 insulin= "Down",	 metformin= "No",	 num_age= "53",	 num_lab_procedures= "18",	 num_procedures= "2",	 number_diagnoses= "8",	 number_emergency= "2",	 number_inpatient= "1",	 number_outpatient= "0",	 pioglitazone= "No",	 rosiglitazone= "No",	 time_in_hospital= "3",	 bmi= "34.4",	 zipcode= "93063",	 pct_calories_from_carbs= "56.5615327142705",	 Daily_Minutes_walking= "58",	 sd_glucose= "5.48283740092985",	 readmitted= "<30",
                //admission_source_id=                "7",	 admission_type_id= "1",	 diabetesMed= "Yes",	 diag_132= "250.6",	 discharge_disposition_id= "1",	 gender= "Male",	 insulin= "No",	 metformin= "No",	 num_age= "60",	 num_lab_procedures= "32",	 num_procedures= "1",	 number_diagnoses= "7",	 number_emergency= "0",	 number_inpatient= "0",	 number_outpatient= "0",	 pioglitazone= "No",	 rosiglitazone= "No",	 time_in_hospital= "4",	 bmi= "27.24",	 zipcode= "62694",	 pct_calories_from_carbs= "41.9209257327604",	 Daily_Minutes_walking= "99",	 sd_glucose= "3.24774631082266",	 readmitted= "NO",
                //admission_source_id=                "7",	 admission_type_id= "1",	 diabetesMed= "Yes",	 diag_132= "other",	 discharge_disposition_id= "1",	 gender= "Female",	 insulin= "Steady",	 metformin= "No",	 num_age= "63",	 num_lab_procedures= "64",	 num_procedures= "4",	 number_diagnoses= "9",	 number_emergency= "0",	 number_inpatient= "0",	 number_outpatient= "0",	 pioglitazone= "No",	 rosiglitazone= "No",	 time_in_hospital= "3",	 bmi= "15.05",	 zipcode= "45640",	 pct_calories_from_carbs= "37.9504644960098",	 Daily_Minutes_walking= "240",	 sd_glucose= "2.66693267813295",	 readmitted= "NO",

                new Patient {Id = Guid.NewGuid().ToString(), Patient_num="111",  First_name = "firstname", Last_name ="lastname", Address ="Address here", Encounter_id ="000",  Admission_source_id=                 "7",   Admission_type_id= "1", DiabetesMed= "Yes", Diag_1= "428",    Discharge_disposition_id= "1",  Gender= "Male", Insulin= "Down",    Metformin= "Steady",    Num_age= "79", Discharge_time ="1/1/2015", Date_of_birth = "1/1/1980", Num_lab_procedures= "2",    Num_procedures= "0",    Number_diagnoses= "5",  Number_emergency= "0",  Number_inpatient= "0",  Number_outpatient= "0", Pioglitazone= "No", Rosiglitazone= "No",    Time_in_hospital= "3",  Bmi= "29.56",  Weight = "120", Height = "5.0",   Zipcode= "11370",   Pct_calories_from_carbs= "45.8508140044172",   Daily_minutes_walking= "193", Sd_glucose= "2.0758852287561",  Readmitted= "NO" },
                new Patient {Id = Guid.NewGuid().ToString(), Patient_num="222",  First_name = "firstname", Last_name ="lastname", Address ="Address here", Encounter_id ="001",  Admission_source_id=                "7",    Admission_type_id= "1", DiabetesMed= "Yes", Diag_1= "other",  Discharge_disposition_id= "1",  Gender= "Male", Insulin= "Steady",  Metformin= "No",    Num_age= "35", Discharge_time ="2/1/2015", Date_of_birth = "2/1/1980", Num_lab_procedures= "38",   Num_procedures= "2",    Number_diagnoses= "9",  Number_emergency= "2",  Number_inpatient= "9",  Number_outpatient= "1", Pioglitazone= "No", Rosiglitazone= "No",    Time_in_hospital= "3",  Bmi= "28.54",  Weight = "121", Height = "5.1",    Zipcode= "21220",   Pct_calories_from_carbs= "56.276041315377",    Daily_minutes_walking= "54",  Sd_glucose= "5.1133777716085",  Readmitted= "<30" },
                new Patient {Id = Guid.NewGuid().ToString(), Patient_num="333",  First_name = "firstname", Last_name ="lastname", Address ="Address here", Encounter_id ="002",  Admission_source_id=                "7",    Admission_type_id= "1", DiabetesMed= "Yes", Diag_1= "786",    Discharge_disposition_id= "1",  Gender= "Male", Insulin= "No",  Metformin= "No",    Num_age= "50", Discharge_time ="3/1/2015", Date_of_birth = "3/1/1980",  Num_lab_procedures= "48",   Num_procedures= "0",    Number_diagnoses= "5",  Number_emergency= "0",  Number_inpatient= "0",  Number_outpatient= "0", Pioglitazone= "No", Rosiglitazone= "No",    Time_in_hospital= "1",  Bmi= "28.92", Weight = "122", Height = "5.2",    Zipcode= "3110",    Pct_calories_from_carbs= "51.0927327469283",   Daily_minutes_walking= "88",  Sd_glucose= "3.74782312625926", Readmitted= ">30" },
                new Patient {Id = Guid.NewGuid().ToString(), Patient_num="444",  First_name = "firstname", Last_name ="lastname", Address ="Address here", Encounter_id ="003",  Admission_source_id=               "7",     Admission_type_id= "1", DiabetesMed= "Yes", Diag_1= "276",    Discharge_disposition_id= "4",  Gender= "Female",   Insulin= "Steady",  Metformin= "No",    Num_age= "50", Discharge_time ="4/1/2015", Date_of_birth = "4/1/1980", Num_lab_procedures= "56",   Num_procedures= "0",    Number_diagnoses= "9",  Number_emergency= "0",  Number_inpatient= "1",  Number_outpatient= "0", Pioglitazone= "No", Rosiglitazone= "No",    Time_in_hospital= "4",  Bmi= "38.66",  Weight = "123", Height = "5.3",    Zipcode= "22027",   Pct_calories_from_carbs= "61.7604689066691",   Daily_minutes_walking= "43",  Sd_glucose= "5.10369369379208", Readmitted= "<30" },
                new Patient {Id = Guid.NewGuid().ToString(), Patient_num="555",  First_name = "firstname", Last_name ="lastname", Address ="Address here", Encounter_id ="004",  Admission_source_id=                "1",    Admission_type_id= "2", DiabetesMed= "Yes", Diag_1= "276",    Discharge_disposition_id= "1",  Gender= "Female",   Insulin= "Steady",  Metformin= "No",    Num_age= "87", Discharge_time ="5/1/2015", Date_of_birth = "5/1/1980", Num_lab_procedures= "9",    Num_procedures= "0",    Number_diagnoses= "9",  Number_emergency= "0",  Number_inpatient= "0",  Number_outpatient= "0", Pioglitazone= "No", Rosiglitazone= "No",    Time_in_hospital= "4",  Bmi= "22.85",  Weight = "124", Height = "5.4",   Zipcode= "53105",   Pct_calories_from_carbs= "47.1724618878215",   Daily_minutes_walking= "148", Sd_glucose= "0",    Readmitted= "NO" },
                new Patient {Id = Guid.NewGuid().ToString(), Patient_num="666",  First_name = "firstname", Last_name ="lastname", Address ="Address here", Encounter_id ="005",  Admission_source_id=                "7",    Admission_type_id= "1", DiabetesMed= "Yes", Diag_1= "428",    Discharge_disposition_id= "1",  Gender= "Female",   Insulin= "Steady",  Metformin= "Steady",    Num_age= "65", Discharge_time ="6/1/2015", Date_of_birth = "6/1/1980",  Num_lab_procedures= "26",   Num_procedures= "3",    Number_diagnoses= "9",  Number_emergency= "0",  Number_inpatient= "1",  Number_outpatient= "0", Pioglitazone= "No", Rosiglitazone= "No",    Time_in_hospital= "5",  Bmi= "28.14",  Weight = "125", Height = "5.5",   Zipcode= "48827",   Pct_calories_from_carbs= "55.2926956709948",   Daily_minutes_walking= "178", Sd_glucose= "2.2019238199683",  Readmitted= ">30" },
                new Patient {Id = Guid.NewGuid().ToString(), Patient_num="777",  First_name = "firstname", Last_name ="lastname", Address ="Address here", Encounter_id ="006",  Admission_source_id=                "7",    Admission_type_id= "2", DiabetesMed= "Yes", Diag_1= "other",  Discharge_disposition_id= "1",  Gender= "Male", Insulin= "Steady",  Metformin= "No",    Num_age= "39", Discharge_time ="7/1/2015", Date_of_birth = "7/1/1980", Num_lab_procedures= "47",   Num_procedures= "4",    Number_diagnoses= "9",  Number_emergency= "0",  Number_inpatient= "0",  Number_outpatient= "0", Pioglitazone= "No", Rosiglitazone= "No",    Time_in_hospital= "3",  Bmi= "30.29", Weight = "126", Height = "5.6",    Zipcode= "2139",    Pct_calories_from_carbs= "61.2441934602423",   Daily_minutes_walking= "100", Sd_glucose= "4.21885496107487", Readmitted= "<30" },
                new Patient {Id = Guid.NewGuid().ToString(), Patient_num="888",  First_name = "firstname", Last_name ="lastname", Address ="Address here", Encounter_id ="007",  Admission_source_id=                "17",   Admission_type_id= "5", DiabetesMed= "Yes", Diag_1= "250.7",  Discharge_disposition_id= "1",  Gender= "Male", Insulin= "Down",    Metformin= "No",    Num_age= "53",Discharge_time ="8/1/2015", Date_of_birth = "8/1/1980",  Num_lab_procedures= "18",   Num_procedures= "2",    Number_diagnoses= "8",  Number_emergency= "2",  Number_inpatient= "1",  Number_outpatient= "0", Pioglitazone= "No", Rosiglitazone= "No",    Time_in_hospital= "3",  Bmi= "34.4",  Weight = "127", Height = "5.7",    Zipcode= "93063",   Pct_calories_from_carbs= "56.5615327142705",   Daily_minutes_walking= "58",  Sd_glucose= "5.48283740092985", Readmitted= "<30" },
                new Patient {Id = Guid.NewGuid().ToString(), Patient_num="999",  First_name = "firstname", Last_name ="lastname", Address ="Address here", Encounter_id ="008",  Admission_source_id=                "7",    Admission_type_id= "1", DiabetesMed= "Yes", Diag_1= "250.6",  Discharge_disposition_id= "1",  Gender= "Male", Insulin= "No",  Metformin= "No",    Num_age= "60", Discharge_time ="9/1/2015", Date_of_birth = "9/1/1980", Num_lab_procedures= "32",   Num_procedures= "1",    Number_diagnoses= "7",  Number_emergency= "0",  Number_inpatient= "0",  Number_outpatient= "0", Pioglitazone= "No", Rosiglitazone= "No",    Time_in_hospital= "4",  Bmi= "27.24",   Weight = "128", Height = "5.8",  Zipcode= "62694",   Pct_calories_from_carbs= "41.9209257327604",   Daily_minutes_walking= "99",  Sd_glucose= "3.24774631082266", Readmitted= "NO" },
                new Patient {Id = Guid.NewGuid().ToString(), Patient_num="001",  First_name = "firstname", Last_name ="lastname", Address ="Address here", Encounter_id ="009",  Admission_source_id=                "7",    Admission_type_id= "1", DiabetesMed= "Yes", Diag_1= "other",  Discharge_disposition_id= "1",  Gender= "Female",   Insulin= "Steady",  Metformin= "No",    Num_age= "63", Discharge_time ="1/2/2015", Date_of_birth = "1/2/1980", Num_lab_procedures= "64",   Num_procedures= "4",    Number_diagnoses= "9",  Number_emergency= "0",  Number_inpatient= "0",  Number_outpatient= "0", Pioglitazone= "No", Rosiglitazone= "No",    Time_in_hospital= "3",  Bmi= "15.05", Weight = "129", Height = "5.9",    Zipcode= "45640",   Pct_calories_from_carbs= "37.9504644960098",   Daily_minutes_walking= "240", Sd_glucose= "2.66693267813295", Readmitted= "NO" },

            };

            foreach (Patient patient in patients)
            {
                context.Set<Patient>().Add(patient);
            }

            base.Seed(context);
        }
    }
}

