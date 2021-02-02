
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

        /// <summary>
        /// this returns the found property or null if not found
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">the name of the element we are looking for/param>
        /// <returns>the found json property or null.</returns>
        public static double GetDoubleOrDefault(this JsonElement el, string propertyName)
        {
            JsonElement found;
            if (!el.TryGetProperty(propertyName, out found) || found.ValueKind != JsonValueKind.Number)
                return double.NaN;

            return found.GetDouble();
        }

        /// <summary>
        /// this returns the found property or null if not found
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">the name of the element we are looking for/param>
        /// <returns>the found json property or null.</returns>
        public static long GetLongOrDefault(this JsonElement el, string propertyName)
        {
            JsonElement found;
            if (!el.TryGetProperty(propertyName, out found) || (found.ValueKind != JsonValueKind.String && found.ValueKind != JsonValueKind.Number))
                return 0;

            return found.GetInt64();
        }

        /// <summary>
        /// this returns the found property or null if not found
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">the name of the element we are looking for/param>
        /// <returns>the found json property or null.</returns>
        public static DateTime? GetDateTimeOrDefault(this JsonElement el, string propertyName)
        {
            JsonElement found;
            if (!el.TryGetProperty(propertyName, out found) || (found.ValueKind != JsonValueKind.String && found.ValueKind != JsonValueKind.Number))
                return null;

            return found.GetDateTime();
        }

        /// <summary>
        /// returns true if the property exists, false otherwise
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">the name of the element we are looking for/param>
        /// <returns>true if the element exists otherwise false</returns>
        public static bool ContainsElement(this JsonElement el, string propertyName)
        {
            JsonElement found;
            if (!el.TryGetProperty(propertyName, out found) || found.ValueKind == JsonValueKind.Undefined)
                return false;

            return true;
        }

    }
}
