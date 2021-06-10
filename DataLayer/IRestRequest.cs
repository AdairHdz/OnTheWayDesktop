using RestSharp;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IRestRequest<T> where T: class
    {        
        IRestResponse<T> CreateAsync(string endpoint, object dataToBeSent);
        IRestResponse Delete(object resourceIdentifier);
        IRestResponse<T> Get(string endpoint);
        IRestResponse<List<T>> GetAll(string endpoint);
        T ReadCollection(Func<T, bool> filters);
        T Update(T entity);
    }
}
