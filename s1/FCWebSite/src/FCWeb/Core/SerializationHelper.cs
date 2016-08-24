namespace FCWeb.Core
{
    using System;
    using Newtonsoft.Json;

    public class SerializationHelper
    {
        public static T FromJsonSafely<T>(string json, bool newObjectOnError = true) where T : new()
        {
            T result;

            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
            }
            // TODO: Try to catch JsonException
            catch(Exception ex)
            {
                // TODO: Add trace here

                result = newObjectOnError ? Activator.CreateInstance<T>() : default(T);
            }

            return result;
        }
    }
}
