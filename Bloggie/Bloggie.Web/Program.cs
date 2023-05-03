using Bloggie.Web.Data;
using Bloggie.Web.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web
{
    //Program.cs dosyas�. Uygulaman�n konfig�rasyon ayarlar�n�n yap�ld�g� uygulaman�n baslad�g� dosyad�r. 5.0 ise startup dosyas� bulunur baslama noktas� i�in. 5.0 dan sonra program.cs icine geldi.
    //Builder ad�nda bir middleware imiz bulunmaktad�r. bu middleware ile programda kullanacag�m�z t�m servisleri tan�mlamaktay�z.( AddControllersWithViews vs.). ileride kullanacag�m�z t�m ek servisleri bu alanda tan�ml�yor olacag�z.
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //var app = builder.Build(); �nce connectionstringi  cag�r�yoruz db contexti programa tan�tt�k. 
            builder.Services.AddDbContext<BloggieDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));
			builder.Services.AddScoped<ITagInterface, TagRepository>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app. diyerek kulland�g�m�z request s�recindeki t�m �zellikler a�ag�da tan�mlanm��t�r. ��imize yarayacak olan �zelliklerin tamam�n� da app. diyerek tan�ml�yor olacag�z. �rnegin a�ag�da app.UseAuthorization() �zelligi request pipeline a tan�mlanm�st�r.
            //Bizler ise kimlik kontrol� yapmak istedigimizde  Authorization �zelligini kullan�yor olacag�z. Bunun i�in ileride app.UseAuthorization() isimli �zelligi dahil edecegiz.

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();  // static alanlar� kullan demek

            app.UseRouting();   //Route is�emi icin kullan demek

            app.UseAuthorization();

            // http://localhost:5656/Kitap/Index/6  �nce controller geliyor sonra kitap detay sonra 6. id ye ait kitap gelir.yani controllerdan sonra actiona ait yer gelecek sonra id k�sm�
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}