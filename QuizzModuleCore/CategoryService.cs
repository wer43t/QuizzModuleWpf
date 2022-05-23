using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuizzModuleCore
{
    public class CategoryService
    {
        public List<Category> Categories { get; private set; }

        public Category currentCategory { get; private set; }

        public CategoryService()
        {
            Categories = new List<Category>();
        }

        public void CreateCategory(string name)
        {
            Categories.Add(new Category(name));
        }

        public void CreateCategory(string name, Category category)
        {
            category.Categories.Add(new Category(name));
        }

        public void DeleteCategory(Category category, List<Category> categories)
        {
            if (Categories.Remove(category))
                return;
            foreach (var c in categories)
            {
                if (c.Categories.Remove(category))
                    c.TotalPoints -= category.TotalPoints;
                DeleteCategory(category, c.Categories);
            }
        }

        public void ConsiderPoints()
        {
            foreach (var c in Categories)
            {
                if (c.Categories.Count != 0 || c.Questions.Count == 0)
                {
                    c.TotalPoints += ConsiderPoints(c.Categories);
                }
                else
                {
                    c.TotalPoints = c.TotalPoints;
                }
            }
        }

        private int ConsiderPoints(List<Category> categories)
        {
            int sum = 0;
            foreach (var c in categories)
            {
                if (c.Questions.Count == 0 && c.Categories.Count != 0)
                {
                    c.TotalPoints = ConsiderPoints(c.Categories);
                }
                sum += c.TotalPoints;
            }
            return sum;
        }

        public void Save(string name)
        {
            string filename = name;
            string output = JsonConvert.SerializeObject(Categories);
            File.WriteAllText(name, output);
        }

        public void Load(string filename)
        {
            string input = File.ReadAllText(filename);
            var categories = JsonConvert.DeserializeObject<List<Category>>(input);
            Categories = categories;
        }

        public List<Question> GetQuestions(List<Category> categories)
        {
            foreach (var c in categories)
            {
                if (c.Categories.Count == 0 || c.Name == "Empty")
                {
                    continue;
                }
                else if (c.EarnedPoints != c.TotalPoints)
                {
                    foreach (var category in c.Categories)
                    {
                        if (!CheckAllPoints(category))
                            continue;
                        if (category.Questions.Count != 0 && category.EarnedPoints != category.TotalPoints)
                        {
                            currentCategory = category;
                            return category.Questions;
                        }
                    }
                }
            }
            return null;
        }

        private bool CheckAllPoints(Category category)
        {
            bool check = true;
            foreach (var q in category.Questions)
            {
                if (q.CorrectAnswers.Count != 0)
                    check = false;
                else
                {
                    check = true;
                    break;
                }
            }
            return check;
        }

        public void ConsiderEarnedPoints()
        {
            foreach (var c in Categories)
            {
                if (c.Categories.Count != 0 || c.Questions.Count == 0)
                {
                    c.EarnedPoints = ConsiderEarnedPoints(c.Categories);
                }
                else
                {
                    c.EarnedPoints = 0;
                }
            }
        }

        private int ConsiderEarnedPoints(List<Category> categories)
        {
            int sum = 0;
            foreach (var c in categories)
            {
                if (c.Questions.Count == 0 && c.Categories.Count != 0)
                {
                    c.EarnedPoints = ConsiderEarnedPoints(c.Categories);
                }
                sum += c.EarnedPoints;
            }
            return sum;
        }

        public void ConsiderCurrentCategory()
        {
            currentCategory.EarnedPoints = 0;
            foreach (var c in currentCategory.Questions)
            {
                if (c.CorrectAnswers.Count != 0)
                    currentCategory.EarnedPoints += Convert.ToInt32(c.CorrectAnswers[0]);
            }
        }


        public int GetAllCategoriesCount()
        {
            int sum = 0;
            foreach (var c in Categories)
            {
                if (c.Categories.Count != 0)
                {
                    sum += GetAllCategoriesCount(c);
                }
            }
            return sum;
        }

        private int GetAllCategoriesCount(Category category)
        {
            int sum = 0;
            foreach (var c in category.Categories)
            {
                if (c.Categories.Count != 0)
                {
                    GetAllCategoriesCount(c);
                }
                else
                {
                    sum++;
                }
            }
            return sum;
        }

        public int GetAllSuccessfullCategoriesCount()
        {
            int sum = 0;
            foreach (var c in Categories)
            {
                if (c.Categories.Count != 0)
                {
                    sum += GetAllSuccessfullCategoriesCount(c);
                }
            }
            return sum;
        }

        private int GetAllSuccessfullCategoriesCount(Category category)
        {
            int sum = 0;
            foreach (var c in category.Categories)
            {
                if (c.Categories.Count != 0)
                {
                    GetAllSuccessfullCategoriesCount(c);
                }
                else
                {
                    if (!CheckAllPoints(c))
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }


        public void CreateReport()
        {
            decimal sum = 0;

            var application = new Excel.Application();
            application.SheetsInNewWorkbook = 1;


            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = application.Worksheets.Item[1];

            worksheet.Name = "Отчет";
            int row = 0;
            int col = 1;
            foreach (var c in Categories)
            {
                row++;
                if (c.Name != "Empty" || c.Categories.Count != 0)
                {
                    worksheet.Cells[col][row] = c.Name;
                    worksheet.Cells[col + 1][row] = Convert.ToDecimal(c.EarnedPoints) / 5 / c.Categories.Count;
                    sum += (decimal)worksheet.Cells[col + 1][row].Value;
                    //Excel.Range worksheetRange = worksheet.Range[row][col];
                    (worksheet.Cells[col][row]).Font.Bold = true;
                    for (int i = 0; i < c.Categories.Count; i++)
                    {
                        row++;
                        worksheet.Cells[col][row] = c.Categories[i].Name;
                        worksheet.Cells[col + 1][row] = Convert.ToDecimal(c.Categories[i].EarnedPoints) / 5;
                    }
                }
                row += 2;
            }

            sum = sum / (Categories.Count - 1);

            worksheet.Cells[col][row] = "Экспертное заключение:";
            worksheet.Cells[col + 1][row] = sum;

            row++;
            if ((sum >= (decimal)3.3) && (sum < (decimal)4.29))
            {
                worksheet.Cells[col][row] = "Соответствует первой квалификационной категории";
                worksheet.Cells[col][row].Interior.Color = Excel.XlRgbColor.rgbGreen;
            }
            else if (sum >= (decimal)4.3)
            {
                worksheet.Cells[col][row] = "Соответствует высшей квалификационной категории";
                worksheet.Cells[col][row].Interior.Color = Excel.XlRgbColor.rgbGreen;
            }
            else
            {
                worksheet.Cells[col][row] = "Не соответствует квалификационной категории";
                worksheet.Cells[col][row].Interior.Color = Excel.XlRgbColor.rgbRed;
            }
            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();
            application.Visible = true;



        }

        //private List<Category> GetCategory(Category category)
        //{
        //    if (category.Categories.Count != 0)
        //    {
        //        foreach(var c in category.Categories)
        //        {
        //            GetCategory(c);
        //        }
        //    }
        //    else
        //    {
        //        ret
        //    }
        //}

    }
}
