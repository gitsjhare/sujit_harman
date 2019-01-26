using SharedModel;
using System.Collections.Generic;

namespace PatientContracts
{
    public interface IPatient
    {
        Patient GetPatient(int id);
        bool SavePatient(Patient _paitent);
        IList<Patient> GetAllPatients();
    }

}