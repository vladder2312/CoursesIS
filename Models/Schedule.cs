using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Courses
{
    class Schedule
    {
        public string Преподаватель { get; set; }
        public string Предмет { get; set; }
        public string Начало { get; set; }
        public string Конец { get; set; }

        public Schedule(string преподаватель, string предмет, string начало, string конец)
        {
            Преподаватель = преподаватель;
            Предмет = предмет;
            Начало = начало;
            Конец = конец;
        }

        public static List<Schedule> Transform(DataRowCollection data)
        {
            var result = new List<Schedule>();
            foreach(DataRow row in data)
            {
                result.Add(new Schedule(row[0].ToString(), row[1].ToString(), DateTime.Parse(row[2].ToString()).ToShortDateString(), DateTime.Parse(row[3].ToString()).ToShortDateString()));
            }
            return result;
        }
    }
}
