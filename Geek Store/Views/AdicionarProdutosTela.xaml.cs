using System.Globalization;
using GeekStore.Shared.Data;
using GeekStore.Shared.Models;

namespace Geek_Store.Views;
public partial class AdicionarProdutosTela : ContentPage
{
	private readonly GeekStoreDataContext _context;
	public AdicionarProdutosTela(GeekStoreDataContext context)
	{
		InitializeComponent();
		_context = context;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		var nome = txt_Nome.Text;
		var descricao = txt_Descricao.Text;
		var inputCompra = txt_PrecoCompra.Text.Trim();
		var inputVenda = txt_PrecoVenda.Text.Trim();
		var inputQuantidade = txt_Quantidade.Text;

		if(string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(descricao))
		{
			await DisplayAlert("Alerta", "Preencha todos os campos", "OK");
			return;
        }

        // Verifica se valor de compra e venda são válidos e converte para decimal
        if (!decimal.TryParse(inputCompra, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal precoCompra)
			|| !decimal.TryParse(inputVenda, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal precoVenda))
		{
			await DisplayAlert("Alerta", "Valor de compra ou venda inválido!", "OK");
			return;
		}

        // Verifica se Quantidade é válido e converte para int
        if (!int.TryParse(inputQuantidade, out int quantidade))
		{
            await DisplayAlert("Alerta", "Valor de quantidade inválido!", "OK");
            return;
        }

		try
		{
			var NovoProduto = new Produto
			{
				Nome = nome,
				Descricao = descricao,
				PrecoCompra = precoCompra,
				PrecoVenda = precoVenda,
				Quantidade = quantidade
			};


			await _context.Produtos.AddAsync(NovoProduto);
			await _context.SaveChangesAsync();

			await DisplayAlert("Sucesso", $"Produto '{nome}' salvo com sucesso!", "OK");
			await Shell.Current.GoToAsync("produtosTela");
        } 
		catch(Exception ex) { await DisplayAlert("Ops", $"Erro - AP01: {ex.Message}", "OK"); }
    }
}