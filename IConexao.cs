using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUZ_TREINAMENTO
{
    public interface IConexao
    {
        public int AdicionarNaTabela(Student student);
        public void RemoverDaTabela(Student student);
        public void AtualizarNaTabela(Student student, Student SelectedStudent);
        public ObservableCollection<Student> ReceberTabela();
    }
}
