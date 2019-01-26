using System;
using System.Collections.Generic;
using PatientContracts;
using SharedModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PatientDL
{
    public class PatientDTO : IPatient
    {
        private readonly string ConnectionString = ConfigurationManager.AppSettings["connectionString"];

        public Patient GetPatient(int id)
        {
            Patient _patient = new Patient();
            if (ConnectionString != null)
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandText = "FetchPatient";
                        sqlCommand.Parameters.Add("@PATIENT_ID", SqlDbType.Int).Value = id;
                        sqlConnection.Open();
                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _patient.Patient_ID = Convert.ToInt32(reader["patient_id"]);
                                _patient.Forname = Convert.ToString(reader["fornames"]);
                                _patient.Firstname = Convert.ToString(reader["firstname"]);
                                _patient.Surname = Convert.ToString(reader["surname"]);
                                _patient.DateofBirth = Convert.ToDateTime(reader["dob"]);
                                _patient.Gender = Convert.ToString(reader["gender"]);
                                _patient.Home = Convert.ToString(reader["home"]);
                                _patient.Work = Convert.ToString(reader["work"]);
                                _patient.Mobile = Convert.ToString(reader["mobile"]);


                            }
                        }
                        sqlConnection.Close();
                    }
                }
            }
            return _patient;
        }

        public bool SavePatient(Patient _paitent)
        {
            int Ret = 0;
            if (ConnectionString != null)
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandText = "SavePatient";
                        sqlCommand.Parameters.Add("@PATIENT_ID", SqlDbType.Int).Value = _paitent.Patient_ID;
                        sqlCommand.Parameters.Add("@FORNAME", SqlDbType.VarChar, 50).Value = _paitent.Forname;
                        sqlCommand.Parameters.Add("@FIRSTNAME", SqlDbType.VarChar, 150).Value = _paitent.Firstname;
                        sqlCommand.Parameters.Add("@LASTNAME", SqlDbType.VarChar, 150).Value = _paitent.Surname;
                        sqlCommand.Parameters.Add("@DOB", SqlDbType.DateTime).Value = AssignSqlParameter<DateTime>(_paitent.DateofBirth??DateTime.MaxValue);// Convert.ToDateTime(_paitent.DateofBirth);
                        sqlCommand.Parameters.Add("@GENDER", SqlDbType.VarChar, 50).Value = _paitent.Gender;
                        sqlCommand.Parameters.Add("@HOME", SqlDbType.VarChar, 50).Value = AssignSqlParameter<string>(_paitent.Home);
                        sqlCommand.Parameters.Add("@WORK", SqlDbType.VarChar, 50).Value = AssignSqlParameter<string>(_paitent.Work);
                        sqlCommand.Parameters.Add("@MOBILE", SqlDbType.VarChar, 50).Value = AssignSqlParameter<string>(_paitent.Mobile);
                        sqlConnection.Open();
                        Ret = sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
            }
            return Ret <= 0 ? true : false;
        }

        public IList<Patient> GetAllPatients()
        {
            IList<Patient> _PatientList = new List<Patient>();
            if (ConnectionString != null)
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandText = "FetchAllPatients";
                        sqlConnection.Open();
                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _PatientList.Add(new Patient()
                                {
                                    Patient_ID = Convert.ToInt32(reader["patient_id"]),
                                    Forname = Convert.ToString(reader["fornames"]),
                                    Firstname = Convert.ToString(reader["firstname"]),
                                    Surname = Convert.ToString(reader["surname"]),
                                    DateofBirth = reader["dob"]==DBNull.Value? DateTime.MaxValue: Convert.ToDateTime(reader["dob"]),
                                    Gender = Convert.ToString(reader["gender"]),
                                    Home = reader["home"]==DBNull.Value?null: Convert.ToString(reader["home"]),
                                    Work = reader["work"] == DBNull.Value ? null : Convert.ToString(reader["work"]),
                                    Mobile = reader["mobile"] == DBNull.Value ? null : Convert.ToString(reader["mobile"]),
                                });

                            }
                        }
                        sqlConnection.Close();
                    }
                }
            }
            foreach(var removemaxdt in _PatientList)
            {
                if(removemaxdt.DateofBirth.HasValue && removemaxdt.DateofBirth == DateTime.MaxValue)
                {
                    removemaxdt.DateofBirth = null;
                }
            }
            return _PatientList;
        }

        private object AssignSqlParameter<T>(T obj) 
        {
            if (typeof(T) == typeof(string) && string.IsNullOrEmpty(Convert.ToString(obj)))
            {
                return DBNull.Value;
            }
            if(typeof(T)==typeof(DateTime) && DateTime.MaxValue == (Convert.ToDateTime(obj)))
            {
                return DBNull.Value;
            }
            return obj;
            
        }
    }
}