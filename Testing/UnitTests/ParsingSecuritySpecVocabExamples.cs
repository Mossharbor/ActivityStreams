using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mossharbor.ActivityStreams;

namespace Mossharbor.ActivityStreams.Security.UnitTests
{
    /// <summary>
    /// this tests the unofficial draft of the security vocab spec 
    /// <see cref="https://w3c-ccg.github.io/security-vocab/"/>
    /// </summary>
    [TestClass]
    public class ParsingSecuritySpecVocabExamples
    {

        /// <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamIdentityProof()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();
            PersonActor activity = (PersonActor)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\identityproofs.json"))
                            .ExpandJsonLD()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("toot"), "toot is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("IdentityProof"), "IdentityProof is missing");
            // Assert.IsTrue(activity.Attachment[0].Obj.ExtendedTypes.Contains("http://joinmastodon.org/ns#IdentityProof"), "extendedTypes expansion is missing");
            Assert.IsInstanceOfType(activity.Attachment[0].Obj, typeof(IdentityProof));
            Assert.AreEqual((activity.Attachment[0].Obj as IdentityProof).SignatureAlgorithm, "keybase");
            Assert.AreEqual((activity.Attachment[0].Obj as IdentityProof).SignatureValue, "5cfc20c7018f2beefb42a68836da59a792e55daa4d118498c9b1898de7e845690f");
        }

        /// <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamIdentityProofWithOutExpansion()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();
            PersonActor activity = (PersonActor)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\identityproofs.json"))
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("toot"), "toot is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("IdentityProof"), "IdentityProof is missing");
            Assert.IsInstanceOfType(activity.Attachment[0].Obj, typeof(IdentityProof));
            Assert.AreEqual((activity.Attachment[0].Obj as IdentityProof).SignatureAlgorithm, "keybase");
            Assert.AreEqual((activity.Attachment[0].Obj as IdentityProof).SignatureValue, "5cfc20c7018f2beefb42a68836da59a792e55daa4d118498c9b1898de7e845690f");
        }

        /// <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample001()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example001.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 2
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample002()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example002.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 3
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample003()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example003.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 4
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample004()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example004.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 5
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample005()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();

            Digest activity = (Digest)builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example005.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.ExtendedContexts.ContainsKey("https://w3id.org/security/v1"), "the activity stream context was null");
            Assert.AreEqual("http://www.w3.org/2000/09/xmldsig#sha1", activity.Algorithm);
            Assert.AreEqual("981ec496092bf6ee18d6255d96069b528633268b", activity.DigestValue);
        }

        /// <summary>
        /// Testing Spec example 6
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample006()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();
            EncryptedMessage activity = (EncryptedMessage)builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example006.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.ExtendedContexts.ContainsKey("https://w3id.org/security/v1"), "the activity stream context was null");
            Assert.AreEqual("aes-128-gcm", activity.Algorithm);
            Assert.AreEqual("q25H1CzsE731OmeyEle93w==", activity.Authtag);
            Assert.AreEqual("vcDU1eWTy8vVGhNOszREhSblFVqVnGpBUm0zMTRmcWtMrRX==", activity.IV);
            Assert.AreEqual("https://example.com/people/john/keys/23", activity.PublicKey);
            Assert.IsTrue(activity.CipherKey.StartsWith("uATtey0c4nvZ") && activity.CipherKey.EndsWith("HExjXQE="));
            Assert.IsTrue(activity.CipherData.StartsWith("VTJGc2RHVmt") && activity.CipherData.EndsWith("bktFb009Cg=="));
        }

        /// <summary>
        /// Testing Spec example 7
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample007()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example007.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");

            var sig = activity.GetSignature();
            Assert.AreEqual("GraphSignature2012", sig.SignatureType);
            Assert.AreEqual("http://manu.sporny.org/keys/5", sig.Creator);
            Assert.AreEqual("OGQzNGVkMzVmMmQ3ODIyOWM32MzQzNmExMgoYzI4ZDY3NjI4NTIyZTk=", sig.SignatureValue);
            Assert.AreEqual("URGNA2012", sig.CanonicalizationAlgorithm);
            Assert.AreEqual("sha256", sig.DigestAlgorithm);
            Assert.AreEqual("rsa-sha256", sig.SignatureAlgorithm);
            Assert.IsInstanceOfType(sig, typeof(GraphSignature2012));
        }

        /// <summary>
        /// Testing Spec example 8
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample008()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example008.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");

            var sig = activity.GetSignature();
            Assert.AreEqual("LinkedDataSignature2015", sig.SignatureType);
            Assert.AreEqual("http://manu.sporny.org/keys/5", sig.Creator);
            Assert.AreEqual(DateTime.Parse("2015-09-23T20:21:34Z").ToUniversalTime(), sig.Created.Value);
            Assert.AreEqual("OGQzNGVkMzVmMmQ3ODIyOWM32MzQzNmExMgoYzI4ZDY3NjI4NTIyZTk=", sig.SignatureValue);
            Assert.AreEqual("URDNA2015", sig.CanonicalizationAlgorithm);
            Assert.AreEqual("sha256", sig.DigestAlgorithm);
            Assert.AreEqual("rsa-sha256", sig.SignatureAlgorithm);
            Assert.IsInstanceOfType(sig, typeof(LinkedDataSignature2015));
        }

        /// <summary>
        /// Testing Spec example 9
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample009()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example009.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");

            var sig = activity.GetSignature();
            Assert.AreEqual("LinkedDataSignature2016", sig.SignatureType);
            Assert.AreEqual("https://w3id.org/people/dave/keys/2", sig.Creator);
            Assert.AreEqual(DateTime.Parse("2016-11-05T03:12:54Z").ToUniversalTime(), sig.Created.Value);
            Assert.AreEqual("OGQzNGVkMzVmMmQ3ODIyOWM32MzQzNmExMgoYzI4ZDY3NjI4NTIyZTk=", sig.SignatureValue);
            Assert.AreEqual("URDNA2015", sig.CanonicalizationAlgorithm);
            Assert.AreEqual("sha256", sig.DigestAlgorithm);
            Assert.AreEqual("rsa-sha256", sig.SignatureAlgorithm);
            Assert.IsInstanceOfType(sig, typeof(LinkedDataSignature2016));
        }

        /// <summary>
        /// Testing Spec example 10
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample010()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example010.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            var proof = activity.GetProof();
            Assert.AreEqual("MerkleProof2019", proof.ProofType);
            Assert.AreEqual("assertionMethod", proof.ProofPurpose);
            Assert.AreEqual(DateTime.Parse("2020-11-03T14:13:42.808099Z").ToUniversalTime(), proof.Created.Value);
            Assert.AreEqual("did:example:23adb1f712ebc6f1c276eba4dfa#key-1", proof.VerifcationMethod);
            Assert.IsNull(proof.Challenge);
            Assert.IsNull(proof.Domain);
            Assert.IsNull(proof.ProofValue);
        }

        /// <summary>
        /// Testing Spec example 11
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample011()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example011.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 12
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample012()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example012.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            var proof = activity.GetProof();
            Assert.AreEqual("Ed25519Signature2018", proof.ProofType);
            Assert.AreEqual("assertionMethod", proof.ProofPurpose);
            Assert.AreEqual(DateTime.Parse("2016-11-05T03:12:54Z").ToUniversalTime(), proof.Created.Value);
            Assert.AreEqual("https://w3id.org/people/dave/keys/2", proof.VerifcationMethod);
            Assert.IsNull(proof.Challenge);
            Assert.IsNull(proof.Domain);
            Assert.AreEqual("eyJhbGciOiJFZERTQSIsImI2NCI6ZmFsc2UsImNyaXQiOlsiYjY0Il19..dXNHwJ-9iPMRQ4AUcv9j-7LuImTiWAG0sDYbRRDDiyAjOV9CUmjLMKiePpytoAmGNGNTHDlEOsTa4CS3dZ7yBg", proof.ProofValue);
        }

        /// <summary>
        /// Testing Spec example 12
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample012ByType()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example012.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            var proof = activity.GetProof();
            Assert.AreEqual("Ed25519Signature2018", proof.ProofType);
            Assert.AreEqual("assertionMethod", proof.ProofPurpose);
            Assert.AreEqual(DateTime.Parse("2016-11-05T03:12:54Z").ToUniversalTime(), proof.Created.Value);
            Assert.AreEqual("https://w3id.org/people/dave/keys/2", proof.VerifcationMethod);
            Assert.IsNull(proof.Challenge);
            Assert.IsNull(proof.Domain);
            Assert.AreEqual("eyJhbGciOiJFZERTQSIsImI2NCI6ZmFsc2UsImNyaXQiOlsiYjY0Il19..dXNHwJ-9iPMRQ4AUcv9j-7LuImTiWAG0sDYbRRDDiyAjOV9CUmjLMKiePpytoAmGNGNTHDlEOsTa4CS3dZ7yBg", proof.ProofValue);
            Assert.IsInstanceOfType(proof, typeof(Ed25519Signature2018));
        }

        /// <summary>
        /// Testing Spec example 13
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample013()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example013.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 14
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample014()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example014.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            var proof = activity.GetProof();
            Assert.AreEqual("JsonWebSignature2020", proof.ProofType);
            Assert.AreEqual("assertionMethod", proof.ProofPurpose);
            Assert.AreEqual(DateTime.Parse("2016-11-05T03:12:54Z").ToUniversalTime(), proof.Created.Value);
            Assert.AreEqual("https://w3id.org/people/dave/keys/2", proof.VerifcationMethod);
            Assert.IsNull(proof.Challenge);
            Assert.IsNull(proof.Domain);
            Assert.AreEqual("eyJhbGciOiJFZERTQSIsImI2NCI6ZmFsc2UsImNyaXQiOlsiYjY0Il19..dXNHwJ-9iPMRQ4AUcv9j-7LuImTiWAG0sDYbRRDDiyAjOV9CUmjLMKiePpytoAmGNGNTHDlEOsTa4CS3dZ7yBg", proof.ProofValue);
            Assert.IsInstanceOfType(proof, typeof(JsonWebSignature2020));
        }

        /// <summary>
        /// Testing Spec example 15
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample015()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example015.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 16
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample016()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example016.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 17
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample017()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example017.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 18
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample018()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example018.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 19
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample019()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example019.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            var key = activity.GetKey();
            Assert.AreEqual("https://payswarm.example.com/i/bob", key.Owner);
            Assert.AreEqual("-----BEGIN PRIVATE KEY-----\nMIIBG0BA...OClDQAB\n-----END PRIVATE KEY-----\n", key.PrivateKeyPem);
            Assert.AreEqual("-----BEGIN PUBLIC KEY-----\nMII8YbF3s8q3c...j8Fk88FsRa3K\n-----END PUBLIC KEY-----\n", key.PublicKeyPem);
        }

        /// <summary>
        /// Testing Spec example 20
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample020()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();
            EncryptedMessage activity = (EncryptedMessage)builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example006.json"))
                           .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.ExtendedContexts.ContainsKey("https://w3id.org/security/v1"), "the activity stream context was null");
            Assert.AreEqual("aes-128-gcm", activity.Algorithm);
            Assert.AreEqual("q25H1CzsE731OmeyEle93w==", activity.Authtag);
            Assert.AreEqual("vcDU1eWTy8vVGhNOszREhSblFVqVnGpBUm0zMTRmcWtMrRX==", activity.IV);
            Assert.AreEqual("https://example.com/people/john/keys/23", activity.PublicKey);
            Assert.IsTrue(activity.CipherKey.StartsWith("uATtey0c4nvZ") && activity.CipherKey.EndsWith("HExjXQE="));
            Assert.IsTrue(activity.CipherData.StartsWith("VTJGc2RHVmt") && activity.CipherData.EndsWith("bktFb009Cg=="));
        }

        /// <summary>
        /// Testing Spec example 22
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample022()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();

            builder.AddSecurityTypes();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example022.json"))
                            .Build();

            var sig = activity.GetSignature();
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual("https://w3id.org/jsonld#UGNA2012", sig.CanonicalizationAlgorithm);
            Assert.AreEqual("http://example.com/digests#sha512", sig.DigestAlgorithm);
            Assert.AreEqual("http://www.w3.org/2000/09/xmldsig#rsa-sha1", sig.SignatureAlgorithm);
        }

        /// <summary>
        /// Testing Spec example 25
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample025()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example025.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 26
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample026()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example026.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 27
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample027()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example027.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 28
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample028()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example028.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 29
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample029()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example029.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            var key = activity.GetKey();
            Assert.AreEqual("https://payswarm.example.com/i/bob", key.Owner);
            Assert.IsNull(key.PrivateKeyPem);
            Assert.AreEqual("-----BEGIN PUBLIC KEY-----\nMII8YbF3s8q3c...j8Fk88FsRa3K\n-----END PUBLIC KEY-----\n", key.PublicKeyPem);
            Assert.AreEqual(DateTime.Parse("2012-01-03T14:34:57+0000").ToUniversalTime(), key.Created);
            Assert.AreEqual(DateTime.Parse("2014-01-03T14:34:57+0000").ToUniversalTime(), key.Expires);
        }

        /// <summary>
        /// Testing Spec example 30
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample030()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.AddSecurityTypes();
            var activity = (EncryptedMessage)builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example030.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.ExtendedContexts.ContainsKey("https://w3id.org/security/v1"), "the activity stream context was null");
            Assert.AreEqual("aes-128-gcm", activity.Algorithm);
            Assert.AreEqual("q25H1CzsE731OmeyEle93w==", activity.Authtag);
            Assert.AreEqual("vcDU1eWTy8vVGhNOszREhSblFVqVnGpBUm0zMTRmcWtMrRX==", activity.IV);
            Assert.AreEqual("https://example.com/people/john/keys/23", activity.PublicKey);
            Assert.IsTrue(activity.CipherKey.StartsWith("uATtey0c4nvZ") && activity.CipherKey.EndsWith("HExjXQE="));
            Assert.IsTrue(activity.CipherData.StartsWith("VTJGc2RHVmt") && activity.CipherData.EndsWith("bktFb009Cg=="));
        }

        /// <summary>
        /// Testing Spec example 31
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample031()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example031.json"))
                            .Build();

            var sig = activity.GetSignature();
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual("8495723045.84957", sig.Nonce);
        }

        /// <summary>
        /// Testing Spec example 35
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample035()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example035.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 37
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample037()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example037.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 38
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample038()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example038.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 39
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample039()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example039.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 40
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample040()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example040.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 41
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample041()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example041.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 42
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample042()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example042.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 43
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample043()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example043.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 45
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample045()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example045.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 46
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample046()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example046.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 47
        /// </summary>
        [TestMethod]
        public void ParseSecurityStreamSpecExample047()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example047.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            var key = activity.GetKey();
            Assert.AreEqual("https://payswarm.example.com/i/bob", key.Owner);
            Assert.IsNull(key.PrivateKeyPem);
            Assert.AreEqual("-----BEGIN PUBLIC KEY-----\nMII8YbF3s8q3c...j8Fk88FsRa3K\n-----END PUBLIC KEY-----\n", key.PublicKeyPem);
            Assert.AreEqual(DateTime.Parse("2012-01-03T14:34:57+0000").ToUniversalTime(), key.Created);
            Assert.AreEqual(DateTime.Parse("2012-05-01T18:11:19+0000").ToUniversalTime(), key.Revoked);
        }

        /// <summary>
        /// Testing Spec example 48
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample048()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example048.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 49
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample049()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example049.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 50
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample050()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example050.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 51
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample051()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example051.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 52
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample052()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example052.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 53
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample053()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example053.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 54
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample054()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example054.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 57
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample057()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example057.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 58
        /// </summary>
        [TestMethod]
        [Ignore("Section in Spec is not stable")]
        public void ParseSecurityStreamSpecExample058()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SecuritySpecVocabTestFiles\example058.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }


    }
}
