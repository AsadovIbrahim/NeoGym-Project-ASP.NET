using DataBase.Entities.Concretes;
using DataBase.Repositories.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace NeoGym_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenuController(ITrainerRepository trainerRepository, IWebHostEnvironment webHostEnvironment)
        {
            _trainerRepository = trainerRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _trainerRepository.GetAllAsync();
            return View(data);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult>Create(Trainer trainer)
        {
            if (!ModelState.IsValid)
            {
                return View(trainer);
            }

            string path = _webHostEnvironment.WebRootPath + @"\Upload\Manage\";
            string fileName = Guid.NewGuid() + trainer.ImgFile.FileName;
            string fullPath=Path.Combine(path, fileName);

            using(FileStream stream=new FileStream(fullPath, FileMode.Create))
            {
                trainer.ImgFile.CopyTo(stream);
            }
            trainer.ImgUrl = fileName;

            await _trainerRepository.AddAsync(trainer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult>Remove(int id)
        {
            await _trainerRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var existingItem = await _trainerRepository.GetByIdAsync(id);
            return View(existingItem);
        }

        [HttpPost]

        public IActionResult Update(Trainer trainer)
        {
            if (!ModelState.IsValid)
            {
                return View(trainer);
            }

            if(trainer.ImgFile != null)
            {
                string path = _webHostEnvironment.WebRootPath + @"\Upload\Manage\";
                string fileName = Guid.NewGuid() + trainer.ImgFile!.FileName;
                string fullPath = Path.Combine(path, fileName);

                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    trainer.ImgFile.CopyTo(stream);
                }
                trainer.ImgUrl = fileName;
            }
            _trainerRepository.UpdateAsync(trainer);
            return RedirectToAction("Index");
        }


    }
}
