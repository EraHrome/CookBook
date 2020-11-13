using Microsoft.AspNetCore.Http;
using MyCodeServer.Repositories;
using System;

namespace MyCodeServer.Providers
{
    public class CookieProvider
    {

        private readonly MongoAuthorizationRepository _mongoAuthorizationRepository;

        public CookieProvider(MongoAuthorizationRepository mongoAuthorizationRepository) 
        {

            _mongoAuthorizationRepository = mongoAuthorizationRepository;

        }

        public bool IsAuthentificated(HttpContext httpContext)
        {
            var guid = GetGuidFromCookies(httpContext);
            if (String.IsNullOrEmpty(guid))
            {
                return false;
            }

            return _mongoAuthorizationRepository.LoginnedByToken(guid) != null;

        }

        public void UpdateGuidInCookies(HttpContext httpContext, string guid)
        {
            httpContext.Response.Cookies.Delete("token");
            httpContext.Response.Cookies.Append("token", guid);
        }

        public string GetGuidFromCookies(HttpContext httpContext)
        {
            var userGuid = GetContextToken(httpContext);

            if (String.IsNullOrEmpty(userGuid))
            {
                userGuid = Guid.NewGuid().ToString();
                httpContext.Response.Cookies.Append("token", userGuid);
            }

            return userGuid;
        }

        public string GetContextToken(HttpContext httpContext)
        {
            string userGuid;
            httpContext.Request.Cookies.TryGetValue("token", out userGuid);
            return userGuid;

        }

        public void DeleteGuidFromCookies(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete("token");
        }

    }
}
