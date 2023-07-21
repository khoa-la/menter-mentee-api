using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Role;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using _2MC.DataTier.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using Reso.Core.Custom;

namespace _2MC.BusinessTier.Services
{
    public interface IRoleService
    {
        BaseResponsePagingViewModel<RoleViewModel> GetRoles(RoleViewModel filter, PagingModel paging);
        RoleViewModel GetRoleById(int roleId);
        RoleViewModel CreateRole(CreateRoleRequestModel roleRequestModel);
        RoleViewModel UpdateRole(int roleId, UpdateRoleRequestModel roleRequestModel);
        void DeleteRole(int roleId);//chua co bang status
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public RoleViewModel CreateRole(CreateRoleRequestModel roleRequestModel)
        {
            var role = _mapper.Map<Role>(roleRequestModel);
            return _mapper.Map<RoleViewModel>(_repository.Create(role));
        }

        public void DeleteRole(int roleId)
        {
            var role = _repository.Get(roleId);
            if (role == null)
            {
                throw new ErrorResponse(404, "Not found roleId!");
            }

            //role.Status = 0;//Chưa biết nen cho đại ko hoạt động là 0
            _repository.Update(role);
        }

        public RoleViewModel GetRoleById(int roleId)
        {
            var result = _repository.Get(roleId);
            if (result == null)
            {
                throw new ErrorResponse(404, "Not found roleId!");
            }

            return _mapper.Map<RoleViewModel>(result);
        }

        public BaseResponsePagingViewModel<RoleViewModel> GetRoles(RoleViewModel filter, PagingModel paging)
        {
            var result = _repository.Get()
                 .ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider)
                 .DynamicFilter<RoleViewModel>(filter)
                 .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging,
                     CommonConstants.DefaultPaging);

            return new BaseResponsePagingViewModel<RoleViewModel>()
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

        public RoleViewModel UpdateRole(int roleId, UpdateRoleRequestModel roleRequestModel)
        {
            var role = _repository.Get(roleId);
            if (role == null)
            {
                throw new ErrorResponse(404, "Not found roleId!");
            }

            _mapper.Map<UpdateRoleRequestModel, Role>(roleRequestModel, role);

            return _mapper.Map<RoleViewModel>(_repository.Update(role));
        }
    }
}
