using Microsoft.Azure.Mobile.Server;


using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace zxdemoService.DataObjects
{
    //public class Patient : ITableData // EntityData
    public class Patient :  EntityData
    {
        //https://azure.microsoft.com/en-us/documentation/articles/app-service-mobile-net-upgrading-from-mobile-services/
        //It seems that all properties must start with a capital letter. Otherwise the mobile apps does not include them in the data model
        //The reason is perhaps that all system properties start with lowercase, e.g. createdAt, updatedAt, deleted, version
        //In my view this is a bug that needs to be fixed

        //https://social.msdn.microsoft.com/Forums/en-US/b706bbe4-2898-4487-82a8-e66c7a71d870/updateasync-not-working?forum=azuremobile
        //public string Id { get; set; }

        //public string Text { get; set; }

        //public bool Complete { get; set; }



        //public string Patient_id { get; set; }

        public string Patient_num { get; set; }
        public string Encounter_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Address { get; set; }

        public string Admission_source_id {get; set;}
        public string Admission_type_id { get; set; }
        public string DiabetesMed { get; set; }
        public string Diag_1 { get; set; }
        public string Discharge_disposition_id { get; set; }
        public string Gender { get; set; }
        public string Insulin { get; set; }
        public string Metformin { get; set; }
        public string Num_age { get; set; }
        public string Discharge_time { get; set; }
        public string Date_of_birth { get; set; }

        public string Num_lab_procedures { get; set; }
        public string Num_procedures { get; set; }
        public string Number_diagnoses { get; set; }
        public string Number_emergency { get; set; }
        public string Number_inpatient { get; set; }
        public string Number_outpatient { get; set; }
        public string Pioglitazone { get; set; }
        public string Rosiglitazone { get; set; }
        public string Time_in_hospital { get; set; }
        public string Bmi { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
         

        public string Zipcode { get; set; }
        public string Pct_calories_from_carbs { get; set; }
        public string Daily_minutes_walking { get; set; }
        public string Sd_glucose { get; set; }
        public string Readmitted { get; set; }

        public string Imageuri { get; set; }



        //[NotMapped]
        //public DateTimeOffset? CreatedAt { get; set; }

        //[NotMapped]
        //public DateTimeOffset? UpdatedAt { get; set; }

        //[NotMapped]
        //public bool Deleted { get; set; }

        //[NotMapped]
        //public byte[] Version { get; set; }


    }
}