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
    public class RoomShiftController : ApiController
    {

        private readonly Hospital_Management_SystemEntities2 db = new Hospital_Management_SystemEntities2();
        readonly SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = Hospital_Management_System;Integrated Security = True; MultipleActiveResultSets=True");

        Room_Assignment obj = new Room_Assignment();

        [HttpGet]
        public IEnumerable<Room_Assignment> List()
        {
            return db.Room_Assignment.ToList();
        }
        [HttpGet]
        public Room_Assignment Search(int id)
        {
            Room_Assignment obj = db.Room_Assignment.Find(id);
            return obj;
        }
        [HttpPut]
        public HttpResponseMessage Shift(int id, Room_Assignment data)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                if (data != null)
                {
                    SqlCommand cmd = new SqlCommand("ShiftRoom", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Assignment_no", data.Assignment_no);
                    cmd.Parameters.AddWithValue("@Room_Id", data.Room_Id);
                    cmd.Parameters.AddWithValue("@Patient_Id", data.Patient_Id);
                    cmd.Parameters.AddWithValue("@Admission_Date", data.Admission_Date);
                    cmd.Parameters.AddWithValue("@Patient_Condition", data.Patient_Condition);
                    cmd.Parameters.AddWithValue("@Doctor_Id", data.Doctor_Id);
                    cmd.Parameters.AddWithValue("@Patient_Name", data.Patient_Name);
                    cmd.Parameters.AddWithValue("@Room_Type", data.Room_Type);
                    cmd.Parameters.AddWithValue("@Doctor_Name", data.Doctor_Name);


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
    }
}
