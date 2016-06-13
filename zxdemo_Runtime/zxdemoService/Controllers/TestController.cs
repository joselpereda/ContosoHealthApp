using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using zxdemoService.DataObjects;
using zxdemoService.Models;

namespace zxdemoService.Controllers
{

    public class TestController : ApiController
    {
        [HttpGet, Route("api/test")]
        public IEnumerable<Patient> GetAllPatients()
        {
            List<Patient> Patients = new List<Patient>
            {
                //new Patient { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                //new Patient { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
            };
            return Patients;
        }

        [HttpGet, Route("api/test/id")]
        public IHttpActionResult GetItem(string id)
        {

            return Ok("sample item");
        }
    }
}
