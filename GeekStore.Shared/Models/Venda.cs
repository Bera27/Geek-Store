namespace GeekStore.Shared.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int IdFuncionario { get; set; }
        public Funcionario Funcionario { get; set; }

        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }

        public ICollection<VendaItem> VendaItens { get; set; } = new List<VendaItem>();
    }
}
