using System.Globalization;
using System.Threading.Tasks;
using GeekStore.Shared.Data;
using GeekStore.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Geek_Store.Views;

[QueryProperty(nameof(ProdutoId), "id")]
public partial class EditarProduto : ContentPage
{
	private readonly GeekStoreDataContext _context;

    public string ProdutoId { set => CarregarDados(value); }
    private Produto _produto;

    // Recebe o produto selecionado na ProdutosTela via navegação Shell
    public Produto ProdRecebido
	{
		get => _produto;
		set
		{
			_produto = value;
			OnPropertyChanged(nameof(ProdRecebido));
		}
	}

    public EditarProduto(GeekStoreDataContext context)
	{
		InitializeComponent();
		_context = context;
		BindingContext = this;
	}

	public async void CarregarDados(string idStr)
	{
		if(int.TryParse(idStr, out var id))
		{
            ProdRecebido = await _context.Produtos
							.AsNoTracking()
							.FirstOrDefaultAsync(p => p.Id == id);

			if (ProdRecebido == null)
			{
				await DisplayAlert("Erro", "Produto não encontrado.", "OK");
				return;
			}
        }
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var nome = txt_Nome.Text;
        var descricao = txt_Descricao.Text;
        var inputCompra = txt_PrecoCompra.Text.Trim();
        var inputVenda = txt_PrecoVenda.Text.Trim();
        var inputQuantidade = txt_Quantidade.Text;

		if (!decimal.TryParse(inputCompra, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal precoCompra)
			|| !decimal.TryParse(inputVenda, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal precoVenda))
		{
            await DisplayAlert("Alerta", "Valor de compra ou venda inválido!", "OK");
            return;
        }

		if (!int.TryParse(inputQuantidade, out var quantidade))
		{
			await DisplayAlert("Alerta", "Valor de quantidade inválido!", "OK");
			return;
		}

		try
		{
			var editProd = new Produto
			{
				Id = ProdRecebido.Id,
				Nome = nome,
				Descricao = descricao,
				PrecoCompra = precoCompra,
				PrecoVenda = precoVenda,
				Quantidade = quantidade
			};

			_context.Update(editProd);
			await _context.SaveChangesAsync();
            await DisplayAlert("Sucesso", $"Produto '{nome}' atualizado com sucesso!", "OK");
			await Shell.Current.GoToAsync("produtosTela");
        }
		catch (Exception ex) { await DisplayAlert("Ops", $"Erro - EP01: {ex.Message}", "OK"); }
    }
}