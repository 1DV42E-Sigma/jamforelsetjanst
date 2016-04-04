using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownComparisons.Domain.Helpers
{
    public interface ICache
    {
        Object GetCache(string key);
        void SetCache(string key, Object data, int cacheItemPolicy);
        void SetCache(string key, object data);
        bool HasValue(string key);
        void RemoveFromCache(string key);
    }
}
