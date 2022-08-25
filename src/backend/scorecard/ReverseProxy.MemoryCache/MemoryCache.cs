// Licensed under the Apache License, Version 1.0 (the "License").

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Scorecard.MemoryCache.Contracts;

namespace Scorecard.MemoryCache;
public class Cache : ICache
{
    private MemoryCacheReloadToken _reloadToken = new MemoryCacheReloadToken();
    public event ConfigChangeHandler ChangeConfig;
    IMemoryCache _memoryCache;
    public Cache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public T GetCacheByKey<T>(string Key)
    {
        
        return _memoryCache.Get<T>(Key);
    }

    public void ReloadCache<T>(string Key, T cache)
    {
        _memoryCache.Set(Key, cache);
        Interlocked.Exchange<MemoryCacheReloadToken>(ref this._reloadToken,
                new MemoryCacheReloadToken()).OnReload();
    }
    public void RemoveCache<T>(string Key)
    {
        _memoryCache.Remove(Key);
    }
}
