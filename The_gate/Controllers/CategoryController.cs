using Application.Interfaces;
using Domain.Domains;
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
            var cat = _category.GetAll();
            return PartialView("_IndexToCategoryPartial", cat);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cat = _category.GetById(id);
            var dto = new EditCategoryDto
            {
                NameA = cat.NameA,
                NameE = cat.NameE,
                IsActive = cat.IsActive,
               
            };
            return View(dto);

        }
        [HttpPost]
        public IActionResult Edit(int id, EditCategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _category.GetById(id); // جلب الكاتيجوري من قاعدة البيانات
                if (existingCategory == null)
                {
                    return NotFound();
                }

                // إذا تم رفع صورة جديدة
                if (categoryDto.Imagefile != null && categoryDto.Imagefile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + categoryDto.Imagefile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        categoryDto.Imagefile.CopyTo(stream);
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
                existingCategory.NameA = categoryDto.NameA;
                existingCategory.NameE = categoryDto.NameE;
                existingCategory.IsActive = categoryDto.IsActive;
              


                _category.Update(existingCategory); // تحديث الكائن
                _category.Save(); // حفظ التغييرات

                return RedirectToAction("HomePage", "Admin");
            }
            ViewBag.sub = _category.GetAll();

            return View(categoryDto);

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
                if (category.Imagefile != null && category.Imagefile.Length > 0)
                {
                   
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + category.Imagefile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        category.Imagefile.CopyTo(stream);
                    }

                    
                    imagePath = "/images/" + uniqueFileName;
                }
                var cat=new Category { Image= imagePath,
                    IsActive=category.IsActive,NameA=category.NameA,
                NameE=category.NameE};
                _category.Add(cat);
                _category.Save();
                return RedirectToAction("HomePage", "Admin");
            }
            return View(category);
        }
        public IActionResult Delete(int id)
        {
            _category.Delete(id);
            _category.Save();
            return RedirectToAction("HomePage", "Admin");
        }

    }
}
