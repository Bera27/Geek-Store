using System.Threading.Tasks;
using GeekStore.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace Geek_Store.Views;

public partial class ProdutosTela : ContentPage
{
    private readonly GeekStoreDataContext _context;
	public ProdutosTela(GeekStoreDataContext context)
	{
		InitializeComponent();
        _context = context;
	}
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        => Shell.Current.GoToAsync("adicionarProdutosTela");
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarDados();
        
    }

    public async Task CarregarDados()
    {
        var produtosList = await _context.Produtos
                                  .AsNoTracking()
                                  .Select(p => new
                                  {
                                      p.Id,
                                      p.Nome,
                                      p.Descricao,
                                      p.PrecoCompra,
                                      p.PrecoVenda,
                                      Quantidade = p.Estoques.Select(q => q.Quantidade).FirstOrDefault(),
                                  })
                                  .ToListAsync();

        CollectionViewProduto.ItemsSource = produtosList;
    }
}