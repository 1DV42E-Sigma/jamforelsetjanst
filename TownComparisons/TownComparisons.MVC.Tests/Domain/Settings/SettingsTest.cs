using System;
using System.IO;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownComparisons.MVC.Tests.mock_stub_shim_fake;

namespace TownComparisons.MVC.Tests.Domain.Settings
{
    [TestClass]
    public class SettingsTest
    {
        private TownComparisons.Domain.ISettings _settings;

        [TestInitialize]
        public void SetUp()
        {
            string path = Directory.GetCurrentDirectory();
            string addDirToPath = Path.Combine(path, "App_Data");
            string fullpath = Path.Combine(addDirToPath, "settingsConfig.json");
            _settings = new SettingsForTesting(fullpath);

        }

        /// <summary>
        /// Test that Settings will not throw an exception when writing to config file
        /// </summary>
        [TestMethod]
        public void Test_Settings_WriteToSettingsConfigFile()
        {
            string location = "Göteborg";

            _settings.MunicipalityName = location;
            _settings.Save();
        }


        /// <summary>
        /// Test reading from config file
        /// </summary>
        [TestMethod]
        public void Test_Settings_ReadSettingsConfigFile()
        {
            string expected = "Växsö";
            _settings.MunicipalityName = expected;
            _settings.Save();

            _settings.Load();
            var actual = _settings.MunicipalityName;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Test_Settings_GetIdForName()
        {
            string city = "Växjö";
            string cityId = "1290";
            _settings.MunicipalityName = city;
            _settings.MunicipalityId = cityId;
            _settings.Save();

            _settings.Load();
            var getCity = _settings.MunicipalityName;
            var getId = _settings.MunicipalityId;

            Assert.AreEqual(getCity, getCity);
            Assert.AreEqual(getId, "1290");
        }

    }
}
