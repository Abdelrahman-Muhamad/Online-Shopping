using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using FinalProject.Services;
 
using Microsoft.AspNetCore.Identity;
using PurchasingSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
   [Authorize(Roles = "Customer")]
    [Area("Customers")]
    public class CustomerController : Controller
    {
        public IItemRepos ItemRepos { get; set; }
        public ICategoryRepos CategoryRepos { get; set; }
        public IOrderRepos OrderRepos { get; set; }
        public IOrdersItemsRepos OrdersItemsRepos { get; }

        public static List<Item> CartItems=new List<Item>();
        public static string detail = " ";


        private readonly UserManager<AppUser> _userManager;
      
        
        public CustomerController(UserManager<AppUser> usermang, IItemRepos itemRepos,
                               ICategoryRepos categoryRepos, IOrderRepos orderRepos, IOrdersItemsRepos ordersItemsRepos)
        {
            _userManager = usermang;
            ItemRepos = itemRepos;
            CategoryRepos = categoryRepos;
            OrderRepos = orderRepos;
            OrdersItemsRepos = ordersItemsRepos;
        }

        // GET: CustomerController
        public ActionResult Index()
        {

            ViewBag.Cats = CategoryRepos.GetAll();
            var rand = new Random();
            List<Item> Items = ItemRepos.GetAll();
            List<Item> NItems = new List<Item>();
            for(int i=1;i<CategoryRepos.GetAll().Count();i++)
            {
               
                for(int j=0;j<3;j++)
                {
                    NItems.Add( Items.FindAll(e => e.CategoryID == i+1).ElementAt(rand.Next(Items.FindAll(e => e.CategoryID == i + 1).Count())));
                }
            }


            return View(NItems);
        }

        // GET: CustomerController/Details/5
        public ActionResult details(int id)
        {
            Item item = ItemRepos.GetDetails(id);
            ViewBag.Cat = CategoryRepos.GetDetails(item.CategoryID);
            ViewBag.Cats = CategoryRepos.GetAll();
            ViewBag.Mes = null;
            return View(item);
        }
        [HttpPost]
        public ActionResult details(int id, int num, string Sizes = "", string Colors = "", string mes = "Added")
        {
            Item item = ItemRepos.GetDetails(id);
            for (int i = 0; i < num + 1; i++)
            { CartItems.Add(item); }
            detail += (num + 1)+" "+item.Name+" & : "+ Sizes+" , " + Colors+"/";
            ViewBag.Cat = CategoryRepos.GetDetails(item.CategoryID);
            ViewBag.Cats = CategoryRepos.GetAll();
            ViewBag.Mes = mes;


            return View(item);
        }


        // GET: CustomerController/Create
        public ActionResult products()
        {
            ViewBag.Cats = CategoryRepos.GetAll();

            return View(ItemRepos.GetAll());
        }
        public ActionResult Contact()
        {
           

            return View();
        }
        public ActionResult About()
        {


            return View();
        }

        public ActionResult Catproducts(int id)
        {
            ViewBag.Cats = CategoryRepos.GetAll();
            ViewBag.Cat = CategoryRepos.GetDetails(id);

            return View(ItemRepos.GetAll().Where(e => e.CategoryID == id));
        }
        public ActionResult Search(string name="@@@")
        {
            var items =new List<Item>();
            if (name != null)
            {
                 items = ItemRepos.GetAll().Where(i => i.Name.ToLower().Contains(name.ToLower()))?.ToList();
            }
            ViewBag.Cats = CategoryRepos.GetAll();
            ViewBag.Items =items;
            return View();
            
        }
        public ActionResult AddToCart()
        {

            return View(CartItems);
        }

        [HttpPost]
        public ActionResult AddToCart(int id)
        {

            var item = CartItems.Find(e => e.ID == id);
            CartItems.Remove(item);

            return View(CartItems);
        }
        
        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
       
        public async Task<ActionResult> Checkout()
        {
            var user = await GetCurrentUserAsync();
            //var user = user;
            var userId = user?.Id;
            ViewBag.Cust = user;
            return View(CartItems);
        }

        [HttpPost]
        public async Task<ActionResult> Checkout(List<Item> checkItems)
        {
            var user = await GetCurrentUserAsync();
            CartItems = checkItems;
            ViewBag.Cust = user;
            return View(CartItems);
        }

        [HttpPost]
        public async Task<ActionResult> CheckoutConfirmed(decimal total)
        {
         

            var user = await GetCurrentUserAsync();
            //var user = user;
            var userId = user?.Id;
            Order order = new()
            {
                CustomerID = userId,
                Date = DateTime.Now,
                TotalCost = total,
                details = detail
            };

            OrderRepos.Insert(order);

            var allOrd = OrderRepos.GetAll().ToList();

            order = allOrd.Last();

            foreach (var item in CartItems)
            {
                OrdersItemsRepos.Insert(new OrderItem() { OrderId = order.ID, ItemId = item.ID }); 
            }

            return View(order);
        }


    }
}
