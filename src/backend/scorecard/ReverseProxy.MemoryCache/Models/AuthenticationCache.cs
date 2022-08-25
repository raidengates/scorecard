// Licensed under the Apache License, Version 1.0 (the "License").

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.MemoryCache.Models;
public class AuthenticationCache
{
    public Guid UserId { get; set; }
    public List<string> Permissions { get; set; }
}
