using QuizzModuleCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace QuizzModuleWpf
{
    /// <summary>
    /// Логика взаимодействия для CategoriesEditing.xaml
    /// </summary>
    public partial class CategoriesEditing : Page
    {
        public Category Category { get; set; }
        public CategoriesEditing(Category category)
        {
            InitializeComponent();
            Category = category;
            DataContext = this;
        }

        private void addQuestion_Click(object sender, RoutedEventArgs e)
        {
            Category.AddQuestion(new Question("Enter the question text"));
            lvQuestions.Items.Refresh();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
