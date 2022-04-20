using EnergyAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EnergyAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnergyController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<String>> GetEnergyType()
        {
            EnergyType energyType = new();
            return energyType.GetEnergyTypeList();
        }

    }
}
