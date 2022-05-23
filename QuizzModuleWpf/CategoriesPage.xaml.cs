using Microsoft.Win32;
using QuizzModuleCore;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace QuizzModuleWpf
{
    /// <summary>
    /// Логика взаимодействия для CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        CategoryService service = new CategoryService();
        public List<Category> Categories { get; set; }
        PassingTheTestWindow window;
        PassingTheTestPage page = new PassingTheTestPage();
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

        private void count_Click(object sender, RoutedEventArgs e)
        {
            service.ConsiderPoints();
            tvCategory.Items.Refresh();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            service.ConsiderPoints();
            tvCategory.Items.Refresh();
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*",
                CreatePrompt = true,
                OverwritePrompt = true

            };
            if (fileDialog.ShowDialog().Value)
            {
                MessageBox.Show(fileDialog.FileName);
                service.Save(fileDialog.FileName);
            }
            //service.Save("test");
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*"
            };
            if (fileDialog.ShowDialog().Value)
            {
                service.Load(fileDialog.FileName);
            }
            Categories = service.Categories;
            tvCategory.ItemsSource = Categories;
        }

        private void PassTest_Click(object sender, RoutedEventArgs e)
        {
            window = new PassingTheTestWindow(page);
            window.Show();
        }
    }
}


