using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzModuleCore
{
    public class Category
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private List<Question> _questions = new List<Question>();

        public List<Question> Questions
        {
            get { return _questions; }
            set { _questions = value; }
        }

        private List<Category> _categories = new List<Category>();

        public List<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        public void AddQuestion(Question question)
        {
            _questions.Add(question);
        }

        public void DeleteQuestion(Question question)
        {
            _questions.Remove(_questions.FirstOrDefault(x => x.Guid == question.Guid) as Question);
        }

        public Category(string name)
        {
            Name = name;
        }
    }
}
