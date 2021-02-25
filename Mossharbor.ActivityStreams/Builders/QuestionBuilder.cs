using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    public class QuestionBuilder : IntransitiveActivityBuilder
    {
        public enum AnswerType { AnyOf, OneOf };

        private AnswerType answerType;

        private int answerCount = 0;
        private int addedCount = 0;

        private readonly Dictionary<string, Func<ActivityObject>> TypeToObjectMap;

        public QuestionBuilder(Dictionary<string, Func<ActivityObject>> TypeToObjectMap, AnswerType answerType, IActivityObject activity)
            :base (activity)
        {
            this.TypeToObjectMap = TypeToObjectMap;
            this.answerType = answerType;
        }

        public QuestionBuilder AddAnswer(string answer, string type = Mossharbor.ActivityStreams.Activity.ActivityType)
        {
            if (!this.TypeToObjectMap.ContainsKey(type))
                throw new UnknownActivityTypeException(type);

            var answerActivity = this.TypeToObjectMap[type]();

            ++answerCount;
            this.fn = Compose(this.fn, (activity) =>
            {
                switch (answerType)
                {
                    case AnswerType.AnyOf:
                        if ((activity as QuestionActivity).AnyOf == null)
                            (activity as QuestionActivity).AnyOf = new IActivityObjectOrLink[answerCount];

                        (activity as QuestionActivity).AnyOf[addedCount]= new ActivityObjectOrLink();
                        (activity as QuestionActivity).AnyOf[addedCount++].Obj = new ActivityObjectBuilder(answerActivity).Name(answer).Build();
                        break;
                    case AnswerType.OneOf:
                        if ((activity as QuestionActivity).OneOf == null)
                            (activity as QuestionActivity).OneOf = new IActivityObjectOrLink[answerCount];
                        (activity as QuestionActivity).OneOf[addedCount] = new ActivityObjectOrLink();
                        (activity as QuestionActivity).OneOf[addedCount++].Obj = new ActivityObjectBuilder(answerActivity).Name(answer).Build();
                        break;
                }
                return activity;
            });
            return this;
        }

        public override IActivityObject Build(BuildValidationLevel validationLevel = BuildValidationLevel.Off)
        {
            IActivityObject ac = this.fn(null);
            return ValidateActivity(ac, validationLevel);
        }
    }
}
