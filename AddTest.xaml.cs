using Courses.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Courses
{
    /// <summary> Логика взаимодействия для AddTest.xaml </summary>
    public partial class AddTest : Window
    {
        List<Question> questions = new List<Question>(); //Список вопросов
        int selected = 0; //Выбранный вопрос

        public AddTest()
        {
            InitializeComponent();
            LoadSubjects();
        }

        /// <summary> Обработка нажатия на "Очистить" </summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            QuestionText.Text = "";
            Variant1.Text = "";
            Variant2.Text = "";
            Variant3.Text = "";
            Variant4.Text = "";
            TrueVariant.Text = "";
        }

        /// <summary> Обработка нажатия на "Добавить" </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Question question;
            try
            {
                question = new Question(QuestionText.Text,
                                        Variant1.Text, Variant2.Text, Variant3.Text, Variant4.Text,
                                        Convert.ToInt32(TrueVariant.Text));
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            questions.Add(question);
            TopMenu.Items.Add(new MenuItem());
            ((MenuItem)(TopMenu.Items[questions.Count])).Header = questions.Count+1;
            ((MenuItem)(TopMenu.Items[questions.Count])).Click += MenuItem_Click;
            Clear_Click(sender, e);
        }

        /// <summary> Обработка нажатия на "Готово" </summary>
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            string name = TestNameTB.Text;
            int subject = SubjectCB.SelectedIndex, testId;
            if (!name.Equals("") && subject!=-1 && questions.Count!=0)
            {
                Query.Execute(Query.INSERT_TEST(subject, name));
                DataRowCollection drc = Query.Execute(Query.ID_TEST(name));
                testId = Convert.ToInt32(Query.Execute(Query.ID_TEST(name))[0][0]);
                foreach(Question question in questions)
                {
                    Query.Execute(Query.INSERT_QUESTION(testId,
                                                        question.Вопрос,
                                                        question.ПравильныйОтвет,
                                                        question.Вариант1,
                                                        question.Вариант2,
                                                        question.Вариант3,
                                                        question.Вариант4));
                } 
            }
        }

        /// <summary> Обработка нажатия на номер вопроса </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            selected = Convert.ToInt32(((MenuItem)sender).Header) - 1;

            if (selected < questions.Count)
            {
                QuestionText.Text = questions[selected].Вопрос;
                Variant1.Text = questions[selected].Вариант1;
                Variant2.Text = questions[selected].Вариант2;
                Variant3.Text = questions[selected].Вариант3;
                Variant4.Text = questions[selected].Вариант4;
                TrueVariant.Text = questions[selected].ПравильныйОтвет.ToString();
            } else
            {
                Clear_Click(sender, e);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (selected < questions.Count)
            {
                questions[selected].Вопрос = QuestionText.Text;
                questions[selected].Вариант1 = Variant1.Text;
                questions[selected].Вариант2 = Variant2.Text;
                questions[selected].Вариант3 = Variant3.Text;
                questions[selected].Вариант4 = Variant4.Text;
                questions[selected].ПравильныйОтвет = Convert.ToInt32(TrueVariant.Text);
            }
        }

        private void LoadSubjects()
        {
            foreach (DataRow row in Query.Execute(Query.SUBJECTS())) SubjectCB.Items.Add(row[0]);
        }
    }
}
