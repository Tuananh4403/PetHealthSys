using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Helpers
{
    public static class CommonHelpers
    {
          public static int? GetUserIdByToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {

                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;


                var userIdClaim = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "id");


                if (int.TryParse(userIdClaim?.Value, out int userId))
                {
                    return userId;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}
