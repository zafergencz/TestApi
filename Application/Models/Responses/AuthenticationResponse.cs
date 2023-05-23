namespace TestApi.Application.Models.Responses;

public class AuthenticationResponse{

    public bool fail {get;set;}
    public int statusCode {get;set;}
    public AuthenticationResult? result {get;set;}
    public int count {get;set;}
    public string? responseCode {get;set;}
    public string? responseMessage {get;set;}
}