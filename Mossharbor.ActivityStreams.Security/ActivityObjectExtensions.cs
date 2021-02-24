using System;
using System.Data;
using System.Text.Json;

namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// security extensions to activity objects
    /// </summary>
    public static class ActivityObjectExtensions
    {
        /// <summary>
        /// this returns the signature for an activity object
        /// </summary>
        /// <param name="activity">the activty object that has the signature</param>
        /// <returns>the signature</returns>
        public static SignatureBase GetSignature(this IActivityObject activity)
        {
            if (activity.Extensions == null || (!activity.Extensions.ContainsKey("signature") && !activity.ExtensionsOutOfContext.ContainsKey("signature")))
                return null;

            string sigJson = activity.Extensions.ContainsKey("signature") ? activity.Extensions["signature"] : activity.ExtensionsOutOfContext["signature"];

            using (JsonDocument document = JsonDocument.Parse(sigJson, ActivityObjectBuilder.JsonparsingOptions))
            {
                var type = document.RootElement.GetStringOrDefault("type");
                if (null == type)
                    type = document.RootElement.GetStringOrDefault("@type");

                if (ActivityStreamsParser.TypeToObjectMap.ContainsKey(type))
                {
                    var t = ActivityStreamsParser.TypeToObjectMap[type]();
                    t.PerformCustomParsing(document.RootElement);
                    return t as SignatureBase;

                }

                SignatureBase signature = new SignatureBase();
                signature.PerformCustomParsing(document.RootElement);

                return signature;
            }

        }

        /// <summary>
        /// this returns the proof for an activity object in order to describes a mechanism for ensuring the authenticity and integrity
        /// </summary>
        /// <param name="activity">the activty object that has the proof</param>
        /// <returns>the signature</returns>
        public static ProofBase GetProof(this IActivityObject activity)
        {
            if (activity.Extensions == null || (!activity.Extensions.ContainsKey("proof") && !activity.ExtensionsOutOfContext.ContainsKey("proof")))
                return null;

            string proofJson = activity.Extensions.ContainsKey("proof") ? activity.Extensions["proof"] : activity.ExtensionsOutOfContext["proof"];

            using (JsonDocument document = JsonDocument.Parse(proofJson, ActivityObjectBuilder.JsonparsingOptions))
            {
                var type = document.RootElement.GetStringOrDefault("type");
                if (null == type)
                    type = document.RootElement.GetStringOrDefault("@type");

                if (ActivityStreamsParser.TypeToObjectMap.ContainsKey(type))
                {
                    var t = ActivityStreamsParser.TypeToObjectMap[type]();
                    t.PerformCustomParsing(document.RootElement);
                    return t as ProofBase;

                }

                ProofBase proof = new ProofBase();
                proof.PerformCustomParsing(document.RootElement);

                return proof;
            }

        }

        /// <summary>
        /// this returns the proof for an activity object in order to describes a mechanism for ensuring the authenticity and integrity
        /// </summary>
        /// <param name="activity">the activty object that has the proof</param>
        /// <returns>the signature</returns>
        public static KeyBase GetKey(this IActivityObject activity)
        {
            if (activity.Extensions == null || (!activity.Extensions.ContainsKey("publicKey") && !activity.ExtensionsOutOfContext.ContainsKey("publicKey")))
                return null;

            string proofJson = activity.Extensions.ContainsKey("publicKey") ? activity.Extensions["publicKey"] : activity.ExtensionsOutOfContext["publicKey"];

            using (JsonDocument document = JsonDocument.Parse(proofJson, ActivityObjectBuilder.JsonparsingOptions))
            {
                var type = document.RootElement.GetStringOrDefault("type");
                if (null == type)
                    type = document.RootElement.GetStringOrDefault("@type");

                if (ActivityStreamsParser.TypeToObjectMap.ContainsKey(type))
                {
                    var t = ActivityStreamsParser.TypeToObjectMap[type]();
                    t.PerformCustomParsing(document.RootElement);
                    return t as KeyBase;

                }

                KeyBase proof = new KeyBase();
                proof.PerformCustomParsing(document.RootElement);

                return proof;
            }

        }
    }
}
