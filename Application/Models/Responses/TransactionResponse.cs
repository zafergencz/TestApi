namespace TestApi.Application.Models.Responses;

public class TransactionResponse: AbstractResponseModel{

    public long? transactionId { get; set; }
    public int? statusCode { get; set; }
}