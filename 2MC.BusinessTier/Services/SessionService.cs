using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Session;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using _2MC.DataTier.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using Reso.Core.Custom;

namespace _2MC.BusinessTier.Services
{
    public interface ISessionService
    {
        BaseResponsePagingViewModel<SessionViewModel> GetSessions(SessionViewModel filter, PagingModel paging);
        SessionViewModel GetSessionById(int sessionId);
        SessionViewModel CreateSession(CreateSessionRequestModel sessionRequestModel);
        SessionViewModel UpdateSession(UpdateSessionRequestModel sessionRequestModel);
        void DeleteSession(int idsessionId);
    }

    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repo;
        private readonly IMapper _mapper;

        public SessionService(ISessionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public SessionViewModel CreateSession(CreateSessionRequestModel sessionRequestModel)
        {
            var session = _mapper.Map<Session>(sessionRequestModel);
            return _mapper.Map<SessionViewModel>(_repo.Create(session));
        }

        public void DeleteSession(int sessionId)
        {
            var session = _repo.Get(sessionId);
            if (session == null)
            {
                throw new ErrorResponse(404, "Not found sessionId!");
            }

            _repo.Delete(session);
        }

        public SessionViewModel GetSessionById(int sessionId)
        {
            var result = _repo.Get(sessionId);
            if (result == null)
            {
                throw new ErrorResponse(404, "Not found sessionId!");
            }

            return _mapper.Map<SessionViewModel>(result);
        }

        public BaseResponsePagingViewModel<SessionViewModel> GetSessions(SessionViewModel filter, PagingModel paging)
        {
            var result = _repo.Get()
                .ProjectTo<SessionViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(filter)
                .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging, CommonConstants.DefaultPaging);

            return new BaseResponsePagingViewModel<SessionViewModel>()
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

        public SessionViewModel UpdateSession(UpdateSessionRequestModel sessionRequestModel)
        {
            var session = _repo.Get(sessionRequestModel.Id);
            if (session == null)
            {
                throw new ErrorResponse(404, "Not found sessionId!");
            }

            _mapper.Map(sessionRequestModel, session);
            return _mapper.Map<SessionViewModel>(_repo.Update(session));
        }
    }
}