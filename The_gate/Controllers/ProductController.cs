using Application.Interfaces;
using Application.Services;
using Domain.Domains;
using Domain.DTOs.Product;
using Domain.DTOs.SubCategory;
using Microsoft.AspNetCore.Mvc;

namespace The_gate.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct product;
        private readonly ICategory category;
        private readonly ISubCategory subCategory;
        private readonly IImageService image;
        public ProductController(IProduct _product, ICategory _category, ISubCategory _subCategory, IImageService _image)
        {
            product = _product;
            category = _category;
            subCategory = _subCategory;
            image = _image;
        }

        public IActionResult GetAllProduct()
        {
            var pro = product.GetAll();
            return PartialView("_ProductPartial", pro);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.cat = category.GetAll();
            ViewBag.sub = subCategory.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductDto _product)
        {

            if (ModelState.IsValid)
            {

                string imagePath = image.SaveImage(_product.Imagefile, "images");

                var cat = new Product
                {
                    Imagep = imagePath,
                    Price = _product.Price,
                    NameA = _product.NameA,
                    NameE = _product.NameE,
                    categoryId = _product.categoryId,
                    SubCategoryId = _product.SubCategoryId
                };

                product.Add(cat);
                product.Save();

                return RedirectToAction("HomePage", "Admin");

            }

            ViewBag.sub = category.GetAll();
            return View(product);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.cat = category.GetAll();
            ViewBag.sub = subCategory.GetAll();
            var cat = product.GetById(id);
            var dto = new ProductDto
            {
                NameA = cat.NameA,
                NameE = cat.NameE,
                Price = cat.Price,
                categoryId = cat.categoryId,
                SubCategoryId = cat.SubCategoryId
            };
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit(ProductDto dto, int id)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = product.GetById(id);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                // إذا تم رفع صورة جديدة
                if (dto.Imagefile != null && dto.Imagefile.Length > 0)
                {
                    // حذف الصورة القديمة إن وجدت
                    if (!string.IsNullOrEmpty(existingCategory.Imagep))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCategory.Imagep.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // حفظ الصورة الجديدة باستخدام الخدمة
                    string imagePath = image.SaveImage(dto.Imagefile, "images");
                    existingCategory.Imagep = imagePath;
                }

                // تحديث باقي البيانات
                existingCategory.NameA = dto.NameA;
                existingCategory.NameE = dto.NameE;
                existingCategory.Price = dto.Price;
                existingCategory.categoryId = dto.categoryId;
                existingCategory.SubCategoryId = dto.SubCategoryId;

                product.Update(existingCategory);
                product.Save();

                return RedirectToAction("HomePage", "Admin");
            }

            ViewBag.cat = category.GetAll();
            ViewBag.sub = subCategory.GetAll();
            return View(dto);

        }
        public IActionResult Delete(int id)
        {
            product.Delete(id);
            product.Save();
            return RedirectToAction("HomePage", "Admin");
        }
    }
}
