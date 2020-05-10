using Courses.Models;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Courses
{
    /// <summary> Логика взаимодействия для MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {

        /// <summary> Подключение к базе данных </summary>
        public static SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database.mdf;Integrated Security=True");

        public MainWindow()
        {
            InitializeComponent();
            sqlConnection.Open();
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

        /// <summary> Обработка нажатия на Тестирование->Запустить </summary>
        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            Window testing = new Testing();
            testing.Show();
        }

        /// <summary> Обработка нажатия на Тестирование->Результаты </summary>
        private void ShowResultsOfTests_Click(object sender, RoutedEventArgs e)
        {

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

        /// <summary> Обработка нажатия на "Найти курс" </summary>
        private void Submit_PriceList_Click(object sender, RoutedEventArgs e)
        {
            PriceListGrid.Visibility = Visibility.Hidden;
            string sqlCommand = "Select Organizations.Name, Subjects.Name, Courses.Duration, Prices.Price, Prices.PriceWithNDS, StartDate, EndDate From DocTeachersCourses " +
                                "Inner Join Courses on DocTeachersCourses.Id_course = Courses.Id " +
                                "Inner Join Subjects on Courses.Id_subject = Subjects.id " +
                                "Inner Join DocPricesCourses on DocPricesCourses.Id_course = Courses.Id " +
                                "Inner Join Organizations on Courses.Id_organization = Organizations.Id " +
                                "Inner Join Prices on DocPricesCourses.Id_price = Prices.Id " +
                                "Where DocTeachersCourses.Id_course in (Select Id From Courses Where Id_organization = " + PriceListOrgCB.SelectedIndex + ") " +
                                "and DocTeachersCourses.StartDate<'" + PriceListDatePicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd") + 
                                "' and DocTeachersCourses.EndDate>'" + PriceListDatePicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd") + "'";
            DataRowCollection data = Query(sqlCommand);
            if (data.Count == 0)
            {
                MessageBox.Show("Не найдено", "Сообщение");
                return;
            }
            Table.ItemsSource = PriceList.Transform(data);
        }

        /// <summary> Обработка нажатия на "Найти курс" </summary>
        private void Submit_FindCourse_Click(object sender, RoutedEventArgs e)
        {
            FindCourseGrid.Visibility = Visibility.Hidden;
            string sqlCommand = "Select Subjects.Name, Courses.Ammount, Requests.Ammount, DocTeachersCourses.StartDate, DocTeachersCourses.EndDate From Courses " +
                "Right Join Requests on Courses.Id = Requests.Id_course " +
                "Inner Join Subjects on Courses.Id_subject = Subjects.Id " +
                "Inner Join DocTeachersCourses on Courses.Id = DocTeachersCourses.Id_course " +
                "Where DocTeachersCourses.StartDate > '" + FindDateStartCB.SelectedDate.Value.Date.ToString("yyyy-MM-dd") +
                "' and DocTeachersCourses.EndDate < '" + FindDateEndCB.SelectedDate.Value.Date.ToString("yyyy-MM-dd") +
                "' and Subjects.Name = N'" + CourseCB.Text + "'";
            DataRowCollection data = Query(sqlCommand);
            if (data.Count == 0)
            {
                MessageBox.Show("Не найдено", "Сообщение");
                return;
            }
            Table.ItemsSource = Course.Transform(data);
        }

        /// <summary> Обработка нажатия на "Найти курс" </summary>
        private void Submit_Schedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleGrid.Visibility = Visibility.Hidden;
            string sqlCommand = "Select Teachers.Fio, Subjects.Name, DocTeachersCourses.StartDate, DocTeachersCourses.EndDate From Teachers " +
                "Inner Join DocTeachersCourses on DocTeachersCourses.Id_teacher = Teachers.Id " +
                "Inner Join Courses on DocTeachersCourses.Id_course = Courses.Id " +
                "Inner Join Subjects on Courses.Id_subject = Subjects.Id " +
                "Where Teachers.Fio = N'" + ScheduleTeacherCB.Text + "' and DocTeachersCourses.StartDate>'" + ScheduleStartPicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd") + "' and DocTeachersCourses.EndDate<'" + ScheduleEndPicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd") + "'";
            DataRowCollection data = Query(sqlCommand);
            if (data.Count == 0)
            {
                MessageBox.Show("Не найдено", "Сообщение");
                return;
            }
            Table.ItemsSource = Schedule.Transform(data);
        }

        /// <summary> SQL запрос </summary>
        public static DataRowCollection Query(string command)
        {
            DataSet list = new DataSet("Table");
            DataTable dataTable = new DataTable();
            if (command.ToLower().Contains("select"))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command, sqlConnection);
                adapter.Fill(list, "Table");
                dataTable = list.Tables["Table"];
            }
            if (command.ToLower().Contains("insert"))
            {
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.ExecuteNonQuery();
            }
            return dataTable.Rows;
        }

        /// <summary> Загрузка организаций из БД </summary>
        private void LoadOrganizations()
        {
            PriceListOrgCB.Items.Clear();
            foreach (DataRow row in Query("Select Name From Organizations")) PriceListOrgCB.Items.Add(row[0]);
        }

        /// <summary> Загрузка преподавателей из БД </summary>
        private void LoadTeachers()
        {
            ScheduleTeacherCB.Items.Clear();
            foreach (DataRow row in Query("Select Fio From Teachers")) ScheduleTeacherCB.Items.Add(row[0]);
        }

        /// <summary> Загрузка курсов из БД </summary>
        private void LoadCourses()
        {
            CourseCB.Items.Clear();
            foreach (DataRow row in Query("Select Name From Subjects")) CourseCB.Items.Add(row[0]);
        }
    }
}
