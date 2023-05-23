using Microsoft.AspNetCore.Mvc;
using Serilog;
using Newtonsoft.Json;
using System.Text;
using Swashbuckle.AspNetCore.Annotations;
using TestApi.Application.Models;
using TestApi.Application.Models.Requests;
using TestApi.Application.Models.Responses;
using TestApi.Application.Interfaces;
using TestApi.Application.Common;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace TestApi.Application.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SendTransactionController:ControllerBase{

        private readonly ITransactionRepository _transactionRepository;
        private readonly string _authanticationServiceUrl;  
        private readonly string _transactionApiServiceUrl;  

        public SendTransactionController(ITransactionRepository transactionRepository, IConfiguration configuration)
        {
            _transactionRepository = transactionRepository;
            _authanticationServiceUrl = configuration.GetValue<string>("authanticationApiService") ?? Constants.SERVICE_URL_AUTHENTICATION;
            _transactionApiServiceUrl = configuration.GetValue<string>("transactionApiService") ?? Constants.SERVICE_URL_API_TRANSACTION;
        }

   
        [SwaggerOperation(OperationId = "sendTransaction")]
        [HttpPost(Name = "sendTransaction")]        
        public async Task<TransactionResponse> sendTransaction(TransactionRequest request){

            TransactionResponse response = new TransactionResponse();
            if(String.IsNullOrEmpty(request.email)){
                response.responseCode = Constants.RESPONSE_CODE_EMPTY_FIELDS;
                response.responseMessage = Constants.RESPONSE_MESSAGE_CUSTOMER_SERVICE_EMPTY_MAIL;
                return response;
            }

            String lang = String.IsNullOrEmpty(request.lang) ? Constants.KEY_LANG : request.lang;
            String apiKey = String.IsNullOrEmpty(request.apiKey) ? Constants.KEY_API : request.apiKey; 

            String? token = await getTokenFromApi(request.email, lang, apiKey);

            if(String.IsNullOrEmpty(token)){
                response.responseCode = Constants.RESPONSE_CODE_AUTHANTICATION_TOKEN_IS_NULL;
                response.responseMessage = Constants.RESPONSE_MESSAGE_AUTHANTICATION_TOKEN_IS_NULL;
                return response;
            }

            ApiTransactionResponse? apiTransactionResponse = await sendTransactionToApi(createApiTransactionRequest(request, apiKey), token);

            try{
                Transaction transaction = new Transaction();
                transaction.amount = request.amount;
                transaction.cardPan = request.cardPan;
                transaction.customerId = request.customerId;
                transaction.orderId = request.orderId;

                if(apiTransactionResponse == null || apiTransactionResponse.result == null){
                    transaction.responseCode = null;
                    transaction.responseMessage = null;
                }else{
                    transaction.responseCode = apiTransactionResponse.result.responseCode;
                    transaction.responseMessage = apiTransactionResponse.result.responseMessage;
                }
                
                transaction.status = apiTransactionResponse == null ? null : apiTransactionResponse.statusCode;
                transaction.typeId = request.txnType;

                _transactionRepository.Add(transaction);   

                response.transactionId = transaction.transactionId;
                response.statusCode = transaction.status;
                response.responseMessage = transaction.responseMessage;
                response.responseCode = transaction.responseCode;

                return response;

            }catch(Exception ex){
                response.responseCode = Constants.RESPONSE_CODE_INTERNAL_ERROR;
                response.responseMessage = ex.Message;
                return response;
            }
        }        

        private async Task<ApiTransactionResponse?> sendTransactionToApi(ApiTransactionRequest apiTransactionRequest, string? token){

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add(Constants.KEY_AUTHORIZATION_HEADER, token);
            var jsonBody = JsonConvert.SerializeObject(apiTransactionRequest);
            var requestBody = new StringContent(jsonBody, Encoding.UTF8, Constants.KEY_APPLICATION_JSON);
            var response = await httpClient.PostAsync(_transactionApiServiceUrl, requestBody);

            if (!response.IsSuccessStatusCode){
                Log.Warning(Constants.RESPONSE_MESSAGE_TRANSACTION_API_ERROR_SERVICE_CALL);
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            ApiTransactionResponse? responseObject = JsonConvert.DeserializeObject<ApiTransactionResponse>(responseBody);

            if(responseObject == null || responseObject.result == null){
                Log.Warning(Constants.RESPONSE_MESSAGE_TRANSACTION_API_ERROR_RESPONSE_NULL); 
                return null;
            }

            if(String.IsNullOrEmpty(responseObject.result.responseCode) || !responseObject.result.responseCode.Equals(Constants.RESPONSE_CODE_API_SUCCESS)){
                Log.Warning(Constants.RESPONSE_MESSAGE_TRANSACTION_API_ERROR_FAILURE_RESPONSE_CODE + responseObject.result.responseCode + " " + responseObject.result.responseMessage);
                return null;
            }

            return responseObject; 
        }      

        private async Task<String?> getTokenFromApi(String? email, string lang, string apiKey){


            AuthenticationRequest authenticationRequest = new AuthenticationRequest();
            authenticationRequest.ApiKey = apiKey;
            authenticationRequest.Lang = lang;
            authenticationRequest.Email = email;

            var httpClient = new HttpClient();
            var jsonBody = JsonConvert.SerializeObject(authenticationRequest);
            var requestBody = new StringContent(jsonBody, Encoding.UTF8, Constants.KEY_APPLICATION_JSON);


            var response = await httpClient.PostAsync(_authanticationServiceUrl, requestBody);

            if(!response.IsSuccessStatusCode){
                Log.Warning(Constants.RESPONSE_MESSAGE_TRANSACTION_AUTHANTICATION_ERROR_SERVICE_CALL);
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            AuthenticationResponse? responseObject = JsonConvert.DeserializeObject<AuthenticationResponse>(responseBody);

            if(responseObject == null || responseObject.result == null){
                Log.Warning(Constants.RESPONSE_MESSAGE_TRANSACTION_AUTHANTICATION_ERROR_RESPONSE_NULL);
                return null;
            }

            if(String.IsNullOrEmpty(responseObject.responseCode) || !responseObject.responseCode.Equals(Constants.RESPONSE_CODE_API_SUCCESS)){
                Log.Warning(Constants.RESPONSE_MESSAGE_TRANSACTION_AUTHANTICATION_ERROR_FAILURE_RESPONSE_CODE);
                return null;
            }

            return responseObject.result.token;
        }

        private ApiTransactionRequest createApiTransactionRequest(TransactionRequest request, string apiKey){

            ApiTransactionRequest apiTransactionRequest = new ApiTransactionRequest();
            apiTransactionRequest.amount = request.amount;
            apiTransactionRequest.cardPan = request.cardPan;
            apiTransactionRequest.commissionRate = request.commissionRate;
            apiTransactionRequest.currency = request.currency;
            apiTransactionRequest.customerId = request.customerId;
            apiTransactionRequest.expiryDateMonth = request.expiryDateMonth;
            apiTransactionRequest.expiryDateYear = request.expiryDateYear;
            apiTransactionRequest.installmentCount = request.installmentCount;
            apiTransactionRequest.memberId = request.memberId;
            apiTransactionRequest.orderId = request.orderId;
            apiTransactionRequest.productId = request.productId;
            apiTransactionRequest.productName = request.productName;
            apiTransactionRequest.rnd = request.rnd;
            apiTransactionRequest.totalAmount = request.totalAmount;
            apiTransactionRequest.txnType = request.txnType;
            apiTransactionRequest.userCode = request.userCode;
            apiTransactionRequest.hash = paymentHash(request, apiKey);
            return apiTransactionRequest;
        }

        private string paymentHash(TransactionRequest request, string apiKey){

            string hashString = apiKey + request.userCode + request.rnd + request.txnType 
                + request.totalAmount + request.customerId + request.orderId;
            
            SHA512 s512 = SHA512.Create();

            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            byte[] bytes = s512.ComputeHash(ByteConverter.GetBytes(hashString));

            var hash = BitConverter.ToString(bytes).Replace("-","");

            return hash;
        }

    }
}