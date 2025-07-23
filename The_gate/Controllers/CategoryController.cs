using Application.Interfaces;
using Domain.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace The_gate.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory _category;
        private object _mapper;

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
        public IActionResult Edit(int id, EditCategoryDto category)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _category.GetById(id); 
                if (existingCategory == null)
                {
                    return NotFound();
                }

                
                if (category.Imagefile != null && category.Imagefile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + category.Imagefile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        category.Imagefile.CopyTo(stream);
                    }

                    
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

                existingCategory.NameA = category.NameA;
                existingCategory.NameE = category.NameE;
                existingCategory.IsActive = category.IsActive;

                _category.Update(existingCategory); 
                _category.Save(); 

                return RedirectToAction("HomePage","Admin");
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
