using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses
{
    class Query
    {
        public static string TEACHERS()
        {
            return "Select Fio From Teachers";
        }

        public static string SUBJECTS()
        {
            return "Select Name From Subjects";
        }

        public static string ORGANIZATIONS()
        {
            return "Select Name From Organizations";
        }

        public static string PRICES()
        {
            return "Select Price From Prices";
        }

        public static string PRICE_LIST(int OrganizationId, string StartDate, string EndDate)
        {
            return "Select Organizations.Name, Subjects.Name, Courses.Duration, Prices.Price, Prices.PriceWithNDS, StartDate, EndDate From DocTeachersCourses " +
                   "Inner Join Courses on DocTeachersCourses.Id_course = Courses.Id " +
                   "Inner Join Subjects on Courses.Id_subject = Subjects.id " +
                   "Inner Join DocPricesCourses on DocPricesCourses.Id_course = Courses.Id " +
                   "Inner Join Organizations on Courses.Id_organization = Organizations.Id " +
                   "Inner Join Prices on DocPricesCourses.Id_price = Prices.Id " +
                   "Where DocTeachersCourses.Id_course in (Select Id From Courses Where Id_organization = " + OrganizationId + ") " +
                   "and DocTeachersCourses.StartDate<'" + StartDate +
                   "' and DocTeachersCourses.EndDate>'" + EndDate + "'";
        }

        public static string FIND_COURSE(string StartDate, string EndDate, string Subject)
        {
            return "Select Subjects.Name, Courses.Ammount, Requests.Ammount, DocTeachersCourses.StartDate, DocTeachersCourses.EndDate From Courses " +
                   "Right Join Requests on Courses.Id = Requests.Id_course " +
                   "Inner Join Subjects on Courses.Id_subject = Subjects.Id " +
                   "Inner Join DocTeachersCourses on Courses.Id = DocTeachersCourses.Id_course " +
                   "Where DocTeachersCourses.StartDate > '" + StartDate +
                   "' and DocTeachersCourses.EndDate < '" + EndDate +
                   "' and Subjects.Name = N'" + Subject + "'";
        }

        public static string SCHEDULE(string Teacher, string StartDate, string EndDate)
        {
            return "Select Teachers.Fio, Subjects.Name, DocTeachersCourses.StartDate, DocTeachersCourses.EndDate From Teachers " +
                   "Inner Join DocTeachersCourses on DocTeachersCourses.Id_teacher = Teachers.Id " +
                   "Inner Join Courses on DocTeachersCourses.Id_course = Courses.Id " +
                   "Inner Join Subjects on Courses.Id_subject = Subjects.Id " +
                   "Where Teachers.Fio = N'" + Teacher +
                   "' and DocTeachersCourses.StartDate>'" + StartDate +
                   "' and DocTeachersCourses.EndDate<'" + EndDate + "'";
        }

        public static string STUDENTS_ON_COURSE(string Course)
        {
            return "Select Fio From Students " +
                   "Inner Join Requests on Students.Id_request = Requests.Id " +
                   "Inner Join Courses on Requests.Id_course = Courses.Id " +
                   "Inner Join Subjects on Courses.Id_subject = Subjects.Id " +
                   "Where Subjects.Name = N'" + Course + "'";
        }

        public static string TESTS_ON_COURSE(string Course)
        {
            return "Select Tests.Name From Tests " +
                   "Inner Join Subjects on Tests.Id_subject = Subjects.Id " +
                   "Where Subjects.Name = N'" + Course + "'";
        }

        public static string TEST(string Name)
        {
            return "Select Title, Variant1, Variant2, Variant3, Variant4, Answer From Questions " +
                   "Inner Join Tests on Questions.Id_test = Tests.Id " +
                   "Where Tests.Name = N'" + Name + "'";
        }

        public static string INSERT_COURSES(int Id, int IdOrganization, int IdSubject, string Duration, string Ammount)
        {
            return "Insert into Courses(Id, Id_organization, Id_subject, Duration, Ammount) Values(" +
                    Id + ", " + IdOrganization + ", " + IdSubject + ", " + Duration + ", " + Ammount + ")";
        }

        public static string INSERT_PRICES_COURSES(int IdPrice, int IdCourse, string Date)
        {
            return "Insert into DocPricesCourses(Id_price, Id_course, Date) Values(" +
                    + IdPrice + ", " + IdCourse + ", '" + Date + "')";
        }

        public static string INSERT_TEACHERS_COURSES(int IdTeacher, int IdCourse, string StartDate, string EndDate)
        {
            return "Insert into DocTeachersCourses(Id_teacher, Id_course, StartDate, EndDate) Values(" +
                    + IdTeacher + ", " + IdCourse + ", '" + StartDate + "', '" + EndDate + "')";
        }
    }
}
