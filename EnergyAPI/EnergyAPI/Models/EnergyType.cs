using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyAPI.Models
{
    public class EnergyType
    {
        //List<String> listEnergyType = new(){"Gas","Electricity"};
        List<String> listEnergyType = new() {};

        public List<String> GetEnergyTypeList()
        {
            return listEnergyType;
        }
    }
}
