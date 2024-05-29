using DesafioFundamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioFundamentos.Controllers
{
    public class EstacionamentoController
    {
        private Estacionamento _estacionamento = null;
        
        public EstacionamentoController(int quantidadeDeVagas, double precoInicial, double precoPorHora) 
        {
            _estacionamento = new Estacionamento();
            _estacionamento.quantidadeVagas = quantidadeDeVagas;
            _estacionamento.precoInicial = precoInicial;
            _estacionamento.precoPorHora = precoPorHora;
            _estacionamento.veiculos = new List<Vaga>();
        }
        public void ExibirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1 - Cadastrar veículo");
            Console.WriteLine("2 - Remover veículo");
            Console.WriteLine("3 - Listar veículos");
            Console.WriteLine("4 - Encerrar");
        }

        public void AdicionarVeiculo()
        {
            Console.Clear();
            Console.WriteLine("Adicionar Veiculo");
            Console.WriteLine("Palca: ");
            var placa = Console.ReadLine();

            try
            {
                Vaga vaga = new Vaga();
                vaga.PlacaDoCarroEstacionado = placa;
                vaga.Valor = _estacionamento.precoInicial;
                _estacionamento.veiculos.Add(vaga);

                Console.WriteLine("Veiculo Cadastrado. Pressione 0 para voltar voltar ao MENU");
                Console.Read();
                ExibirMenuPrincipal();

            }
            catch 
            {
               throw new Exception("Falha ao cadastrar veiculo verificque os dados de entrada e tente novamente");
            }
        }

        public void RemoverVeiculo()
        {
            Console.Clear();
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = Console.ReadLine();

            var vaga = _estacionamento.veiculos.Find(x => x.PlacaDoCarroEstacionado.ToUpper() == placa.ToUpper());
            if(vaga == null)
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                Console.Read();
                ExibirMenuPrincipal();
            }

            var permanencia = DateTime.Now.Minute - vaga.HorarioInicial.Minute;
            var valor = vaga.Valor + (_estacionamento.precoPorHora * permanencia);

            Console.WriteLine($"Veiculo: {vaga.PlacaDoCarroEstacionado} \nHora Entarda:{vaga.HorarioInicial.ToString("dd/MM HH:mm")} \nTempo de Permanecia (Minutos): {permanencia} \nValor Total: R${valor} ");
            Console.WriteLine( "Confirmar Saida e pagamento?  (SIM [1] - NAO [2])" );
            var confirmacao = Console.ReadLine();
            if (confirmacao == "1")
                _estacionamento.veiculos.Remove(vaga);
            
            ExibirMenuPrincipal();

        }

        public void ListarVeiculos()
        {
            Console.Clear();
            // Verifica se há veículos no estacionamento
            if (!_estacionamento.veiculos.Any())
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
            else
            {
                Console.WriteLine("Os veículos estacionados são:");
                _estacionamento.veiculos.ForEach(x => Console.WriteLine($"Placa: {x.PlacaDoCarroEstacionado} | Entrada: {x.HorarioInicial.ToString("dd/MM HH:mm")}"));
            }

            Console.WriteLine("Ação Finalizada. Tecle 0 para voltar ao MENU");
            Console.Read();
            ExibirMenuPrincipal();

        }
    }
}
