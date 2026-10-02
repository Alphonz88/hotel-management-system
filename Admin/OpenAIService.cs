
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using DocumentFormat.OpenXml.EMMA;
public class OpenAIOptions
{
	public string ApiKey { get; set; }
	public string Endpoint { get; set; }
	public string GptModel { get; set; }
}

public class OpenAIService
{
	private readonly HttpClient _httpClient;
	private readonly string _apiKey;
	private readonly string _endpoint;
	private readonly string _model;

	public OpenAIService( IOptions<OpenAIOptions> options)
	{
		_apiKey = options.Value.ApiKey;
		_endpoint = options.Value.Endpoint;
		_model = options.Value.GptModel;
	}

	public async Task<string> AnalyzeImageWithGPT(string base64Image, string prompt = null)
	{
		using (var httpClient = new HttpClient())
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

			var payload = new
			{
				model = _model,
				messages = new object[]
				{
			  new {
              role="system",
              content = "You are an assistant that answers questions about images."
              },
              new{
				  role = "user",
				  content = new object[]
				  {
					  new { type = "text", text = "Is there a human in this image? Answer yes or no." },
					  new {
						  type = "image_url",
						  image_url = new {
							  url = $"data:image/jpeg;base64,{base64Image}"
						  }
					  }
				  }
			  }
		  },
				max_tokens = 100
			};

			var jsonPayload = JsonConvert.SerializeObject(payload);
			var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(_endpoint, content);
			var result = await response.Content.ReadAsStringAsync();

			dynamic json = JsonConvert.DeserializeObject(result);
			return json?.choices[0]?.message?.content ?? "No response";
		}
	}
public async Task<string> GenerateAlertContent(string prompt)
	{
		using (var httpClient = new HttpClient())
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

			var payload = new
			{
				model = _model,
				messages = new[]
				{
				new { role = "user", content = prompt }
			},
				temperature = 0.5,
				max_tokens = 1000
			};

			var jsonPayload = JsonConvert.SerializeObject(payload);
			var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(_endpoint, content);
			var result = await response.Content.ReadAsStringAsync();

			dynamic json = JsonConvert.DeserializeObject(result);
			return json?.choices[0]?.message?.content ?? "No response";
		}
	}

}


