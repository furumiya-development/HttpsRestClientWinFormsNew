using System.Text.Json;
using HttpsRestClientWinForms.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.DataFormats;
using System.Windows.Forms;
using System.Net.Http.Headers;

namespace HttpsRestClientWinForms.Model.Service
{
    public class ShohinService
    {
        private static HttpClient? httpClient;
        private HttpSetting settings = new HttpSetting();
        private bool Fauthentication = true; // 認証済みOK/NG
        private string CONTENT_TYPE = @"application/json";
        private HttpStatusCode lastStatusCode;
        private HttpResponseHeaders? lastHeaders;
        private string lastBody = "";

        public ShohinService(HttpClient client)
        {
            httpClient = client;
        }

        public async Task<HttpResponseMessage> HttpGet(string uriStr)
        {
            return await HttpRequest(HttpMethod.Get, uriStr, @"dummy text");
            //JsonData = await httpClient!.GetStringAsync(uri);
        }

        public async Task<HttpResponseMessage> HttpPost(string uriStr, string content)
        {
            return await HttpRequest(HttpMethod.Post, uriStr, content);
        }

        public async Task<HttpResponseMessage> HttpPut(string uriStr, string content)
        {
            return await HttpRequest(HttpMethod.Put, uriStr, content);
        }

        public async Task<HttpResponseMessage> HttpDelete(string uriStr, string content)
        {
            return await HttpRequest(HttpMethod.Delete, uriStr, content);
        }

        private async Task<HttpResponseMessage> HttpRequest(HttpMethod method, string uriStr, string content)
        {
            var uri = new Uri(uriStr);
            var response = new HttpResponseMessage();
            //string resStr = "";

            response = await httpClient!.SendAsync(RequestSetting(method, uri, content));

            lastStatusCode = response.StatusCode;
            lastHeaders = response.Headers;
            lastBody = await response.Content.ReadAsStringAsync();

            //if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.NoContent)
            //{
            //    resStr = await response.Content.ReadAsStringAsync();
            //    richTextBox1.AppendText(response.Headers.ToString());
            //}
            //else
            //{
            //    switch (response.StatusCode)
            //    {
            //        case HttpStatusCode.Unauthorized: // 認証が必要
            //            richTextBox1.AppendText(response.Headers.ToString());
            //            new FormAuth().ShowDialog();
            //            Fauthentication = true;
            //            response = await httpClient.SendAsync(RequestSetting(method, uri, content));
            //            if (response.StatusCode == HttpStatusCode.Unauthorized)
            //            {
            //                Authentication.UserID = "";
            //                Authentication.Password = "";
            //                Fauthentication = false;
            //                MessageBox.Show(response.Headers.ToString(), "認証に失敗しました。");
            //            }
            //            else
            //            {
            //                resStr = await response.Content.ReadAsStringAsync();
            //                richTextBox1.AppendText(response.Headers.ToString());
            //            }
            //            break;
            //        case HttpStatusCode.BadRequest:
            //            resStr = await response.Content.ReadAsStringAsync();
            //            richTextBox1.AppendText(response.Headers.ToString());
            //            break;
            //        default: //その他のエラー
            //            MessageBox.Show(response.Headers.ToString(), response.StatusCode.ToString());
            //            break;
            //    }
            //}
            //textBoxReqBody.Text = resStr;

            return response;
        }

        private HttpRequestMessage RequestSetting(HttpMethod method, Uri uri, string content)
        {
            
            ServicePointManager.SecurityProtocol = settings.SslProtocolVer;
            var request = new HttpRequestMessage();
            request.Method = method;
            request.RequestUri = uri;
            request.Version = settings.HttpVer;
            request.Content = new StringContent(content, Encoding.UTF8, CONTENT_TYPE);
            if (Fauthentication) //プログラムを変更したので常にtrueにしなければならないかも。このフラグは必要ないかも
            {
                string BasicStr = Authentication.BasicRequestHeader();
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", BasicStr);
            }

            return request;
        }

        public HttpStatusCode LastStatusCode
        {
            get => lastStatusCode;
        }
        public HttpResponseHeaders LastHeaders
        {
            get => lastHeaders!;
        }
        public string LastBody
        {
            get => lastBody;
        }
    }
}