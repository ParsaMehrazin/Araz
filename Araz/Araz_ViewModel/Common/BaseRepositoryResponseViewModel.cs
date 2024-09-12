using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class BaseRepositoryResponseViewModel : BaseResponseViewModel
    {
        public long Result { get; set; }
        public string msg { get; set; }
    }

    public class BaseResponseViewModel
    {
        public BaseResponseViewModel()
        {
            ResponseMessages = new List<ResponseMessage>();
        }
        public bool Success { get; set; }
        public List<ResponseMessage> ResponseMessages { get; set; }
    }
    public class ResponseMessage
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }


}
