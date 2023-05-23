namespace TestApi.Application.Models.Responses;

public class ApiTransactionResponse{
    public bool? fail {get;set;}
    public int? statusCode {get;set;}
    public ApiTransactionResult? result {get;set;}      
}