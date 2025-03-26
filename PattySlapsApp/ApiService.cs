using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
namespace PattySlapsApp
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7143/api/") };
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("Employee");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Employee>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<HttpResponseMessage> UpdateEmployeeAsync(Employee employee)
        {
            return await _httpClient.PutAsJsonAsync($"Employee/{employee.EmployeeID}", employee);
        }

        public async Task<HttpResponseMessage> DeleteEmployeeAsync(int employeeId)
        {
            string fullUrl = _httpClient.BaseAddress + $"Employee/{employeeId}";

            var response = await _httpClient.DeleteAsync($"Employee/{employeeId}");

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ API Delete Error: {errorMessage}");
                MessageBox.Show($"❌ Failed to delete. Status: {response.StatusCode}\nError: {errorMessage}");
            }

            return response;
        }

        public async Task<List<InventoryRecord>> GetInventoryRecordsAsync()
        {
            var response = await _httpClient.GetAsync("Inventory");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<InventoryRecord>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<HttpResponseMessage> AddInventoryRecordAsync(InventoryRecord record)
        {
            return await _httpClient.PostAsJsonAsync("Inventory", record);
        }

        public async Task<HttpResponseMessage> UpdateInventoryRecordAsync(int recordId, InventoryRecord record)
        {
            return await _httpClient.PutAsJsonAsync($"api/InventoryRecord/{recordId}", record);
        }

        public async Task<HttpResponseMessage> DeleteInventoryRecordAsync(int id)
        {
            return await _httpClient.DeleteAsync($"Inventory/{id}");
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            var response = await _httpClient.GetAsync("Item");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Item>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            var response = await _httpClient.GetAsync($"Item/{itemId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Item>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}