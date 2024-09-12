namespace ViewModel.ViewModels
{
    public class View_SysOption : BaseLogResponseViewModel
    {
        public int pkOption { get; set; }
        public int? ParentID { get; set; }
        public string ParentTitle { get; set; }
        public string ParentDescription { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public bool? isDefault { get; set; }
        public bool isActive { get; set; }
        public bool isDelete { get; set; }
        public bool isLastOption { get; set; }
        public bool isSystemicOption { get; set; }
    }
}
