using Grafana.Backend.Model;
using TcOpenHammer.Grafana.API.Transformation;

namespace TcOpenHammer.Grafana.API
{
    public interface IQueryableService
    {
        public ITable ExecuteQuery(QueryRequest request, string target);
    }
}
