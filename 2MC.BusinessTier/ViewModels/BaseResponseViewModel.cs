namespace _2MC.BusinessTier.ViewModels
{
    public class BaseResponseViewModel<T>
    {
        public StatusModel Status { get; set; }
        public T Data { get; set; }
    }

    public class StatusModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
}