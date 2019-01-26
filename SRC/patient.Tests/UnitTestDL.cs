using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PatientContracts;
using PatientDL;
using System.Collections.Generic;
using SharedModel;

namespace patient.Tests
{
    [TestClass]
    public class UnitTestDL
    {
        private Mock<IPatient> _IPatient;
        private PatientDTO _PatientDL;


        [TestInitialize]
        public void Initialize()
        {
            _IPatient = new Mock<IPatient>();
            _PatientDL = new PatientDTO();

        }

        [TestMethod]
        public void TestGetPatientDL()
        {
            var Response = _PatientDL.GetPatient(int.MinValue);
            Assert.IsNotNull(Response);
        }

        [TestMethod]
        public void TestGetAllPatientsDL()
        {
            var Response = _PatientDL.GetAllPatients();
            Assert.IsNotNull(Response);
            Assert.IsInstanceOfType(Response, typeof(List<Patient>));
        }

        [TestMethod]
        public void TestSavePatientDL()
        {
            var Response = _PatientDL.SavePatient(new Patient());
            Assert.IsTrue(Response);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _PatientDL = null;
            _IPatient = null;
        }
    }
}
