using JWT;
using JWT.Serializers;

namespace DataLayer.Helpers
{
    public class TokenDeserializer
    {
        public static T Deserialize<T>(string token) where T : class
        {
            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();
            var decoder = new JwtDecoder(serializer, urlEncoder);

            var decodedToken = decoder.Decode(token);
            JsonNetSerializer jsonSerializer = new JsonNetSerializer();
            var claims = jsonSerializer.Deserialize<T>(decodedToken);
            return claims;
        }
    }
}
