using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Gddkia.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gddkia.Clients
{
    public class GddkiaClient : IGddkiaClient
    {
        private readonly HttpClient _httpClient;

        public GddkiaClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GddkiaResponse> GetReport()
        {
            var data = await _httpClient.GetStringAsync("https://www.gddkia.gov.pl/dane/zima_html/utrdane.xml");
            var doc = new XmlDocument();
            doc.LoadXml(data);
            var json = JsonConvert.SerializeXmlNode(doc);
                var foo = JsonConvert.DeserializeObject<GddkiaResponse>(json, new JsonSerializerSettings
            {
                Error = HandleDeserializationError
            });
            return foo;
        }
        
        public void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }
    }
}