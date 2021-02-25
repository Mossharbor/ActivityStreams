using System;

namespace Mossharbor.ActivityStreams.Security
{
    public static class ActivityObjectBuilderExtensions
    {
        public static void AddSecurityTypes(this ActivityObjectBuilder builder)
        {
            builder.TryRegisterType("AesKeyWrappingKey2019", () => new AesKeyWrappingKey2019());
            builder.TryRegisterType("DeleteKeyOperation", () => new DeleteKeyOperation());
            builder.TryRegisterType("DeriveSecretOperation", () => new DeriveSecretOperation());
            //builder.RegisterType("EcdsaSecp256k1Signature2019", () => new EcdsaSecp256k1Signature2019());
            //builder.RegisterType("EcdsaSecp256r1Signature2019", () => new EcdsaSecp256r1Signature2019());
            //builder.RegisterType("EcdsaSecp256k1VerificationKey2019", () => new EcdsaSecp256k1VerificationKey2019());
            //builder.RegisterType("EcdsaSecp256r1VerificationKey2019", () => new EcdsaSecp256r1VerificationKey2019());
            builder.TryRegisterType("Ed25519Signature2018", () => new Ed25519Signature2018());
            //builder.RegisterType("Ed25519VerificationKey2018", () => new Ed25519VerificationKey2018());
            //builder.RegisterType("EquihashProof2018", () => new EquihashProof2018());
            //builder.RegisterType("ExportKeyOperation", () => new ExportKeyOperation());
            //builder.RegisterType("GenerateKeyOperation", () => new GenerateKeyOperation());
            //builder.RegisterType("KmsOperation", () => new KmsOperation());
            //builder.RegisterType("RevokeKeyOperation", () => new RevokeKeyOperation());
            //builder.RegisterType("RsaSignature2018", () => new RsaSignature2018());
            //builder.RegisterType("RsaVerificationKey2018", () => new RsaVerificationKey2018());
            //builder.RegisterType("Sha256HmacKey2019", () => new Sha256HmacKey2019());
            //builder.RegisterType("SignOperation", () => new SignOperation());
            //builder.RegisterType("UnwrapKeyOperation", () => new UnwrapKeyOperation());
            //builder.RegisterType("VerifyOperation", () => new VerifyOperation());
            //builder.RegisterType("WrapKeyOperation", () => new WrapKeyOperation());
            //builder.RegisterType("X25519KeyAgreementKey2019", () => new X25519KeyAgreementKey2019());
            builder.TryRegisterType("Digest", () => new Digest());
            builder.TryRegisterType("EncryptedMessage", () => new EncryptedMessage());
            builder.TryRegisterType("JsonWebSignature2020", () => new JsonWebSignature2020());
            builder.TryRegisterType("GraphSignature2012", () => new GraphSignature2012());
            builder.TryRegisterType("LinkedDataSignature2015", () => new LinkedDataSignature2015());
            builder.TryRegisterType("LinkedDataSignature2016", () => new LinkedDataSignature2016());
            builder.TryRegisterType("IdentityProof", () => new IdentityProof());
        }
    }
}
