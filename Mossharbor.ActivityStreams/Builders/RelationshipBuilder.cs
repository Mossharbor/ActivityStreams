using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    public class RelationshipBuilder : ActivityObjectBuilder
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
        public ActivityObjectBuilder Object(Action<ActivityObjectBuilder> modifier)
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
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Subject(Action<ActivityObjectBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                System.Diagnostics.Debug.Assert(activity is RelationshipObject);
                (activity as RelationshipObject).Subject = ExpandArray((activity as RelationshipObject).Subject, out int index);
                (activity as RelationshipObject).Subject[index].Obj = RunModifierBuilder(modifier).Build();

                return activity;
            });

            return this;
        }

    }
}
