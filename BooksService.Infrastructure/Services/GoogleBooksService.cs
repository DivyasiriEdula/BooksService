using BooksService.Application.Abstractions;
using BooksService.Domain.Models;
using BooksService.Infrastructure.Exceptions;
using BooksService.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BooksService.Infrastructure.Services
{
    public class GoogleBooksService : IGoogleBooksService
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleBooksApiOptions _apiOptions;

        public GoogleBooksService(IOptions<GoogleBooksApiOptions> apiOptions, HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiOptions = apiOptions.Value;
        }

        public async Task<IEnumerable<Item>> GetBooksAsync(string title, int? maxResults)
        {
            var queryParameters = new Dictionary<string, string> {
            { "q", title },
            {"key", _apiOptions.ApiKey },
            { "maxResults", maxResults?.ToString() }};

            var queryString = string.Join("&", queryParameters
                                    .Where(x => !string.IsNullOrEmpty(x.Value))
                                    .Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));

            string url = $"{_apiOptions.ApiURI}?{queryString}";

            try
            {              

                var response = await _httpClient.GetFromJsonAsync<BooksCollection>(url);

                // Check if the response is successful
                if (response != null && response.items != null)
                {
                    return response.items;
                }
                else
                {
                    // If response is null or items are null, it indicates an issue with the API
                    throw new BooksApiException("Unable to retrieve books. API response is null or empty.");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new BooksApiException("Unable to retrieve books. Network error occurred.", ex);
            }
            catch (JsonException ex)
            {
                throw new BooksApiException("Unable to parse API response. Invalid JSON format.", ex);
            }
            catch (Exception ex)
            {
                throw new BooksApiException("An error occurred while retrieving books.", ex);
            }


        }

    }
}
