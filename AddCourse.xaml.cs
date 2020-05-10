using System;
using System.Data;
using System.Windows;

namespace Courses
{
    /// <summary> Логика взаимодействия для AddCourse.xaml </summary>
    public partial class AddCourse : Window
    {
        /// <summary> ID записи </summary>
        private Int32 id; 

        public AddCourse()
        {
            InitializeComponent();

            DataRowCollection data = MainWindow.ExecuteQuery("Select Name From Organizations"); //Загрузка организаций
            foreach (DataRow cat in data) OrgPicker.Items.Add(cat.ItemArray[0]);

            data = MainWindow.ExecuteQuery("Select Fio From Teachers"); //Загрузка преподавателей
            foreach (DataRow teacher in data) TeachPicker.Items.Add(teacher.ItemArray[0]);

            data = MainWindow.ExecuteQuery("Select Price From Prices"); //Загрузка цен
            foreach (DataRow price in data) PriceCB.Items.Add(price.ItemArray[0]);

            data = MainWindow.ExecuteQuery("Select Name From Subjects"); //Загрузка предметов
            foreach (DataRow subject in data) SubjectCB.Items.Add(subject.ItemArray[0]);
        }

        /// <summary> Обработка нажатия на "Добавить" </summary>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DateTime startdate = DatePicker.SelectedDate.Value.Date;
            string start = startdate.ToString("yyyy-MM-dd"); //Приведение даты начала к строке
            startdate = startdate.AddDays(Convert.ToDouble(AmmountTB.Text)); //Добавление дней к начальной дате
            string end = startdate.ToString("yyyy-MM-dd");  //Преобразование даты к строке
            errMessage.Content = "";
            try
            {
                DataRowCollection data = MainWindow.ExecuteQuery("Select Max(Id) From Courses"); //Получение максимального ID в таблице
                id = Convert.ToInt32(data[0].ItemArray[0]) + 1; //Добавление к ID единицы, это ID будущей записи
                MainWindow.ExecuteQuery("Insert into Courses(Id, Id_organization, Id_subject, Duration, Ammount) Values(" +
                    id+", "+OrgPicker.SelectedIndex + ", " + SubjectCB.SelectedIndex + ", " + DatePicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd") + ", " + AmmountTB.Text + ")"); //Добавление записи в Courses
                MainWindow.ExecuteQuery("Insert into DocPricesCourses(Id_price, Id_course, Date) Values(" +
                    +PriceCB.SelectedIndex + ", " + id + ", '" + start + "')"); //Добавление записи в DocPricesCourses
                MainWindow.ExecuteQuery("Insert into DocTeachersCourses(Id_teacher, Id_course, StartDate, EndDate) Values(" +
                    +TeachPicker.SelectedIndex + ", " + id + ", '" + start + "', '" + end + "')"); //Добавление записи в DocTeachersCourses
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errMessage.Content = "Ошибка при записи";
                return;
            }
            errMessage.Content = "Курс успешно создан!";
        }
    }
}
