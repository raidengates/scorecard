// Licensed under the Apache License, Version 1.0 (the "License").

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scorecard.MemoryCache.Models;

namespace Scorecard.MemoryCache.Contracts;
/// <summary>
/// UserId as cache key 
/// </summary>
/// <typeparam name="T">T as type for Role</typeparam>
public interface IUserStore<T> : IBaseStore where T : class
{
    T GetUserCache(Guid UserId);
    void ReloadUserCache(Guid UserId, T cache);
}
