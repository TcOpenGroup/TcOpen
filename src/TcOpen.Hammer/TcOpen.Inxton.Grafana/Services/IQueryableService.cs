using TcOpenHammer.Grafana.API.Transformation;
using Grafana.Backend.Model;

namespace TcOpenHammer.Grafana.API
{
    public interface IQueryableService
    {
        public ITable ExecuteQuery(QueryRequest request, string target);
    }
}
