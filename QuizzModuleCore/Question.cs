using System;
using System.Collections.Generic;

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

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private List<string> _answers;

        public List<string> Answers
        {
            get { return _answers; }
            set { _answers = value; }
        }

        private List<string> _correctAnswers = new List<string>();

        public List<string> CorrectAnswers
        {
            get
            {
                if (_correctAnswers is null)
                {
                    _correctAnswers = new List<string>();
                }
                return _correctAnswers;
            }
            set { _correctAnswers = value; }
        }

        public Question(string Text)
        {
            this.Text = Text;
        }

        public void AddCorrectAnswer(string n)
        {
            if (_correctAnswers is null)
            {
                _correctAnswers = new List<string>();
            }
            CorrectAnswers.Insert(0, n);
        }

    }
}
