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
using QuizzModuleCore;

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
            Category.AddQuestion(new Question("misha"));
            lvQuestions.Items.Refresh();
        }
    }
}
