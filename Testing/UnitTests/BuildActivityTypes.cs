using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mossharbor.ActivityStreams;

namespace Mossharbor.ActivityStreams.UnitTests
{
    [TestClass]
    public class BuildActivityTypes
    {
        [TestMethod]
        public void BuildSimpleQuestion()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            QuestionActivity activity = (QuestionActivity)builder
                .Question(
                    "What is the answer?",
                    QuestionBuilder.AnswerType.OneOf,
                    i => i.AddAnswer("Option A", type: NoteObject.NoteType)
                          .AddAnswer("Option B", type: NoteObject.NoteType))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "What is the answer?");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Question", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(QuestionActivity));

            Assert.AreEqual(2, (activity as QuestionActivity).OneOf.Length, "one of count was set incorrectly.");
            Assert.IsInstanceOfType((activity as QuestionActivity).OneOf[0].Obj, typeof(NoteObject));
            Assert.AreEqual("Note", (activity as QuestionActivity).OneOf[0].Obj.Type, "one of type was set incorrectly.");
            Assert.AreEqual("Option A", (activity as QuestionActivity).OneOf[0].Obj.Name, "one of name was set incorrectly.");

            Assert.IsInstanceOfType((activity as QuestionActivity).OneOf[1].Obj, typeof(NoteObject));
            Assert.AreEqual("Note", (activity as QuestionActivity).OneOf[1].Obj.Type, "one of type was set incorrectly.");
            Assert.AreEqual("Option B", (activity as QuestionActivity).OneOf[1].Obj.Name, "one of name was set incorrectly.");
        }

        [TestMethod]
        public void BuildNoteWithAttachments()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            NoteObject activity = (NoteObject)builder
                .Note("Have you seen my cat?",
                      i => i.Attachment(
                           a => a.Type("Image")
                                 .Content("This is what he looks like.")))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Content, "Have you seen my cat?");

            Assert.IsNotNull(activity.Type, "the sub object was null and should not have been");
            Assert.AreEqual("Note", activity.Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));

            Assert.IsNotNull((activity as NoteObject).Attachment);
            Assert.AreEqual("Image", (activity as NoteObject).Attachment[0].Obj.Type);
            Assert.AreEqual("This is what he looks like.", (activity as NoteObject).Attachment[0].Obj.Content);
        }
    }
}
