using System;
using System.Data;

namespace Courses.Models
{
    class Question
    {
        public string Вопрос { get; set; }
        public string Вариант1 { get; set; }
        public string Вариант2 { get; set; }
        public string Вариант3 { get; set; }
        public string Вариант4 { get; set; }
        public int ПравильныйОтвет { get; set; }

        public Question(string вопрос, string вариант1, string вариант2, string вариант3, string вариант4, int правильныйОтвет)
        {
            Вопрос = вопрос;
            Вариант1 = вариант1;
            Вариант2 = вариант2;
            Вариант3 = вариант3;
            Вариант4 = вариант4;
            ПравильныйОтвет = правильныйОтвет;
        }

        public static Question Transform(DataRow row)
        {
            return new Question(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), Convert.ToInt32(row[5]));
        }
    }
}
