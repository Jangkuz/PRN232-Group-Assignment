using AirWaterStore.Web.Hubs;
using AirWaterStore.Web.Services;

namespace AirWaterStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Games/Index", "");
            });
            builder.Services.AddSession();
            builder.Services.AddSignalR();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddRefitClient<ICatalogService>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler
                    {
                        // Skip ssl, DEVELOPMENT ONLY
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                    };
                })
            ;
            ////Configure DbContext
            //builder.Services.AddDbContext<AirWaterStoreContext>(options =>
            // options.UseSqlServer(builder.Configuration.GetConnectionString("DockerConnection")));
            ////  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //// Register repositories	
            //builder.Services.AddScoped<IGameRepository, GameRepository>();
            //builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            //builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            //builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            //builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            //builder.Services.AddScoped<IMessageRepository, MessageRepository>();

            //// Register services	
            //builder.Services.AddScoped<IGameService, GameService>();
            //builder.Services.AddScoped<IOrderService, OrderService>();
            //builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
            //builder.Services.AddScoped<IReviewService, ReviewService>();
            //builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IChatRoomService, ChatRoomService>();
            //builder.Services.AddScoped<IMessageService, MessageService>();
            //builder.Services.AddScoped<IVnPayService, VnPayService>();

            //builder.Services.Configure<VnPayConfig>(builder.Configuration.GetSection("VnPay"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapHub<ChatHub>("/chathub");

            app.Run();
        }
    }
}