using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;
using T109.ActiveDive.DataAccess.Abstract;
using T109.ActiveDive.DataAccess.Models;
using T109.ActiveDive.Service;

namespace T109.ActiveDive.DataAccess.DataAccess
{
    public class WebApiAsyncRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        //GenericRepository на webApi

        private static readonly object _locker = new object();

        private HttpClient httpClient;

        private string countHostPath;
        private string getAllHostPath;
        private string insertHostPath;
        private string updateHostPath;
        private string deleteHostPath;
        private string searchHostPath;
        private string getByIdOrNullHostPath;

        public Serilog.ILogger Logger;

        public WebApiAsyncRepository<T> SetCountHostPath(string text)
        {
            countHostPath = text;
            return this;
        }
        public WebApiAsyncRepository<T> SetGetAllHostPath(string text)
        {
            getAllHostPath = text;
            return this;
        }
        public WebApiAsyncRepository<T> SetInsertHostPath(string text)
        {
            insertHostPath = text;
            return this;
        }
        public WebApiAsyncRepository<T> SetUpdateHostPath(string text)
        {
            updateHostPath = text;
            return this;
        }
        public WebApiAsyncRepository<T> SetDeleteHostPath(string text)
        {
            deleteHostPath = text;
            return this;
        }

        public WebApiAsyncRepository<T> SetSearchHostPath(string text)
        {
            searchHostPath = text;
            return this;
        }
        public WebApiAsyncRepository<T> SetGetByIdOrNullHostPath(string text)
        {
            getByIdOrNullHostPath = text;
            return this;
        }
        public WebApiAsyncRepository<T> SetBaseAddress(string baseAddress)
        {
            httpClient.BaseAddress = new Uri(baseAddress);
            return this;
        }

        public WebApiAsyncRepository(Serilog.ILogger logger)
        {
            httpClient = new System.Net.Http.HttpClient(new HttpClientHandler());
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Logger = logger;
        }

        public async Task<int> GetCountAsync()
        {
            int rez = -1;
            try
            {
                var response = await httpClient.GetAsync($"{countHostPath}");
                var json = response.Content.ReadAsStringAsync().Result;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:

                        rez = JsonConvert.DeserializeObject<int>(json);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return rez;
        }

        public async Task<IEnumerable<T>> Search(string searchText)
        {
            IEnumerable<T> items =  new List<T>();
            try
            {
                Logger.Information($"This is WebApiAsyncRepository.search searchText={searchText}");
                
                Logger.Information($"Sending reqyest to {httpClient.BaseAddress}");

                var response = await httpClient.GetAsync($"{searchHostPath}/{searchText}");
                
                var json = await response.Content.ReadAsStringAsync();

                Logger.Information($"Received json {json}");

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        items = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR: in WebApiAsyncRepository.search {ex.Message}");
            }
            return items;
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> items = new List<T>();
            try
            {
                var response = await httpClient.GetAsync($"{getAllHostPath}");
                var json = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        items = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return items;
        }
        public async Task<T> GetByIdOrNullAsync(Guid id)
        {
            T item = null;
            try
            {
                var response = await httpClient.GetAsync($"{getByIdOrNullHostPath}");
                var json = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        item = JsonConvert.DeserializeObject<T>(json);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return item;
        }

        public async Task<bool> Exists(Guid id)
        {
            var rez = await GetByIdOrNullAsync(id);
            return rez != null;
        }

        public async Task<CommonOperationResult> AddAsync(T t)
        {
            CommonOperationResult rez;
            try
            {
                string json;
                StringContent jsonContent;

                json = JsonConvert.SerializeObject(t, Formatting.Indented,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{insertHostPath}", jsonContent);

                switch (response.StatusCode)
                {

                    case System.Net.HttpStatusCode.OK:
                        rez = CommonOperationResult.SayOk(response.Content.ReadAsStringAsync().Result);
                        break;

                    default:
                        throw new Exception();

                }
            }
            catch (Exception ex)
            {
                rez = CommonOperationResult.SayFail($"WebApiRepository caught an exception: {ex.Message}");
            }
            return rez;
        }

        public async Task<CommonOperationResult> UpdateAsync(T t)
        {
            CommonOperationResult rez;
            try
            {
                string json;

                StringContent jsonContent;

                json = JsonConvert.SerializeObject(t, Formatting.Indented,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"{updateHostPath}", jsonContent);

                switch (response.StatusCode)
                {

                    case System.Net.HttpStatusCode.OK:
                        rez = CommonOperationResult.SayOk(response.Content.ReadAsStringAsync().Result);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                rez = CommonOperationResult.SayFail($"{ex.Message}");
            }
            return rez;
        }

        public async Task<CommonOperationResult> DeleteAsync(Guid id)
        {

            CommonOperationResult rez;
            try
            {
                var response = await httpClient.DeleteAsync($"{deleteHostPath}/{id}");

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        rez = CommonOperationResult.SayOk(response.Content.ReadAsStringAsync().Result);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                rez = CommonOperationResult.SayFail($"{ex.Message}");
            }
            return rez;

        }

        public async Task<CommonOperationResult> InitAsync(bool deleteDb)
        {
            return await Task.FromResult(CommonOperationResult.SayOk());
        }
    }
}