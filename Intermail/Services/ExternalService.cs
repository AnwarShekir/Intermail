using System;
using System.Net;
using Intermail.Dto;

namespace Intermail.Services
{
	public class ExternalService : IExternalService
	{
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAppConfiguration _appConfig;
        private readonly IAppLogger _logger;

        public ExternalService(IHttpClientFactory httpClientFactory, IAppConfiguration appConfig, IAppLogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _appConfig = appConfig;
            _logger = logger;
        }

        public async Task SendLoyaltyPoint(RequestDto request)
        {
            _logger.Info($"New request. UserId: {request.CustomerId}, Amount: {request.Amount}");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _appConfig.ExternalToken);
            var url = _appConfig.ExternalUrl + $"/api/customer/{request.CustomerId}/loyaltypoints";
            var response = await client.PostAsJsonAsync(url, new ExternalRequestDto(request.Amount));

            if(response.StatusCode != HttpStatusCode.OK)
            {
                _logger.Error($"New Error reponse: CusomterId: {request.CustomerId} ErrorCode: {response.StatusCode}");
                throw new Exception("Error at external service");
            }

            _logger.Info($"New Ok Reponse: CusomterId: {request.CustomerId} ErrorCode: {response.StatusCode}");

        }
    }

    
}

