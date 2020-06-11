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
        public string Цена_руб { get; set; }
        public string Цена_с_НДС_руб { get; set; }
        public string Начало { get; set; }
        public string Конец { get; set; }

        public PriceList(string organization, string subject, string ammount, string price, string priceWithNDS, string dateStart, string dateEnd)
        {
            Организация = organization;
            Предмет = subject;
            Дней = ammount;
            Цена_руб = price;
            Цена_с_НДС_руб = priceWithNDS;
            Начало = dateStart;
            Конец = dateEnd;
        }

        public static List<PriceList> Transform(DataRowCollection data)
        {
            var result = new List<PriceList>();
            foreach(DataRow row in data)
            {
                result.Add(new PriceList(row[0].ToString(),
                    row[1].ToString(), row[2].ToString(),
                    Math.Round(Convert.ToDouble(row[3]), 2).ToString(),
                    Math.Round(Convert.ToDouble(row[4]), 2).ToString(),
                    DateTime.Parse(row[5].ToString()).ToShortDateString(),
                    DateTime.Parse(row[6].ToString()).ToShortDateString()
                    ));
            }
            return result;
        }
    }
}
