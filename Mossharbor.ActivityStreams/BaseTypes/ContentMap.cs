
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

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
                this.contentMap.Add(t.Key.ToLower(), t.Value);
            }
        }

        /// <summary>
        /// Gets the content for the language one passed in
        /// </summary>
        /// <param name="twoLetterLanguageName">the culture info</param>
        /// <returns>the content for the lanage or null if it does not exist.</returns>
        public string GetContent(string twoLetterLanguageName)
        {
            this.TryGetValue(twoLetterLanguageName?.ToLower(), out string curLangChar);
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
