using ALR.Data.Database;
using ALR.Data.Database.Abstract;
using ALR.Data.Database.Repositories;
using ALR.Data.Dto;
using ALR.Infrastructure.APIConfig.Mapping;
using ALR.Services.Authentication.Abstract;
using ALR.Services.Authentication.Implement;
using ALR.Services.Common.Abstract;
using ALR.Services.Common.Implement;
using ALR.Services.Common.Payment;
using ALR.Services.MainServices.Abstract;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using ALR.Services.MainServices.Abstract.TenantInterface;
using ALR.Services.MainServices.Implement;
using ALR.Services.MainServices.Implement.LandLordImplement;
using ALR.Services.MainServices.Implement.TenantImplement;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ALR.Infrastructure.Configuration
{
    public static class ConfigurationService
    {
        public static void RegisterContextDB(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ALRDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DATN_Spring24_ALR"),
                    b => b.MigrationsAssembly(typeof(ALRDBContext).Assembly.FullName))
     );

        }

        public static void RegiserDI(this IServiceCollection service)
        {

            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            service.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));
            service.AddScoped(typeof(IRequestServices), typeof(RequestServices));
            service.AddScoped(typeof(IAdminAccountService), typeof(AdminAccountServices));
            service.AddScoped(typeof(IAdminServiceAdvanceServices), typeof(AdminServiceAdvanceServices));
            service.AddScoped(typeof(IAdminPackageAdvanceServices), typeof(AdminPackageAdvanceServices));
            service.AddScoped(typeof(IGenarateFileToExcel), typeof(GenarateFileToExcel));
            service.AddScoped(typeof(IBillManageService), typeof(BillManageService));

            service.AddScoped(typeof(IRegistrationAccountService), typeof(RegistrationAccountService));
            service.AddScoped(typeof(ISaveUserToken), typeof(SaveUserToken));
            service.AddScoped(typeof(IPostService), typeof(PostService));
            service.AddScoped(typeof(IAdminBillServices), typeof(AdminBillServices));
            service.AddScoped(typeof(IBookingScheduleService), typeof(BookingScheduleServices));

            service.AddScoped(typeof(IAdminBillServices), typeof(AdminBillServices));


            service.AddScoped(typeof(IBoxChatService), typeof(BoxChatService));
            service.AddScoped(typeof(IMessageService), typeof(MessageService));
            service.AddScoped(typeof(IManagerTenantService), typeof(ManagerTenantService));
            service.AddScoped(typeof(ILandlordRoomServices), typeof(LanlordRoomServices));
            service.AddScoped(typeof(IEmailServices), typeof(EmailServices));
            service.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            service.AddScoped(typeof(ILandlordServicePackageServices), typeof(LanlordServicePackageServices));
            service.AddScoped(typeof(ILandlordManageMotelServices), typeof(LandlordManageMotelServices));
            service.AddScoped(typeof(IFeedBackService), typeof(FeedBackService));
            service.AddScoped(typeof(ITenantBookingScheduleService), typeof(TenantBookingScheduleService));
            service.AddScoped(typeof(ILandlordManageTenantBookingServices), typeof(LandlordManageTenantBookingServices));
            service.AddScoped(typeof(IProfileService), typeof(ProfileService));
            service.AddScoped(typeof(IDeserializerAddress), typeof(DeserializerAddress));
            service.AddScoped(typeof(ILanlordPaymentService), typeof(LandlordPaymentService));
            service.AddScoped(typeof(IManageTenantService), typeof(ManageTenantServices));




        }

        public static void RegisterCors(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .SetIsOriginAllowed(x => true)
                            .Build();
                    });
            });
        }

        public static void RegiserMapper(this IServiceCollection service)
        {

            service.AddAutoMapper(typeof(UserMappings));
            service.AddAutoMapper(typeof(PackageServiceMappings));
            service.AddAutoMapper(typeof(AdminServiceMappings));
            service.AddAutoMapper(typeof(BillHistoryMappings));
            service.AddAutoMapper(typeof(BookingScheduleMappings));
            service.AddAutoMapper(typeof(RoomMappings));
            service.AddAutoMapper(typeof(TenantManageMappings));
            service.AddAutoMapper(typeof(RequestMapping));
            service.AddAutoMapper(typeof(FeedbackMapping));
            service.AddAutoMapper(typeof(PostMapping));
            service.AddAutoMapper(typeof(ProfileMapping));
        }

    }
}



