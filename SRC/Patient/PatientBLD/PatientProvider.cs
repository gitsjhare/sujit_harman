using System;
using System.Collections.Generic;
using PatientContracts;
using SharedModel;
using Unity.Attributes;

namespace PatientProvider{
    public class PatientProviderBLD : IPatient
    {
        IPatient _patientdto;
        public PatientProviderBLD([Dependency("DTO")]IPatient patientdto)
        {
            _patientdto=patientdto;
        }

        public IList<Patient> GetAllPatients()
        {
            return _patientdto.GetAllPatients();
        }

        public Patient GetPatient(int id)
        {
            return _patientdto.GetPatient(id);
        }

        public bool SavePatient(Patient _paitent)
        {
            return _patientdto.SavePatient(_paitent);
        }
    }
}