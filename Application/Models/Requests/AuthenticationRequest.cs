namespace TestApi.Application.Models.Requests;

public class AuthenticationRequest{

    public string? ApiKey {get;set;}
    public string? Email {get;set;}
    public string? Lang {get;set;}
}