using System;
using ViewModel.ViewModels;

namespace Araz_ViewModel
{
    public class View_Person :BaseLogResponseViewModel
    {
        public long? pkPersonID { get; set; }                    //-----آی دی مشتری
        public string PersonName { get; set; }                   //-----نام 
        public string PersonLastName { get; set; }               //-----نام خانوادگی
        public string FullName { get; set; }                     //-----نام و نام خانوادگی
        public string Sex { get; set; }                            //-----جنسیت
        public int? PersonAge { get; set; }                      //-----سن
        public long? fkEducationID { get; set; }                //-----اف کا تحصیلات
        public string Education {  get; set; }                   //-----تحصیلات
        public string Mobile { get; set; }                       //-----موبایل
        public string Tel { get; set; }                          //-----تلفن
        public string NationalCode { get; set; }                 //-----کد ملی
        public string Email { get; set; }                        //-----ایمیل
        public long? fkCityID { get; set; }                      //-----اف کا شهر 
        public long? fkProvinceID { get; set; }                  //-----اف کا استان
        public string CityName { get; set; }                     //-----استان و شهر
        public string Address { get; set; }                      //-----آدرس
        public string PostalCode { get; set; }                   //-----کد پستی
        public long? fkRoleID { get; set; }                      //-----آی دی سمت
        public string PersonRole { get; set; }                   //-----سمت


    }
}