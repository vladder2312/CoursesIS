using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Courses
{
    /// <summary> Логика взаимодействия для Testing.xaml </summary>
    public partial class Testing : Window
    {
        public Testing()
        {
            InitializeComponent();
            LoadCourses();
        }

        /// <summary> Обработка нажатия на "Начать" </summary>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            string course = CourseCB.Text, student = StudentCB.Text, test = TestCB.Text;

            if(!course.Equals("") || !student.Equals("") || !test.Equals(""))
            {
                InitGrid.Visibility = Visibility.Hidden;
            }
        }

        /// <summary> Загрузка курсов из БД </summary>
        private void LoadCourses()
        {
            foreach (DataRow subject in MainWindow.ExecuteQuery(Query.SUBJECTS())) CourseCB.Items.Add(subject[0]);
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
    }
}
