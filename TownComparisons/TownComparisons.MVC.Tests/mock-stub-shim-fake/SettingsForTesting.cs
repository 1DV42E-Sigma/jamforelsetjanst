using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TownComparisons.Domain;
using TownComparisons.Domain.Abstract;

namespace TownComparisons.MVC.Tests.mock_stub_shim_fake
{
    class SettingsForTesting : ISettings
    {
        private const string FILENAME = "settingsConfig.json";
        private readonly string _filePath;

        ////Properties
        public string MunicipalityName { get; set; }
        public string MunicipalityId { get; set; } // id from Kolada API

        public string CountyName { get; set; }
        public string CountyId { get; set; } // id from Kolada API

        public int CacheSeconds_PropertyQueries { get; set; }
        public int CacheSeconds_OrganisationalUnits { get; set; }
        public int CacheSeconds_PropertyResult { get; set; }


        
        public SettingsForTesting(bool load = false)
        {
            string pathAppData = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/");
            _filePath = Path.Combine(pathAppData, FILENAME);

            Initialize(load);
        }
        public SettingsForTesting(string filepath, bool load = false)
        {
            _filePath = filepath;

            Initialize(load);
        }

        //Methods
        public void Initialize(bool load = false)
        {
            //default values:
            CacheSeconds_PropertyQueries = (60 * 60 * 24); // 1 day
            CacheSeconds_OrganisationalUnits = (60 * 60 * 24); // 1 day
            CacheSeconds_PropertyResult = (60 * 60 * 24); // 1 day

            if (load)
            {
                Load();
            }
        }
        public void Load()
        {
            string jsonString = string.Empty;
            if (System.IO.File.Exists(_filePath))
            { 
                using (StreamReader file = File.OpenText(_filePath))
                {
                    jsonString = file.ReadToEnd();
                }

            var jObj = JObject.Parse(jsonString);
            //set all properties
            MunicipalityName = jObj.Value<string>("MunicipalityName");
            MunicipalityId = jObj.Value<string>("MunicipalityId");
            CountyName = jObj.Value<string>("CountyName");
            CountyId = jObj.Value<string>("CountyId");
            CacheSeconds_PropertyQueries = jObj.Value<int>("CacheSeconds_PropertyQueries");
            CacheSeconds_OrganisationalUnits = jObj.Value<int>("CacheSeconds_OrganisationalUnits");

            }
            else
            {
                Save();
            }
        }
        public void Save()
        {
            try
            {
                using (StreamWriter file = File.CreateText(_filePath))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    string json = JsonConvert.SerializeObject(this);
                    writer.WriteRaw(json);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not write to settings file. " + ex);
            }
        }
    }
}
