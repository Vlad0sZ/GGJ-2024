using BackendGGJ.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BackendGGJ.Database;

public class ActionDatabase
{
    private const string Key = "action_data";
    private readonly IMemoryCache _memoryCache;

    public ActionDatabase(IMemoryCache memoryCache) =>
        _memoryCache = memoryCache;

    public IEnumerable<ActionData> GetAllData()
    {
        if (_memoryCache.TryGetValue(Key, out ActionDataCollection cachedAction))
            return cachedAction.ActionData;

        string jsonContent = File.ReadAllText($"{Key}.json");
        var array = JsonConvert.DeserializeObject<ActionData[]>(jsonContent) ?? Array.Empty<ActionData>();

        _memoryCache.Set(Key, new ActionDataCollection(array), TimeSpan.FromMinutes(3));
        return array;
    }

    [System.Serializable]
    private class ActionDataCollection
    {
        public ActionData[] ActionData;

        public ActionDataCollection(ActionData[] array) =>
            ActionData = array;
    }
}