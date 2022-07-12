using AppMarvel.API.Services.Parameters;
using AppMarvel.API.Services.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace AppMarvel.API.Services
{
    public class CharacterService
    {
        private string _url = "https://gateway.marvel.com:443/v1/public";
        private string _publicApiKey = "d2f4dba1963588b9f0327d0839138831";
        private string _privateApiKey = "2c733fac840ae3b81cd0ae2b66ae1b1297addb17";
        protected IRestClient Client;

        internal RestRequest CreateRequest(string requestUrl)
        {
            var request = new RestRequest(string.Concat(_url, requestUrl));
            var timestamp = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds.ToString();

            request.AddParameter("apikey", _publicApiKey);
            request.AddParameter("ts", timestamp);
            request.AddParameter("hash", BaseRequest.CreateHash($"{timestamp}{_privateApiKey}{_publicApiKey}"));
            request.AddHeader("Accept", "*/*");

            return request;
        }

        public async Task<BaseResponse> GetAll(CharacterParameter characterParameter)
        {
            var request = CreateRequest("/characters");

            if (!string.IsNullOrWhiteSpace(characterParameter.Name))
            {
                request.AddParameter("name", characterParameter.Name);
            }

            if (!string.IsNullOrWhiteSpace(characterParameter.NameStartsWith))
            {
                request.AddParameter("nameStartsWith", characterParameter.NameStartsWith);
            }

            RestClient client = new();

            IRestResponse<BaseResponse> response = await client.ExecuteAsync<BaseResponse>(request);

            return JsonConvert.DeserializeObject<BaseResponse>(response.Content);
        }

        public async Task<BaseResponse> GetById(int id)
        {
            var request = CreateRequest("/characters/" + id);

            RestClient client = new();

            IRestResponse<BaseResponse> response = await client.ExecuteAsync<BaseResponse>(request);

            return JsonConvert.DeserializeObject<BaseResponse>(response.Content);
        }
    }
}