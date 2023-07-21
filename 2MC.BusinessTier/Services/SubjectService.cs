using System.Linq;
using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Subject;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using _2MC.DataTier.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Reso.Core.Custom;

namespace _2MC.BusinessTier.Services
{
    public interface ISubjectService
    {
        BaseResponsePagingViewModel<SubjectViewModel> GetSubjects(SubjectViewModel filter, PagingModel paging);
        SubjectViewModel GetSubjectById(int subjectId);
        SubjectViewModel CreateSubject(CreateSubjectRequestModel subjectRequestModel);
        SubjectViewModel UpdateSubject(UpdateSubjectRequestModel subjectRequestModel);
        void DeleteSubject(int subjectId);
    }

    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public BaseResponsePagingViewModel<SubjectViewModel> GetSubjects(SubjectViewModel filter, PagingModel paging)
        {
            var result = _repository.Get()
                .ProjectTo<SubjectViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter<SubjectViewModel>(filter)
                .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging, CommonConstants.DefaultPaging);

            return new BaseResponsePagingViewModel<SubjectViewModel>()
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

        public SubjectViewModel GetSubjectById(int subjectId)
        {
            var result = _repository.Get(subjectId);
            if (result == null)
            {
                throw new ErrorResponse(404, "Not found subjectId!");
            }

            return _mapper.Map<SubjectViewModel>(result);
        }

        public SubjectViewModel CreateSubject(CreateSubjectRequestModel subjectRequestModel)
        {
            var subject = _mapper.Map<Subject>(subjectRequestModel);
            subject.CreateDate = Commons.Utils.GetCurrentDateTime();
            return _mapper.Map<SubjectViewModel>(_repository.Create(subject));
        }

        public SubjectViewModel UpdateSubject(UpdateSubjectRequestModel subjectRequestModel)
        {
            var subject = _repository.Get(subjectRequestModel.Id);
            if (subject == null)
            {
                throw new ErrorResponse(404, "Not found subjectId!");
            }

            _mapper.Map<UpdateSubjectRequestModel, Subject>(subjectRequestModel, subject);

            return _mapper.Map<SubjectViewModel>(_repository.Update(subject));
        }

        public void DeleteSubject(int subjectId)
        {
            var subject = _repository.Get(subjectId);
            if (subject == null)
            {
                throw new ErrorResponse(404, "Not found subjectId!");
            }

            _repository.Delete(subject);
        }
    }
}