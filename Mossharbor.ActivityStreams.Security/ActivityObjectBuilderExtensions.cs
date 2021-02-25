using System;

namespace Mossharbor.ActivityStreams.Security
{
    public static class ActivityObjectBuilderExtensions
    {
        public static bool added = false;

        public static void AddSecurityTypes(this ActivityObjectBuilder builder)
        {
            if (added)
                return;
            added = true;
            ActivityStreamsParser.TypeToObjectMap.Add("AesKeyWrappingKey2019", () => new AesKeyWrappingKey2019());
            ActivityStreamsParser.TypeToObjectMap.Add("DeleteKeyOperation", () => new DeleteKeyOperation());
            ActivityStreamsParser.TypeToObjectMap.Add("DeriveSecretOperation", () => new DeriveSecretOperation());
            //ActivityStreamsParser.TypeToObjectMap.Add("EcdsaSecp256k1Signature2019", () => new EcdsaSecp256k1Signature2019());
            //ActivityStreamsParser.TypeToObjectMap.Add("EcdsaSecp256r1Signature2019", () => new EcdsaSecp256r1Signature2019());
            //ActivityStreamsParser.TypeToObjectMap.Add("EcdsaSecp256k1VerificationKey2019", () => new EcdsaSecp256k1VerificationKey2019());
            //ActivityStreamsParser.TypeToObjectMap.Add("EcdsaSecp256r1VerificationKey2019", () => new EcdsaSecp256r1VerificationKey2019());
            ActivityStreamsParser.TypeToObjectMap.Add("Ed25519Signature2018", () => new Ed25519Signature2018());
            //ActivityStreamsParser.TypeToObjectMap.Add("Ed25519VerificationKey2018", () => new Ed25519VerificationKey2018());
            //ActivityStreamsParser.TypeToObjectMap.Add("EquihashProof2018", () => new EquihashProof2018());
            //ActivityStreamsParser.TypeToObjectMap.Add("ExportKeyOperation", () => new ExportKeyOperation());
            //ActivityStreamsParser.TypeToObjectMap.Add("GenerateKeyOperation", () => new GenerateKeyOperation());
            //ActivityStreamsParser.TypeToObjectMap.Add("KmsOperation", () => new KmsOperation());
            //ActivityStreamsParser.TypeToObjectMap.Add("RevokeKeyOperation", () => new RevokeKeyOperation());
            //ActivityStreamsParser.TypeToObjectMap.Add("RsaSignature2018", () => new RsaSignature2018());
            //ActivityStreamsParser.TypeToObjectMap.Add("RsaVerificationKey2018", () => new RsaVerificationKey2018());
            //ActivityStreamsParser.TypeToObjectMap.Add("Sha256HmacKey2019", () => new Sha256HmacKey2019());
            //ActivityStreamsParser.TypeToObjectMap.Add("SignOperation", () => new SignOperation());
            //ActivityStreamsParser.TypeToObjectMap.Add("UnwrapKeyOperation", () => new UnwrapKeyOperation());
            //ActivityStreamsParser.TypeToObjectMap.Add("VerifyOperation", () => new VerifyOperation());
            //ActivityStreamsParser.TypeToObjectMap.Add("WrapKeyOperation", () => new WrapKeyOperation());
            //ActivityStreamsParser.TypeToObjectMap.Add("X25519KeyAgreementKey2019", () => new X25519KeyAgreementKey2019());
            ActivityStreamsParser.TypeToObjectMap.Add("Digest", () => new Digest());
            ActivityStreamsParser.TypeToObjectMap.Add("EncryptedMessage", () => new EncryptedMessage());
            ActivityStreamsParser.TypeToObjectMap.Add("JsonWebSignature2020", () => new JsonWebSignature2020());
            ActivityStreamsParser.TypeToObjectMap.Add("GraphSignature2012", () => new GraphSignature2012());
            ActivityStreamsParser.TypeToObjectMap.Add("LinkedDataSignature2015", () => new LinkedDataSignature2015());
            ActivityStreamsParser.TypeToObjectMap.Add("LinkedDataSignature2016", () => new LinkedDataSignature2016());
            ActivityStreamsParser.TypeToObjectMap.Add("IdentityProof", () => new IdentityProof());
        }
    }
}
