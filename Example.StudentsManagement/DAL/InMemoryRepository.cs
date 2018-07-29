using System.Collections.Generic;

namespace Example.StudentsManagement.DAL
{
    public class InMemoryRepository
    {
        private static Dictionary<string, List<object>> data = new Dictionary<string, List<object>>();

        public void Add<T>(T obj)
        {
            string key = typeof(T).Name;
            if (!data.ContainsKey(key))
            {
                data.Add(key, new List<object>());
            }
            data[key].Add(obj);
        }

        public List<T> GetAll<T>()
        {
            string key = typeof(T).Name;
            var result = new List<T>();
            if (data.ContainsKey(key))
            {
                foreach (var o in data[key])
                {
                    result.Add((T)o);
                }
            }
            return result;
        }

        internal void Remove<T>(T obj)
        {
            string key = typeof(T).Name;
            if (data.ContainsKey(key))
            {
                data[key].Remove(obj);
            }
        }
    }
}