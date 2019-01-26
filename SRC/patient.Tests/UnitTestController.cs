using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PatientContracts;
using patient.Controllers;
using SharedModel;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;

namespace patient.Tests
{
    [TestClass]
    public class UnitTestPatientController
    {
       private Mock<IPatient> MockPatientBLD;
       private PatientController _patientcontroller;

        
        [TestInitialize]
        public void Inilize()
        {
            MockPatientBLD = new Mock<IPatient>();
            _patientcontroller = new PatientController(MockPatientBLD.Object);
            _patientcontroller.Request = new HttpRequestMessage();
            _patientcontroller.Configuration = new HttpConfiguration();


            MockPatientBLD.Setup(x => x.GetPatient(It.IsAny<int>())).Returns(new Patient { Patient_ID = 1 });
            MockPatientBLD.Setup(x => x.GetAllPatients()).Returns(new List<Patient>());
            MockPatientBLD.Setup(x => x.SavePatient(new Patient())).Returns(true);
            
        }
        [TestMethod]
        public void TestFetchSinglePatient()
        {
           
            var Response = _patientcontroller.Get(int.MinValue);

            Assert.IsNotNull(Response);
            Assert.IsTrue(Response.Patient_ID == 1);
        }

        [TestMethod]
        public void TestFetchAllPatients()
        {
            var Response = _patientcontroller.GetAllPatients();
            Assert.IsNotNull(Response);
            Assert.IsInstanceOfType(Response,typeof(List<Patient>));
        }

        [TestMethod]
        public void TestSavePatient()
        {
            var Response = _patientcontroller.Post(new Patient());
            Assert.IsNotNull(Response);
            Assert.IsTrue(!Response);
        }

        [TestCleanup]
        public void Cleanup()
        {
            MockPatientBLD = null;
            _patientcontroller = null;
        }
    }
}
