namespace GeekStore.Shared.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }

        public ICollection<Estoque> Estoques { get; set; } = new List<Estoque>();
        public ICollection<VendaItem> VendaItens { get; set; } = new List<VendaItem>();
    }
}
