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
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Object(Action<IntransitiveActivityBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                System.Diagnostics.Debug.Assert((activity as Activity) != null);

                if (null != objectModifier)
                {
                    (activity as Activity).Object = ExpandArray((activity as Activity).Object, out int index);
                    (activity as Activity).Object[index] = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    (activity as Activity).Object = ExpandArray((activity as Activity).Object, out int index);
                    (activity as Activity).Object[index] = new ActivityObject();
                    (activity as Activity).Object[index].Type = ActivityLink.ActivityLinkType;
                    (activity as Activity).Object[index].Url = ExpandArray((activity as Activity).Object[index].Url, out int urlIndex);
                    (activity as Activity).Object[index].Url[urlIndex] = RunModifierBuilder(linkModifier).Build();
                }

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
