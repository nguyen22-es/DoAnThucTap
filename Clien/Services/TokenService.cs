using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Client.Services
{
    /* public class TokenService : ITokenService
     {
         public readonly IOptions<IdentityServerSettings> identityServerSettings;
         public readonly DiscoveryDocumentResponse discoveryDocument;
         private readonly HttpClient httpClient;

         public TokenService(IOptions<IdentityServerSettings> identityServerSettings)
         {
             this.identityServerSettings = identityServerSettings;
             httpClient = new HttpClient();
             discoveryDocument = httpClient.GetDiscoveryDocumentAsync(this.identityServerSettings.Value.DiscoveryUrl).Result;

             if (discoveryDocument.IsError)
             {
                 throw new Exception("Unable to get discovery document", discoveryDocument.Exception);
             }
         }

         public async Task<TokenResponse> GetToken(string scope)
         {
             var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
             {
                 Address = discoveryDocument.TokenEndpoint,
                 ClientId = identityServerSettings.Value.ClientName,
                 ClientSecret = identityServerSettings.Value.ClientPassword,
                 Scope = "CoffeeAPI.read",

             });

             if (tokenResponse.IsError)
             {
                 throw new Exception("Unable to get token", tokenResponse.Exception);
             }

             return tokenResponse;
         }
     }*/
    public class TokenService : ITokenService
    {
        private DiscoveryDocumentResponse _discDocument { get; set; }
        public TokenService()
        {
            using (var client = new HttpClient())
            {
                _discDocument = client.GetDiscoveryDocumentAsync("https://localhost:5443/.well-known/openid-configuration").Result;
            }
        }
        public async Task<TokenResponse> GetToken(string scope)
        {
            using (var client = new HttpClient())
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _discDocument.TokenEndpoint,
                    ClientId = "cwm.client",
                    Scope = scope,
                    ClientSecret = "secret"
                });
                if (tokenResponse.IsError)
                {
                    throw new Exception("Token Error");
                }
                return tokenResponse;
            }
        }
    }
}
