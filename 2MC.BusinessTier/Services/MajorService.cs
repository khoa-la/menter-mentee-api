using System.Linq;
using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Major;
using _2MC.BusinessTier.ViewModels;
using _2MC.BusinessTier.ViewModels.FilterModels;
using _2MC.DataTier.Models;
using _2MC.DataTier.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Reso.Core.Custom;

namespace _2MC.BusinessTier.Services
{
    public interface IMajorService
    {
        BaseResponsePagingViewModel<MajorViewModel> GetMajors(MajorViewModel filter, PagingModel paging);
        MajorViewModel GetMajorById(int majorId);
        MajorViewModel CreateMajor(CreateMajorRequestModel majorRequestModel);
        MajorViewModel UpdateMajor(UpdateMajorRequestModel majorRequestModel);
        void DeleteMajor(int majorId);
    }

    public class MajorService : IMajorService
    {
        private readonly IMajorRepository _repository;
        private readonly IMapper _mapper;

        public MajorService(IMajorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public MajorViewModel CreateMajor(CreateMajorRequestModel majorRequestModel)
        {
            var major = _mapper.Map<Major>(majorRequestModel);
            return _mapper.Map<MajorViewModel>(_repository.Create(major));
        }

        public void DeleteMajor(int majorId)
        {
            var major = _repository.Get(majorId);
            if (major == null)
            {
                throw new ErrorResponse(404, "Not found majorId!");
            }

            _repository.Delete(major);
        }

        public MajorViewModel GetMajorById(int majorId)
        {
            var result = _repository.Get(majorId);
            if (result == null)
            {
                throw new ErrorResponse(404, "Not found majorId!");
            }

            return _mapper.Map<MajorViewModel>(result);
        }

        public BaseResponsePagingViewModel<MajorViewModel> GetMajors(MajorViewModel filter, PagingModel paging)
        {
            var result = _repository.Get()
                .ProjectTo<MajorViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(filter)
                .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging,
                    CommonConstants.DefaultPaging);

            return new BaseResponsePagingViewModel<MajorViewModel>()
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

        public MajorViewModel UpdateMajor(UpdateMajorRequestModel majorRequestModel)
        {
            var major = _repository.Get(majorRequestModel.Id);
            if (major == null)
            {
                throw new ErrorResponse(404, "Not found majorId!");
            }

            _mapper.Map(majorRequestModel, major);
            return _mapper.Map<MajorViewModel>(_repository.Update(major));
        }
    }
}