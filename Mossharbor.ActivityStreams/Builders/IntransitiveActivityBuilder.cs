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
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public IntransitiveActivityBuilder Actor(Action<IntransitiveActivityBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                (activity as IntransitiveActivity).Actor = ExpandArray((activity as IntransitiveActivity).Actor, out int index);
                (activity as IntransitiveActivity).Actor[index].Obj = RunModifierBuilder(modifier).Build();

                return activity;
            });

            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Target"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public IntransitiveActivityBuilder Target(Action<IntransitiveActivityBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                (activity as IntransitiveActivity).Target = ExpandArray((activity as IntransitiveActivity).Actor, out int index);
                (activity as IntransitiveActivity).Target[index].Obj = RunModifierBuilder(modifier).Build();

                return activity;
            });

            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Result"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public IntransitiveActivityBuilder Result(Action<IntransitiveActivityBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                (activity as IntransitiveActivity).Result = ExpandArray((activity as IntransitiveActivity).Actor, out int index);
                (activity as IntransitiveActivity).Result[index].Obj = RunModifierBuilder(modifier).Build();

                return activity;
            });

            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Origin"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public IntransitiveActivityBuilder Origin(Action<IntransitiveActivityBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                (activity as IntransitiveActivity).Origin.Obj = RunModifierBuilder(modifier).Build();

                return activity;
            });

            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Instrument"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public IntransitiveActivityBuilder Instrument(Action<IntransitiveActivityBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                (activity as IntransitiveActivity).Instrument = ExpandArray((activity as IntransitiveActivity).Actor, out int index);
                (activity as IntransitiveActivity).Instrument[index].Obj = RunModifierBuilder(modifier).Build();

                return activity;
            });

            return this;
        }

        protected ActivityObjectBuilder RunModifierBuilder(Action<IntransitiveActivityBuilder> modifier)
        {
            IntransitiveActivity ac = new IntransitiveActivity();
            IntransitiveActivityBuilder abuilder = new IntransitiveActivityBuilder(ac);
            modifier(abuilder);
            return abuilder;
        }
    }
}
