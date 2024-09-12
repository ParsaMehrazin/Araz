namespace ViewModel.ViewModels
{
    public class View_SysSetting : BaseLogResponseViewModel
    {
        public int pkSetting { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool isEditable { get; set; }
        public bool ShowInForm { get; set; }
    }
}
