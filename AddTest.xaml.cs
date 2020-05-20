using Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Courses
{
    /// <summary> Логика взаимодействия для AddTest.xaml </summary>
    public partial class AddTest : Window
    {
        List<Question> questions = new List<Question>(); //Список вопросов
        int selected = 1; //Выбранный вопрос

        public AddTest()
        {
            InitializeComponent();
        }

        /// <summary> Обработка нажатия на "Очистить" </summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary> Обработка нажатия на "Добавить" </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary> Обработка нажатия на "Готово" </summary>
        private void Done_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary> Обработка нажатия на номер вопроса </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int number = Convert.ToInt32(((MenuItem)sender).Header);
            

        }
    }
}
