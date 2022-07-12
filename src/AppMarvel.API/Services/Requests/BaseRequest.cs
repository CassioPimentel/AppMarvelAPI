using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppMarvel.API.Services
{
    public static class BaseRequest
    {
        public static async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
        }

        public static string CreateHash(string input)
        {
            var hash = string.Empty;
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                hash = sBuilder.ToString();
            }
            return hash;
        }
    }
}