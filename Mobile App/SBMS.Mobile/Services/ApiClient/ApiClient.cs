using SBMS.Mobile;
using SBMS.Mobile.Common;
using SBMS.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SBMS.Mobile.Services
{
    public class ApiClient
    {
        protected const string DefaultContentType = "application/json";
        private string _accessToken;
        private string _serverUrl;

        public ApiClient()
        {
            //_accessToken = getAccessToken();
        }

        public async Task<ApiBaseModel<T>> Call<T>(HttpMethods method, string path) where T : class
        {
            return await Call<T>(method, path, string.Empty);
        }

        public async Task<ApiBaseModel<T>> Call<T>(HttpMethods method, string path, object callParams) where T : class
        {
            var objResult = new ApiBaseModel<T>();
            try
            {
                _accessToken = Convert.ToString(App.GetPropertyValue("SBMS.MobileApiToken"));
                _serverUrl = Convert.ToString(App.GetPropertyValue("SBMS.MobileApiBaseURL"));

                string requestUriString = string.Format("{0}/{1}", _serverUrl, path);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
                httpWebRequest.Method = method.ToString();
                if (callParams != null && callParams != "")
                {
                    if (method == HttpMethods.Get || method == HttpMethods.Delete)
                    {
                        httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}?{1}", requestUriString, callParams));
                    }

                    if (method == HttpMethods.Post || method == HttpMethods.Put)
                    {
                        //using (new MemoryStream())
                        //{
                        //    using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
                        //    {
                        //        streamWriter.Write(callParams);
                        //    }
                        //}
                        using (var stream = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            var serialized = JsonConvert.SerializeObject(callParams);
                            stream.Write(serialized);
                        }
                    }
                }

                httpWebRequest.ContentType = DefaultContentType;

                //httpWebRequest.Headers.Add("Authorization", string.Format("Bearer {0}", _accessToken));
                httpWebRequest.Headers["Authorization"] = string.Format("Bearer {0}", _accessToken);

                httpWebRequest.Method = ((object)method).ToString();

                var httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                string encodedData = string.Empty;

                using (Stream responseStream = httpWebResponse.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var streamReader = new StreamReader(responseStream))
                        {
                            encodedData = streamReader.ReadToEnd();
                            if (typeof(T) != typeof(object))
                            {
                                objResult = JsonConvert.DeserializeObject<ApiBaseModel<T>> (encodedData);
                            }
                        }
                    }
                }

                return objResult;

            }

            catch (WebException ex)
            {
                //if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
                //{

                //}
                //else
                //{
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        objResult.ErrorMessage = ex.Message;
                    }
                //}
            }
            catch (Exception ex)
            {
                objResult.ErrorMessage= ex.Message;
            }
            return objResult;
        }

        public async Task<ApiBaseModel<T>> Get<T>(string path) where T : class
        {
            return await Get<T>(path, "");
        }
        public async Task<ApiBaseModel<T>> Get<T>(string path, string callParams) where T : class
        {
            return await Call<T>(HttpMethods.Get, path, callParams);
        }
        public async Task<ApiBaseModel<T>> Get<T>(string path, BaseParametersModel callParams=null) where T : class
        {
            callParams = callParams ?? new BaseParametersModel();
            string searchParam = string.Empty;
            if (callParams != null)
            {
                searchParam = CommonExtensions.GetPropertyValues(callParams);
                searchParam += CommonExtensions.ConvertDictionaryToString(callParams.OtherParams);
                searchParam = searchParam.Length > 1 ? searchParam.Substring(searchParam.Length - 1) == "&" ? searchParam.Substring(0, searchParam.Length - 1) : searchParam : searchParam;
            }
            return await Call<T>(HttpMethods.Get, path, searchParam);
        }

        public async Task<ApiBaseModel<T>> Post<T>(string path, object data) where T : class
        {
            return await Call<T>(HttpMethods.Post, path, data);
        }

        public async Task<ApiBaseModel<T>> Put<T>(string path, object data) where T : class
        {
            return await Call<T>(HttpMethods.Put, path, data);
        }

        public async Task<ApiBaseModel<T>> Delete<T>(string path) where T : class
        {
            return await Call<T>(HttpMethods.Delete, path);
        }

    }
}