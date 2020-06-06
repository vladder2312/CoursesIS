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

            DataRowCollection data = Query.Execute(Query.ORGANIZATIONS()); //Загрузка организаций
            foreach (DataRow cat in data) OrgPicker.Items.Add(cat.ItemArray[0]);

            data = Query.Execute(Query.TEACHERS()); //Загрузка преподавателей
            foreach (DataRow teacher in data) TeachPicker.Items.Add(teacher.ItemArray[0]);

            data = Query.Execute(Query.PRICES()); //Загрузка цен
            foreach (DataRow price in data) PriceCB.Items.Add(price.ItemArray[0]);

            data = Query.Execute(Query.SUBJECTS()); //Загрузка предметов
            foreach (DataRow subject in data) SubjectCB.Items.Add(subject.ItemArray[0]);
        }

        /// <summary> Обработка нажатия на "Добавить" </summary>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string start, end;
            DateTime startdate;
            try
            {
                startdate = DatePicker.SelectedDate.Value.Date;
                start = startdate.ToString("yyyy-MM-dd"); //Приведение даты начала к строке
                startdate = startdate.AddDays(Convert.ToDouble(DaysTB.Text)); //Добавление дней к начальной дате
                end = startdate.ToString("yyyy-MM-dd");  //Преобразование даты к строке
            } catch (Exception)
            {
                errMessage.Content = "Неверный ввод!";
                return;
            }
            errMessage.Content = "";
            try
            {
                DataRowCollection data = Query.Execute("Select Max(Id) From Courses"); //Получение максимального ID в таблице
                id = Convert.ToInt32(data[0].ItemArray[0]) + 1; //Добавление к ID единицы, это ID будущей записи
                Query.Execute(Query.INSERT_COURSES(id, OrgPicker.SelectedIndex, SubjectCB.SelectedIndex, DaysTB.Text, AmmountTB.Text)); //Добавление записи в Courses
                Query.Execute(Query.INSERT_PRICES_COURSES(PriceCB.SelectedIndex, id, start)); //Добавление записи в DocPricesCourses
                Query.Execute(Query.INSERT_TEACHERS_COURSES(TeachPicker.SelectedIndex, id, start, end)); //Добавление записи в DocTeachersCourses
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
