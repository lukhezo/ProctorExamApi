using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api_request_helpers.ExtensionMethods
{
    internal static class GenerateParameters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary">A dictionary unsigned with a HMAC SHA256 signature</param>
        /// <returns>A dictionary signed with a HMAC SHA256 signature</returns>
        internal static Dictionary<string, string> GenerateParamsWithSignature(Dictionary<string, string> dictionary)
        {
            DateTime seconds = DateTime.UtcNow;
            long nonce = ((DateTimeOffset)seconds).ToUnixTimeSeconds();

            DateTime milliseconds = DateTime.UtcNow;
            long timestamp = ((DateTimeOffset)milliseconds).ToUnixTimeMilliseconds();
    
            dictionary.Add("nonce", nonce.ToString());
            dictionary.Add("timestamp", timestamp.ToString());

            // Acquire keys and sort them.
            var list = dictionary.Keys.ToList();
            list.Sort();

            StringBuilder sb = new StringBuilder();

            StringBuilder noId = new StringBuilder();

            // build a signature using the user secret_key and a string build using params ordered in alphabetical order by key and joined with "&" in the form "key=value"
            foreach (var key in list)
            {
                sb.Append($"{key}={dictionary[key]}?");
            }

            // build a signature using the user secret_key and a string build using params ordered in alphabetical order by key and joined with "&" in the form "key=value"
            foreach (var key in list)
            {
                if (key != "id")
                {            
                    noId.Append($"{key}={dictionary[key]}?");
                }
            }

            // remove trailing ?
            string url = sb.ToString().TrimEnd('?');

            string signature = url.ToHmacSha256String(Credential.SecretKey);            
            string signatureUrl = noId + "signature=" + signature;

            dictionary.Add("signature", null);
            dictionary["signature"] = signature;
            dictionary.Add("queryString", signatureUrl.Replace('?', '&')); 
            return dictionary;
        }
    }
}


