using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class CollectionPage : Collection
    {
        public CollectionPage(): base(CollectionPageType)
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
    }
}
