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
    public class BillController : ApiController
    {
        private readonly Hospital_Management_SystemEntities2 db = new Hospital_Management_SystemEntities2();
        readonly SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = Hospital_Management_System;Integrated Security = True; MultipleActiveResultSets=True");

        [HttpGet]
        public IEnumerable<Billing> List()
        {
            return db.Billings.ToList();
        }

        [HttpPost]
        public HttpResponseMessage AddBill(Billing obj)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                if (obj != null)
                {
                    SqlCommand cmd = new SqlCommand("BillAmount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Rid", obj.Rid);
                    cmd.Parameters.AddWithValue("@Did", obj.Did);
                    cmd.Parameters.AddWithValue("@Pid", obj.Pid);
                    cmd.Parameters.AddWithValue("@Doctor_Name", obj.Doctor_Name);
                    cmd.Parameters.AddWithValue("@Patient_Name", obj.Patient_Name);
                    cmd.Parameters.AddWithValue("@Room_Type", obj.Room_Type);
                    cmd.Parameters.AddWithValue("@Aid", obj.Aid);
                    cmd.Parameters.AddWithValue("@Bill_Date", obj.Bill_Date);
                    cmd.Parameters.AddWithValue("@Medicine_Bill", obj.Medicine_Bill);
                    cmd.Parameters.AddWithValue("@Status", obj.Status);

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
        [HttpPut]
        public HttpResponseMessage Update(int id, Bill obj)
        {
            try
            {
                if (id == obj.Patient_Billid)
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
            Billing obj = db.Billings.Find(id);
            if (obj != null)
            {
                db.Billings.Remove(obj);
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
        [HttpGet]
        public Billing Search(int id)
        {
            Billing obj = db.Billings.Find(id);
            return obj;
        }
    }
}


