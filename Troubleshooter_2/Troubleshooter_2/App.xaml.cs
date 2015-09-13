namespace Troubleshooter_2
{
    using System.Windows;

    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Class.Program.OnStartup(sender, e);
        }
    }
}
