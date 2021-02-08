using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    public class ActivityBuilder : IntransitiveActivityBuilder
    {
        public ActivityBuilder(IActivityObject activity)
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
                System.Diagnostics.Debug.Assert((activity as Activity) != null);
                (activity as Activity).Object = ExpandArray((activity as Activity).Object, out int index);
                (activity as Activity).Object[index] = RunModifierBuilder(modifier).Build();

                return activity;
            });

            return this;
        }

        protected ActivityBuilder RunModifierBuilder(Action<ActivityBuilder> modifier)
        {
            Activity ac = new Activity();
            ActivityBuilder abuilder = new ActivityBuilder(ac);
            modifier(abuilder);
            return abuilder;
        }

    }
}
