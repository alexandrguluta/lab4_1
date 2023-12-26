using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAppIImpl.remote;
using WebAppIImpl.remote.models;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient()
    {
        _httpClient = new HttpClient();
        
        _httpClient.BaseAddress = new Uri("https://localhost:7276/api/");
    }

    public async Task<string?>? LoginUserAsync(string username, string password)
    {
       
        var authData = new { Username = username, Password = password };

        var json = JsonConvert.SerializeObject(authData);
        
        var response = await _httpClient.PostAsync("authentication/login", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<TokenModel>(jsonResult);
            return authResult?.Token;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<string?> RegistrationUserAsync(RegistrationModel registrationModel)
    {
        var json = JsonConvert.SerializeObject(registrationModel);
        
        var response = await _httpClient.PostAsync("authentication", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<TokenModel>(jsonResult);
            return authResult?.Token;
        }
        else
        {
            return null;
        }
    }

    public async Task<ObservableCollection<UserModel>?> GetUsersAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync("Users");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<UserModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<ObservableCollection<CompanyModel>?> GetCompaniesAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync("companies");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<CompanyModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }

    public async Task<ObservableCollection<AdminModel>?> GetAdminsByUserAsync(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync($"Users/{id}/Admins");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<AdminModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<ObservableCollection<EmployeeModel>?> GetEmployeesByCompanyAsync(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync($"companies/{id}/employees");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<EmployeeModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<UserModel?> PostCreateUserAsync(UserCreationModel UserCreationModel)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        var json = JsonConvert.SerializeObject(UserCreationModel);
        
        HttpResponseMessage response = await _httpClient.PostAsync($"Users", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<UserModel>(jsonResult);
            return authResult;
        }

        return null;
    }

    public async Task<AdminModel?> PostAdminForUserAsync(AdminCreationModel AdminCreationModel, Guid UserId)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);

        var json = JsonConvert.SerializeObject(AdminCreationModel);

        HttpResponseMessage response = await _httpClient.PostAsync($"Users/{UserId}/Admins", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<AdminModel>(jsonResult);
            return authResult;
        }

        return null;
    }

    public async Task DeleteUserdAsync(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);

        await _httpClient.DeleteAsync($"Users/{id}");
    }
    
    public async Task<UserModel?> PutUpdateUserdAsync(UserCreationModel AdminBrandCreationModel, Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        var json = JsonConvert.SerializeObject(AdminBrandCreationModel);
        
        HttpResponseMessage response = await _httpClient.PutAsync($"Users/{id}", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<UserModel>(jsonResult);
            return authResult;
        }

        return null;
    }
    
    public async Task DeleteAdminAsync(Guid id, Guid UserId)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);

        await _httpClient.DeleteAsync($"Users/{UserId}/Admins/{id}");
    }
}