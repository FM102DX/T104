using T104.Store.Engine.Models;
using T104.Store.Shared.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace T104.Store.AdminClient.Connectors
{
    public class WorkflowSettingsConnector
    {
        private readonly HttpClient _httpClient;

        public WorkflowSettingsConnector(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WorkflowSettings>> GetSettingsAsync()
        {

            var collection = new List<WorkflowSettings>();

            try
            {
                var response = await _httpClient.GetAsync("WorkflowSettings/GetCollection");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var parser = new WorkflowSettingsParser();

                    collection = parser.ParseCollection(jsonString);

                }

            }
            catch (AccessTokenNotAvailableException exception)
            {
               // exception.Redirect();
            }
            catch (Exception e)
            {
             //   Debug.WriteLine($"Cannot get items. Message: {e.Message}");
            }

            return collection;
        }
    }
}
