using Hospital_Management.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Hospital_Management.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PatientWebAPIController : ApiController
    {
        private readonly Hospital_Management_SystemEntities2 db = new Hospital_Management_SystemEntities2();
        readonly SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = Hospital_Management_System;Integrated Security = True; MultipleActiveResultSets=True");

        Patient obj = new Patient();

        [HttpGet]
        public IEnumerable<Patient> List()
        {
            return db.Patients.ToList();
        }


        [HttpPost]
        public HttpResponseMessage AddPatient(Patient data)
        {

            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                if (data != null)
                {
                    SqlCommand cmd = new SqlCommand("AddPatient", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Patient_Name", data.Patient_Name);
                    cmd.Parameters.AddWithValue("@Gender", data.Gender);
                    cmd.Parameters.AddWithValue("@Age", data.Age);
                    cmd.Parameters.AddWithValue("@Patient_Address", data.Patient_Address);
                    cmd.Parameters.AddWithValue("@Contact_No", data.Contact_No);
                    cmd.Parameters.AddWithValue("@Guardian_Name", data.Guardian_Name);
                    cmd.Parameters.AddWithValue("@Emergency_contact_no", data.Emergency_contact_no);
                    cmd.Parameters.AddWithValue("@Nature_Of_Disease", data.Nature_Of_Disease);
                    cmd.Parameters.AddWithValue("@Patient_Condition", data.Patient_Condition);
                    cmd.Parameters.AddWithValue("@Doctor_Name", data.Doctor_Name);
                    cmd.Parameters.AddWithValue("@Room_Type", data.Room_Type);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        response = new HttpResponseMessage(HttpStatusCode.OK);


                    }
                    else
                    {
                        response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

                    }
                }
                return response;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public Patient Search(int id)
        {
            Patient obj = db.Patients.Find(id);
            return obj;
        }

        [HttpPut]
        public HttpResponseMessage Edit(int id, Patient obj)
        {
            try
            {
                if (id == obj.Patient_Id)
                {
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
            }
            catch (Exception)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Patient obj = db.Patients.Find(id);
            if (obj != null)
            {
                db.Patients.Remove(obj);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }


    }
}


