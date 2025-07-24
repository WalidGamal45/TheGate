using Application.Interfaces;
using Domain.Domains;
using Domain.DTOs.Category;
using Domain.DTOs.SubCategory;
using Microsoft.AspNetCore.Mvc;

namespace The_gate.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategory subCategory;
        private readonly ICategory Category;
        public SubCategoryController(ISubCategory sub, ICategory category)
        {
            subCategory = sub;
            Category = category;
        }
        public IActionResult Index()
        {
            var sub = subCategory.GetAll();
            return PartialView("_IndexToSubCategory", sub);
        }
        [HttpGet]
        public IActionResult Add_SubCategory()
        {

            ViewBag.sub = Category.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Add_SubCategory(SubCategoryDto category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string imagePath = "";
                    if (category.Imagefile != null && category.Imagefile.Length > 0)
                    {
                        // تحديد المسار الذي ستحفظ فيه الصورة
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        // إنشاء اسم فريد للصورة لتفادي التكرار
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + category.Imagefile.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // حفظ الصورة فعلياً
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            category.Imagefile.CopyTo(stream);
                        }

                        // حفظ المسار النسبي للصورة لقاعدة البيانات
                        imagePath = "/images/" + uniqueFileName;
                    }
                    var cat = new SubCategory
                    {
                        Image = imagePath,
                        IsActive = category.IsActive,
                        NameA = category.NameA,
                        NameE = category.NameE,
                        categoryId = category.categoryId,

                    };
                    subCategory.Add(cat);
                    subCategory.Save();
                    return RedirectToAction("HomePage", "Admin");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("categoryId", "error in categoryid");
            }
            ViewBag.sub = Category.GetAll();
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit_SubCategory(int id)
        {
            ViewBag.sub = Category.GetAll();
            var cat = subCategory.GetById(id);
            var dto = new SubCategoryDto
            {
                NameA = cat.NameA,
                NameE = cat.NameE,
                IsActive = cat.IsActive,
                categoryId = cat.categoryId,
            };
            return View(dto);

        }
        [HttpPost]
        public IActionResult Edit_SubCategory(SubCategoryDto category, int id)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = subCategory.GetById(id); // جلب الكاتيجوري من قاعدة البيانات
                if (existingCategory == null)
                {
                    return NotFound();
                }

                // إذا تم رفع صورة جديدة
                if (category.Imagefile != null && category.Imagefile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + category.Imagefile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        category.Imagefile.CopyTo(stream);
                    }

                    // حذف الصورة القديمة إن وجدت (اختياري)
                    if (!string.IsNullOrEmpty(existingCategory.Image))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCategory.Image.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    existingCategory.Image = "/images/" + uniqueFileName;
                }

                // تحديث البيانات الأخرى
                existingCategory.NameA = category.NameA;
                existingCategory.NameE = category.NameE;
                existingCategory.IsActive = category.IsActive;
                existingCategory.categoryId = category.categoryId;


                subCategory.Update(existingCategory); // تحديث الكائن
                subCategory.Save(); // حفظ التغييرات

                return RedirectToAction("HomePage", "Admin");
            }
            ViewBag.sub = Category.GetAll();

            return View(category);

        }
        public IActionResult Delete(int id)
        {
            subCategory.Delete(id);
            subCategory.Save();
            return RedirectToAction("HomePage", "Admin");
        }
    }
}
