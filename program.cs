using System; // Biblioteca para operações básicas, como entrada e saída.
using System.Collections.Generic; // Biblioteca para usar coleções genéricas como List.
using System.Linq; // Biblioteca para realizar operações como buscas e filtros em coleções.

namespace GerenciadorTarefas // Define o espaço de nomes do projeto.
{
    public class Tarefa // Classe que representa uma tarefa.
    {
        public int Id { get; set; } // Identificador único da tarefa.
        public string Nome { get; set; } // Nome ou título da tarefa.
        public string Categoria { get; set; } // Categoria da tarefa (ex.: trabalho, pessoal).
        public DateTime Prazo { get; set; } // Data de prazo para a tarefa.
        public bool Concluida { get; set; } // Indica se a tarefa está concluída.

        public Tarefa(int id, string nome, string categoria, DateTime prazo) // Construtor da classe Tarefa.
        {
            Id = id; // Inicializa o ID.
            Nome = nome; // Inicializa o Nome.
            Categoria = categoria; // Inicializa a Categoria.
            Prazo = prazo; // Inicializa o Prazo.
            Concluida = false; // Define a tarefa como não concluída por padrão.
        }

        public override string ToString() // Sobrescreve o método ToString para exibir as informações formatadas.
        {
            string status = Concluida ? "\u001b[32m[Concluída]\u001b[0m" : "\u001b[31m[Pendente]\u001b[0m"; // Define status com cor.
            return $"\u001b[33mID: {Id}\u001b[0m | Nome: {Nome} | Categoria: {Categoria} | Prazo: {Prazo:dd/MM/yyyy} | Status: {status}"; // Formata os dados da tarefa.
        }
    }

    class Program // Classe principal do programa.
    {
        static List<Tarefa> tarefas = new List<Tarefa>(); // Lista para armazenar todas as tarefas.
        static int proximoId = 1; // Variável para gerar IDs únicos.

        static void Main(string[] args) // Método principal do programa.
        {
            while (true) // Loop infinito para manter o programa rodando.
            {
                MostrarMenu(); // Exibe o menu principal.
            }
        }

        static void MostrarMenu() // Método para exibir o menu principal.
        {
            Console.Clear(); // Limpa o console.
            EscreverComCor("=== Gerenciador Moderno de Tarefas ===", ConsoleColor.Cyan); // Exibe o título com cor.
            Console.WriteLine("1. Adicionar Tarefa"); // Opção para adicionar tarefa.
            Console.WriteLine("2. Listar Tarefas"); // Opção para listar tarefas.
            Console.WriteLine("3. Concluir Tarefa"); // Opção para concluir tarefa.
            Console.WriteLine("4. Remover Tarefa"); // Opção para remover tarefa.
            Console.WriteLine("5. Resumo de Tarefas"); // Opção para exibir resumo.
            Console.WriteLine("6. Sair"); // Opção para sair do programa.
            Console.Write("\nEscolha uma opção: "); // Solicita a escolha do usuário.

            string opcao = Console.ReadLine(); // Lê a escolha do usuário.
            switch (opcao) // Verifica a opção escolhida.
            {
                case "1":
                    AdicionarTarefa(); // Chama o método para adicionar tarefa.
                    break;
                case "2":
                    ListarTarefas(); // Chama o método para listar tarefas.
                    break;
                case "3":
                    ConcluirTarefa(); // Chama o método para concluir tarefa.
                    break;
                case "4":
                    RemoverTarefa(); // Chama o método para remover tarefa.
                    break;
                case "5":
                    ResumoTarefas(); // Chama o método para exibir resumo.
                    break;
                case "6":
                    Sair(); // Encerra o programa.
                    break;
                default:
                    EscreverComCor("Opção inválida! Pressione Enter para tentar novamente.", ConsoleColor.Red); // Mensagem de erro para entrada inválida.
                    Console.ReadLine(); // Aguarda o usuário pressionar Enter.
                    break;
            }
        }

        static void AdicionarTarefa() // Método para adicionar uma nova tarefa.
        {
            Console.Clear(); // Limpa o console.
            EscreverComCor("=== Adicionar Nova Tarefa ===", ConsoleColor.Cyan); // Exibe o título com cor.

            Console.Write("Nome da Tarefa: "); // Solicita o nome da tarefa.
            string nome = Console.ReadLine(); // Lê o nome.

            Console.Write("Categoria: "); // Solicita a categoria da tarefa.
            string categoria = Console.ReadLine(); // Lê a categoria.

            Console.Write("Prazo (dd/mm/yyyy): "); // Solicita o prazo da tarefa.
            if (DateTime.TryParse(Console.ReadLine(), out DateTime prazo)) // Tenta converter a entrada para uma data.
            {
                tarefas.Add(new Tarefa(proximoId++, nome, categoria, prazo)); // Adiciona a nova tarefa na lista.
                EscreverComCor("Tarefa adicionada com sucesso!", ConsoleColor.Green); // Exibe mensagem de sucesso.
            }
            else
            {
                EscreverComCor("Data inválida. Tente novamente.", ConsoleColor.Red); // Exibe mensagem de erro.
            }

            Pausar(); // Aguarda o usuário antes de voltar ao menu.
        }

        static void ListarTarefas() // Método para listar todas as tarefas.
        {
            Console.Clear(); // Limpa o console.
            EscreverComCor("=== Lista de Tarefas ===", ConsoleColor.Cyan); // Exibe o título com cor.

            if (tarefas.Count == 0) // Verifica se não há tarefas cadastradas.
            {
                EscreverComCor("Nenhuma tarefa cadastrada.", ConsoleColor.Yellow); // Mensagem para lista vazia.
            }
            else
            {
                foreach (var tarefa in tarefas) // Itera sobre a lista de tarefas.
                {
                    Console.WriteLine(tarefa); // Exibe cada tarefa formatada.
                }
            }

            Pausar(); // Aguarda o usuário antes de voltar ao menu.
        }

        static void ConcluirTarefa() // Método para marcar uma tarefa como concluída.
        {
            Console.Clear(); // Limpa o console.
            EscreverComCor("=== Concluir Tarefa ===", ConsoleColor.Cyan); // Exibe o título com cor.

            ListarTarefas(); // Exibe a lista de tarefas.
            Console.Write("\nDigite o ID da tarefa a concluir: "); // Solicita o ID da tarefa.
            if (int.TryParse(Console.ReadLine(), out int id)) // Tenta converter a entrada para inteiro.
            {
                var tarefa = tarefas.FirstOrDefault(t => t.Id == id); // Busca a tarefa pelo ID.
                if (tarefa != null) // Verifica se a tarefa foi encontrada.
                {
                    tarefa.Concluida = true; // Marca a tarefa como concluída.
                    EscreverComCor($"Tarefa '{tarefa.Nome}' concluída com sucesso!", ConsoleColor.Green); // Exibe mensagem de sucesso.
                }
                else
                {
                    EscreverComCor("Tarefa não encontrada.", ConsoleColor.Red); // Mensagem de erro se não encontrar.
                }
            }
            else
            {
                EscreverComCor("ID inválido.", ConsoleColor.Red); // Mensagem de erro para entrada inválida.
            }

            Pausar(); // Aguarda o usuário antes de voltar ao menu.
        }

        static void RemoverTarefa() // Método para remover uma tarefa.
        {
            Console.Clear(); // Limpa o console.
            EscreverComCor("=== Remover Tarefa ===", ConsoleColor.Cyan); // Exibe o título com cor.

            ListarTarefas(); // Exibe a lista de tarefas.
            Console.Write("\nDigite o ID da tarefa a remover: "); // Solicita o ID da tarefa.
            if (int.TryParse(Console.ReadLine(), out int id)) // Tenta converter a entrada para inteiro.
            {
                var tarefa = tarefas.FirstOrDefault(t => t.Id == id); // Busca a tarefa pelo ID.
                if (tarefa != null) // Verifica se a tarefa foi encontrada.
                {
                    tarefas.Remove(tarefa); // Remove a tarefa da lista.
                    EscreverComCor($"Tarefa '{tarefa.Nome}' removida com sucesso!", ConsoleColor.Green); // Exibe mensagem de sucesso.
                }
                else
                {
                    EscreverComCor("Tarefa não encontrada.", ConsoleColor.Red); // Mensagem de erro se não encontrar.
                }
            }
            else
            {
                EscreverComCor("ID inválido.", ConsoleColor.Red); // Mensagem de erro para entrada inválida.
            }

            Pausar(); // Aguarda o usuário antes de voltar ao menu.
        }

        static void ResumoTarefas() // Método para exibir o resumo das tarefas.
        {
            Console.Clear(); // Limpa o console.
            EscreverComCor("=== Resumo de Tarefas ===", ConsoleColor.Cyan); // Exibe o título com cor.

            int concluidas = tarefas.Count(t => t.Concluida); // Conta as tarefas concluídas.
            int pendentes = tarefas.Count - concluidas; // Calcula as tarefas pendentes.

            EscreverComCor($"Tarefas Concluídas: {concluidas}", ConsoleColor.Green); // Exibe a quantidade de concluídas.
            EscreverComCor($"Tarefas Pendentes: {pendentes}", ConsoleColor.Red); // Exibe a quantidade de pendentes.

            Pausar(); // Aguarda o usuário antes de voltar ao menu.
        }

        static void Sair() // Método para encerrar o programa.
        {
            Console.Clear(); // Limpa o console.
            EscreverComCor("Obrigado por usar o Gerenciador Moderno de Tarefas!", ConsoleColor.Green); // Mensagem de despedida.
            Environment.Exit(0); // Finaliza a execução.
        }

        static void EscreverComCor(string texto, ConsoleColor cor) // Método para exibir texto colorido.
        {
            Console.ForegroundColor = cor; // Define a cor do texto.
            Console.WriteLine(texto); // Exibe o texto.
            Console.ResetColor(); // Restaura a cor padrão.
        }

        static void Pausar() // Método para pausar o programa.
        {
            Console.WriteLine("\nPressione Enter para voltar ao menu..."); // Exibe a mensagem de pausa.
            Console.ReadLine(); // Aguarda o usuário pressionar Enter.
        }
    }
}
