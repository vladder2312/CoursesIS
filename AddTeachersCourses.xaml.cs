using System;
using System.Data;
using System.Windows;

namespace Courses
{
    /// <summary> Логика взаимодействия для AddTeachersCourses.xaml </summary>
    public partial class AddTeachersCourses : Window
    {
        public AddTeachersCourses()
        {
            InitializeComponent();
            LoadCourses();
            LoadTeachers();
        }

        /// <summary> Загрузка курсов из БД </summary>
        private void LoadCourses()
        {

            foreach (DataRow course in Query.Execute(Query.COURSES())) CourseCB.Items.Add(course[0]+" "+course[1]);
        }

        /// <summary> Загрузка учителей из БД </summary>
        private void LoadTeachers()
        {
            foreach (DataRow teacher in Query.Execute(Query.TEACHERS())) TeacherCB.Items.Add(teacher[0]);
        }

        /// <summary> Обработка нажатия на "Добавить" </summary>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate;
            string start, end;

            try
            {
                startDate = StartPicker.SelectedDate.Value.Date;
                start = startDate.ToString("yyyy-MM-dd");
                startDate = startDate.AddDays(Convert.ToDouble(DaysTB.Text));
                end = startDate.ToString("yyyy-MM-dd");
            }
            catch (Exception)
            {
                OutputLabel.Content = "Неверный ввод!";
                return;
            }
            OutputLabel.Content = "";
            try
            {
                Query.Execute(Query.INSERT_TEACHERS_COURSES(TeacherCB.SelectedIndex, CourseCB.SelectedIndex, start, end));
            }
            catch (Exception)
            {
                OutputLabel.Content = "Не удалось";
                return;
            }
            OutputLabel.Content = "Успешно!";
        }
    }
}
