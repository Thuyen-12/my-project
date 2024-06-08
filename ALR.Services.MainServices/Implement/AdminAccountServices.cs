using ALR.Data.Database.Abstract;
using ALR.Data.Database;
using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Services.MainServices.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ALR.Data.Base;
using System;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using ALR.Data.Dto.PagingDto;
using static ALR.Data.Base.EnumBase;

public class AdminAccountServices : IAdminAccountService
{
    private readonly IRepository<UserEntity> _repository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AdminAccountServices(IRepository<UserEntity> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
       
        _mapper = mapper;

        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserEntity> CreateAccountByAdmin(AdminCreateAccountDto dto)
    {
        try
        {

            var userEntity = _mapper.Map<UserEntity>(dto);
            var profileEntity = _mapper.Map<ProfileEntity>(dto);
            userEntity.ProfileID = profileEntity.ProfileID;
            userEntity.Profile = profileEntity;

           
               _repository.InsertAsync(userEntity);
              await _repository.CommitChangeAsync();

            return  userEntity;

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<UserEntity>> GetListUser(List<Guid> userIds)
    {
        var listUserEntity = await _repository.GetDataIncludeAsync(user => userIds.Contains(user.UserEntityID), user => user.Profile);
        return listUserEntity.ToList();
    }

    public async Task<PagingListDto<UserEntity>> GetListUser(int startIndex, int pageSize)
    {
        try
        {
            var listUserEntity = await _repository.GetOnlyDataIncludeAsync(x => x.Profile);
            int totalCount = listUserEntity.Count();
            PagingListDto<UserEntity> result = new PagingListDto<UserEntity>()
            {
                Data = listUserEntity.Skip(startIndex).Take(pageSize).ToList(),
                TotalCount = totalCount,
            };
            return result;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.ToString());
        }
        
    }

    public async Task<UserEntity> GetCurrentUser(Guid userID)
    {
        if(userID == Guid.Empty)
        {
            return null;
        }
        var result = await _repository.GetByConditionAsync(x => x.UserEntityID.Equals(userID));
        return result;
    }

    public async Task<AlrResult> UnactiveAccount(Guid accountId,int status)
    {
        var acc = await _repository.GetByConditionAsync(x => x.UserEntityID.Equals(accountId));
        if(status == null || acc == null) {
            return AlrResult.NullObject;
        }
       acc.Active = status;
        _repository.UpdateAsync(acc);
       await _repository.CommitChangeAsync();
        return AlrResult.Success;
    }
}
