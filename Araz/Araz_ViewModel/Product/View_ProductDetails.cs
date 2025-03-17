using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Araz_ViewModel
{
    public class View_ProductDetails
    {
        public bool Selected { get; set; }
        public long pkGroup1 { get; set; }
        public long? ParentGroup1 { get; set; }
        public string NameGroup1 { get; set; }
        public long pkGroup2 { get; set; }
        public long? ParentGroup2 { get; set; }
        public string NameGroup2 { get; set; }
        public long pkProductID { get; set; }
        public long? parentProductID { get; set; }
        public string BarCode { get; set; }
        public string ProductName { get; set; }
        public long? fkTypeID { get; set; }
        public string TypeName { get; set; }
        public double? PriceSell { get; set; }
        public double? PriceBuy { get; set; }
        public double? PriceSellOne { get; set; }
        public double? PriceBuyOne { get; set; }
        public double? CountSell { get; set; }
        public double? CountBuy { get; set; }
        public double? AllPriceSell { get; set; }
        public double? AllPriceBuy { get; set; }
        public double? Count { get; set; }
        public double? CountOne { get; set; }
        public long? pkPriceID { get; set; }
        public string Invoice { get; set; }
        public long pkPersonID { get; set; }
        public string PersonName { get; set; }                   //-----نام                              
        public string PersonLastName { get; set; }               //-----نام خانوادگی                            
        public string FullName { get; set; }                     //-----نام و نام خانوادگی                            
        public string Sex { get; set; }                          //-----جنسیت                             
        public DateTime AgeDate { get; set; }                    //-----تاریخ تولد                                
        public int? PersonAge { get; set; }                      //-----سن                            
        public long? fkEducationID { get; set; }                 //-----اف کا تحصیلات                           
        public string Education { get; set; }                   //-----تحصیلات                             
        public string Mobile { get; set; }                       //-----موبایل                         
        public string Tel { get; set; }                          //-----تلفن                           
        public string NationalCode { get; set; }                 //-----کد ملی                         
        public string Email { get; set; }                        //-----ایمیل                          
        public string Password { get; set; }                     //-----رمز عبور                       
        public long? fkCityID { get; set; }                      //-----اف کا شهر                      
        public long? fkProvinceID { get; set; }                  //-----اف کا استان                    
        public string Province { get; set; }                     //-----استان                          
        public string CityName { get; set; }                     //-----شهر                            
        public string Address { get; set; }                      //-----آدرس                           
        public string PostalCode { get; set; }                   //-----کد پستی                        
        public long? fkRoleID { get; set; }                      //-----آی دی سمت                      
        public string RoleName { get; set; }                     //-----سمت                            
        public DateTime MiladiDate { get; set; }                 // تاریخ میلادی                          
        public string ShamsiDate { get; set; }                   //تاریخ شمسی                       
        public long pkInvoiceBuyID { get; set; }
        public long fkFinnantialYear { get; set; }
        public long FinnantialYear { get; set; }
        public string InvoiceBuyNumber { get; set; }
        public string PurchaseInvoiceNumber { get; set; }
        public string ComputedInvoiceBuyNumber { get; set; }
        public long fkPersonID { get; set; }
        public long fkProductID { get; set; }
        public long fkPrice { get; set; }
        public double? BuyInvoice { get; set; }
        public double? Buyquantity { get; set; }
        public string Description { get; set; }
        public int percentdiscount { get; set; }
        public double? discountamount { get; set; }
        public long pkInvoiceSellID { get; set; }
        public long fkFinnantialYearSell { get; set; }
        public long FinnantialYearSell { get; set; }
        public string InvoiceSellNumber { get; set; }
        public string PurchaseInvoiceNumberSell { get; set; }
        public string ComputedInvoiceSellNumberSell { get; set; }
        public long fkPersonIDSell { get; set; }
        public long fkProductIDSell { get; set; }
        public long fkPriceSell { get; set; }
        public double? SellInvoice { get; set; }
        public double? Sellquantity { get; set; }
        public string DescriptionSell { get; set; }
        public int percentdiscountSell { get; set; }
        public double? discountamountSell { get; set; }

    }
}
