using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Enums
    {
        public enum Roles
        {
            Admin = 1,
            ExportDataToExcel = 2
        }

        public enum CrudMod   // نوع عملیات در دیتابیس
        {
            Create = 1,
            Edit = 2,
            Delete = 3,
            CheckCondition = 4,
            Select = 5

        }

        public enum AshkhasTozihatType   // نوع عملیات در دیتابیس
        {
            //20	NULL    توضیحات اشخاص
            //21	20	تلفن دفتر
            //22	20	تلفن منزل
            //23	20	تلفن کارگاه
            //24	20	آدرس منزل
            //25	20	آدرس کارخانه
            //26	20	آدرس دفتر
            //27	20	همراه
            //28	20	شماره حساب
            //29	20	شرح
            //30	20	ایمیل
            //31	20	فکس
            //32	20	وبسایت

            ParentID = 20,
            AccountNo = 28

        }
        public enum OptionLeadStatus // وضعیت سرنخ
        {
            ParentID = 114,
            New = 115, // جدید
            SendToFinancialApproval = 116, // در انتظار مدیر مالی 
            SendToSalesApproval = 117, // در انتظار مدیر فروش
            ApproveFinancial = 118, // مدیر مالی تائید کرده است
            RejectFinancial = 119, // مدیر مالی رد کرده است
            ApproveSales = 120, // مدیر فروش تائید کرده است
            RejectSales = 121, // مدیر فروش رد کرده است
            Close = 122, // بسته شده
            CloseSuccess = 123, // بسته شده موفق
            CloseError = 124 // بسته شده نا موفق
        }

        public enum OptionCurrencyType // واحد های پول
        {
            ParentID = 100,
            IRR = 101, // ریال
            TMN = 102, // تومان
            USD = 103, // دلار
            AED = 104, // درهم
            CNY = 105, // یوان
            TRY = 106, // لیر
            EUR = 107, // یورو
            IQD = 108, // دینار
            GBP = 109  // پوند
        }


        public enum OptionConnectionType  // نوع ارتباط و آشنایی با مشتری
        {
            ParentID = 90,
            Exhibition = 1, // نمایشگاه
            CompanyInstagram = 2, // اینستاگرام شرکت
            CompanyWebsite = 3, // وبسایت شرکت
            UrbanAdvertisingCompany = 4, // تبلیغات شهری شرکت
            AnotherCustomer = 5, // مشتری دیگر
            CompanyYouTube = 6, // یوتیوب شرکت
            Other = 7 // غیره

        }

        public enum OptionInsertFrom  // ثبت از محل
        {
            ParentID = 0,
            LeadForm = 1,
            CustomerDefine = 2,
            Other = 3

        }

        public enum OptionJobPosition // واحد های شغلی
        {
            ParentID = 2
        }

        public enum OptionJobTitle // سمت های شغلی
        {
            ParentID = 5
        }

        public enum OptionEducational // مدرک تحصیلی
        {
            ParentID = 7
        }

        public enum OptionSex // جنسیت
        {
            ParentID = 13
        }

        public enum OptionJobBuilding // ساختمان های شرکت
        {
            ParentID = 55
        }

        public enum OptionFinancialYear // سال مالی
        {
            ParentID = 27,
            Y1395 = 29,
            Y1396 = 30,
            Y1397 = 31,
            Y1398 = 32,
            Y1399 = 33,
            Y1400 = 34,
            Y1401 = 35,
            Y1402 = 36

        }

        public enum OptionProductType // نوع کالا
        {
            ParentID = 120,
            Rol = 121,
            Sinusi = 122
        }

        public enum OptionValueType // نوع مقدار
        {
            ParentID = 44,
            Weight = 45,
            Length = 49,
            String = 53,
            Other = 98
        }

        public enum OptionValueUnit //نوع واحد مقدار
        {
            String = 54,
            Gram = 46,
            Kilogram = 47,
            Ton = 48,
            Millimeter = 50,
            Centimeter = 51,
            Meter = 52
        }


        public enum OptionTicketUnit //  واحد های مربوطه جهت ثبت تیکت
        {
            ParentID = 58
        }

        public enum OptionTicketCategory
        {
            ParentID = -1
        }

        public enum OptionTicketStatus
        {
            ParentID = 74,
            New = 1,
            Close = 1,
            NewAnswered = 1,
        }

        public enum TypeProcess
        {
            Coefficient = 1, // ضریب
            Static = 3
        }

        public enum OptionPriority
        {
            ParentID = 110,
            Low = 111,
            Medium = 112,
            High = 113,
        }

        public enum OptionJobCategory //  لیست گروه های فعالیت های مشتریان
        {
            ParentID = 84
        }

        public enum EvidenceType //  انواع سند
        {
            OpeneningFinancialYear = 1,     // افتتاحیه سال مالی
            ClosingFinancialYear = 2,       // اختتامیه سال مالی
            InputAssignment = 3,            // تخصیص ورودی
            InputAssignmentWaybill = 4,     // بارنامه تخصیص ورودی
            InputAssignmentProduct = 5,     // اقلام تخصیص ورودی
            OutputAssignment = 6,           // تخصیص خروجی
            OutputAssignmentWaybill = 7,    // بارنامه تخصیص خروجی
            OutputAssignmentProduct = 8,    // اقلام تخصیص خروجی
            InputEvidence = 9,              // ورودی انبار
            OutputEvidence = 10,            // خروجی انبار
            ProductSeperation = 11,        // تفکیک کالا
            ProductAggregate = 12           // تحمیع کالا
        }

        public enum CostInputEvidence
        {
            ParentID = 110,        // انواع هزینه های ورودی انبار
            Keraye = 111,          // کرایه
            Reshve = 112,          // رشوه
            Other = 113,           // متفرقه
        }

        public enum EvidenceStatus
        {
            //            ارسال شده جهت تائید
            //باز
            //بسته
            //تحویل گرفته شده
            //در انتظار تحویل
            ParentID = 70,
            New = 71,
            Modify = 72,
            Approved = 73,
            Canceled = 74,
            SendToApproval = 75,
            Open = 76,
            Closed = 77,
            Deliverd = 78,
            PendToDeliver = 79
        }



        public static int GetFinancialYearNumber(int year)
        {
            var financialYears = new Dictionary<int, int>
            {
                 { 1403, 1 },
                 { 1404, 2 },
                 { 1405, 3 },
                 { 1406, 4 },
                 { 1407, 5 },
                 { 1408, 6 },
                 { 1409, 7 },
                 { 1410, 8 },
                 { 1411, 9 },
                 { 1412, 10 },
                 { 1413, 11 },
                 { 1414, 12 },
                 { 1415, 13 },
                 { 1416, 14 },
                 { 1417, 15 },
                 { 1418, 16 },
                 { 1419, 17 },
                 { 1420, 18 },
                 { 1421, 19 }
            };
            return financialYears.ContainsKey(year) ? financialYears[year] : -1;
        }
    }

}
