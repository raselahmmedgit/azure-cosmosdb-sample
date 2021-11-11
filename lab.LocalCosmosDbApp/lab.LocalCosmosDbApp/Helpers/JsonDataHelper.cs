using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Helpers
{
    public class JsonDataHelper
    {
        #region Global Variable Declaration
        private readonly IHostingEnvironment _iHostingEnvironment;
        private readonly string _fileName = string.Empty;
        private readonly string _filePath = string.Empty;
        #endregion

        #region Constructor
        public JsonDataHelper(IHostingEnvironment iHostingEnvironment, string fileName)
        {
            _iHostingEnvironment = iHostingEnvironment;
            _fileName = fileName;
            _filePath = $"{_iHostingEnvironment.WebRootPath}\\data\\{_fileName}";
        }
        #endregion

        #region Actions

        public async Task<string> ReadJsonData()
        {
            try
            {
                string jsonData = string.Empty;

                if (!File.Exists(_filePath))
                {
                    File.CreateText(_filePath).Close();
                }

                if (File.Exists(_filePath))
                {
                    jsonData = await File.ReadAllTextAsync(_filePath);
                }

                return jsonData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> WriteJsonData(string jsonData)
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    File.CreateText(_filePath).Close();
                }

                if (File.Exists(_filePath))
                {
                    await File.WriteAllTextAsync(_filePath, jsonData);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
