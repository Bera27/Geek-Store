namespace Geek_Store
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState) =>
            new Window(new AppShell())
            {
                Width = 1980,
                Height = 900
            };
    }
}