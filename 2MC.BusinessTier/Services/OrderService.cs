using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Reso.Core.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.BusinessTier.Services
{
    public interface IOrderService
    {
        BaseResponsePagingViewModel<OrderViewModel> GetOrders(OrderViewModel filter, PagingModel paging);
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public BaseResponsePagingViewModel<OrderViewModel> GetOrders(OrderViewModel filter, PagingModel paging)
        {
            var result = _repo.Get().OrderBy(x => x.CreateDate)
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(filter)
                .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging, CommonConstants.DefaultPaging);

            return new BaseResponsePagingViewModel<OrderViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = result.Item1
                },
                Data = result.Item2.ToList()
            };
        }
    }
}
