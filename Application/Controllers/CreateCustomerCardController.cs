using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using TestApi.Application.Models;
using TestApi.Application.Models.Requests;
using TestApi.Application.Models.Responses;
using TestApi.Application.Interfaces;
using TestApi.Application.Common;

namespace TestApi.Application.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CreateCustomerCardController:ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IKpsServiceClient _kpsServiceClient;    
       

        public CreateCustomerCardController( ICustomerRepository customerRepository, IKpsServiceClient kpsServiceClient)
        {
            _customerRepository = customerRepository;
            _kpsServiceClient = kpsServiceClient;
        }

        [HttpPost(Name = "createCustomerCard")]
        [SwaggerOperation(OperationId = "createCustomerCard")]
        public async Task<CustomerCardCreationResponse> createCustomerCard(CustomerCardCreationRequest request){
            
            CustomerCardCreationResponse response = new CustomerCardCreationResponse();
            response.name = request.name;
            response.surname = request.surname;
            response.identityNo = request.identityNo;

            if(String.IsNullOrEmpty(request.name) || String.IsNullOrEmpty(request.surname) || request.identityNo == null || request.birthDate == null){
                response.responseCode = Constants.RESPONSE_CODE_EMPTY_FIELDS;
                response.responseMessage = Constants.RESPONSE_MESSAGE_CUSTOMER_EMPTY_FIELDS;
                return response;
            }

            Customer cust = new Customer();
            cust.name = request.name;
            cust.surname = request.surname;
            cust.identityNo = request.identityNo;
            cust.birthDate = request.birthDate;
            cust.identityVerified = false;
            cust.status = Constants.CUSTOMER_WAITING;
        

            try{
                Customer? customerCheck = _customerRepository.GetByIdentityNoAndStatus(cust.identityNo, Constants.CUSTOMER_ACTIVE);
                if(customerCheck != null){
                    response.responseMessage = Constants.RESPONSE_MESSAGE_ALREADY_ACTIVE_CUSTOMER;
                    response.responseCode = Constants.RESPONSE_CODE_ALREADY_ACTIVE_CUSTOMER;
                    response.customerStatus = customerCheck.status;
                    return response;
                }

                _customerRepository.Add(cust);           
                bool identityVerification =  await _kpsServiceClient.identityVerification((long)request.identityNo, request.name, request.surname, (int)request.birthDate);
                cust.identityVerified = identityVerification;
                cust.status = identityVerification ? Constants.CUSTOMER_ACTIVE: Constants.CUSTOMER_CANCEL;
                _customerRepository.Update(cust);
                response.responseCode = Constants.RESPONSE_CODE_SUCCESS;
                response.responseMessage = Constants.RESPONSE_MESSAGE_SUCCESSFULL;
                response.customerId = cust.customerId;
                response.customerStatus = cust.status;
                response.status = true;
                return response;
            }catch(Exception ex){
                Log.Error(ex.Message);
                response.responseMessage = ex.Message;
                response.responseCode = Constants.RESPONSE_CODE_INTERNAL_ERROR;
                return response;
            }
        }
    }
}