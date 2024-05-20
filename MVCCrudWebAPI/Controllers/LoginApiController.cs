using MVCCrud.Helpers.Helper;
using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using MVCCrud.Repository.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MVCCrudWebAPI.Controllers
{
    public class LoginApiController : ApiController
    {
        private readonly TeachersServices _teacher = new TeachersServices();
        private readonly StudentsServices _student = new StudentsServices();
        private readonly StateCityServices _states = new StateCityServices();

        [Route("api/LoginApi/SignIn")]
        public SessionModel SignIn(LoginModel credentials)
        {
            try
            {
                if (credentials.Role.Equals("Student"))
                {
                    Students authenticateStudent = _student.AuthenticStudent(credentials);
                    if (authenticateStudent != null)
                    {
                        SessionModel studModel = ModelConverterHelper.convertPersonToSessionModal(authenticateStudent);
                        return studModel;
                    }
                    return null;
                }
                else if (credentials.Role.Equals("Teacher"))
                {
                    Teachers authenticateTeacher = _teacher.AuthenticTeacher(credentials);
                    if (authenticateTeacher != null)
                    {
                        SessionModel teacherModel = ModelConverterHelper.convertPersonToSessionModal(authenticateTeacher);
                        return teacherModel;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [Route("api/LoginApi/SignUp")]
        public string SignUp(NewRegistrationModel newPerson)
        {
            try
            {
                if (newPerson.Role.Equals("Student"))
                {
                    if (_student.DoesStudentExist(newPerson.Username))
                    {
                        return "exist";
                    }
                    Students newStudent = ModelConverterHelper.convertStudentModalToStudent(newPerson);
                    _student.RegisterStudent(newStudent);
                    return "success";
                }
                else if (newPerson.Role.Equals("Teacher"))
                {
                    if (_teacher.DoesTeacherExist(newPerson.Username))
                    {
                        return "exist";
                    }
                    Teachers newTeacher = ModelConverterHelper.convertTeacherModalToTeacher(newPerson);
                    _teacher.RegisterTeacher(newTeacher);
                    return "success";
                }
                return "fail";
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [Route("api/LoginApi/GetAllStates")]
        public List<StateModel> GetAllStates()
        {
            try
            {
                List<StateModel> states = _states.GetAllStates();
                return states;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("api/LoginApi/GetAllCities")]
        public List<CityModel> GetAllCities(int stateID)
        {
            try
            {
                List<CityModel> cities = _states.GetAllCities(stateID);
                return cities;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}