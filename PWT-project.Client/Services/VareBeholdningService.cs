using PWT_project.Client.Models;
using System.Net.Http.Json;

namespace PWT_project.Client.Services
{
	public class VareBeholdningService
	{
		private readonly HttpClient _httpClient;
			
		public VareBeholdningService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}	

		public async Task<List<VareBeholdningDto>> GetAllAsync()
		{
            return await _httpClient.GetFromJsonAsync<List<VareBeholdningDto>>("https://localhost:7140/api/varer-beholdning");

        }
    }
}