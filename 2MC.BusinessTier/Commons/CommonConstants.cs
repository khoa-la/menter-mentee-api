namespace _2MC.BusinessTier.Commons
{
    public class CommonConstants
    {
        public const long MaxUploadFileSize = 25000000;//in bytes
        public const int DefaultPaging = 50;
        public const int LimitPaging = 500;
        
        public class ErrorMessage
        {
            public const string InvalidProduct = "Sản phẩm không tồn tại!";
            public const string InvalidQuantity = "Số lượng không phải kiểu số!";
        }
    }
}