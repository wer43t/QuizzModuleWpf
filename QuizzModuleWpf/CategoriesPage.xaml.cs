using QuizzModuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizzModuleWpf
{
    /// <summary>
    /// Логика взаимодействия для CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        CategoryService service = new CategoryService();
        public List<Category> Categories { get; set; }
        public CategoriesPage()
        {
            InitializeComponent();
            Categories = service.Categories;
            service.CreateCategory("Empty");
            this.DataContext = this;
            tvCategory.ItemsSource = Categories;
        }

        private void addCategory_Click(object sender, RoutedEventArgs e)
        {
            if ((tvCategory.SelectedItem as Category).Name != "Empty")
                //(tvCategory.SelectedItem as Category).Categories.Add(CreateCategory(tbCategoryName?.Text);
                service.CreateCategory(tbCategoryName?.Text, tvCategory.SelectedItem as Category);
            else
                service.CreateCategory(tbCategoryName?.Text);

            tvCategory.Items.Refresh();
        }

        private void deleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if ((tvCategory.SelectedItem as Category).Name != "Empty")
                service.DeleteCategory(tvCategory.SelectedItem as Category, Categories);
            tvCategory.Items.Refresh();

        }

        private void editCategory_Click(object sender, RoutedEventArgs e)
        {
            if ((tvCategory.SelectedItem as Category).Name != "Empty")
                NavigationService.Navigate(new CategoriesEditing(tvCategory.SelectedItem as Category));
        }
    }
}


