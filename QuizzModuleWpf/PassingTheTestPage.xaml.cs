using QuizzModuleCore;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QuizzModuleWpf
{
    /// <summary>
    /// Логика взаимодействия для PassingTheTestPage.xaml
    /// </summary>
    public partial class PassingTheTestPage : Page
    {
        CategoryService service;
        public List<Category> Categories { get; set; }
        public List<Question> Questions { get; set; }
        public Category currentCategory { get; set; }
        public PassingTheTestPage(CategoryService service)
        {
            this.service = service;
            InitializeComponent();
            service.Load(@"C:\Users\Salavat\source\repos\QuizzModuleWpf\QuizzModuleWpf\bin\Debug\test.txt");
            Categories = service.Categories;
            Questions = service.GetQuestions(Categories);
            currentCategory = service.currentCategory;
            DataContext = this;
            tvCategory.ItemsSource = Categories;
            lvQuestions.ItemsSource = Questions;
            progressTest.Maximum = service.GetAllCategoriesCount();
            progressTest.Value = 0;
        }


        private void btnGetNextQuestions_Click(object sender, RoutedEventArgs e)
        {

        }


        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            string pointsCost = (sender as RadioButton).Content.ToString();
            ((sender as RadioButton).DataContext as Question).AddCorrectAnswer(pointsCost);
            service.ConsiderCurrentCategory();
        }

        private void getNextQuestion_Click(object sender, RoutedEventArgs e)
        {
            service.ConsiderEarnedPoints();
            Questions = service.GetQuestions(Categories);
            lblCtgName.Text = service.currentCategory.Name;
            lvQuestions.ItemsSource = Questions;
            tvCategory.Items.Refresh();
            DataContext = this;
            progressTest.Value = service.GetAllSuccessfullCategoriesCount();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if((sender as Page).ActualHeight < 450)
            {
                svContent.Height = 250;
            }
            else
            {
                svContent.Height = 1000;
            }
        }
    }
}