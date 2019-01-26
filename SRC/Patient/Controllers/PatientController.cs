using PatientContracts;
using SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity.Attributes;

namespace patient.Controllers
{
    [RoutePrefix("api")]
    public class PatientController : ApiController
    {

        private IPatient _patient;
        public PatientController([Dependency("BLD")]IPatient patient)
        {
            _patient = patient;
        }

        // GET api/values
        [HttpGet]
        [Route("patient/{id:int?}")]
        public Patient Get(int id)
        {
           return _patient.GetPatient(id);
        }

        [HttpGet]
        [Route("AllPatients")]
        public IEnumerable<Patient> GetAllPatients()
        {
            return _patient.GetAllPatients();
        }

        // POST api/values
        [HttpPost]
        [Route("SavePatient")]
        [ValidateModel]
        public bool Post([FromBody] Patient value)
        {
            if (ModelState.IsValid)
            {
                return _patient.SavePatient(value);
            }
            return false;
        }


    }
}
