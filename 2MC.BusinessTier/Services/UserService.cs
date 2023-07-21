using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.User;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using _2MC.DataTier.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reso.Core.Custom;

namespace _2MC.BusinessTier.Services
{
    public interface IUserService
    {
        BaseResponsePagingViewModel<UserViewModel> GetUsers(UserViewModel filter, PagingModel paging);
        UserViewModel GetUserById(int userId);
        UserViewModel CreateUser(CreateUserRequestModel userRequestModel);
        UserViewModel UpdateLoginUser(UpdateLoginUserRequestModel userRequestModel);
        void DeleteUser(int userId);
        Task<object> LoginByEmail(string idToken, string fcmToken);
        UserViewModel GetCurrentLoginUser();
        UserViewModel UpdateUser(UpdateUserRequestModel userRequestModel);
        BaseResponsePagingViewModel<OrderViewModel> GetOrdersByUser(int userId, PagingModel paging);
        UserViewModel GetMentorById(int id);
    }

    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IFireBaseService _fireBaseService;
        private readonly IUserRepository _repository;
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository repository, IOrderRepository orderRepo, IMapper mapper,
            IFireBaseService fireBaseService,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _orderRepo = orderRepo;
            _mapper = mapper;
            _fireBaseService = fireBaseService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<dynamic> LoginByEmail(string idToken, string fcmToken)
        {
            UserRecord userRecord;
            try
            {
                userRecord = await _fireBaseService.GetUserRecordByIdToken(idToken);
            }
            catch (Exception e)
            {
                var responseFailValidToken = new
                {
                    status = new
                    {
                        success = false,
                        message = e.Message,
                        status = 400,
                    },
                    data = new
                        { }
                };
                return responseFailValidToken;
            }

            if (string.IsNullOrEmpty(userRecord.PhoneNumber) && string.IsNullOrEmpty(userRecord.Email))
            {
                return null;
            }

            var user = !string.IsNullOrEmpty(userRecord.PhoneNumber)
                ? _repository.Get(x => x.Phone.Equals(userRecord.PhoneNumber)).Include(x => x.Role).FirstOrDefault()
                : _repository.Get(x => x.Email.Equals(userRecord.Email)).Include(x => x.Role).FirstOrDefault();

            try
            {
                if (user == null)
                {
                    //Create new user
                    User newUser = new User()
                    {
                        FullName = userRecord.DisplayName,
                        Email = userRecord.Email,
                        ImageUrl = userRecord.PhotoUrl,
                        Phone = userRecord.PhoneNumber ?? "",
                        CreateDate = Commons.Utils.GetCurrentDateTime(),
                        Status = 1,
                        RoleId = (int)RoleEnum.Mentee
                    };

                    var addedUser = _repository.Create(newUser);
                    var userInDb = _repository.Get(x => x.Id == addedUser.Id).Include(x => x.Role).FirstOrDefault();
                    if (addedUser != null)
                    {
                        string[] roles = { userInDb?.Role.Name };
                        var newToken =
                            AccessTokenManager.GenerateJwtToken(string.IsNullOrEmpty(addedUser.FullName)
                                ? ""
                                : addedUser.FullName, roles, addedUser.Id, _configuration);

                        var responseSuccess = new BaseResponseViewModel<LoginViewModel>()
                        {
                            Status = new StatusModel()
                            {
                                Status = 200,
                                Message = "Success",
                                Success = true
                            },
                            Data = new LoginViewModel()
                            {
                                AccessToken = newToken,
                                UserId = addedUser.Id,
                                Phone = addedUser.Phone,
                                Email = addedUser.Email,
                                Name = addedUser.FullName,
                                ImageUrl = addedUser.ImageUrl,
                                IsFirstLogin = true,
                                Role = addedUser.Role.Name
                            }
                        };
                        return responseSuccess;
                    }

                    var responseFail = new BaseResponseViewModel<object>()
                    {
                        Status = new StatusModel()
                        {
                            Status = 200,
                            Message = "Success",
                            Success = true
                        },
                        Data = new { }
                    };
                    return responseFail;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            if (!string.IsNullOrEmpty(user.FullName) && !string.IsNullOrEmpty(user.Email))
            {
                string[] roles = { user.Role.Name };
                var newToken =
                    AccessTokenManager.GenerateJwtToken(string.IsNullOrEmpty(user.FullName)
                        ? ""
                        : user.FullName, roles, user.Id, _configuration);

                var responseSuccess = new BaseResponseViewModel<LoginViewModel>()
                {
                    Status = new StatusModel()
                    {
                        Status = 200,
                        Message = "Success",
                        Success = true
                    },
                    Data = new LoginViewModel()
                    {
                        AccessToken = newToken,
                        UserId = user.Id,
                        Phone = user.Phone,
                        Email = user.Email,
                        Name = user.FullName,
                        ImageUrl = user.ImageUrl,
                        Role = user.Role.Name
                    }
                };
                return responseSuccess;
            }

            var response = new BaseResponseViewModel<object>()
            {
                Status = new StatusModel()
                {
                    Success = false,
                    Message = "Fail",
                    Status = 200
                },
                Data = new
                {
                    Phone = userRecord.PhoneNumber,
                    Email = userRecord.Email,
                    ImageUrl = userRecord.PhotoUrl
                }
            };
            return response;
        }

        public UserViewModel CreateUser(CreateUserRequestModel userRequestModel)
        {
            var user = _mapper.Map<User>(userRequestModel);
            return _mapper.Map<UserViewModel>(_repository.Create(user));
        }

        public void DeleteUser(int userId)
        {
            var user = _repository.Get(userId);
            if (user == null)
            {
                throw new ErrorResponse(404, "Not found userId!");
            }

            user.Status = (int?)UserStatusEnum.UnActive;
            _repository.Update(user);
        }

        public UserViewModel GetUserById(int userId)
        {
            var result = _repository.Get(userId);
            if (result == null)
            {
                throw new ErrorResponse(404, "Not found userId!");
            }

            return _mapper.Map<UserViewModel>(result);
        }

        public UserViewModel GetCurrentLoginUser()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]?.ToString());

            var result = _repository.Get().SingleOrDefault(x => x.Id == userId);
            if (result == null)
            {
                throw new ErrorResponse(404, "Not found userId!");
            }

            return _mapper.Map<UserViewModel>(result);
        }

        public UserViewModel GetMentorById(int id)
        {
            var result = _repository.Get().SingleOrDefault(x =>
                x.Id == id && (x.RoleId == (int?)RoleEnum.Mentor || x.RoleId == (int?)RoleEnum.Mentee));
            if (result == null)
            {
                throw new ErrorResponse(404, "Not found id!");
            }

            return _mapper.Map<UserViewModel>(result);
        }

        public BaseResponsePagingViewModel<UserViewModel> GetUsers(UserViewModel filter, PagingModel paging)
        {
            var result = _repository.Get()
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter<UserViewModel>(filter)
                .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging,
                    CommonConstants.DefaultPaging);

            return new BaseResponsePagingViewModel<UserViewModel>()
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

        public UserViewModel UpdateLoginUser(UpdateLoginUserRequestModel userRequestModel)
        {
            int loginUserId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]?.ToString());

            var user = _repository.Get(loginUserId);
            if (user == null)
            {
                throw new ErrorResponse(404, "Not found userId!");
            }

            _mapper.Map<UpdateLoginUserRequestModel, User>(userRequestModel, user);

            return _mapper.Map<UserViewModel>(_repository.Update(user));
        }

        public UserViewModel UpdateUser(UpdateUserRequestModel userRequestModel)
        {
            var user = _repository.Get(userRequestModel.Id);
            if (user == null)
            {
                throw new ErrorResponse(404, "Not found userId!");
            }

            _mapper.Map<UpdateUserRequestModel, User>(userRequestModel, user);

            return _mapper.Map<UserViewModel>(_repository.Update(user));
        }

        public BaseResponsePagingViewModel<OrderViewModel> GetOrdersByUser(int userId, PagingModel paging)
        {
            var result = _orderRepo.Get(x => x.MenteeId == userId)
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
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