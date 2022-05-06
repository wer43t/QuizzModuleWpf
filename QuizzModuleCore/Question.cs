using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzModuleCore
{
    public class Question
    {
        private Guid _guid = Guid.NewGuid();

        public Guid Guid
        {
            get { return _guid; }
            
        }


        private string _text;

        public  string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private List<string> _answers;

        public  List<string> Answers
        {
            get { return _answers; }
            set { _answers = value; }
        }

        private List<string> _correctAnswers;

        public List<string> CorrectAnswers
        {
            get { return _correctAnswers; }
            set { _correctAnswers = value; }
        }

        public Question(string Text)
        {
            this.Text = Text;
        }

    }
}
