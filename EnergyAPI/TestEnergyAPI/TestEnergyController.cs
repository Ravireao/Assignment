using EnergyAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestEnergyAPI
{
    public class TestEnergyController
    {
        [Fact]
        public void TestGetEnergyType()
        {
            List<String> expected = new() { "Gas", "Electricity" };
            var controller = new EnergyController();
            ActionResult<IEnumerable<String>> actual = controller.GetEnergyType();
            CollectionAssert.AreEqual(expected, actual.Value.ToList<string>());
        }

        /// <summary>
        /// To test clear listEnergyType from EnergyType class. 
        /// </summary>
        [Fact]
        public void TestGetEnergyTypeForEmptyResult()
        {
            var controller = new EnergyController();
            ActionResult<IEnumerable<String>> actual = controller.GetEnergyType();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(actual.Value.ToList<string>().Count> 0);
        }
    }
}
