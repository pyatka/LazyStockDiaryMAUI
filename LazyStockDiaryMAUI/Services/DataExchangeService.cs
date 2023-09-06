using System;
namespace LazyStockDiaryMAUI.Services
{
    public class DataExchangeService
    {
        private Dictionary<string, object> dataStorage;
        public DataExchangeService()
        {
            dataStorage = new Dictionary<string, object>();
        }

        public string Put(object value)
        {
            string newKey = Guid.NewGuid().ToString();
            dataStorage[newKey] = value;
            return newKey;
        }

        public object Pop(string key)
        {
            var value = dataStorage[key];
            dataStorage.Remove(key);
            return value;
        }
    }
}

