using Lab.WEB.Repositories;
// proyecto web
namespace Lab.WEB.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> Get<T>(string url);

        // parte objectual 
        Task<HttpResponseWrapper<object>> Post<T>(string url, T model);
        // Parte de la respuesta 
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T model);

        Task<HttpResponseWrapper<object>> Delete(string url);

        Task<HttpResponseWrapper<object>> Put<T>(string url, T model);

        Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T model);

    }
}
