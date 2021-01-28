
using System;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    ///  extensions to the JsonElement class <see cref="JsonElement"/>
    /// </summary>
    public static class JsonElementExtensions
    {
        /// <summary>
        /// this returns the found property or default(JsonElment) if not found
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">thename of the element we are looking for/param>
        /// <returns>the found json property or default(JsonElment).</returns>
        public static JsonElement GetPropertyOrDefault(this JsonElement el, string propertyName)
        {
            JsonElement found;
            if (!el.TryGetProperty(propertyName, out found))
                return default(JsonElement);

            return found;
        }

        /// <summary>
        /// this returns the found property or null if not found
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">the name of the element we are looking for/param>
        /// <returns>the found json property or null.</returns>
        public static Uri GetUriOrDefault(this JsonElement el, string uriPropertyName)
        {
            JsonElement found;
            if (!el.TryGetProperty(uriPropertyName, out found) || found.ValueKind != JsonValueKind.String)
                return null;

            return new Uri(found.GetString());
        }

        /// <summary>
        /// this returns the found property or null if not found
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">the name of the element we are looking for/param>
        /// <returns>the found json property or null.</returns>
        public static string GetStringOrDefault(this JsonElement el, string propertyName)
        {
            JsonElement found;
            if (!el.TryGetProperty(propertyName, out found) || found.ValueKind != JsonValueKind.String)
                return null;

            return found.GetString();
        }
    }
}
