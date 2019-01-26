using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PatientDL;
using PatientProvider;
using PatientContracts;
using System.Collections.Generic;
using SharedModel;

namespace patient.Tests
{
    [TestClass]
    public class UnitTestBLD
    {
       private PatientProviderBLD _patientProviderBld;
       private Mock<IPatient> _IPatient;

        [TestInitialize]
        public void Initialize()
        {
            _IPatient = new Mock<IPatient>();
            _patientProviderBld = new PatientProviderBLD(_IPatient.Object);

            _IPatient.Setup(x => x.GetAllPatients()).Returns(new List<Patient>());
            _IPatient.Setup(x => x.GetPatient(It.IsAny<int>())).Returns(new Patient { Patient_ID = 1 });
            _IPatient.Setup(x => x.SavePatient(new Patient())).Returns(false);
        }


        [TestMethod]
        public void TestGetPatient()
        {
            var Response = _patientProviderBld.GetPatient(int.MinValue);
            Assert.IsNotNull(Response);
            Assert.IsTrue(Response.Patient_ID == 1);
        }

        [TestMethod]
        public void TestGetAllPatients()
        {
            var Response=_patientProviderBld.GetAllPatients();
            Assert.IsNotNull(Response);
            Assert.IsInstanceOfType(Response, typeof(List<Patient>));
        }

        [TestMethod]
        public void TestSavePatient()
        {
            var Response = _patientProviderBld.SavePatient(new Patient());
            Assert.IsNotNull(Response);
            Assert.IsTrue(!Response);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _patientProviderBld = null;
            _IPatient = null;
        }
    }
}
