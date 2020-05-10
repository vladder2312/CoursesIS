using System;
using System.Collections.Generic;
using System.Data;

namespace Courses.Models
{
    class TestResults
    {
        public string Ученик { get; set; }
        public string Предмет { get; set; }
        public string Тест { get; set; }
        public string Балл { get; set; }

        public TestResults(string ученик, string предмет, string тест, string балл)
        {
            Ученик = ученик;
            Предмет = предмет;
            Тест = тест;
            Балл = балл;
        }

        public static List<TestResults> Transform(DataRowCollection data)
        {
            var result = new List<TestResults>();
            foreach(DataRow row in data)
            {
                result.Add(new TestResults(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString()));
            }
            return result;
        }
    }
}
