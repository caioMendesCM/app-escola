using System.Data.Common;

namespace LUZ_TREINAMENTO
{
    public interface IConexao
    {
        public void StartConnection();
        public int AdicionarNaTabela(string table, string parameters, string items);
        public void AtualizarNaTabela(string table, string items, string where);
        public void RemoverDaTabela(string table, string where);
        public void CloseConnections();
        public DbDataReader ReceberTabela(string table);
    }
}
