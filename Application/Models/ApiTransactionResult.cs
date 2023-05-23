namespace TestApi.Application.Models;

public class ApiTransactionResult{
    public string? responseCode {get; set;}
    public string? responseMessage {get; set;}
    public string? orderId {get; set;}
    public string? txnType {get; set;}
    public string? txnStatus {get; set;}
    public int? vposId {get; set;}
    public string? vposName {get; set;}
    public string? authCode {get; set;}
    public string? hostReference {get; set;}
    public string? totalAmount {get; set;}
}