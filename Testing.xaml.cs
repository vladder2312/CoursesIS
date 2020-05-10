using Courses.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Courses
{
    /// <summary> Логика взаимодействия для Testing.xaml </summary>
    public partial class Testing : Window
    {
        /// <summary> Размеры окна </summary>
        private const int MINWIDTH = 370, MINHEIGHT = 230,
                          MAXWIDTH = 500, MAXHEIGHT = 320;

        /// <summary> Список вопросов </summary>
        private List<Question> Questions;
        private List<int> Answers;
        private int CurrentQuestion;

        public Testing()
        {
            InitializeComponent();
            ChangeSize(true);
            LoadCourses();
        }

        /// <summary> Обработка нажатия на "Начать" </summary>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            string course = CourseCB.Text, student = StudentCB.Text, test = TestCB.Text;

            if(!course.Equals("") || !student.Equals("") || !test.Equals(""))
            {
                Questions = new List<Question>();
                Answers = new List<int>();
                CurrentQuestion = -1;
                ChangeSize(false);
                LoadTest();
                InitGrid.Visibility = Visibility.Hidden;
                TestGrid.Visibility = Visibility.Visible;
                ShowNextQuestion();
            }
        }

        /// <summary> Загрузка курсов из БД </summary>
        private void LoadCourses()
        {
            foreach (DataRow subject in MainWindow.ExecuteQuery(Query.SUBJECTS())) CourseCB.Items.Add(subject[0]);
        }

        /// <summary> Загрузка выбранного теста </summary>
        private void LoadTest()
        {
            foreach (DataRow question in MainWindow.ExecuteQuery(Query.TEST(TestCB.Text)))
                Questions.Add(Question.Transform(question));
        }

        /// <summary> Обработка изменения выбранного курса </summary>
        private void CourseCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string course = CourseCB.SelectedItem.ToString();
            TestCB.Items.Clear();
            StudentCB.Items.Clear();

            foreach (DataRow student in MainWindow.ExecuteQuery(Query.STUDENTS_ON_COURSE(course))) StudentCB.Items.Add(student[0]);
            foreach (DataRow test in MainWindow.ExecuteQuery(Query.TESTS_ON_COURSE(course))) TestCB.Items.Add(test[0]);
        }

        /// <summary> Изменение размера окна </summary>
        private void ChangeSize(Boolean isSmall)
        {
            if (isSmall)
            {
                Width = MINWIDTH;
                Height = MINHEIGHT;
                MaxWidth = MINWIDTH;
                MinWidth = MINWIDTH;
                MinHeight = MINHEIGHT;
                MaxHeight = MINHEIGHT;
            } else
            {
                Width = MAXWIDTH;
                Height = MAXHEIGHT;
                MaxWidth = MAXWIDTH;
                MinWidth = MAXWIDTH;
                MinHeight = MAXHEIGHT;
                MaxHeight = MAXHEIGHT;
            }
        }

        /// <summary> Обработка нажатия на "Ок", при ответе на вопрос </summary>
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var trueAnswer = Questions[CurrentQuestion].ПравильныйОтвет;
            if(Var1.IsChecked == true && trueAnswer == 1)
            {
                Answers.Add(100);
            } else if(Var2.IsChecked == true && trueAnswer == 2)
            {
                Answers.Add(100);
            } else if(Var3.IsChecked == true && trueAnswer == 3)
            {
                Answers.Add(100);
            } else if(Var4.IsChecked == true && trueAnswer == 4)
            {
                Answers.Add(100);
            } else
            {
                Answers.Add(0);
            }
            ShowNextQuestion();
        }

        /// <summary> Вывод следующего вопроса </summary>
        private void ShowNextQuestion()
        {
            CurrentQuestion++;
            if (CurrentQuestion >= Questions.Count)
            {
                ShowResults();
                SaveResults();
                TestGrid.Visibility = Visibility.Hidden;
                ResultsGrid.Visibility = Visibility.Visible;
            } else
            {
                QuestionBlock.Text = Questions[CurrentQuestion].Вопрос;
                Var1.Content = Questions[CurrentQuestion].Вариант1;
                Var2.Content = Questions[CurrentQuestion].Вариант2;
                Var3.Content = Questions[CurrentQuestion].Вариант3;
                Var4.Content = Questions[CurrentQuestion].Вариант4;
            }
        }

        /// <summary> Вывод результатов теста </summary>
        private void ShowResults()
        {
            ResultsBox.Text = "Средний балл: "+Answers.Average();
            string answer;
            for(int i=0;i<Questions.Count; i++)
            {
                switch (Questions[i].ПравильныйОтвет)
                {
                    case 1: answer = Questions[i].Вариант1; break;
                    case 2: answer = Questions[i].Вариант2; break;
                    case 3: answer = Questions[i].Вариант3; break;
                    case 4: answer = Questions[i].Вариант4; break;
                    default: return;
                }
                ResultList.Items.Add(Questions[i].Вопрос+" Правильный ответ: "+answer);
            }
        }

        /// <summary> Сохранение результатов теста в БД </summary>
        private void SaveResults()
        {
            MainWindow.ExecuteQuery(Query.INSERT_RESULTS(TestCB.Text, Convert.ToInt32(Answers.Average())));
        }

        /// <summary> Обработка нажатия на "Закрыть" </summary>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
