using System.Windows;
using System.Windows.Controls;

namespace QuizzModuleWpf
{
    /// <summary>
    /// Логика взаимодействия для PassingTheTestWindow.xaml
    /// </summary>
    public partial class PassingTheTestWindow : Window
    {

        public PassingTheTestWindow(Page page)
        {
            InitializeComponent();
            mainFrame.NavigationService.Navigate(page);
        }
    }
}
