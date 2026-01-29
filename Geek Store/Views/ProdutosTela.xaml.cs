using System.Threading.Tasks;
using GeekStore.Shared.Data;
using GeekStore.Shared.Models;
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

    private async void CollectionViewProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selecionado = e.CurrentSelection?.FirstOrDefault();

        if(selecionado is Produto produto)
        {
            await Shell.Current.GoToAsync($"editarProduto?id={produto.Id}");
        }

        if (sender is CollectionView cv)
            cv.SelectedItem = null;
    }
}