using System.Collections.Generic;

namespace _2MC.BusinessTier.ViewModels
{
    public class BaseResponsePagingViewModel<T>
    {
        public PagingMetadata Metadata { get; set; }
        public List<T> Data { get; set; }
    }
    public class PagingMetadata
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
    }
}
