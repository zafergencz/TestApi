namespace TestApi.Application.Models.Responses;

    public abstract class AbstractResponseModel{
        public string? responseCode {get;set;}
        public string? responseMessage {get;set;}
        public bool status {get;set;}
    } 


