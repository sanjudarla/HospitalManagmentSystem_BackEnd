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
    public class AssignRoomAndDoctorController : ApiController
    {
        private readonly Hospital_Management_SystemEntities2 db = new Hospital_Management_SystemEntities2();
        readonly SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = Hospital_Management_System;Integrated Security = True; MultipleActiveResultSets=True");

        Room_Assignment obj = new Room_Assignment();

        [HttpGet]
        public IEnumerable<Room_Assignment> List()
        {
            return db.Room_Assignment.ToList();
        }


        [HttpPost]
        public HttpResponseMessage AddRoomandDoc(Room_Assignment data)
        {

            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                if (data != null)
                {
                    SqlCommand cmd = new SqlCommand("AssignRoom", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Room_Id", data.Room_Id);
                    cmd.Parameters.AddWithValue("@Patient_Id", data.Patient_Id);
                    cmd.Parameters.AddWithValue("@Doctor_Name", data.Doctor_Name);
                    cmd.Parameters.AddWithValue("@Patient_Condition", data.Patient_Condition);
                    cmd.Parameters.AddWithValue("@Doctor_Id", data.Doctor_Id);
                    cmd.Parameters.AddWithValue("@Patient_Name", data.Patient_Name);
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
        public Room_Assignment SearchRoomandDoc(int id)
        {
            Room_Assignment obj = db.Room_Assignment.Find(id);
            return obj;
        }

        //[HttpPut]
        //public HttpResponseMessage EditRoomandDoc(int id, Room_Assignment roomobj)
        //{
        //    try
        //    {
        //        if (id == roomobj.Assignment_no)
        //        {
        //            db.Entry(roomobj).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        //            return response;
        //        }
        //        else
        //        {
        //            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
        //            return response;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //        return response;
        //    }
        //}
        [HttpDelete]
        public HttpResponseMessage DeleteRoomandDoc(int id)
        {
            Room_Assignment roomobj = db.Room_Assignment.Find(id);
            if (roomobj != null)
            {
                db.Room_Assignment.Remove(roomobj);
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

