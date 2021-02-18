using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    public class CollectionPage : Collection, IParsesChildObjectOrLinks, IParsesChildLinks
    {
        public CollectionPage() : base(CollectionPageType)
        {
        }

        private string type;

        public override string Type
        {
            get
            {
                if (string.IsNullOrEmpty(type))
                {
                    if (Items != null && Items.Length != 0)
                        type = CollectionPageType;
                    else
                        type = OrderedCollectionPageType;
                }

                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// the type constant for this colletion
        /// </summary>
        public const string CollectionPageType = "CollectionPage";

        /// <summary>
        /// the type constant for this collection
        /// </summary>
        public const string OrderedCollectionPageType = "OrderedCollectionPage";

        [JsonPropertyName("partOf")]
        public IActivityObjectOrLink PartOf { get; set; }

        [JsonPropertyName("next")]
        public IActivityLink Next { get; set; }

        [JsonPropertyName("prev")]
        public IActivityLink Prev { get; set; }

        /// <inheritdoc/>
        public override void PerformCustomObjectOrLinkParsing(JsonElement el, Func<JsonElement, IActivityObject, IActivityObjectOrLink[]> activtyOrLinkObjectParser)
        {
            base.PerformCustomObjectOrLinkParsing(el, activtyOrLinkObjectParser);

            if (el.ContainsElement("partOf"))
            {
                this.PartOf = activtyOrLinkObjectParser(el.GetProperty("partOf"), this).FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public override void PerformCustomLinkParsing(JsonElement el, Func<JsonElement, IActivityLink[]> activtyLinkParser)
        {
            base.PerformCustomLinkParsing(el, activtyLinkParser);

            if (el.ContainsElement("next"))
                this.Next = activtyLinkParser(el.GetProperty("next")).FirstOrDefault();
            if (el.ContainsElement("prev"))
                this.Prev = activtyLinkParser(el.GetProperty("prev")).FirstOrDefault();
        }
    }
}
