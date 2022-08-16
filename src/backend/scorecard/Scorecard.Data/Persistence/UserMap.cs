using MongoDB.Bson.Serialization;
using Scorecard.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Data.Persistence
{
    internal class UserMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                //map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Username).SetIsRequired(true);
                map.MapMember(x => x.Email).SetIsRequired(true);
                map.MapMember(x => x.Password).SetIsRequired(true);
            });
        }
    }

}
