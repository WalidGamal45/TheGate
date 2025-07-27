using Application.Interfaces;
using Domain.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace The_gate.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory _category;
        private readonly IImageService _imageService;

        public CategoryController(ICategory category, IImageService imageService)
        {
            _category = category;
            _imageService = imageService;
        }
        //*****************************************************************************************
        public IActionResult Index()
        {
            var cat = _category.GetAll();
            return PartialView("_IndexToCategoryPartial", cat);
        }
        //*****************************************************************************************

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
        //*****************************************************************************************

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
                    // حذف الصورة القديمة (اختياري)
                    if (!string.IsNullOrEmpty(existingCategory.Image))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCategory.Image.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // حفظ الصورة الجديدة باستخدام الخدمة
                    string imagePath = _imageService.SaveImage(categoryDto.Imagefile, "images");
                    existingCategory.Image = imagePath;
                }

                // تحديث باقي البيانات
                existingCategory.NameA = categoryDto.NameA;
                existingCategory.NameE = categoryDto.NameE;
                existingCategory.IsActive = categoryDto.IsActive;

                _category.Update(existingCategory);
                _category.Save();

                return RedirectToAction("HomePage", "Admin");
            }


          
            return View(categoryDto);

        }
        //*****************************************************************************************


        public IActionResult Add()
        {
            return View();
        }
        //*****************************************************************************************

        [HttpPost]
        public IActionResult Add(AddCategryDto category)
        {
            if (ModelState.IsValid)
            {
                string imagePath = _imageService.SaveImage(category.Imagefile, "images");

                var cat = new Category
                {
                    Image = imagePath,
                    IsActive = category.IsActive,
                    NameA = category.NameA,
                    NameE = category.NameE
                };

                _category.Add(cat);
                _category.Save();

                return RedirectToAction("HomePage", "Admin");
            }


            return View(category);

        }
        //*****************************************************************************************

        public IActionResult Delete(int id)
        {
            _category.Delete(id);
            _category.Save();
            return RedirectToAction("HomePage", "Admin");
        }

    }
}
