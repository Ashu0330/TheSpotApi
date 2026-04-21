using Newtonsoft.Json;


namespace ArtistHub.Presentation.Helper
{
    public class JsonHelper<T> where T : new()
    {
        public static IEnumerable<T> DeserializeList(object data)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(JsonConvert.SerializeObject(data)) ?? new List<T>();
        }

        public static T Deserialize(object data)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data)) ?? new T();
        }
        public static IEnumerable<T> DeserializeObject(string data)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(data) ?? new List<T>();
        }
    }
}
