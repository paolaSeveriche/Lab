using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Lab.WEB.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //Tiempo de espera
            await Task.Delay(3000);// espera 1,2,3
            var anonimous = new ClaimsIdentity();
            //oapuser usario del seeDd

            //Claim-> Especie de rol ó firma
            var oapUser = new ClaimsIdentity(new List<Claim>
        {
            new Claim("FirstName", "Luis"),
            new Claim("LastName", "O"),
            new Claim(ClaimTypes.Name, "oap@yopmail.com"),
             new Claim(ClaimTypes.Role, "Admin")
            },

            authenticationType: "test");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(oapUser)));
        }
    }
}

