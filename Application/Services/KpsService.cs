using ServiceReference; 
using TestApi.Application.Common;

namespace TestApi.Application.Services;
public class KpsServiceClient:IKpsServiceClient
{
    private readonly KPSPublicSoapClient _serviceClient;


    public KpsServiceClient(IConfiguration configuration)
    {
        string serviceUrl = configuration.GetValue<string>("kpsService") ?? Constants.SERVICE_URL_KPS;

        _serviceClient = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap12, serviceUrl);
    }

    public async Task<bool> identityVerification(long identity, String name, String surname, int birthDate)
    {
        var response = await _serviceClient.TCKimlikNoDogrulaAsync(identity, name, surname, birthDate);

        return response.Body.TCKimlikNoDogrulaResult;
    
    }
}

