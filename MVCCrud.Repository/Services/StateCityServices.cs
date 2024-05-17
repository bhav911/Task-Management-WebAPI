using MVCCrud.Helpers.Helper;
using MVCCrud.Models.Context;
using MVCCrud.Models.CustomModel;
using MVCCrud.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Repository.Services
{
    public class StateCityServices : IStateCityInterface
    {
        private readonly TaskManagement_490Entities _context = new TaskManagement_490Entities();

        public List<StateModel> GetAllStates()
        {
            try
            {
                List<States> stateList = _context.States.ToList();
                List<StateModel> convertedStateList = ModelConverterHelper.ConvertStateToStateModel(stateList);
                return convertedStateList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CityModel> GetAllCities(int stateID)
        {
            try
            {
                List<Cities> cityList = _context.Cities.Where(c => c.StateID == stateID).ToList();
                List<CityModel> convertedCityList = ModelConverterHelper.ConvertCityToCityModel(cityList);
                return convertedCityList;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
