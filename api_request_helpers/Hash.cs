using System.Security.Cryptography;
using System.Text;

namespace api_request_helpers
{
    public static class Hash
    {
        /// <summary>
        /// HMACSHA256 is a type of keyed hash algorithm that is constructed from the SHA-256 hash function and used as a Hash-based Message Authentication Code (HMAC).
        /// The HMAC process mixes a secret key with the message data, hashes the result with the hash function, mixes that hash value with the secret key again, and 
        /// then applies the hash function a second time. The output hash is 256 bits in length.
        /// An HMAC can be used to determine whether a message sent over an insecure channel has been tampered with, provided that the sender and receiver share a 
        /// secret key.The sender computes the hash value for the original data and sends both the original data and hash value as a single message.
        /// The receiver recalculates the hash value on the received message and checks that the computed HMAC matches the transmitted HMAC.
        /// Any change to the data or the hash value results in a mismatch, because knowledge of the secret key is required to change the message and reproduce the 
        /// correct hash value.Therefore, if the original and computed hash values match, the message is authenticated.
        /// HMACSHA256 accepts keys of any size, and produces a hash sequence 256 bits in length.
        /// </summary>
        /// <param name="str">The String</param>
        /// <param name="secretKey">The Secret Key</param>
        /// <returns>The output hash which is 256 bits in length</returns>
        public static string ToHmacSha256String(this string str, string secretKey)
        {
            StringBuilder sb = new StringBuilder();
            Encoding enc = Encoding.Default;
            Encoding keyEnc = Encoding.Default;      

            using (HMACSHA256 hash = new HMACSHA256(keyEnc.GetBytes(secretKey)))
            {           
                byte[] result = hash.ComputeHash(enc.GetBytes(str));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}
