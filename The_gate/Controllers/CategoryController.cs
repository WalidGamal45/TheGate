using Application.Interfaces;
using Domain.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace The_gate.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }
        public IActionResult Index()
        {
            var cat = _category.GetAdmins();
            return View(cat);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cat =_category.GetById(id);
           
            return View(cat);

        }
        [HttpPost]
        public IActionResult Edit(int id, AddCategryDto category)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _category.GetById(id); // جلب الكاتيجوري من قاعدة البيانات
                if (existingCategory == null)
                {
                    return NotFound();
                }

                // إذا تم رفع صورة جديدة
                if (category.Image != null && category.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + category.Image.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        category.Image.CopyTo(stream);
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

                _category.Update(existingCategory); // تحديث الكائن
                _category.Save(); // حفظ التغييرات

                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddCategryDto category)
        {
            if(ModelState.IsValid)
            {
                string imagePath = "";
                if (category.Image != null && category.Image.Length > 0)
                {
                    // تحديد المسار الذي ستحفظ فيه الصورة
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    // إنشاء اسم فريد للصورة لتفادي التكرار
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + category.Image.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // حفظ الصورة فعلياً
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        category.Image.CopyTo(stream);
                    }

                    // حفظ المسار النسبي للصورة لقاعدة البيانات
                    imagePath = "/images/" + uniqueFileName;
                }
                var cat=new Category { Image= imagePath,
                    IsActive=category.IsActive,NameA=category.NameA,
                NameE=category.NameE};
                _category.Add(cat);
                _category.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Delete(int id)
        {
            _category.Delete(id);
            _category.Save();
            return RedirectToAction("Index");
        }

    }
}
