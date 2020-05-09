using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses
{
    class PriceList
    {
        public string Организация { get; set; }
        public string Предмет { get; set; }
        public string Дней { get; set; }
        public string Цена { get; set; }
        public string Цена_с_НДС { get; set; }
        public string Начало { get; set; }
        public string Конец { get; set; }

        public PriceList(string organization, string course, string ammount, string subject, string priceWithNDS, string dateStart, string dateEnd)
        {
            Организация = organization;
            Предмет = course;
            Дней = ammount;
            Цена = subject;
            Цена_с_НДС = priceWithNDS;
            Начало = dateStart;
            Конец = dateEnd;
        }

        public static List<PriceList> Transform(DataRowCollection data)
        {
            var result = new List<PriceList>();
            foreach(DataRow row in data)
            {
                result.Add(new PriceList(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString()));
            }
            return result;
        }
    }
}
