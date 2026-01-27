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
    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var busca = e.NewTextValue;

        if (string.IsNullOrWhiteSpace(busca))
        {
            await CarregarDados();
            return;
        }

        var consulta = await _context.Produtos
                        .AsNoTracking()
                        .Where(x => x.Nome.ToLower().Contains(busca.ToLower()))
                        .Select(p => new
                        {
                            p.Id,
                            p.Nome,
                            p.Descricao,
                            p.PrecoCompra,
                            p.PrecoVenda,
                            Quantidade = p.Estoques.Select(q => q.Quantidade).FirstOrDefault()
                        })
                        .ToListAsync();

        CollectionViewProduto.ItemsSource = consulta;
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

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        dynamic itemSelect = button.BindingContext;

        int id = itemSelect.Id;
        string nome = itemSelect.Nome;

        bool confirmacao = await DisplayAlert("Excluir", $"Tem certeza que deseja EXCLUIR o produto: 'ID: {id} - {nome}'", "Sim", "Não");
        if(confirmacao)
        {
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
                if (produto == null)
                    return;

                _context.Remove(produto);
                await _context.SaveChangesAsync();

                await CarregarDados();
                await DisplayAlert("Sucesso!", "Produto removido com sucesso!", "Ok");
            }
            catch (Exception ex)
            { await DisplayAlert("Ops", $"Erro - PT01: {ex.Message}", "Ok"); }
        }
    }
}