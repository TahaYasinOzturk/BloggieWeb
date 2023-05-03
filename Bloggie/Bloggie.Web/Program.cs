using Bloggie.Web.Data;
using Bloggie.Web.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web
{
    //Program.cs dosyasý. Uygulamanýn konfigürasyon ayarlarýnýn yapýldýgý uygulamanýn basladýgý dosyadýr. 5.0 ise startup dosyasý bulunur baslama noktasý için. 5.0 dan sonra program.cs icine geldi.
    //Builder adýnda bir middleware imiz bulunmaktadýr. bu middleware ile programda kullanacagýmýz tüm servisleri tanýmlamaktayýz.( AddControllersWithViews vs.). ileride kullanacagýmýz tüm ek servisleri bu alanda tanýmlýyor olacagýz.
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //var app = builder.Build(); önce connectionstringi  cagýrýyoruz db contexti programa tanýttýk. 
            builder.Services.AddDbContext<BloggieDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));
			builder.Services.AddScoped<ITagInterface, TagRepository>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app. diyerek kullandýgýmýz request sürecindeki tüm özellikler aþagýda tanýmlanmýþtýr. Ýþimize yarayacak olan özelliklerin tamamýný da app. diyerek tanýmlýyor olacagýz. örnegin aþagýda app.UseAuthorization() özelligi request pipeline a tanýmlanmýstýr.
            //Bizler ise kimlik kontrolü yapmak istedigimizde  Authorization özelligini kullanýyor olacagýz. Bunun için ileride app.UseAuthorization() isimli özelligi dahil edecegiz.

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();  // static alanlarý kullan demek

            app.UseRouting();   //Route isþemi icin kullan demek

            app.UseAuthorization();

            // http://localhost:5656/Kitap/Index/6  önce controller geliyor sonra kitap detay sonra 6. id ye ait kitap gelir.yani controllerdan sonra actiona ait yer gelecek sonra id kýsmý
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}