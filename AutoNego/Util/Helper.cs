using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AutoNego.Util
{
    public class Helper
    {
        #region MicroServices
        public static async Task<HttpResponseMessage> HitMicroServices_Put(string type, string function, dynamic request)
        {
            using var _cts = new CancellationTokenSource(TimeSpan.FromMinutes(30));

            // URL API yang akan diakses
            var encoding = new ASCIIEncoding();

            var url = new Uri(GetURL(type, function));

            string jsonReq = JsonConvert.SerializeObject(request);
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler) { Timeout = Timeout.InfiniteTimeSpan })
            {

                // Mengirim GET request ke API dengan parameter
                var content = new StringContent(jsonReq, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri($"{url.Scheme}://{url.Host}:{url.Port}");
                var result = await client.PutAsync(url.PathAndQuery, content, _cts.Token);

                string message = (result.Content.Headers.ContentLength == 0) ? (int)result.StatusCode + " - " + result.ReasonPhrase + " | " + result.RequestMessage.RequestUri.AbsoluteUri + ". Result: " + result.Content.ReadAsStringAsync().Result : result.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Request: {JsonConvert.SerializeObject(request)}, Response: {result}, Message: {message} [{nameof(Helper)}][{nameof(HitMicroServices_Put)}]");
                return result;
            }
        }

        public static async Task<HttpResponseMessage> HitMicroServices_Post(string type, string function, dynamic request)
        {
            using var _cts = new CancellationTokenSource(TimeSpan.FromMinutes(30));

            // URL API yang akan diakses
            var encoding = new ASCIIEncoding();

            var url = new Uri(GetURL(type, function));

            string jsonReq = JsonConvert.SerializeObject(request);
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler) { Timeout = Timeout.InfiniteTimeSpan })
            {

                // Mengirim GET request ke API dengan parameter
                var content = new StringContent(jsonReq, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri($"{url.Scheme}://{url.Host}:{url.Port}");
                var result = await client.PostAsync(url.PathAndQuery, content, _cts.Token);

                string message = (result.Content.Headers.ContentLength == 0) ? (int)result.StatusCode + " - " + result.ReasonPhrase + " | " + result.RequestMessage.RequestUri.AbsoluteUri + ". Result: " + result.Content.ReadAsStringAsync().Result : result.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Request: {JsonConvert.SerializeObject(request)}, Response: {result}, Message: {message} [{nameof(Helper)}][{nameof(HitMicroServices_Post)}]");
                return result;
            }
        }

        public static async Task<HttpResponseMessage> HitMicroServices_Delete(string type, string function, string? param = null)
        {
            using var _cts = new CancellationTokenSource(TimeSpan.FromMinutes(30));

            string url = GetURL(type, function) + ((string.IsNullOrEmpty(param)) ? String.Empty : param);

            //Hit API
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (var httpClient = new HttpClient(clientHandler) { Timeout = Timeout.InfiniteTimeSpan })
            {
                var result = await httpClient.DeleteAsync(url, _cts.Token);

                string message = (result.Content.Headers.ContentLength == 0) ? (int)result.StatusCode + " - " + result.ReasonPhrase + " | " + result.RequestMessage.RequestUri.AbsoluteUri + ". Result: " + result.Content.ReadAsStringAsync().Result : result.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Request: {url}, Response: {result}, Message: {message} [{nameof(Helper)}][{nameof(HitMicroServices_Delete)}]");
                return result;
            }
        }

        public static async Task<HttpResponseMessage> HitMicroServices_Get(string type, string function, string? param = null)
        {
            using var _cts = new CancellationTokenSource(TimeSpan.FromMinutes(30));

            string url = GetURL(type, function) + ((string.IsNullOrEmpty(param)) ? String.Empty : param);

            //Hit API
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            try
            {
                using (var httpClient = new HttpClient(clientHandler) { Timeout = Timeout.InfiniteTimeSpan })
                {
                    var result = await httpClient.GetAsync(url, _cts.Token);

                    string message = (result.Content.Headers.ContentLength == 0) ? (int)result.StatusCode + " - " + result.ReasonPhrase + " | " + result.RequestMessage.RequestUri.AbsoluteUri + ". Result: " + result.Content.ReadAsStringAsync().Result : result.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Request: {url}, Response: {result}, Message: {message} [{nameof(Helper)}][{nameof(HitMicroServices_Get)}]");
                    return result;
                }
            }
            catch (HttpRequestException ex)
            {
                return HandleHttpRequestException(ex);
            }
            catch (Exception ex)
            {
                HttpRequestException reqEx = new HttpRequestException(ex.Message, ex);
                return HandleHttpRequestException(reqEx);
            }
        }
        #endregion

        static HttpResponseMessage HandleHttpRequestException(HttpRequestException ex)
        {
            // Handle the exception here and return an appropriate HttpResponseMessage
            // In this example, just returning a 500 Internal Server Error response
            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                ReasonPhrase = "Internal Server Error",
                Content = new StringContent($"An error occurred while processing the request: {ex.Message}")
            };
        }

        public static string GetURL(string type, string function)
        {
            string apps = String.Empty;
            if (type.ToLower() == "checkquota")
            {
                apps = "CheckQuota";
            }

            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
            var WebAPI = configuration.GetSection("URL").GetSection(apps).GetSection(function).Value;

            return WebAPI;
        }
    }
}
