using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;
        //DbContext icin dependency injection işlemi gerçekleştirildi.

        //db ye postla  veri göndercez önce ctor yapıyoz .using Bloggie.Web.Data; ekledi.BloggieDbContext bloggieDbContext isimde atadık aynı isimle sonra ctrl. yaptık ikinci olana tıkladık. creat field .. yazıyordu.
        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        //yapmıs oldugumuz MVC routing sekli sunun gibi olacaktır.  Admintags controller
        //Add de action tarafı [HttpGet] arka planda calısır.
        //http://localhost:154/AdminTags/Add
        public IActionResult Add()
        {
            return View();
        }
        //not [] alttakinin özelligi belirtiyoruz post olduugnu view da iise submite e basıldıktan sonra view add diyoruz o sayfada tutmasını istiyoruz.
        //Http Post metodu çalıştırıyoruz. Burada amacımız Add View ında oluşturmus oldugumuz form içerisinde gerçekleşen POST metodu butona tıklandıgında submit olsun. Ve bu submit sonrasında asagidaki action ımız devreye girsin.
        //Peki kullanıcının bu bahsi gecen view üzerinde inputlara girmiş oldugu verileri sayfamıza nasıl cekecegiz?
        //1.Yöntem Bunun icin önce view a gidecegiz ve inputlara birer Name verecegiz.id="name" den sonra name="name" dedik inputlara. Sonra da controllerin icerisinde birer degiskende formdan gelen inputları asagıdaki gibi tutacagız. 
        //SubmitTag() Add olması lazım ama aynı isim oldugu icin hata verir.
        //Fakat get metodu kullandıgımız Add action ı ile post metodunda kullandıgımız Add action ının adları aynı olmamaktadır. Çünkü bir metot aynı sayıda parametreyi alarak yeniden aynı isimde kullanılamamaktadır.
        //Dolayısıyla biz de metodumuzun adının farklı olmasını fakat davranış biçiminin aynı olmasını saglamak adına ActionName adında bir özellik kullanarak bu metodun da Add Action ı icinde davranıs sergilemesini sagladık. [ActionName("Add")] 
        //2.Yöntem olarak da bir viewModel tanımlanır. ve view da kullanıcının girdigi veriler bu viewmodela aktarılır. amac domain modelin bir kopyasını olusturmak ve viewdan gelen verileri buraya aktarmaktır. daha sonra veritabanına bu verileri kaydederken viewmodel üzerinden cekip kaydediyor olacagız. name="name" sildik orada @model Bloggie.Web.Models.ViewModels.AddTagRequest  yazdık ara katmana yolluyoruz submit işlemi oldugu zaman. asp-for="Name" asp-for="DisplayName" inputlara bunları ekledik . name ve displayname i ctrl . ile gördük kendi geldi "" içine.
        // inputlardan gelen verileri viewmodela yolladık (ara katmana )sonra controllera sonra DB ye gidecek. 
        //view üzerinden kullanıcının girecegi veri kadar olan kısım icin bir view model olusturduk. ve orjinal domain modeldeki kullandıgımız ihtiyacımız olan propertyleri icerisine ekledik. Viewda artık bu viewmodela erişmek icin en üst kısma bir model tanımlaması yapıp viewmodelimizin bulundugu namespace i tanımladık. sonra da inputlardaki degişiklikleri bu viewmodela aktarmak icin her input a birer asp-for property si ekledik.
        //bunun da amacı suydu, sayfanın en üstünde tanımlamış oldugumuz model icindeki propertylerden  hangilerini doldurmamız gerektigini tanımladıgımız alandı. yani kullanıcı name bölümüne  bişeyler yazdıgında viewmodel icinde bulunan name kısmı dolduruluyor olacaktır. Akabinde doldurulması gereken her yer doldurulduktan sonra submit işlemi gerçekleştiginde bu verileri view model üzerinden controller a gönderiyor olacagız.
        //[ActionName("Add")] gerek kalmadı ve   Submittag i Add olarak degistirdik.
        //breakpoint koyup inputa girdigimiz veriyi name ve displayname e atandıgını kontrol ettik


        [HttpPost]
        //[ActionName("Add")]
        public async Task<IActionResult> AddAsync(AddTagRequest addTagRequest)

        {
            //1.yöntem
            //var name = Request.Form["name"];
            //var displayName = Request.Form["displayName"];


            //2.yöntemde yazdık yukarda ctor yaptık sonra burayı sildik
            //var name = addTagRequest.Name;
            //var displayName = addTagRequest.DisplayName;

            //2.yöntemdensonra yazdık 
            //tag e crl . yaptık using i ekledik using Bloggie.Web.Models.Domain;

            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            //veri girişini yaptık CRUD daki Add işlemi gerçekleşti. siteden veri submitledik sonra  ssms de execute diyip gelenleri gördük.

            //Submit işlemi gerçekleştikten sonra bizi Add.cshtml de tutmasını istiyoruz.
            //return View("Add");
            // Step:4 Submit işlemi gerçekleştikten sonra bizi List.cshtml e göndermesini istiyoruz.
            return RedirectToAction("List");



        }
        //Step:1 Tagleri Listeleme  
        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            //use dbcontext to read tags (tag(hashtag)leri okuyabilmek adına dbcontextimiz ile ilişki kurduk).Akabinde bu tagleri ait olan view sayfamıza yolladık(List.cshtml).
            //@model List<Bloggie.Web.Models.Domain.Tag>
            //domain deki tag model üzerindeki verileri listele  List.cshtml

            var tags = await bloggieDbContext.Tags.ToListAsync(); // Db den Tags  verileri getir listele.

            return View(tags); //listcshtml e gönderiyoruz tags yazarak. to list ile herseyi listeledik.birde list e sag tik view dedik ve view admintags altında list olusturdu.
            //Step:2 List.cshtml i olusturduk sonra Step:3) layout da dropdown altına bir Tag list ekledik asp-action List yaptık sonra da add tag yaptıktan sonra yukarda Step:4) add kısmında 
            //return View("Add"); yazan yeri  //return RedirectToAction("List"); yapıyoruz Add Submit deyince oraya yönlendiriyor.

            //Step:6  List e edit yeri ekledik  <td><a asp-action="Edit" asp-controller="AdminTags" asp-route-id="@item.Id">Edit</a></td>  
            //asp-route-id="@item.Id"  asp-route-x  x yerine istedigini yaz ordan bilgileri cekiyoruz edit üstüne geldigimizde sol allta id ler her bir edit icin  farklı geliyor sol attlat url uzantısı görünüyor ve tıklandıgında ise o params li url sayfasına yönlendiriyor.. 
              //https://localhost:7170/AdminTags/Edit/d70c5459-8bf7-4ec3-54a8-08db35e3ac21
              //    pattern: "{controller=Home}/{action=Index}/{id?}"  id kismini yapmıs olduk asp-route-id ile yaptık.
            //Step:7 sonra Edit list olusturduk düzenledik.
        }
        //Step:5) edit i tanımlıyoruz.
        //Edit get metodu için view sayfamıza verilerin ulaştırılacağı bir view model hazırladık. Bu view model üzerinde id name ve display name olarak üç özellik tanımladık. Metodumuza parametre olarak Guid tipinde id yolladık. Ve cshtml dosyasında model olarak bu view modeli kullandık. Daha öncesinde list sayfasındaki edit butonunda asp-route-id olarak id tanımlaması yaptığımız için doğrudan bu idye ait olan veriler ekrana controllerın aşağıdaki hali sayesinde geliyor oldu. aşağıda parametre olarak gönderilen id ile veritabanı sorgusu yaparak bu idye denk gelen objeyi çağırmış olduk ve viewmodeldaki propertyler bu objeyle eşitlendi. View sayfası da model olarak bu viewmodeli kullandığı için verilerimiz get metoduyla otomatik olarak ilgili inputlara yüklenmiş oldu. 
        [HttpGet]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            //Step:8 1.Yöntem  
            //var tag = bloggieDbContext.Tags.Find(id);
            // 2. Yöntem  Step: 9) sonra viewmodel icin edittagrequest olusturduk domain tagden bakıp kopyaladik.
            var tag =  await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id); // sorguyla obje yakalasın tag degsikene atsın.

            //Step:10 veriye find or first le eriştik. edit  fonksiyone Guid id yolladık. 
            if (tag != null) 
            {
                //gelen veriyi viewmodela yolladık ara katmana da bunları eşitledik viewmodel degişmiş oldu. sonra update yaıcaz .+ idler  edittagrequeste götürdük  asp-for ile. yaptık.+ orda dönen submit post metodu kullanır edit cs sayfasında . 
                var editTagRequest = new EditTagRequest // yeni obje olusturduk burda esleştirme yaptık aradıgımız nesnedekilerle atamasını yaptık viewmodel üzerinden artık cekebiliriz.
                {   Id= tag.Id,
                    Name = tag.Name, 
                    DisplayName = tag.DisplayName 
                };
                return View(editTagRequest);  // edit tag requestin icini oku 
            }

            return View(null); //diger türlü boş yolla yada yazmana gerek yok.edit sayfasına gidiyoz.

            //Step:11 edit.cs sayfası düzenledik asagıdaki asp-for="Id"  normalde işlevi yok zaten ayni sayfadayız. ilerde belki lazım olur.

            //< div class="mb-3">
            //    <label class="form-label">Id</label>
            //    <input type = "text" class="form-control" id="id" asp-for="Id" readonly />
            //</div>
        }


        //Step:12  Post için metodu yazdık 
        //Post işlemi için ilgili edit sayfamızda model olarak kullandığımız edittagrequest isimli viewmodelımızı yine kullanıyor olacağız. Çünkü formumuz post işlemi submit etmektedir. Bu yüzden aynı isimdeki IActionResult üzerindeki HttpPost olan kısım çalışıyor olacaktır. Öncelikle Veritabanı modelimiz olan Tag model üzerinden yeni bir nesne üretim propertylerini viewmodella eşitliyoruz. Sonrasında bu idye id olan veritabanı nesnemizi bulmak için find metodu kullanıyoruz. Bu bu id ait olan nesnenin veritabanındaki prpertylerini yeni oluşturmuş olduğumuz tag nesnesinin propertyleriyle eşitleyerek yeni atamayı yapmış oluyoruz. Akabinde savechanges diyerek veritabanını güncellemiş oluyoruz.

        //step:13 Delete butonu ekleyecegiz editte 
        //<button type="submit" class="btn btn-dark ms-2">Delete</button> ekledik

        [HttpPost]
        public async Task<IActionResult> EditAsync(EditTagRequest editTagRequest) // edit.cs deki 
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };
            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id); // mevcut olanı bulduk edittagrequest üzerinden bulduk. 
            if (existingTag != null) // eger null degilse
            {
                existingTag.Name = tag.Name;                    // bunları eşlestir ve degistir yaptık
                existingTag.DisplayName = tag.DisplayName;

                await bloggieDbContext.SaveChangesAsync();             // degişiklikleri kaydettik
                return RedirectToAction("List");  // list sayfasına geri yollar.
            }
            return View();
        }

        //[HttpPost]
        //public IActionResult Delete(EditTagRequest editTagRequest)
        //{
        //    var tag = bloggieDbContext.Tags.Find(editTagRequest.Id);

        //    if(tag != null)
        //    {
        //        bloggieDbContext.Tags.Remove(tag);
        //        bloggieDbContext.SaveChanges();

        //        //show a success notification  istersen ekle biz eklemedik

        //        return RedirectToAction("List");
        //    }
        //    //showing notification error
        //    return View();
        //}



        //2.yöntem   deletei list  sayfasına  koyduk.  
        //<button type="submit" asp-controller="AdminTags" asp-action="Delete" asp-route-id="@Model.Id" class="btn btn-dark ms-2">Delete</button> editte ise asp-route-id model id ekledik ama bundan öncede calısıyordu . guid id yolladik asadıga ve delete butonuna basınca sildi aynı sayfa icerisinde. 
        //[HttpPost]
        //public IActionResult Delete(EditTagRequest editTagRequest)
        //{
        //    var tag = bloggieDbContext.Tags.Find(editTagRequest.Id);
        //bunu yoruma  aldık sonra altta  Guid id ile cektigimiz deletein post methodunu düzenledik   (taghelpers a bak.)
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tag = await bloggieDbContext.Tags.FindAsync(id);

            if (tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
               await bloggieDbContext.SaveChangesAsync();

                //show a success notification  istersen ekle biz eklemedik

                return RedirectToAction("List");
            }
            //showing notification error
            return View();
        }

    }
}
