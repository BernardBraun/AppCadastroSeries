using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCadastroSeries.Interfaces;

namespace AppCadastroSeries
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

        private static void ExcluirSerie()
        {
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        private static void AtualizarSerie()
        {
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de Início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        private static void ListarSeries()
        {
            Console.WriteLine("*----------------------------------*");
            Console.WriteLine("|          Listar  séries          |");
            Console.WriteLine("*----------------------------------*");
            var lista = repositorio.Lista();
            if(lista.Count == 0)
            {
                Console.WriteLine("*----------------------------------*");
                Console.WriteLine("|     Nenhuma série cadastrada     |");
                Console.WriteLine("*----------------------------------*");
                return;
            }
            foreach (var serie in lista)
            {
                Console.WriteLine("*-------------------------------------------------*");
                Console.WriteLine("|#ID {0}: {1}|",serie.retornaId(), serie.retornaTitulo());
                Console.WriteLine("*-------------------------------------------------*");
                return;
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("*-------------------------------------------------*");
            Console.WriteLine("|              Insira uma nova série              |");
            Console.WriteLine("*-------------------------------------------------*");
            Console.WriteLine();
            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano de estréia da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite uma sinópse para a série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("*----------------------------------*");
            Console.WriteLine("| BernFlix Series a seu dispor!!!  |");
            Console.WriteLine("| Informa a opção desejada abaixo: |");
            Console.WriteLine("*----------------------------------*");
            Console.WriteLine("*----------------------------------*");
            Console.WriteLine("| 1 - Listar séries                |");
            Console.WriteLine("| 2 - Inserir nova série           |");
            Console.WriteLine("| 3 - Atualizar série              |");
            Console.WriteLine("| 4 - Excluir série                |");
            Console.WriteLine("| 5 - Visualizar série             |");
            Console.WriteLine("| C - Limpar tela                  |");
            Console.WriteLine("| X - Excluir                      |");
            Console.WriteLine("*----------------------------------*");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
