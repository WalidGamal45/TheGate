using Application.Interfaces;
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
        public ProductController(IProduct _product, ICategory _category, ISubCategory _subCategory)
        {
            product = _product;
            category = _category;
            subCategory = _subCategory;
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
        public IActionResult AddProduct(ProductDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        string imagePath = "";
                        if (dto.Imagefile != null && dto.Imagefile.Length > 0)
                        {
                            // تحديد المسار الذي ستحفظ فيه الصورة
                            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                            // إنشاء اسم فريد للصورة لتفادي التكرار
                            var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.Imagefile.FileName;
                            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            // حفظ الصورة فعلياً
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                dto.Imagefile.CopyTo(stream);
                            }

                            // حفظ المسار النسبي للصورة لقاعدة البيانات
                            imagePath = "/images/" + uniqueFileName;
                        }
                        var cat = new Product
                        {
                            Imagep = imagePath,
                            Price = dto.Price,
                            NameA = dto.NameA,
                            NameE = dto.NameE,
                            categoryId = dto.categoryId,
                            SubCategoryId = dto.SubCategoryId,

                        };
                        product.Add(cat);
                        product.Save();
                        return RedirectToAction("HomePage", "Admin");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("categoryId", "error in categoryid");
                    ModelState.AddModelError("SubCategoryId", "error in SubCategoryId");
                }
            }
            ViewBag.cat = category.GetAll();
            ViewBag.sub = subCategory.GetAll();
            return View(dto);
        }
        [HttpGet]
        public IActionResult Edit (int id)
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
        public IActionResult Edit(ProductDto dto,int id)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = product.GetById(id); 
                if (existingCategory == null)
                {
                    return NotFound();
                }

                
                if (dto.Imagefile != null && dto.Imagefile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.Imagefile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        dto.Imagefile.CopyTo(stream);
                    }

                    
                    if (!string.IsNullOrEmpty(existingCategory.Imagep))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCategory.Imagep.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    existingCategory.Imagep = "/images/" + uniqueFileName;
                }

               
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
            return RedirectToAction("HomePage","Admin");
        }
    }
}
