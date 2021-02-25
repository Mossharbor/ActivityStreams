using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    internal static class JsonLDExpandor
    {

        internal static void ExpandExtentionsForActivity(bool overwrite, IEnumerable<IActivityObject> activities)
        {
            if (null == activities || !activities.Any())
                return;

            foreach (var t in activities)
                ExpandExtentionsForActivity(overwrite, t);
        }

        internal static void ExpandExtentionsForActivity(bool overwrite, IEnumerable<IActivityObjectOrLink> activities)
        {
            if (null == activities || !activities.Any())
                return;

            foreach (var t in activities)
                ExpandExtentionsForActivity(overwrite, t.Obj);
        }

        internal static void ExpandExtentionsForActivity(bool overwrite, IActivityObjectOrLink activities)
        {
            if (null == activities || null == activities.Obj)
                return;

            ExpandExtentionsForActivity(overwrite, activities.Obj);
        }

        internal static void ExpandExtentionsForActivity(bool overwrite, IActivityObject activity)
        {
            if (null == activity)
                return;

            if (activity.ExtendedContexts == null || !activity.ExtendedContexts.Any())
                return;

            ExpandActivityContexts(activity);
            ExpandActivityExtensionNames(overwrite, activity);
            ExpandActivityExtensionValues(activity);
            ExpandActivityIds(overwrite, activity);

            if (activity is Activity)
                ExpandExtentionsForActivity(overwrite, (activity as Activity).Object);

            if (activity is IntransitiveActivity)
            {
                ExpandExtentionsForActivity(overwrite, (activity as IntransitiveActivity).Actor);
                ExpandExtentionsForActivity(overwrite, (activity as IntransitiveActivity).Target);
                ExpandExtentionsForActivity(overwrite, (activity as IntransitiveActivity).Result);
                ExpandExtentionsForActivity(overwrite, (activity as IntransitiveActivity).Origin);
                ExpandExtentionsForActivity(overwrite, (activity as IntransitiveActivity).Instrument);
            }

            if ((activity is Collection) || (activity is CollectionPage))
            {

                ExpandExtentionsForActivity(overwrite, (activity as Collection).First);
                ExpandExtentionsForActivity(overwrite, (activity as Collection).Last);
                ExpandExtentionsForActivity(overwrite, (activity as Collection).Current);
                ExpandExtentionsForActivity(overwrite, (activity as Collection).Items);
                ExpandExtentionsForActivity(overwrite, (activity as Collection).OrderedItems);
            }

            if (activity is CollectionPage)
            {
                ExpandExtentionsForActivity(overwrite, (activity as CollectionPage).PartOf);
            }

            if (activity is QuestionActivity)
            {
                ExpandExtentionsForActivity(overwrite, (activity as QuestionActivity).OneOf);
                ExpandExtentionsForActivity(overwrite, (activity as QuestionActivity).AnyOf);
            }

            if (activity is RelationshipObject)
            {
                ExpandExtentionsForActivity(overwrite, (activity as RelationshipObject).Subject);
                ExpandExtentionsForActivity(overwrite, (activity as RelationshipObject).Object);
            }

            ExpandExtentionsForActivity(overwrite, activity.Attachment);
            ExpandExtentionsForActivity(overwrite, activity.AttributedTo);
            ExpandExtentionsForActivity(overwrite, activity.Audience);
            ExpandExtentionsForActivity(overwrite, activity.bcc);
            ExpandExtentionsForActivity(overwrite, activity.Bto);
            ExpandExtentionsForActivity(overwrite, activity.CC);
            ExpandExtentionsForActivity(overwrite, activity.Tag);
            ExpandExtentionsForActivity(overwrite, activity.Generator);
            ExpandExtentionsForActivity(overwrite, activity.Icons);
            ExpandExtentionsForActivity(overwrite, activity.Images);
            ExpandExtentionsForActivity(overwrite, activity.InReplyTo);
            ExpandExtentionsForActivity(overwrite, activity.Preview);
            ExpandExtentionsForActivity(overwrite, activity.To);
            ExpandExtentionsForActivity(overwrite, activity.InReplyTo);
            ExpandExtentionsForActivity(overwrite, activity.InReplyTo);
            ExpandExtentionsForActivity(overwrite, activity.InReplyTo);
            ExpandExtentionsForActivity(overwrite, activity.InReplyTo);
            ExpandExtentionsForActivity(overwrite, activity.InReplyTo);
            ExpandExtentionsForActivity(overwrite, activity.InReplyTo);
            ExpandExtentionsForActivity(overwrite, activity.InReplyTo);
            ExpandExtentionsForActivity(overwrite, activity.InReplyTo);
        }

        private static bool ExpandActivityContexts(IActivityObject activity)
        {
            if (null == activity || !activity.ExtendedContexts.Any())
                return false;

            bool changed = false;
            for (int j = 0; j < activity.ExtendedContexts.Keys.Count(); ++j)
            {
                var exType = activity.ExtendedContexts.Keys.ElementAt(j);
                var exTypeColon = exType + ":";

                for (int k = 0; k < activity.ExtendedContexts.Values.Count(); ++k)
                {
                    if (k == j)
                        continue;

                    var curValue = activity.ExtendedContexts.Values.ElementAt(k);
                    string newValue = null;

                    if (string.IsNullOrEmpty(curValue))
                        continue;
                    else if (curValue.StartsWith(exTypeColon))
                        newValue = curValue.Replace(exTypeColon, activity.ExtendedContexts.Values.ElementAt(j));
                    else if (curValue == exType)
                        newValue = curValue.Replace(exType, activity.ExtendedContexts.Values.ElementAt(j));

                    if (!string.IsNullOrEmpty(newValue))
                    {
                        var curKey = activity.ExtendedContexts.Keys.ElementAt(k);
                        activity.ExtendedContexts[curKey] = newValue;
                        changed = true;
                    }
                }
            }
            return changed;
        }

        private static bool ExpandActivityExtensionNames(bool overwrite, IActivityObject activity)
        {
            if (null == activity.Extensions && null == activity.ExtendedTypes)
                return false;

            if (!activity.Extensions.Any() && !activity.ExtendedTypes.Any())
                return false;

            bool changed = false;
            for (int j = 0; j < activity.ExtendedContexts.Keys.Count(); ++j)
            {
                var exType = activity.ExtendedContexts.Keys.ElementAt(j);
                if (exType.StartsWith("@")) // this means something else in json-LD
                    continue;

                var exTypeColon = exType + ":";
                if (activity.Extensions != null)
                {
                    for (int k = activity.Extensions.Count - 1; k >= 0; --k)
                    {
                        var curKey = activity.Extensions.ElementAt(k).Key;
                        var curValue = activity.Extensions.ElementAt(k).Value;
                        string newValue = null;

                        if (curKey.StartsWith(exTypeColon))
                            newValue = curKey.Replace(exTypeColon, activity.ExtendedContexts.Values.ElementAt(j));
                        else if (curKey == exType)
                            newValue = curKey.Replace(exType, activity.ExtendedContexts.Values.ElementAt(j));

                        if (string.IsNullOrEmpty(newValue))
                            continue;

                        if (overwrite)
                            activity.Extensions.Remove(curKey);
                        activity.Extensions.Add(newValue, curValue);
                        changed = true;
                    }
                }

                if (null == activity.ExtendedTypes)
                    continue;

                for (int k = activity.ExtendedTypes.Count - 1; k >= 0; --k)
                {
                    var curType = activity.ExtendedTypes.ElementAt(k);
                    string newValue = null;

                    if (curType.StartsWith(exTypeColon))
                        newValue = curType.Replace(exTypeColon, activity.ExtendedContexts.Values.ElementAt(j));
                    else if (curType == exType)
                        newValue = curType.Replace(exType, activity.ExtendedContexts.Values.ElementAt(j));
                    else
                        continue;

                    if (overwrite)
                        activity.ExtendedTypes.Remove(curType);
                    activity.ExtendedTypes.Add(newValue);
                    changed = true;
                }
            }
            return changed;
        }

        private static bool ExpandActivityExtensionValues(IActivityObject activity)
        {
            if (null == activity.Extensions && null == activity.ExtendedTypes)
                return false;

            if (!activity.Extensions.Any() && !activity.ExtendedTypes.Any())
                return false;

            var noneEmptyStringProperties = activity.GetType().GetProperties(
                          BindingFlags.FlattenHierarchy |
                          BindingFlags.Instance |
                          BindingFlags.Public).Where(p =>
                          {
                              return (p.PropertyType == typeof(string) && null != p.GetValue(activity));
                          })
                          .ToArray();

            bool changed = false;
            for (int j = 0; j < activity.ExtendedContexts.Keys.Count(); ++j)
            {
                var exType = activity.ExtendedContexts.Keys.ElementAt(j);
                if (exType.StartsWith("@")) // this means something else in json-LD
                    continue;

                var exTypeColon = exType + ":";
                for (int k = 0; k < noneEmptyStringProperties.Count(); ++k)
                {
                    var item = noneEmptyStringProperties[k];
                    var curValue = (string)item.GetValue(activity);

                    string newValue = null;

                    if (curValue.StartsWith(exTypeColon))
                        newValue = curValue.Replace(exTypeColon, activity.ExtendedContexts.Values.ElementAt(j));
                    else
                        continue;
                    // else if (curValue == exType) // this is only supported if the jsonld parameter of replace is set we do not have this feature yet
                    //    newValue = curValue.Replace(exType, activity.ExtendedContexts.Values.ElementAt(j));

                    item.SetValue(activity, curValue);
                    changed = true;
                }

                for (int k = activity.Extensions.Count - 1; k >= 0; k--)
                {
                    var curKey = activity.Extensions.ElementAt(k).Key;
                    var curValue = activity.Extensions.ElementAt(k).Value;
                    string newValue = null;

                    if (curValue.StartsWith(exTypeColon))
                        newValue = curValue.Replace(exTypeColon, activity.ExtendedContexts.Values.ElementAt(j));
                    // else if (curValue == exType) // this is only supported if the jsonld parameter of replace is set we do not have this feature yet
                    //    newValue = curValue.Replace(exType, activity.ExtendedContexts.Values.ElementAt(j));
                    else if (string.IsNullOrEmpty(newValue))
                        continue;
                    else
                        continue;

                    activity.Extensions[curKey] = newValue;
                    changed = true;
                }
            }
            return changed;
        }

        private static bool ExpandActivityIds(bool overwrite, IActivityObject activity)
        {
            if (null == activity.ExtendedIds || !activity.ExtendedIds.Any())
                return false;

            bool changed = false;
            for (int j = 0; j < activity.ExtendedContexts.Keys.Count(); ++j)
            {
                var exType = activity.ExtendedContexts.Keys.ElementAt(j);
                if (exType.StartsWith("@")) // this means something else in json-LD
                    continue;

                var exTypeColon = exType + ":";
                for (int k = activity.ExtendedIds.Count() - 1; k >= 0; --k)
                {
                    var curValue = activity.ExtendedIds.ElementAt(k).Value;
                    if (curValue.Id == null)
                        continue;

                    if (curValue.Id.StartsWith(exTypeColon))
                    {
                        curValue.ExpandedId = curValue.Id.Replace(exTypeColon, activity.ExtendedContexts.Values.ElementAt(j));
                        changed = true;
                    }
                }
            }

            for (int j = 0; j < activity.ExtendedIds.Count(); ++j)
            {
                var exType = activity.ExtendedIds.ElementAt(j).Key;

                if (null == activity.ExtendedIds.ElementAt(j).Value.ExpandedId)
                    continue;

                var exTypeColon = exType + ":";
                if (activity.Extensions != null)
                {
                    for (int k = activity.Extensions.Count - 1; k >= 0; --k)
                    {
                        var curKey = activity.Extensions.ElementAt(k).Key;
                        var curValue = activity.Extensions.ElementAt(k).Value;
                        string newValue = null;

                        if (curKey.StartsWith(exTypeColon))
                            newValue = curKey.Replace(exTypeColon, activity.ExtendedIds.ElementAt(j).Value.ExpandedId);
                        else if (curKey == exType)
                            newValue = curKey.Replace(exType, activity.ExtendedIds.ElementAt(j).Value.ExpandedId);

                        if (string.IsNullOrEmpty(newValue))
                            continue;

                        if (overwrite)
                            activity.Extensions.Remove(curKey);
                        activity.Extensions.Add(newValue, curValue);
                        changed = true;
                    }
                }

                if (null == activity.ExtendedTypes)
                    continue;

                for (int k = activity.ExtendedTypes.Count - 1; k >= 0; --k)
                {
                    var curType = activity.ExtendedTypes.ElementAt(k);
                    string newValue = null;

                    if (curType.StartsWith(exTypeColon))
                        newValue = curType.Replace(exTypeColon, exType);
                    //else if (curType == exType) // NOTE not supported just yet.
                    //    newValue = curType.Replace(exType, exType);

                    if (overwrite)
                        activity.ExtendedTypes.Remove(curType);
                    activity.ExtendedTypes.Add(newValue);
                    changed = true;
                }
            }

            return changed;
        }
    }
}
