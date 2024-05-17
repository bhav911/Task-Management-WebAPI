using MVCCrud.Models.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCrud.Repository.Interface
{
    interface IStateCityInterface
    {
        List<StateModel> GetAllStates();
        List<CityModel> GetAllCities(int stateID);

    }
}
