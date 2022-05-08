using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUZ_TREINAMENTO
{
    public class Escola
    {

        private List<Aluno> listaAluno;

        public Escola()
        {
            this.listaAluno = ListaStaticAluno.listaStaticAluno;
        }

        public void AddAluno(string nomeCompleto, int codAluno, Ano serie)
        {
            this.listaAluno.Add(new Aluno(nomeCompleto, codAluno, serie));
        }

        public void UpdateAluno(Aluno aluno, string? nome = null, Ano? serie = null)
        {
            aluno.NomeCompleto = nome ?? aluno.NomeCompleto;
            aluno.Serie = serie ?? aluno.Serie;
        }

        public void RemoveAluno(Aluno aluno)
        {
            this.listaAluno.Remove(aluno);
        }

        public List<Aluno> ListaAluno
        {
            get
            {
                return this.listaAluno;
            }

        }

        public class Aluno
        {
            private string nomeCompleto;
            private int codAluno;
            private Ano serie;

            public Aluno(string nomeCompleto, int codAluno, Ano serie)
            {
                this.nomeCompleto = nomeCompleto;
                this.codAluno = codAluno;
                this.serie = serie;
            }

            public string NomeCompleto
            {
                get { return nomeCompleto; }
                set { nomeCompleto = value; }
            }

            public int CodAluno
            {
                get { return codAluno; }
                set { codAluno = value; }
            }

            public Ano Serie
            {
                get { return serie; }
                set { serie = value; }
            }

        }

        public enum Ano
        {
            PrimeiraSerie = 1,
            SegundaSerie = 2,
            TerceiraSerie = 3,
            QuartaSerie = 4,
            QuintaSerie = 5,
            SextaSerie = 6,
            SetimaSerie = 7,
            OitavaSerie = 8,
            PrimeiroAnoMedio = 9,
            SegundoAnoMedio = 10,
            TerceiroAnoMedio = 11
        }

        public static class ListaStaticAluno
        {
            public static List<Aluno> listaStaticAluno = new List<Aluno>();
        }

    }
    //Classe de banco de Dados
}
