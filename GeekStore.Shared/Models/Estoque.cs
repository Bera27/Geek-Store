namespace GeekStore.Shared.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
    }
}
