using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using Utils;
using Utils.CustomExceptions;

namespace DataLayer
{
    public class RestRequest<T> : IRestRequest<T> where T : class
    {
        private readonly string _baseURL = "http://localhost:8080/v1";        
        public T Post(string endpoint, object dataToBeSent, bool useAccessToken = true)
        {            
            try
            {
                Url endpointURL = _baseURL
                .AppendPathSegment(endpoint);

                if (useAccessToken)
                {
                    endpoint.WithOAuthBearerToken($"{Session.GetSession().AuthorizationToken}");
                }

                T response = endpointURL
                .PostJsonAsync(dataToBeSent)
                .ReceiveJson<T>().GetAwaiter().GetResult();
                return response;
            }
            catch (FlurlHttpTimeoutException)
            {
                throw new TimeoutException("El servidor tardó demasiado tiempo en responder.");
            }
            catch (FlurlHttpException flurHTTPException) {
                throw new NetworkRequestException(flurHTTPException.StatusCode);
            }
        }        

        public void Delete(object resourceIdentifier)
        {

            throw new NotImplementedException();
        }

        public T Get(string endpoint, bool useAccessToken = true)
        {
            try
            {
                Url endpointURL = _baseURL
                .AppendPathSegment(endpoint);

                if (useAccessToken)
                {
                    return Url.Decode(endpointURL, true).WithOAuthBearerToken($"{Session.GetSession().AuthorizationToken}").GetJsonAsync<T>().GetAwaiter().GetResult();
                }

                return Url.Decode(endpointURL, true).GetJsonAsync<T>().GetAwaiter().GetResult();
            }
            catch (FlurlHttpTimeoutException)
            {
                throw new TimeoutException("El servidor tardó demasiado tiempo en responder.");
            }
            catch (FlurlHttpException flurHTTPException)
            {
                throw new NetworkRequestException(flurHTTPException.StatusCode);
            }
        }

        public List<T> GetAll(string endpoint, bool useAccessToken = true, Dictionary<string, string> queryParameters = null)
        {            
            try
            {
                Url endpointURL = _baseURL
                .AppendPathSegment(endpoint);

                if(useAccessToken)
                {
                    return Url.Decode(endpointURL, true).SetQueryParams(queryParameters).WithOAuthBearerToken($"{Session.GetSession().AuthorizationToken}").GetJsonAsync<List<T>>().GetAwaiter().GetResult();                    
                }

                return Url.Decode(endpointURL, true).SetQueryParams(queryParameters).GetJsonAsync<List<T>>().GetAwaiter().GetResult();
                

            }
            catch (FlurlHttpTimeoutException)
            {
                throw new TimeoutException("El servidor tardó demasiado tiempo en responder.");
            }
            catch (FlurlHttpException flurHTTPException)
            {                
                throw new NetworkRequestException(flurHTTPException.StatusCode);
            }
        }

        public T GetAllWithPagination(string endpoint, bool useAccessToken = true, Dictionary<string, string> queryParameters = null)
        {
            try
            {
                Url endpointURL = _baseURL
                .AppendPathSegment(endpoint);

                if (useAccessToken)
                {
                    return Url.Decode(endpointURL, true).SetQueryParams(queryParameters).WithOAuthBearerToken($"{Session.GetSession().AuthorizationToken}").GetJsonAsync<T>().GetAwaiter().GetResult();
                }

                return Url.Decode(endpointURL, true).SetQueryParams(queryParameters).GetJsonAsync<T>().GetAwaiter().GetResult();


            }
            catch (FlurlHttpTimeoutException)
            {
                throw new TimeoutException("El servidor tardó demasiado tiempo en responder.");
            }
            catch (FlurlHttpException flurHTTPException)
            {
                throw new NetworkRequestException(flurHTTPException.StatusCode);
            }
        }

        public T ReadCollection(Func<T, bool> filters)
        {
            throw new NotImplementedException();
        }

        public bool Patch(string endpoint, object dataToBeSent, bool useAccessToken = true)
        {
            try
            {
                Url endpointURL = _baseURL
                .AppendPathSegment(endpoint);

                if (useAccessToken)
                {
                    endpointURL.WithOAuthBearerToken($"{Session.GetSession().AuthorizationToken}");
                }

                return endpointURL.PatchJsonAsync(dataToBeSent).GetAwaiter().GetResult().StatusCode == 200;

            }
            catch (FlurlHttpTimeoutException)
            {
                throw new TimeoutException("El servidor tardó demasiado tiempo en responder.");
            }
            catch (FlurlHttpException flurHTTPException)
            {
                throw new NetworkRequestException(flurHTTPException.StatusCode);
            }
            throw new NotImplementedException();
        }

        public bool Put(string endpoint, object dataToBeSent, bool useAccessToken = true)
        {
            try
            {
                Url endpointURL = _baseURL
                .AppendPathSegment(endpoint);

                if (useAccessToken)
                {
                    endpointURL.WithOAuthBearerToken($"{Session.GetSession().AuthorizationToken}");
                }

                return endpointURL.PutJsonAsync(dataToBeSent).GetAwaiter().GetResult().StatusCode == 200;

            }
            catch (FlurlHttpTimeoutException)
            {
                throw new TimeoutException("El servidor tardó demasiado tiempo en responder.");
            }
            catch (FlurlHttpException flurHTTPException)
            {
                throw new NetworkRequestException(flurHTTPException.StatusCode);
            }
            throw new NotImplementedException();
        }
    }
}
