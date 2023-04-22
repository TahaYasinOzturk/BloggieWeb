using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
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
        [HttpPost]
        [ActionName("Add")]
        public IActionResult SubmitTag()

        {
            var name = Request.Form["name"];
            var displayName = Request.Form["displayName"];

            //Submit işlemi gerçekleştikten sonra bizi Add.cshtml de tutmasını istiyoruz.
            return View("Add");
        }
    }
}
