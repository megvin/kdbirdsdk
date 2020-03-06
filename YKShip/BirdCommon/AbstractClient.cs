/*
 * Copyright (c) 2018 THL A29 Limited, a Tencent company. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
using Newtonsoft.Json;
using Pathoschild.Http.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YK.Ship.BirdCommon.Http;
using YK.Ship.BirdCommon.Profile;

namespace YK.Ship.BirdCommon
{
    public class AbstractClient
    {
        public const int HTTP_RSP_OK = 200;
        public const string SDK_VERSION = "SDK_NET_3.0.45";

        public AbstractClient( Credential credential,  ClientProfile profile)
        {
            this.Credential = credential;
            this.Profile = profile;
            //this.Endpoint = endpoint;
            //this.Region = region;
            this.SdkVersion = SDK_VERSION;
            //this.ApiVersion = version;
        }

        /// <summary>
        /// Credentials.
        /// </summary>
        public Credential Credential { get; set; }

        /// <summary>
        /// Client profiles.
        /// </summary>
        public ClientProfile Profile { get; set; }

        /// <summary>
        /// Service endpoint, or domain name, such as productName.tencentcloudapi.com.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// Service region, such as ap-guangzhou.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// URL path, for API 3.0, is /.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// SDK version.
        /// </summary>
        public string SdkVersion { get; set; }

        /// <summary>
        /// API version.
        /// </summary>
        public string ApiVersion { get; set; }

        //protected async Task<string> InternalRequest(AbstractModel request, string actionName)
        //{
        //    if ((this.Profile.HttpProfile.ReqMethod != HttpProfile.REQ_GET) && (this.Profile.HttpProfile.ReqMethod != HttpProfile.REQ_POST))
        //    {
        //        throw new Exception("Method only support (GET, POST)");
        //    }

        //    IResponse response = null;

        //    response = await RequestV1(request, actionName);


        //    if ((int)response.Status != HTTP_RSP_OK)
        //    {
        //        throw new Exception(response.Status + await response.Message.Content.ReadAsStringAsync());
        //    }
        //    string strResp = null;
        //    try
        //    {
        //        strResp = await response.AsString();
        //    }
        //    catch (ApiException ex)
        //    {
        //        string responseText = await ex.Response.AsString();
        //        throw new Exception($"The API responded with HTTP {ex.Response.Status}: {responseText}");
        //    }

          
        //    return strResp;
        //}

        protected string InternalRequestSync(AbstractModel request, string actionName)
        {
            if ((this.Profile.HttpProfile.ReqMethod != HttpProfile.REQ_GET) && (this.Profile.HttpProfile.ReqMethod != HttpProfile.REQ_POST))
            {
                throw new Exception("Method only support (GET, POST)");
            }

            HttpWebResponse response = null;

            response = RequestV1Sync(request, actionName);


            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode != HttpStatusCode.OK)
            {
                Encoding encoding = Encoding.UTF8;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        string content = sr.ReadToEnd().ToString();
                        throw new Exception(statusCode.ToString() + content);
                    }
                }
            }
            string strResp = null;
            try
            {
                Encoding encoding = Encoding.UTF8;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        strResp = sr.ReadToEnd().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string responseText = ex.Message;
                throw new Exception($"The API responded with HTTP {responseText}");
            }

            //try
            //{
            //    errResp = JsonConvert.DeserializeObject<JsonResponseModel<JsonResponseErrModel>>(strResp);
            //}
            //catch (JsonSerializationException e)
            //{
            //    throw new Exception(e.Message);
            //}
            //if (errResp.Response.Error != null)
            //{
            //    throw new Exception($"code:{errResp.Response.Error.Code} message:{errResp.Response.Error.Message} ",
            //            errResp.Response.RequestId);
            //}
            return strResp;
        }


        private string BuildContentType()
        {
            string httpRequestMethod = this.Profile.HttpProfile.ReqMethod;
            if (HttpProfile.REQ_POST.Equals(httpRequestMethod))
            {
                return "application/x-www-form-urlencoded";
            }
            else
            {
                return "application/json";
            }
        }

        private string BuildCanonicalQueryString(AbstractModel request)
        {
            string httpRequestMethod = this.Profile.HttpProfile.ReqMethod;
            if (!HttpProfile.REQ_GET.Equals(httpRequestMethod))
            {
                return "";
            }
            Dictionary<string, string> param = new Dictionary<string, string>();
            StringBuilder urlBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in param)
            {
                urlBuilder.Append($"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}&");
            }
            return urlBuilder.ToString().TrimEnd('&');
        }

        private string BuildRequestPayload(AbstractModel request)
        {
            string httpRequestMethod = this.Profile.HttpProfile.ReqMethod;
            if (HttpProfile.REQ_GET.Equals(httpRequestMethod))
            {
                return "";
            }
            return JsonConvert.SerializeObject(request,
                    Newtonsoft.Json.Formatting.None,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

       

        private HttpConnection BuildConnection()
        {
            string endpoint = this.Endpoint;
            if (!string.IsNullOrEmpty(this.Profile.HttpProfile.Endpoint))
            {
                endpoint = this.Profile.HttpProfile.Endpoint;
            }
            HttpConnection conn = new HttpConnection(
                $"{this.Path}",
                this.Profile.HttpProfile.Timeout,
                this.Profile.HttpProfile.WebProxy);
            return conn;
        }

        private Dictionary<string, string> BuildParam(AbstractModel request, string actionName)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            
            string data = AbstractModel.ToJsonString(request);
            Console.WriteLine(data);
            // inplace change
            param = this.FormatRequestData(actionName, data);
            return param;
        }

        //private Dictionary<string, string> BuildParam(AbstractModel request, string actionName)
        //{
        //    Dictionary<string, string> param = new Dictionary<string, string>();
        //    request.ToMap(param, "");
        //    // inplace change
        //    this.FormatRequestData(actionName, param);
        //    return param;
        //}

        private async Task<IResponse> RequestV1(AbstractModel request, string actionName)
        {
            IResponse response = null;
            Dictionary<string, string> param = BuildParam(request, actionName);
            HttpConnection conn = this.BuildConnection();
            try
            {
                if (this.Profile.HttpProfile.ReqMethod == HttpProfile.REQ_GET)
                {
                    response = await conn.GetRequest(this.Path, param);
                }
                else if (this.Profile.HttpProfile.ReqMethod == HttpProfile.REQ_POST)
                {
                    response = await conn.PostRequest(this.Path, param);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"The request with exception: {ex.Message }");
            }

            return response;
        }

        private HttpWebResponse RequestV1Sync(AbstractModel request, string actionName)
        {
            HttpWebResponse response = null;
            Dictionary<string, string> param = BuildParam(request, actionName);
            HttpConnection conn = this.BuildConnection();
            try
            {
                if (this.Profile.HttpProfile.ReqMethod == HttpProfile.REQ_GET)
                {
                    response = conn.GetRequestSync(this.Path, param);
                }
                else if (this.Profile.HttpProfile.ReqMethod == HttpProfile.REQ_POST)
                {
                    response = conn.PostRequestSync(this.Path, param);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"The request with exception: {ex.Message }");
            }

            return response;
        }

        //private Dictionary<string, string> FormatRequestData(string action, Dictionary<string, string> param)
        //{
        //    param.Add("Action", action);
        //    param.Add("RequestClient", this.SdkVersion);
        //    param.Add("Nonce", Math.Abs(new Random().Next()).ToString());

        //    param.Add("Version", this.ApiVersion);

        //    if (!string.IsNullOrEmpty(this.Credential.SecretId))
        //    {
        //        param.Add("SecretId", this.Credential.SecretId);
        //    }

        //    if (!string.IsNullOrEmpty(this.Region))
        //    {
        //        param.Add("Region", this.Region);
        //    }

        //    if (!string.IsNullOrEmpty(this.Profile.SignMethod))
        //    {
        //        param.Add("SignatureMethod", this.Profile.SignMethod);
        //    }

        //    if (!string.IsNullOrEmpty(this.Credential.Token))
        //    {
        //        param.Add("Token", this.Credential.Token);
        //    }

        //    string endpoint = this.Endpoint;
        //    if (!string.IsNullOrEmpty(this.Profile.HttpProfile.Endpoint)) {
        //        endpoint = this.Profile.HttpProfile.Endpoint;
        //    }

        //    string sigInParam = SignHelper.MakeSignPlainText(new SortedDictionary<string, string>(param, StringComparer.Ordinal),
        //        this.Profile.HttpProfile.ReqMethod, endpoint, this.Path);
        //    string sigOutParam = SignHelper.Sign(this.Credential.SecretKey, sigInParam, this.Profile.SignMethod);
        //    param.Add("Signature", sigOutParam);
        //    return param;
        //}


        private Dictionary<string, string> FormatRequestData(string Action, string requestData)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("RequestData", HttpUtility.UrlEncode(requestData, Encoding.UTF8));
            param.Add("EBusinessID", this.Credential.SecretId);
            param.Add("RequestType", Action);
            string dataSign = encrypt(requestData, this.Credential.SecretKey, "UTF-8");

            param.Add("DataSign", HttpUtility.UrlEncode(dataSign, Encoding.UTF8));
            param.Add("DataType", "2");

            return param;
        }

        ///<summary>
        ///电商Sign签名
        ///</summary>
        ///<param name="content">内容</param>
        ///<param name="keyValue">Appkey</param>
        ///<param name="charset">URL编码 </param>
        ///<returns>DataSign签名</returns>
        private string encrypt(String content, String keyValue, String charset)
        {
            if (keyValue != null)
            {
                return base64(MD5(content + keyValue, charset), charset);
            }
            return base64(MD5(content, charset), charset);
        }

        ///<summary>
        /// 字符串MD5加密
        ///</summary>
        ///<param name="str">要加密的字符串</param>
        ///<param name="charset">编码方式</param>
        ///<returns>密文</returns>
        private string MD5(string str, string charset)
        {
            byte[] buffer = System.Text.Encoding.GetEncoding(charset).GetBytes(str);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider check;
                check = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] somme = check.ComputeHash(buffer);
                string ret = "";
                foreach (byte a in somme)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("X");
                    else
                        ret += a.ToString("X");
                }
                return ret.ToLower();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="charset">编码方式</param>
        /// <returns></returns>
        private string base64(String str, String charset)
        {
            return Convert.ToBase64String(System.Text.Encoding.GetEncoding(charset).GetBytes(str));
        }
    }
}
