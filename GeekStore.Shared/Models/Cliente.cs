namespace GeekStore.Shared.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }

        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}
