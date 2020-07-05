using Blog.MVP.Blazor.SSR.Models;
using System;
using System.Collections.Concurrent;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    public class AuthStateCache
    {
        private ConcurrentDictionary<string, ServerAuthModel> Cache
            = new ConcurrentDictionary<string, ServerAuthModel>();

        public bool HasSubjectId(string subjectId)
            => Cache.ContainsKey(subjectId);

        public void Add(string subjectId, DateTimeOffset expiration, string accessToken, string refreshToken)
        {
            System.Diagnostics.Debug.WriteLine($"Caching sid: {subjectId}");

            var data = new ServerAuthModel
            {
                SubjectId = subjectId,
                Expiration = expiration,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            Cache.AddOrUpdate(subjectId, data, (k, v) => data);
        }

        public ServerAuthModel Get(string subjectId)
        {
            Cache.TryGetValue(subjectId, out var data);
            return data;
        }

        public void Remove(string subjectId)
        {
            System.Diagnostics.Debug.WriteLine($"Removing sid: {subjectId}");
            Cache.TryRemove(subjectId, out _);
        }
    }
}
