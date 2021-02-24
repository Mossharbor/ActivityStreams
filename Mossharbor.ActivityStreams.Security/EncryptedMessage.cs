using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Mossharbor.ActivityStreams;

namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// A class of messages that are obfuscated in some cryptographic manner. These messages are incredibly difficult to decrypt without the proper decryption key.
    /// </summary>
    /// <example>{
    ///  "@context": "https://w3id.org/security/v1",
    ///  "@type": "EncryptedMessage",
    ///  "cipherData": "VTJGc2RHVmtYMThOY3h2dnNVN290Zks1dmxWc3labi9sYkU0TGloRTdxY0dpblE4OHgrUXFNNi9l\n↩
    ///a1JMWjdXOApRSGtrbzh6UG5XOFo3WWI4djJBUG1abnlRNW5CVGViWkRGdklpMEliczNWSzRQTGdB\n↩
    ///UFhxYTR2aWFtemwrWGV3Cmw0eFF4ancvOW85dTlEckNURjMrMDBKMVFubGdtci9abkFRSmc5UjdV\n↩
    ///Rk55ckpYalIxZUJuKytaQ0luUTF2cUwKcm5vcDU1eWk3RFVqVnMrRXZZSkx6RVF1VlBVQ0xxdXR4\n↩
    ///L3lvTWd4bkdhSksxOG5ZakdiekJxSGxOYm9pVStUNwpwOTJ1Q0Y0Q2RiR1NqL0U3OUp4Vmh6OXQr\n↩
    ///Mjc2a1V3RUlNY3o2Z3FadXZMU004KzRtWkZiakh6K2N5a1VVQ2xHCi9RcTk3b2o3N2UrYXlhZjhS\n↩
    ///ZmtEZzlUeWk3Q2szREhSblprcy9WWDJWUGhUOEJ5c3RiYndnMnN4eWc5TXhkbHoKUkVESzFvR0FJ\n↩
    ///UDZVQ09NeWJLTGpBUm0zMTRmcWtXSFdDY29mWFNzSGNPRmM2cnp1Wko0RnVWTFNQMGROUkFRRgpB\n↩
    ///bFQ0QUpPbzRBZHpIb2hpTy8vVGhNOTl1U1ZER1NPQ3graFAvY3V4dGNGUFBSRzNrZS8vUk1MVFZO\n↩
    ///YVBlaUp2Ckg4L1ZWUVU4L3dLZUEyeTQ1TzQ2K2lYTnZsOGErbGg0NjRUS3RabktFb009Cg==",
    ///  "cipherKey": "uATtey0c4nvZIsDIfpFffuCyMYGPKRLIVJCsvedE013SpEJ+1uO7x6SK9hIG9zLWRlPpwmbar2bt\n↩
    ///gTX5NBzYC5+c5ZkXtM1O8REwIJ7wmtLdumRYEbr/TghFl3pAgXhv0mVt8XZ+KLWlxMqXnrT+ByNw\n↩
    ///z7u3IxpqNVcXHExjXQE=",
    ///  "cipherAlgorithm": "aes-128-gcm",
    ///  "authenticationTag": "q25H1CzsE731OmeyEle93w==",
    ///  "initializationVector": "vcDU1eWTy8vVGhNOszREhSblFVqVnGpBUm0zMTRmcWtMrRX=="
    ///  "publicKey": "https://example.com/people/john/keys/23"
    ///}
    ///</example>
    ///<remarks>The example below expresses a message that has been encrypted using an AES cipher in Galois/Counter Mode and a 128-bit key (AES-128-GCM). 
    ///The key has been encrypted using an RSA public key. The encrypted message, cipherData, and encrypted key, cipherKey, are both base64-encoded. 
    ///To decrypt the message, first the cipherKey must be decrypted using the private key associated with the publicKey. 
    ///Then, the cipherData can be decrypted using the decrypted cipherKey, cipherAlgorithm, initializationVector, and authenticationTag.</remarks>
    public class EncryptedMessage : SecurityObjectBase, ICustomParser
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string EncryptedMessageType = "EncryptedMessage";

        /// <summary>
        /// Constructor
        /// </summary>
        public EncryptedMessage() : base(EncryptedMessage.EncryptedMessageType) { }

        /// <summary>
        /// The cipher algorithm describes the mechanism used to encrypt a message. It is typically a string expressing the cipher suite, the strength of the cipher, and a block cipher mode.
        /// </summary>
        public string Algorithm { get; set; }

        /// <summary>
        /// The value of the authTag
        /// </summary>
        public string Authtag { get; set; }

        /// <summary>
        /// The initialization Vector
        /// </summary>
        public string IV { get; set; }

        /// <summary>
        /// The Public Key
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// Cipher data an opaque blob of information that is used to specify an encrypted message.
        /// </summary>
        public string CipherData{ get; set; }

        /// <summary>
        /// A cipher key is a symmetric key that is used to encrypt or decrypt a piece of information. The key itself may be expressed in clear text or encrypted.
        /// </summary>
        public string CipherKey { get; set; }

        /// <inheritdoc/>
        public override void PerformCustomParsing(JsonElement el)
        {
            base.PerformCustomParsing(el);

            var algorithm = el.GetStringOrDefault("cipherAlgorithm");
            var authenticationTag = el.GetStringOrDefault("authenticationTag");
            var initializationVector = el.GetStringOrDefault("initializationVector");
            var publicKey = el.GetStringOrDefault("publicKey");
            var cipherData = el.GetStringOrDefault("cipherData");
            var cipherKey = el.GetStringOrDefault("cipherKey");

            this.Algorithm = algorithm;
            this.Authtag = authenticationTag;
            this.IV = initializationVector;
            this.CipherData = cipherData;
            this.CipherKey = cipherKey;
            this.PublicKey = publicKey;
        }
    }
}
