using RestSharp;
using System;
using System.Collections.Generic;
using Utils.CustomExceptions;

namespace DataLayer
{
    public class RestRequest<T> : IRestRequest<T> where T : class
    {

        private readonly IRestClient _restClient = new RestClient("http://localhost:8080/v1");                
        public IRestResponse<T> Create(string endpoint, object dataToBeSent)
        {
            IRestRequest request = new RestRequest(endpoint, Method.POST);
            request.AddJsonBody(dataToBeSent);
            IRestResponse<T> response = _restClient.Execute<T>(request);
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                string exceptionMessage;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                        exceptionMessage = "La petición no pudo ser procesada debido a que tiene una estructura no válida.";
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        exceptionMessage = "La petición no puede ser procesada en este momento. Por favor, intente más tarde,";
                        break;
                    default:
                        exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
                        break;
                }
                throw new NetworkRequestException(response.StatusCode, exceptionMessage);
            }
            return response;
        }

        

        public IRestResponse Delete(object resourceIdentifier)
        {

            throw new NotImplementedException();
        }

        public IRestResponse<T> Get(IRestRequest restRequest)
        {
            //IRestRequest request = new RestRequest(endpoint, Method.GET);
            //IRestResponse<List<T>> response = _restClient.Execute<List<T>>(request);
            
            
            throw new NotImplementedException();
        }

        public IRestResponse<List<T>> GetAll(string endpoint, IRestRequest restRequest = null)
        {
            IRestRequest request = new RestRequest(endpoint, Method.GET);
            if(restRequest != null)
            {
                restRequest.Parameters.ForEach(param =>
                {
                    if(param.Type == ParameterType.QueryString)
                    {                        
                        request.AddQueryParameter(param.Name, param.Value.ToString());
                    }                    
                });
                
            }
            try
            {
                IRestResponse<List<T>> response = _restClient.Execute<List<T>>(request);
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string exceptionMessage;
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                            exceptionMessage = "La petición no pudo ser procesada debido a que tiene una estructura no válida.";
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            exceptionMessage = "La petición no puede ser procesada en este momento. Por favor, intente más tarde,";
                            break;
                        case System.Net.HttpStatusCode.OK:
                            throw new EmptyCollectionException();
                        default:
                            exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
                            break;
                    }
                    throw new NetworkRequestException(response.StatusCode, exceptionMessage);
                }
                return response;
            }
            catch (NullReferenceException)
            {
                throw new EmptyCollectionException();
            }
        }

        public T ReadCollection(Func<T, bool> filters)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
