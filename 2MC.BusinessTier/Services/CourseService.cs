using System;
using System.Collections.Generic;
using System.Linq;
using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Course;
using _2MC.BusinessTier.RequestModels.MenteeSession;
using _2MC.BusinessTier.ViewModels;
using _2MC.BusinessTier.ViewModels.FilterModels;
using _2MC.DataTier.Models;
using _2MC.DataTier.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reso.Core.Custom;

namespace _2MC.BusinessTier.Services
{
    public interface ICourseService
    {
        BaseResponsePagingViewModel<CourseViewModel> GetCourses(CourseViewModel filter, CourseFilterModel filterModel,
            PagingModel paging);

        CourseViewModel GetCourseById(int courseId);
        CourseViewModel CreateCourse(CreateCourseRequestModel courseRequestModel);
        CourseViewModel UpdateCourse(UpdateCourseRequestModel courseRequestModel);
        void DeleteCourseMentor(int courseId);
        void DeleteCourseAdmin(int courseId);
        List<CertificateViewModel> GetCertificatesByCourse(int courseId);
        CourseViewModel UpdateAdminCourse(UpdateCourseRequestModel courseRequestModel);
        List<SessionViewModel> GetSessionsByCourse(int courseId, int userId);
        List<MenteeSessionViewModel> GetMenteesOfSession(int sessionId, int userId);
        void UpdateSessionAttendance(List<UpdateAttendanceRequestModel> attendances);
    }

    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly ICertificateRepository _certificateRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISessionRepository _sessionRepo;
        private readonly IMenteeSessionRepository _menteeSessionRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseService(ICourseRepository repository, IMapper mapper, ISubjectRepository subjectRepository,
            IHttpContextAccessor httpContextAccessor, ICertificateRepository certificateRepository,
            ISessionRepository sessionRepo, IMenteeSessionRepository menteeSessionRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _subjectRepository = subjectRepository;
            _httpContextAccessor = httpContextAccessor;
            _certificateRepository = certificateRepository;
            _sessionRepo = sessionRepo;
            _menteeSessionRepo = menteeSessionRepo;
        }

        public BaseResponsePagingViewModel<CourseViewModel> GetCourses(CourseViewModel filter,
            CourseFilterModel filterModel, PagingModel paging)
        {
            var courses = _repository.Get();

            #region Check Date range

            var from = filterModel?.FromDate;
            var to = filterModel?.ToDate;

            if (to != null && from != null && DateTime.Compare((DateTime)from, (DateTime)to) > 0)
            {
                throw new ErrorResponse(400, "The datetime is invalid!");
            }

            if (from != null && to == null)
            {
                courses = courses.Where(x => DateTime.Compare(x.CreateDate, (DateTime)from) >= 0);
            }

            if (from == null && to != null)
            {
                courses = courses.Where(x => DateTime.Compare(x.CreateDate, (DateTime)to) <= 0);
            }

            if (from != null && to != null)
            {
                courses = courses.Where(x => DateTime.Compare(x.CreateDate, (DateTime)from) >= 0 &&
                                             DateTime.Compare(x.CreateDate, (DateTime)to) <= 0);
            }

            #endregion


            var result = courses.Where(x => x.IsActive == true)
                .OrderBy(x => x.Id)
                .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter<CourseViewModel>(filter)
                .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging,
                    CommonConstants.DefaultPaging);

            return new BaseResponsePagingViewModel<CourseViewModel>()
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

        public CourseViewModel GetCourseById(int courseId)
        {
            var result = _repository.Get()
                .Include(x => x.Mentor)
                .Include(x => x.Subject)
                .SingleOrDefault(x => x.IsActive == true && x.Id == courseId);
            if (result == null)
            {
                throw new ErrorResponse(404, "Not found courseId!");
            }

            return _mapper.Map<CourseViewModel>(result);
        }

        public CourseViewModel CreateCourse(CreateCourseRequestModel courseRequestModel)
        {
            int mentorId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]?.ToString());

            if (_subjectRepository.Get(x => x.Id == courseRequestModel.SubjectId).SingleOrDefault() == null)
            {
                throw new ErrorResponse(404, "Not found subjectId!");
            }

            var course = _mapper.Map<Course>(courseRequestModel);
            course.Status = (int?)CourseStatusEnum.Draft;
            course.IsActive = true;
            course.MentorId = mentorId;
            return _mapper.Map<CourseViewModel>(_repository.Create(course));
        }

        public CourseViewModel UpdateCourse(UpdateCourseRequestModel courseRequestModel)
        {
            int mentorId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]?.ToString());
            var course = _repository.Get()
                .SingleOrDefault(x => x.IsActive == true && x.Id == courseRequestModel.Id && x.MentorId == mentorId);
            if (course == null)
            {
                throw new ErrorResponse(404, "Not found courseId!");
            }

            _mapper.Map<UpdateCourseRequestModel, Course>(courseRequestModel, course);

            course.UpdateDate = Utils.GetCurrentDateTime();
            return _mapper.Map<CourseViewModel>(_repository.Update(course));
        }

        public List<CertificateViewModel> GetCertificatesByCourse(int courseId)
        {
            var course = _repository.Get()
                .Include(x => x.Subject)
                .Include(x => x.Mentor)
                .SingleOrDefault(x => x.Id == courseId);
            if (course == null)
            {
                throw new ErrorResponse(404, "Not found courseId!");
            }

            var certificatesInOfCourse = _certificateRepository
                .Get(x => x.SubjectId == course.SubjectId && x.MentorId == course.MentorId)
                .ProjectTo<CertificateViewModel>(_mapper.ConfigurationProvider).ToList();
            return certificatesInOfCourse;
        }

        public CourseViewModel UpdateAdminCourse(UpdateCourseRequestModel courseRequestModel)
        {
            var course = _repository.Get()
                .SingleOrDefault(x => x.IsActive == true && x.Id == courseRequestModel.Id);
            if (course == null)
            {
                throw new ErrorResponse(404, "Not found courseId!");
            }

            _mapper.Map<UpdateCourseRequestModel, Course>(courseRequestModel, course);

            return _mapper.Map<CourseViewModel>(_repository.Update(course));
        }

        public void DeleteCourseMentor(int courseId)
        {
            int mentorId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]?.ToString());
            var course = _repository.Get()
                .SingleOrDefault(x => x.IsActive == true && x.Id == courseId && x.MentorId == mentorId);
            if (course == null)
            {
                throw new ErrorResponse(404, "Not found courseId!");
            }

            course.UpdateDate = Commons.Utils.GetCurrentDateTime();
            course.IsActive = false;
            _repository.Update(course);
        }

        public void DeleteCourseAdmin(int courseId)
        {
            var course = _repository.Get()
                .SingleOrDefault(x => x.IsActive == true && x.Id == courseId);
            if (course == null)
            {
                throw new ErrorResponse(404, "Not found courseId!");
            }

            course.UpdateDate = Commons.Utils.GetCurrentDateTime();
            course.IsActive = false;
            _repository.Update(course);
        }

        public List<SessionViewModel> GetSessionsByCourse(int courseId, int userId)
        {
            var course = _repository.Get()
               .SingleOrDefault(x => x.Id == courseId && x.MentorId == userId);
            if (course == null)
            {
                throw new ErrorResponse(404, "Not found courseId or course not belong to logged in mentor!");
            }
            var sessionsOfCourse = _sessionRepo
                .Get(x => x.CourseId == course.Id)
                .OrderBy(x => x.StartTime)
                .ProjectTo<SessionViewModel>(_mapper.ConfigurationProvider).ToList();
            return sessionsOfCourse;
        }

        public List<MenteeSessionViewModel> GetMenteesOfSession(int sessionId, int userId)
        {
            var session = _sessionRepo.Get()
               .SingleOrDefault(x => x.Id == sessionId && x.Course.MentorId == userId);
            if (session == null)
            {
                throw new ErrorResponse(404, "Not found sessionId or session not belong to your course!");
            }
            var menteesOfSession = _menteeSessionRepo.Get(x => x.SessionId == session.Id)
                .ProjectTo<MenteeSessionViewModel>(_mapper.ConfigurationProvider).ToList();
            return menteesOfSession;
        }

        public void UpdateSessionAttendance(List<UpdateAttendanceRequestModel> attendances)
        {
            foreach (UpdateAttendanceRequestModel attendance in attendances)
            {
                var menteeSession = _menteeSessionRepo.Get().SingleOrDefault(x=>x.Id == attendance.Id);
                if(menteeSession == null)
                {
                    throw new ErrorResponse(404, "Not found menteeSessionId!");
                }
                menteeSession.IsAttended = attendance.IsAttended;
                _menteeSessionRepo.Update(menteeSession);
            }
        }
    }
}