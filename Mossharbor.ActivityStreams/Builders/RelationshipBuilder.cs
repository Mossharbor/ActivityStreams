using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    public class RelationshipBuilder : ActivityBuilder
    {
        public RelationshipBuilder(IActivityObject activity)
                : base(activity)
        {
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="Activity.Object"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Object(Action<ActivityBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                System.Diagnostics.Debug.Assert(activity is RelationshipObject);
                (activity as RelationshipObject).Object = RunModifierBuilder(modifier).Build();

                return activity;
            });

            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="Activity.Describes"/>
        /// </summary>
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Subject(Action<ActivityBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                System.Diagnostics.Debug.Assert(activity is RelationshipObject);
                (activity as RelationshipObject).Subject = ExpandArray((activity as RelationshipObject).Subject, out int index);
                if (null != objectModifier)
                {
                    (activity as RelationshipObject).Subject[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    (activity as RelationshipObject).Subject[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });

            return this;
        }

    }
}
