using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    class Course
    {
        public string Предмет { get; set; }
        public string Мест_на_курсе { get; set; }
        public string Учащихся { get; set; }
        public string Начало { get; set; }
        public string Конец { get; set; }
        public string Заполнен { get; set; }

        public Course(string предмет, string мест_на_курсе, string учащихся, string начало, string конец, string заполнен)
        {
            Предмет = предмет;
            Мест_на_курсе = мест_на_курсе;
            Учащихся = учащихся;
            Начало = начало;
            Конец = конец;
            Заполнен = заполнен;
        }

        public static List<Course> Transform(DataRowCollection data)
        {
            var result = new List<Course>();
            foreach(DataRow row in data)
            {
                var full = row[1].Equals(row[2])?"Да":"Нет";
                result.Add(new Course(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), full));
            }
            return result;
        }
    }
}
