namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        public double precoInicial { get; set; }
        public double precoPorHora { get; set; }
        public int quantidadeVagas { get; set; } 
        public List<Vaga> veiculos { get; set; }

    }
}
