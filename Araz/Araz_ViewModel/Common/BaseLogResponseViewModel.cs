using System;

namespace ViewModel.ViewModels
{
    public class BaseLogResponseViewModel
    {
        public BaseLogResponseViewModel()
        {
            mod = 4;
        }

        public int? mod { get; set; }
        public long? AshVcCode { get; set; }
        public long? UsrVcCode { get; set; }
        public string EditToken { get; set; }
        public long? InsertUser { get; set; }
        public string InsertUserFullName { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? InsertDateP { get; set; }
        public string InsertIP { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? BeginDateP { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? CreateDateP { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EndDateP { get; set; }
    }
}
