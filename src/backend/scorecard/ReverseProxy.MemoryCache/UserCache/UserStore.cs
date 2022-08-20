// Licensed under the Apache License, Version 1.0 (the "License").

using Microsoft.Extensions.Caching.Memory;
using Scorecard.MemoryCache.Contracts;
using Scorecard.MemoryCache.Models;

namespace Scorecard.MemoryCache.UserCache;
public class UserStore<T>: IUserStore<T> where T : class
{ 
    private MemoryCacheReloadToken _reloadToken = new MemoryCacheReloadToken();
    public event ConfigChangeHandler ChangeConfig;
    IMemoryCache _memoryCache;
    public UserStore(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public T GetUserCache(Guid UserId)
    {
        return _memoryCache.Get<T>(UserId);
    }
    public void ReloadUserCache(Guid UserId, T userCache)
    {
        _memoryCache.Set(UserId, userCache);
        Interlocked.Exchange<MemoryCacheReloadToken>(ref this._reloadToken,
                new MemoryCacheReloadToken()).OnReload();
    }
}
