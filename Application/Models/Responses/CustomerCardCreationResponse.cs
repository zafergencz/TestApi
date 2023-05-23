namespace TestApi.Application.Models.Responses;

    public class CustomerCardCreationResponse : AbstractResponseModel
    {
        public string? name {get;set;}
        public string? surname {get;set;}
        public int? birthDate{get;set;}
        public long? identityNo {get;set;}
        public long customerId {get;set;}
        public string? customerStatus {get;set;}
    }





