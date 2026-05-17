namespace App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage MainPage = new MainPage();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage());
        }
    }
}