using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET105_BANSACH.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace NET105_BANSACH.Controllers
{
    public class BookController : Controller
    {
        AppDBContext _context;
        public BookController()
        {
            _context = new AppDBContext();
        }
        // GET: ProductController
        public ActionResult Index() // Lấy ra danh sách Sản phẩm
        {
            var allProducts = _context.Books.ToList();
            return View(allProducts);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(string id)
        {
            var product = _context.Books.Find(id); // Find là phương thức chỉ áp dụng cho PK
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            Book fakeData = new Book() // Tạo 1 chút thông tin để điền sẵn sang form create
            {
                BookID = GenerateUniqueID(),
                Title = "Conan",
                Description = "Truyện cho người trên 14 tuổi",
                Author = "Kim Đồng",
                Price = new Random().Next(10000, 1000000), // random giá trị từ 10000 đến 1000000
                Amount = new Random().Next(),
                Status = 1,
            };
            return View(fakeData);
        }
        private string GenerateUniqueID()
        {
            // Tạo một chuỗi ngẫu nhiên dựa trên thời gian và một số ngẫu nhiên
            return DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
        }
        [HttpPost]
        public ActionResult Create(Book product)
        {
            try
            {
                _context.Books.Add(product); _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Đã có lỗi gì đó khiến chúng tôi không thể thêm được hihi");
            }
        }
        public ActionResult Edit(string id)
        {
            // Lấy được thông tin cần sửa để điền lên form trước đã
            var editData = _context.Books.Find(id);
            return View(editData);
        }
        [HttpPost]
        public ActionResult Edit(Book product)
        {
            try
            {
                var editData = _context.Books.Find(product.BookID); // Tìm ra đối tượng cần sửa
                editData.Title = product.Title; editData.Description = product.Description;
                _context.Books.Update(editData);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Đâu phải lỗi lầm nào cũng sửa được đâu");
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(string id)
        {
            var deleteData = _context.Books.Find(id);
            _context.Books.Remove(deleteData);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // Thêm sản phẩm vào giỏ hàng
        public IActionResult AddToCart(string id, int quantity)
        {
            // Check xem đã đăng nhập chưa? nếu chưa thì bắt Login
            var check = HttpContext.Session.GetString("account"); // username đăng nhập vào nếu có
            if (String.IsNullOrEmpty(check)) return RedirectToAction("Login", "Account");
            else
            {
                // Xem trong giỏ hàng ứng với user đó đã có sản phẩm với ad này hay chưa?
                var cartItem = _context.CartsDetails.FirstOrDefault(p => p.ProductID == id
                && p.Username == check);
                if (cartItem == null)// Sản phẩm chưa hề nằm trong giỏ hàng => Tạo mới 1 cartDetails
                {
                    CartDetails cartDetails = new CartDetails() // Tạo mới 1 Cartdetails theo thông tin đã nhận
                    {
                        CartDetailsID = Guid.NewGuid(),
                        Username = check,
                        ProductID = id,
                        Quantity = quantity,
                        Status = 1
                    };
                    _context.CartsDetails.Add(cartDetails); _context.SaveChanges(); // thêm vào DB
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity + quantity; // Cộng đồn số lượng (Chưa check quá tổng sp trong kho)
                    _context.CartsDetails.Update(cartItem); _context.SaveChanges();
                }
                return RedirectToAction("Index", "Product"); // Quay lại trang danh sách SP
            }
        }

    }
}
