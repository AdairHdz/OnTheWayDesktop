using RestSharp;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IRestRequest<T> where T: class
    {        
        IRestResponse<T> Create(string endpoint, object dataToBeSent);
        IRestResponse Delete(object resourceIdentifier);
        IRestResponse<T> Get(IRestRequest restRequest);
        IRestResponse<List<T>> GetAll(string endpoint, IRestRequest restRequest = null);
        T ReadCollection(Func<T, bool> filters);
        T Update(T entity);
    }
}
