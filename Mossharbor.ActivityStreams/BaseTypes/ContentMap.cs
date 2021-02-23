
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// this maps the content to multiple languages.
    /// </summary>
    public class ContentMap : IReadOnlyDictionary<string,string>
    {
        private Dictionary<string, string> contentMap = new Dictionary<string, string>();

        internal ContentMap(Dictionary<string,string> contentMap)
        {
            this.contentMap = new Dictionary<string, string>();

            foreach(var t in contentMap)
            {
                this.contentMap.Add(t.Key, t.Value);
            }
        }

        /// <summary>
        /// add a piece of content to the content map
        /// </summary>
        /// <param name="cu">the culture info we are adding</param>
        /// <param name="content">the conetnt we are adding</param>
        public void Add(CultureInfo cu, string content)
        {
            this.contentMap.Add(cu.TwoLetterISOLanguageName, content);
        }

        /// <summary>
        /// add a piece of content to the content map
        /// </summary>
        /// <param name="languageCode">the language of the conetent we are adding</param>
        /// <param name="content">the conetnt we are adding</param>
        public void Add(string languageCode, string content)
        {
            this.contentMap.Add(languageCode, content);
        }

        /// <summary>
        /// Gets the content for the language one passed in
        /// </summary>
        /// <param name="twoLetterLanguageName">the culture info</param>
        /// <returns>the content for the lanage or null if it does not exist.</returns>
        public string GetContent(string twoLetterLanguageName)
        {
            this.TryGetValue(twoLetterLanguageName, out string curLangChar);
            return curLangChar;
        }

        /// <summary>
        /// Gets the content for the language one passed in
        /// </summary>
        /// <param name="cu">the culture info</param>
        /// <returns>the content for the lanage or null if it does not exist.</returns>
        public string GetContent(CultureInfo cu)
        {
            if (cu == null)
                throw new ArgumentNullException(nameof(cu));

            return this.GetContent(cu.TwoLetterISOLanguageName);
        }

        /// <summary>
        /// Gets the content for the current UI language
        /// </summary>
        /// <returns>the content for the lanage or null if it does not exist.</returns>
        public string GetContent()
        {
            return this.GetContent(CultureInfo.InstalledUICulture);
        }

        /// <inheritdoc/>
        public string this[string key] => contentMap[key];

        /// <inheritdoc/>
        public IEnumerable<string> Keys => contentMap.Keys;

        /// <inheritdoc/>
        public IEnumerable<string> Values => contentMap.Values;

        /// <inheritdoc/>
        public int Count => contentMap.Count;

        /// <inheritdoc/>
        public bool ContainsKey(string key)
        {
            return contentMap.ContainsKey(key);
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return contentMap.GetEnumerator();
        }

        /// <inheritdoc/>
        public bool TryGetValue(string key, out string value)
        {
            return contentMap.TryGetValue(key, out value);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return contentMap.GetEnumerator();
        }
    }
}
