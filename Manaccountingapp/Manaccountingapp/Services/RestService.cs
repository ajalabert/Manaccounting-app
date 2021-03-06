﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using Manaccountingapp.Models;
using Newtonsoft.Json;

namespace Manaccountingapp.Services
{
    public class RestService : IRestService
    {
        private readonly HttpClient _client;

        public RestService()
        {
            _client = new HttpClient {MaxResponseContentBufferSize = 256000};
        }

        public async Task<Product> GetProductDataAsync(string url, string token)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "JWT " + token);
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(content);
            }

            return new Product(); //TODO
        }

        public async Task<List<Product>> GetProductsDataAsync(string url, string token)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "JWT " + token);
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Product>>(content);
            }

            return new List<Product>(); //TODO
        }

        public async Task<LoginResponse> LoginUserPostAsync(string url, LoginInfo loginInfo)
        {
            var json = JsonConvert.SerializeObject(loginInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Clear();
            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResponse>(responseContent);
            }

            return new LoginResponse(); //TODO
        }

        public async Task<OrderResponse> OrderPostAsync(string url, OrderInfo orderInfo, string token)
        {
            var json = JsonConvert.SerializeObject(orderInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "JWT " + token);
            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderResponse>(responseContent);
            }

            return new OrderResponse(); //TODO
        }
    }
}