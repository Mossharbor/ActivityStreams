﻿
using System;
using System.Text.Json;
using System.Xml;

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
        /// <param name="propertyName">thename of the element we are looking for</param>
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
        /// <param name="uriPropertyName">the name of the element we are looking for</param>
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
        /// <param name="propertyName">the name of the element we are looking for</param>
        /// <returns>the found json property or null.</returns>
        public static string GetStringOrDefault(this JsonElement el, string propertyName)
        {
            if (el.ValueKind != JsonValueKind.Object)
                return null;

            JsonElement found;
            if (!el.TryGetProperty(propertyName, out found) || found.ValueKind != JsonValueKind.String)
                return null;

            return found.GetString();
        }

        /// <summary>
        /// this returns the found property or null if not found
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">the name of the element we are looking for</param>
        /// <returns>the found json property or null.</returns>
        public static double GetDoubleOrDefault(this JsonElement el, string propertyName)
        {
            JsonElement found;
            if (!el.TryGetProperty(propertyName, out found) || (found.ValueKind != JsonValueKind.Number && found.ValueKind != JsonValueKind.String))
                return double.NaN;

            if (found.ValueKind == JsonValueKind.Number)
                return found.GetDouble();

            if (found.ValueKind == JsonValueKind.String)
                return Double.Parse(found.GetString());

            return double.NaN;
        }

        /// <summary>
        /// this returns the found property or null if not found
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">the name of the element we are looking for</param>
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
        /// <param name="propertyName">the name of the element we are looking for</param>
        /// <returns>the found json property or null.</returns>
        public static DateTime? GetDateTimeOrDefault(this JsonElement el, string propertyName)
        {
            JsonElement found;
            if (!el.TryGetProperty(propertyName, out found) || (found.ValueKind != JsonValueKind.String && found.ValueKind != JsonValueKind.Number))
                return null;

            return found.GetDateTime();
        }

        /// <summary>
        /// this returns the found property or null if not found
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">the name of the element we are looking for</param>
        /// <returns>the found json property or null.</returns>
        public static TimeSpan? GetTimeSpanOrDefault(this JsonElement el, string propertyName)
        {
            string durationString = el.GetStringOrDefault(propertyName);

            if (String.IsNullOrEmpty(durationString))
                return null;

            // NOTE negative durations has issues here: Parsing a valid ISO duration PT-5H fails as it expects negative durations to be formatted as -PT5H.
            return XmlConvert.ToTimeSpan(durationString);
        }

        /// <summary>
        /// returns true if the property exists, false otherwise
        /// </summary>
        /// <param name="el">the json element root we are searching under</param>
        /// <param name="propertyName">the name of the element we are looking for</param>
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
