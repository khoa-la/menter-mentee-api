using System.Linq;
using System.Linq.Dynamic.Core;
using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace _2MC.BusinessTier.Services
{
    
    public interface IMenteeSessionService
    {
        BaseResponsePagingViewModel<MenteeSessionViewModel> GetMenteeSessionByCourse(int courseId, int menteeId, PagingModel paging);
    }
    public class MenteeSessionService:IMenteeSessionService
    {
        private readonly IMenteeSessionRepository _repo;
        private readonly IMapper _mapper;

        public MenteeSessionService(IMenteeSessionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public BaseResponsePagingViewModel<MenteeSessionViewModel> GetMenteeSessionByCourse(int menteeId, int courseId, PagingModel paging)
        {

            var result = _repo.Get()
                .Where(x => x.MenteeId == menteeId  && x.Session.CourseId == courseId)
                .ProjectTo<MenteeSessionViewModel>(_mapper.ConfigurationProvider)
                .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging, CommonConstants.DefaultPaging);

            return new BaseResponsePagingViewModel<MenteeSessionViewModel>()
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