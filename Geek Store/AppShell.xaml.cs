using Geek_Store.Views;

namespace Geek_Store
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeRouting();
            InitializeComponent();
        }

        public static void InitializeRouting()
        {
            Routing.RegisterRoute("produtosTela", typeof(ProdutosTela));
            Routing.RegisterRoute("adicionarProdutosTela", typeof(AdicionarProdutosTela));
            Routing.RegisterRoute("editarProduto", typeof(EditarProduto));
        }
    }
}
