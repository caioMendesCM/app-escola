using System.Data.Common;

namespace LUZ_TREINAMENTO
{
    public interface IConexao
    {
        public void ExecuteNonQuery(string query);
        public DbDataReader ExecuteQuery(string query);
        public void DisposeConnection();
    }
}
