using System;
using System.Text;

namespace BedrockProtocol.Utils
{
    public static class JwtUtils
    {
        public static string DecodePayload(string jwt)
        {
            var parts = jwt.Split('.');
            if (parts.Length < 2) return string.Empty;

            var payload = parts[1];
            payload = payload.Replace('-', '+').Replace('_', '/');
            switch (payload.Length % 4)
            {
                case 2: payload += "=="; break;
                case 3: payload += "="; break;
            }

            var bytes = Convert.FromBase64String(payload);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
