using Flurl.Http;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using Utils;
using Utils.CustomExceptions;

namespace DataLayer
{    
    public class RestRequest<T> : IRestRequest<T> where T : class
    {
        private string _baseURL = "http://192.168.100.41:8080/v1";
        //private static IRestClient _restClient = new RestClient();
        public RestRequest(bool includeAuthenticationToken = true)
        {
            if (includeAuthenticationToken)
            {

            }
        }

        public async System.Threading.Tasks.Task<IRestResponse<T>> CreateAsync(string endpoint, object dataToBeSent)
        {
            /*
            IRestRequest request = new RestRequest($"/v1/{endpoint}", Method.POST);
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
            */
            try
            {
                var person = await $"{_baseURL}"
                .AppendPathSegment(endpoint)
                //.SetQueryParams(new { a = 1, b = 2 })
                .WithOAuthBearerToken($"{Session.GetSession().AuthorizationToken}")
                .PostJsonAsync(dataToBeSent)
                .ReceiveJson<T>();
            }
            catch (FlurlHttpException flurlHTTPException)
            {
                string exceptionMessage;
                switch (flurlHTTPException.StatusCode)
                {
                    case flurlHTTPException.StatusCode == 400:
                        exceptionMessage = "La petición no pudo ser procesada debido a que tiene una estructura no válida.";
                        break;
                    case flurlHTTPException.StatusCode == 500:
                        exceptionMessage = "La petición no puede ser procesada en este momento. Por favor, intente más tarde,";
                        break;
                        exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
                        break;
                }
                throw new NetworkRequestException(flurlHTTPException.StatusCode, exceptionMessage);
            }
            
        }

        

        public IRestResponse Delete(object resourceIdentifier)
        {

            throw new NotImplementedException();
        }

        public IRestResponse<T> Get(string endpoint)
        {
            IRestRequest request = new RestRequest($"/v1/{endpoint}", Method.GET);
            
            try
            {
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
        
        public IRestResponse<List<T>> GetAll(string endpoint)
        {
            RestClient restClient = new RestClient("http://192.168.100.41:8080");
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("Holi uwu", "Bearer");
            RestRequest request = new RestRequest($"v1/{endpoint}", Method.GET);
            request.AddParameter("Authorization", "Holis", ParameterType.HttpHeader);
            //request.AddHeader("Authorization", "Holi uwu");            
            
            if (Session.GetSession().AuthorizationToken != null && Session.GetSession().AuthorizationToken != "")
            {
                
                //restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MjI1MTQyMTEsIlVzZXJJbmZvIjp7IkVtYWlsQWRkcmVzcyI6ImFkYWlyaG8xNkBnbWFpbC5jb20iLCJVc2VyVHlwZSI6MX19.hyjvp4Sv4HNTJ7_UBswMqIOEu3UgzVm_CzjazJYGZZ6JdGWabJMujwuUcrYGfjjTZRBA6FIkD96Kq2T6KkAYEVdNdhmqWWnE_xSuH-aULo0_I_Dr7SQbMDqM0t_hbWLTUIjmq1Bluujfe1VtVLYCxgWIcgRP7BUW1HXErAv4E6rBIJvoocJ0NvnRFAB6GGYjONRhvksGEdVWdPNiUe9qkSlIYa_9KEZaZkIBVySdoZg0VYlAJDq-K0aG1-OAyFIddv2xL5TAurgaerpKKjGqg7MUhlMSi8BPS9ak1bDJnbZ9HePo35vHXldYBVKFtZe9X-pwWgG2w0FstaGuVB4Cvg", "OAuth2");
                //restClient.UnsafeAuthenticatedConnectionSharing = true;
                //restClient.Authenticator.Authenticate(restClient, request);                
                //request.AddHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MjI1MTQyMTEsIlVzZXJJbmZvIjp7IkVtYWlsQWRkcmVzcyI6ImFkYWlyaG8xNkBnbWFpbC5jb20iLCJVc2VyVHlwZSI6MX19.hyjvp4Sv4HNTJ7_UBswMqIOEu3UgzVm_CzjazJYGZZ6JdGWabJMujwuUcrYGfjjTZRBA6FIkD96Kq2T6KkAYEVdNdhmqWWnE_xSuH-aULo0_I_Dr7SQbMDqM0t_hbWLTUIjmq1Bluujfe1VtVLYCxgWIcgRP7BUW1HXErAv4E6rBIJvoocJ0NvnRFAB6GGYjONRhvksGEdVWdPNiUe9qkSlIYa_9KEZaZkIBVySdoZg0VYlAJDq-K0aG1-OAyFIddv2xL5TAurgaerpKKjGqg7MUhlMSi8BPS9ak1bDJnbZ9HePo35vHXldYBVKFtZe9X-pwWgG2w0FstaGuVB4Cvg");
                //_restClient.Authenticator = new JwtAuthenticator(Session.GetSession().AuthorizationToken);
                //_restClient.Authenticator.Authenticate(_restClient, request);
            }
            try
            {
                IRestResponse<List<T>> response = restClient.Execute<List<T>>(request);
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
