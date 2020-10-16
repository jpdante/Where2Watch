using System;
using HtcSharp.HttpModule.Http.Features;
using StackExchange.Redis;
using Where2Watch.Core;

namespace Where2Watch.Extensions {
    public static class SessionExtension {

        public static bool GetAccountId(this ISession session, out long accountId) {
            if (session.TryGetValue("account", out var rawAccountId) && long.TryParse(((RedisValue) rawAccountId).ToString(), out accountId)) return true;
            accountId = -1;
            return false;
        }

        public static bool SetExpireTime(this ISession session, TimeSpan timeSpan) {
            if (!(session is Session coreSession)) return false;
            coreSession.ExpireKey = timeSpan;
            return true;
        }
    }
}