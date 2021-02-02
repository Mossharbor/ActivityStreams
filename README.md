# Mossharbor.ActivityStreams
.net implementation for the W3C spec for Activity Streams

Please Note that this is a brand new library and is still a work in progress, we are currently working on testing the parsing and implmenting Activity Creation.

*Example:*
```cs
using Mossharbor.ActivityStreams;

// Parse a Question Activity
ActivityBuilder builder = new ActivityBuilder();
var activity = builder
               .FromJson(System.IO.File.OpenRead(@".\TestFiles\example040.json"))
               .Build();

Console.WriteLine((activity as QuestionActivity).OneOf[0].Obj.Name);

// Parse a More Complicated Activity
var activity = builder
               .FromJson(System.IO.File.OpenRead(@".\TestFiles\example36.json"))
               .Build();
Console.WriteLine(activity.Summary);

PersonObject personAnnouncing =  (PersonObject)(activity as AnnounceActivity).Actor[0].Obj;
Console.WriteLine("Announcing" + (personAnnouncing.Name) + " has id " + activity.Id);
                        
```