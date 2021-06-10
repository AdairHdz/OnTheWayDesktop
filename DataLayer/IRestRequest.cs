using System;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IRestRequest<T> where T: class
    {        
        T Post(string endpoint, object dataToBeSent, bool useAccessToken);
        void Delete(object resourceIdentifier);
        T Get();
        List<T> GetAll(string endpoint, bool useAccessTokens = true, Dictionary<string, string> queryParameters = null);
        T GetAllWithPagination(string endpoint, bool useAccessToken = true, Dictionary<string, string> queryParameters = null);
        T ReadCollection(Func<T, bool> filters);
        bool Patch(string endpoint, object dataToBeSent, bool useAccessToken = true);
        bool Put(string endpoint, object dataToBeSent, bool useAccessToken = true);
    }
}
