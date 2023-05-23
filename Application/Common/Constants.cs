namespace TestApi.Application.Common;

public static class Constants{

    
    public static readonly string CUSTOMER_ACTIVE = "ACTIVE";
    public static readonly string CUSTOMER_CANCEL = "CANCEL";
    public static readonly string CUSTOMER_WAITING = "WAITING";

    public static  readonly string RESPONSE_CODE_API_SUCCESS = "00";
    public static readonly string RESPONSE_MESSAGE_SUCCESSFULL = "Successfull";
    public static readonly string RESPONSE_CODE_SUCCESS = "200";
    public static readonly string RESPONSE_CODE_INTERNAL_ERROR = "500";
    public static readonly string RESPONSE_CODE_EMPTY_FIELDS = "900";
    public static readonly string RESPONSE_MESSAGE_CUSTOMER_EMPTY_FIELDS = "Please fill customer fields";
    public static readonly string RESPONSE_CODE_ALREADY_ACTIVE_CUSTOMER = "910";
    public static readonly string RESPONSE_MESSAGE_ALREADY_ACTIVE_CUSTOMER = "There is already an active record with this identityNo";
    public static readonly string RESPONSE_MESSAGE_TRANSACTION_API_ERROR_SERVICE_CALL = "Error occured while sending data to transaction service";
    public static readonly string RESPONSE_MESSAGE_TRANSACTION_API_ERROR_RESPONSE_NULL = "Response or Response.Result come null from transaction service";
    public static readonly string RESPONSE_MESSAGE_TRANSACTION_API_ERROR_FAILURE_RESPONSE_CODE = "Transaction Service return failure response code ";
    public static readonly string RESPONSE_MESSAGE_TRANSACTION_AUTHANTICATION_ERROR_SERVICE_CALL = "Error occured while getting token from authentication service";
    public static readonly string RESPONSE_MESSAGE_TRANSACTION_AUTHANTICATION_ERROR_RESPONSE_NULL = "Response or Response.Result come null from authentication service";
    public static readonly string RESPONSE_MESSAGE_TRANSACTION_AUTHANTICATION_ERROR_FAILURE_RESPONSE_CODE = "Authentication Service return failure response code";
    public static readonly string RESPONSE_MESSAGE_CUSTOMER_SERVICE_EMPTY_MAIL = "Please fill email field";
    public static readonly string RESPONSE_MESSAGE_AUTHANTICATION_TOKEN_IS_NULL = "Authantication Token is Null";
    public static readonly string RESPONSE_CODE_AUTHANTICATION_TOKEN_IS_NULL = "920";

    public static  readonly string KEY_API = "kU8@iP3@";
    public static  readonly string KEY_LANG = "TR";
    public static readonly string KEY_AUTHORIZATION_HEADER = "Authorization";
    public static readonly string KEY_APPLICATION_JSON = "application/json";

    public static  readonly string SERVICE_URL_AUTHENTICATION = "https://ppgsecurity-test.birlesikodeme.com:55002/api/ppg/Securities/authenticationMerchant";
    public static  readonly string SERVICE_URL_API_TRANSACTION = "https://ppgpayment-test.birlesikodeme.com:20000/api/ppg/Payment/NoneSecurePayment";
    public static  readonly string SERVICE_URL_KPS = "https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx?wsdl";
    
}