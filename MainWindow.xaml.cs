﻿using Courses.Models;
using System.Data;
using System.Windows;

namespace Courses
{
    /// <summary> Логика взаимодействия для MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Query.sqlConnection.Open();
        }

        /// <summary> Обработка нажатия на Курсы->Добавить </summary>
        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            Window addCourse = new AddCourse(); // Создание окна для добавления курса
            addCourse.Show(); // Открытие окна
        }

        /// <summary> Обработка нажатия на Преподаватели->Добавить </summary>
        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            Window addTeacher = new AddTeacher();
            addTeacher.Show();
        }

        /// <summary> Обработка нажатия на Преподаватели->Назначить курс </summary>
        private void TeacherToCourse_Click(object sender, RoutedEventArgs e)
        {
            Window addTeacherToCourse = new AddTeachersCourses();
            addTeacherToCourse.Show();
        }

        /// <summary> Обработка нажатия на Тестирование->Создать </summary>
        private void CreateTest_Click(object sender, RoutedEventArgs e)
        {
            Window addTest = new AddTest();
            addTest.Show();
        }

        /// <summary> Обработка нажатия на Тестирование->Запустить </summary>
        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            Window testing = new Testing();
            testing.Show();
        }

        /// <summary> Обработка нажатия на Тестирование->Результаты </summary>
        private void ShowResultsOfTests_Click(object sender, RoutedEventArgs e)
        {
            Table.ItemsSource = TestResults.Transform(Query.Execute(Query.TEST_RESULTS()));
        }

        /// <summary> Обработка нажатия на "Прайс-лист организации" </summary>
        private void PriceList_Click(object sender, RoutedEventArgs e)
        {
            PriceListGrid.Visibility = Visibility.Visible;
            ScheduleGrid.Visibility = Visibility.Hidden;
            FindCourseGrid.Visibility = Visibility.Hidden;
            LoadOrganizations();
        }

        /// <summary> Обработка нажатия на "Расписание преподавателя" </summary>
        private void TeacherSchedule_Click(object sender, RoutedEventArgs e)
        {
            PriceListGrid.Visibility = Visibility.Hidden;
            ScheduleGrid.Visibility = Visibility.Visible;
            FindCourseGrid.Visibility = Visibility.Hidden;
            LoadTeachers();
        }

        /// <summary> Обработка нажатия на "Найти курс" </summary>
        private void FindCourse_Click(object sender, RoutedEventArgs e)
        {
            PriceListGrid.Visibility = Visibility.Hidden;
            ScheduleGrid.Visibility = Visibility.Hidden;
            FindCourseGrid.Visibility = Visibility.Visible;
            LoadCourses();
        }

        /// <summary> Обработка нажатия на "Прайс-лист -> Выбрать" </summary>
        private void Submit_PriceList_Click(object sender, RoutedEventArgs e)
        {
            PriceListGrid.Visibility = Visibility.Hidden;
            DataRowCollection data = Query.Execute(Query.PRICE_LIST(PriceListOrgCB.SelectedIndex,
                                                                   PriceListDatePicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd"),
                                                                   PriceListDatePicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd")));
            if (data.Count == 0)
            {
                MessageBox.Show("Не найдено", "Сообщение");
                return;
            }
            Table.ItemsSource = PriceList.Transform(data);
        }

        /// <summary> Обработка нажатия на "Найти курс -> Выбрать" </summary>
        private void Submit_FindCourse_Click(object sender, RoutedEventArgs e)
        {
            FindCourseGrid.Visibility = Visibility.Hidden;
            DataRowCollection data = Query.Execute(Query.FIND_COURSE(FindDateStartCB.SelectedDate.Value.Date.ToString("yyyy-MM-dd"),
                                                                    FindDateEndCB.SelectedDate.Value.Date.ToString("yyyy-MM-dd"),
                                                                    CourseCB.Text));
            if (data.Count == 0)
            {
                MessageBox.Show("Не найдено", "Сообщение");
                return;
            }
            Table.ItemsSource = Course.Transform(data);
        }

        /// <summary> Обработка нажатия на "Расписание -> Выбрать" </summary>
        private void Submit_Schedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleGrid.Visibility = Visibility.Hidden;
            DataRowCollection data = Query.Execute(Query.SCHEDULE(ScheduleTeacherCB.Text,
                                                                 ScheduleStartPicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd"),
                                                                 ScheduleEndPicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd")));
            if (data.Count == 0)
            {
                MessageBox.Show("Не найдено", "Сообщение");
                return;
            }
            Table.ItemsSource = Schedule.Transform(data);
        }

        /// <summary> Загрузка организаций из БД </summary>
        private void LoadOrganizations()
        {
            PriceListOrgCB.Items.Clear();
            foreach (DataRow row in Query.Execute(Query.ORGANIZATIONS())) PriceListOrgCB.Items.Add(row[0]);
        }

        /// <summary> Загрузка преподавателей из БД </summary>
        private void LoadTeachers()
        {
            ScheduleTeacherCB.Items.Clear();
            foreach (DataRow row in Query.Execute(Query.TEACHERS())) ScheduleTeacherCB.Items.Add(row[0]);
        }

        /// <summary> Загрузка курсов из БД </summary>
        private void LoadCourses()
        {
            CourseCB.Items.Clear();
            foreach (DataRow row in Query.Execute(Query.SUBJECTS())) CourseCB.Items.Add(row[0]);
        }

    }
}
