using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Courses
{
    /// <summary> Логика взаимодействия для AddTeacher.xaml </summary>
    public partial class AddTeacher : Window
    {
        public AddTeacher()
        {
            InitializeComponent();
            LoadCategories();
        }

        /// <summary> Загрузка категорий преподавателей из БД </summary>
        private void LoadCategories()
        {
            foreach (DataRow category in MainWindow.ExecuteQuery(Query.CATEGORIES())) CategoryCB.Items.Add(category[0].ToString());
        }

        /// <summary> Обработка нажатия на "Добавить" </summary>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            int categoryId;
            string birth, fio, gender, education;
            try
            {
                categoryId = CategoryCB.SelectedIndex;
                birth = BirthPicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                fio = FioTB.Text;
                gender = GenderTB.Text;
                education = EducationTB.Text;
            } catch (Exception)
            {
                OutputLabel.Content = "Неверный ввод!";
                return;
            }
            try
            {
                MainWindow.ExecuteQuery(Query.INSERT_TEACHERS(categoryId, fio, birth, gender, education));
            } catch (Exception)
            {
                OutputLabel.Content = "Ошибка при добавлении";
                return;
            }
            OutputLabel.Content = "Успешно добавлен!";
        }
    }
}
