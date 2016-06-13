using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace zxdemo
{
    public class Patient
    {
        //JsonPreoprty: only change the first letter to lower case
        //https://social.msdn.microsoft.com/Forums/azure/en-US/da6be597-7060-4827-bc71-11aba3f038c2/help-with-linq-query-error-with-where-clause?forum=azuremobile
        //http://stackoverflow.com/questions/34314505/no-id-member-found-on-type-packinglist-models-reis-uwp-windows-10-app
        //https://azure.microsoft.com/en-us/documentation/articles/app-service-mobile-net-upgrading-from-mobile-services/

        public string Id { get; set; }


        [JsonProperty(PropertyName = "patient_num")]
        public string Patient_num { get; set; }

        [JsonProperty(PropertyName = "encounter_id")]
        public string Encounter_id { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string First_name { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string Last_name { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "admission_source_id")]
        public string Admission_source_id { get; set; }

        [JsonProperty(PropertyName = "admission_type_id")]
        public string Admission_type_id { get; set; }

        [JsonProperty(PropertyName = "diabetesMed")]
        public string DiabetesMed { get; set; }

        [JsonProperty(PropertyName = "diag_1")]
        public string Diag_1 { get; set; }

        [JsonProperty(PropertyName = "discharge_disposition_id")]
        public string Discharge_disposition_id { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "insulin")]
        public string Insulin { get; set; }

        [JsonProperty(PropertyName = "metformin")]
        public string Metformin { get; set; }

        [JsonProperty(PropertyName = "num_age")]
        public string Num_age { get; set; }

        [JsonProperty(PropertyName = "discharge_time")]
        public string Discharge_time { get; set; }

        [JsonProperty(PropertyName = "date_of_birth")]
        public string Date_of_birth { get; set; }

        [JsonProperty(PropertyName = "num_lab_procedures")]
        public string Num_lab_procedures { get; set; }

        [JsonProperty(PropertyName = "num_procedures")]
        public string Num_procedures { get; set; }

        [JsonProperty(PropertyName = "number_diagnoses")]
        public string Number_diagnoses { get; set; }

        [JsonProperty(PropertyName = "number_emergency")]
        public string Number_emergency { get; set; }

        [JsonProperty(PropertyName = "number_inpatient")]
        public string Number_inpatient { get; set; }

        [JsonProperty(PropertyName = "number_outpatient")]
        public string Number_outpatient { get; set; }

        [JsonProperty(PropertyName = "pioglitazone")]
        public string Pioglitazone { get; set; }

        [JsonProperty(PropertyName = "rosiglitazone")]
        public string Rosiglitazone { get; set; }

        [JsonProperty(PropertyName = "time_in_hospital")]
        public string Time_in_hospital { get; set; }

        [JsonProperty(PropertyName = "bmi")]
        public string Bmi { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public string Weight { get; set; }

        [JsonProperty(PropertyName = "height")]
        public string Height { get; set; }

        [JsonProperty(PropertyName = "zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty(PropertyName = "pct_calories_from_carbs")]
        public string Pct_calories_from_carbs { get; set; }

        [JsonProperty(PropertyName = "daily_minutes_walking")]
        public string Daily_minutes_walking { get; set; }

        [JsonProperty(PropertyName = "sd_glucose")]
        public string Sd_glucose { get; set; }

        [JsonProperty(PropertyName = "readmitted")]
        public string Readmitted { get; set; }

        [JsonProperty(PropertyName = "imageuri")]
        public string Imageuri { get; set; }

    }
}
