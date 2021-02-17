using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    public class ProfileBuilder : ActivityObjectBuilder
    {
        public ProfileBuilder(IActivityObject activity)
                : base(activity)
        {
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="Activity.Describes"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Describes(Action<ActivityObjectBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                System.Diagnostics.Debug.Assert(activity is ProfileObject);
                (activity as ProfileObject).Describes = RunModifierBuilder(modifier).Build();

                return activity;
            });

            return this;
        }

    }
}
