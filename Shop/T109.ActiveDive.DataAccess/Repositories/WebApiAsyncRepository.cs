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

        string _prefix;

        public string Alias { get; set; } = "WebApiAsyncRepositoryAlias";

        public HttpClient MyHttpClient
        {
                get { return httpClient; }
                set { httpClient = value; }
        }

        public Serilog.ILogger Logger;

        public WebApiAsyncRepository<T> SetLogger (Serilog.ILogger logger)
        {
            Logger= logger;
            return this;
        }

        public WebApiAsyncRepository(string prefix = "")
        {
            _prefix = prefix;
        }
        public WebApiAsyncRepository(string baseAddress, string prefix="")
        {
            _prefix= prefix;
             httpClient = new System.Net.Http.HttpClient(new HttpClientHandler());
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public WebApiAsyncRepository(HttpClient httpClient, string prefix="")
        {
            _prefix = prefix;
            this.httpClient = httpClient;
        }

        public Task<int> Count
        {
            get
            {
                int rez;
                try
                {
                    Task<HttpResponseMessage> response;
                    string json;
                    response = httpClient.GetAsync($"{_prefix}/Count/");

                    //тут возвращается not found 404
                    switch (response.Result.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            json = response.Result.Content.ReadAsStringAsync().Result;
                            rez = JsonConvert.DeserializeObject<int>(json);
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                        default:
                            rez = -1;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    rez = -1;
                }
                return Task.FromResult(rez);
            }
        }

        public async Task<IEnumerable<T>> Search(string searchText)
        {
            IEnumerable<T> items;
            try
            {
                // Logger.Information($"Sending request to {httpClient.BaseAddress}{_prefix}/getall/, httpClient=null: {httpClient == null} prefix={_prefix} ");

                var response = await httpClient.GetAsync($"{_prefix}/search/"+ searchText);

                // Logger.Information($"Got responce success={response.IsSuccessStatusCode} StatusCode={response.StatusCode}");

                var responseContent = await response.Content.ReadAsStringAsync();

                items = JsonConvert.DeserializeObject<IEnumerable<T>>(responseContent);

                foreach (var x in items)
                {
                  //  Logger.Information($"x={x.ShortString()}");
                }
            }
            catch (Exception ex)
            {
             //   Logger.Information($"ERROR={ex.Message}");
                items = (IEnumerable<T>)new List<T>();
            }
            return items;
        }

        public async Task<List<T>> SearchList(string searchText)
        {
            List<T> rez = new List<T>();

            //Console.WriteLine($"Barsik is talkin mieowwww 1!");
            try
            {
                IEnumerable<T> list = await Search(searchText);

                foreach (T t in list)
                {
                    rez.Add(t);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return rez;
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> items;
            try
            {
                Logger.Information($"Sending request to {httpClient.BaseAddress}{_prefix}/getall/, httpClient=null: {httpClient == null} prefix={_prefix} ");

                var response = await httpClient.GetAsync($"{_prefix}/getall/");

                Logger.Information($"Got responce success={response.IsSuccessStatusCode} StatusCode={response.StatusCode}");

                var responseContent = await response.Content.ReadAsStringAsync();

                items = JsonConvert.DeserializeObject<IEnumerable<T>>(responseContent);

                foreach (var x in items)
                {
                    Logger.Information($"x={x.ShortString()}");
                }
            }
            catch (Exception ex)
            {
                Logger.Information($"ERROR={ex.Message}");
                items = (IEnumerable<T>)new List<T>();
            }
            return items;
        }

        public async Task<T> GetByIdOrNullAsync(Guid id)
        {
            T item;
            
            try
            {
               string json;

               var response = await httpClient.GetAsync($"{_prefix}/GetByIdOrNull/{id}");

                //тут возвращается not found 404
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        json = response.Content.ReadAsStringAsync().Result;
                        item = JsonConvert.DeserializeObject<T>(json);
                        break;

                    case System.Net.HttpStatusCode.NotFound:
                        item=null;
                        break;

                    default:
                        item = null;
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebApiRepository error: {ex.Message}");
                item = null;
            }
            return item;
        }

        public async Task<bool> Exists(Guid id)
        {
            var rez = await GetByIdOrNullAsync(id);
            return  rez!= null;
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

               var response = await httpClient.PostAsync($"{_prefix}/", jsonContent);

                switch (response.StatusCode)
                {
                    default:
                    case System.Net.HttpStatusCode.OK:
                        rez = CommonOperationResult.sayOk(response.Content.ReadAsStringAsync().Result);
                        break;

                    case System.Net.HttpStatusCode.Conflict:
                        rez = CommonOperationResult.sayFail(response.Content.ReadAsStringAsync().Result);
                        break;
                }
            }
            catch (Exception ex)
            {
                rez = CommonOperationResult.sayFail($"WebApiRepository error: {ex.Message}");
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

                Logger.Information($"json={json}");

                jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

              //  Logger.Information($"jsonContent={string.Join(',', jsonContent.Headers)}");

                var response = await httpClient.PutAsync($"{_prefix}/", jsonContent);

                switch (response.StatusCode)
                {
                    default:
                    case System.Net.HttpStatusCode.OK:
                        rez = CommonOperationResult.sayOk(response.Content.ReadAsStringAsync().Result);
                        break;

                    case System.Net.HttpStatusCode.Conflict:
                        rez = CommonOperationResult.sayFail(response.Content.ReadAsStringAsync().Result);
                        break;
                }
            }
            catch (Exception ex)
            {
                rez = CommonOperationResult.sayFail($"WebApiRepository error: {ex.Message}");
            }
            return rez;
        }

        public async Task<CommonOperationResult> DeleteAsync(Guid id)
        {

            CommonOperationResult rez;
            try
                {
                   var response = await httpClient.DeleteAsync($"{_prefix}/{id}");

                switch (response.StatusCode)
                {
                    default:
                    case System.Net.HttpStatusCode.OK:
                        rez = CommonOperationResult.sayOk(response.Content.ReadAsStringAsync().Result);
                        break;

                    case System.Net.HttpStatusCode.Conflict:
                        rez = CommonOperationResult.sayFail(response.Content.ReadAsStringAsync().Result);
                        break;
                }
            }
            catch (Exception ex)
            {
                rez = CommonOperationResult.sayFail($"WebApiRepository error: {ex.Message}");
            }
            return rez;

        }

        public async Task<List<T>> GetItemsListAsync()
        {
            List<T> rez = new List<T>();

            //Console.WriteLine($"Barsik is talkin mieowwww 1!");
            try
            {
                IEnumerable<T> list = await GetAllAsync();

                foreach (T t in list)
                {
                    rez.Add(t);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( $"{ex.Message}");
            }
            return rez;
        }

        public Task<CommonOperationResult> InitAsync(bool deleteDb)
        {
            CommonOperationResult rez = CommonOperationResult.sayOk();
            return Task.FromResult(rez);
        }
    }
}