using System;

namespace Araz_ViewModel
{
    public class View_Person
    {
        public long? pkPersonID { get; set; }               //-----آی دی مشتری
        public string PersonName { get; set; }              //-----نام 
        public string PersonFamily { get; set; }            //-----نام خانوادگی
        public string FullName { get; set; }                //-----نام و نام خانوادگی
        public int? PersonAge { get; set; }                 //-----سن
        public string Education { get; set; }              //-----تحصیلات
        public string Mobile { get; set; }                 //-----موبایل
        public string Tel { get; set; }                    //-----تلفن
        public string Codemeli { get; set; }                //-----کد ملی
        public string Email { get; set; }                   //-----ایمیل
        public string Address { get; set; }                 //-----آدرس
        public string PostalCode { get; set; }              //-----کد پستی
        public string fkRoleID { get; set; }                //-----آی دی سمت
        public string PersonRole { get; set; }             //-----سمت

    }
}