using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    public class IntransitiveActivityBuilder : ActivityObjectBuilder
    {
        public IntransitiveActivityBuilder(IActivityObject activity)
              : base(activity)
        {
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Actor"/>
        /// </summary>
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public IntransitiveActivityBuilder Actor(Action<IntransitiveActivityBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                (activity as IntransitiveActivity).Actor = ExpandArray((activity as IntransitiveActivity).Actor, out int index);
                if (null != objectModifier)
                {
                    (activity as IntransitiveActivity).Actor[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    (activity as IntransitiveActivity).Actor[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });

            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Target"/>
        /// </summary>
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public IntransitiveActivityBuilder Target(Action<IntransitiveActivityBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                (activity as IntransitiveActivity).Target = ExpandArray((activity as IntransitiveActivity).Target, out int index);
                if (null != objectModifier)
                {
                    (activity as IntransitiveActivity).Target[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    (activity as IntransitiveActivity).Target[index].Link = RunModifierBuilder(linkModifier).Build();
                }
                return activity;
            });

            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Result"/>
        /// </summary>
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public IntransitiveActivityBuilder Result(Action<IntransitiveActivityBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                (activity as IntransitiveActivity).Result = ExpandArray((activity as IntransitiveActivity).Result, out int index);
                if (null != objectModifier)
                {
                    (activity as IntransitiveActivity).Result[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    (activity as IntransitiveActivity).Result[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });

            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Origin"/>
        /// </summary>
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public IntransitiveActivityBuilder Origin(Action<IntransitiveActivityBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                (activity as IntransitiveActivity).Origin = new ActivityObjectOrLink();
                if (null != objectModifier)
                {
                    (activity as IntransitiveActivity).Origin.Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    (activity as IntransitiveActivity).Origin.Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });

            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Instrument"/>
        /// </summary>
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public IntransitiveActivityBuilder Instrument(Action<IntransitiveActivityBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                (activity as IntransitiveActivity).Instrument = ExpandArray((activity as IntransitiveActivity).Instrument, out int index);
                if (null != objectModifier)
                {
                    (activity as IntransitiveActivity).Instrument[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    (activity as IntransitiveActivity).Instrument[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });

            return this;
        }

        protected IntransitiveActivityBuilder RunModifierBuilder(Action<IntransitiveActivityBuilder> modifier)
        {
            IntransitiveActivity ac = new IntransitiveActivity();
            IntransitiveActivityBuilder abuilder = new IntransitiveActivityBuilder(ac);
            modifier(abuilder);
            return abuilder;
        }
    }
}
