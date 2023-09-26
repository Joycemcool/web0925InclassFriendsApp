using FriendsWebApp.Database;
using FriendsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FriendsWebApp.Pages
{
    public class CreateModel : PageModel
        
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _env;

        private string? connectionString = null;


        [BindProperty]
        public Friend Friend { get; set; } = new Friend();

        [BindProperty]
        public IFormFile? ImageUpload { get; set; } = null;//validator don't show warning

        public CreateModel(ILogger<IndexModel> logger, IConfiguration _configuration, IWebHostEnvironment env)
        {
            this._logger = logger;
            this.connectionString = _configuration.GetConnectionString("sqlConnection");
            this._env = env;
            //ImageUpload = imageUpload;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            int rowsAffected = 0;

           // string imageName = DateTime.Now.ToString()+ImageUpload.FileName;//Create a unique file name here
            Friend.ImageName = ImageUpload.FileName;
            if (connectionString != null)
            {
                DbContext dbContext = new DbContext(connectionString);
                Friend.ImageName = ImageUpload.FileName;
                rowsAffected=dbContext.AddNewFriend(Friend);
            }

            if (rowsAffected == 1)                 
            {
                //Upload the image
                //Save image to /images folder
                string path = Path.Combine(_env.ContentRootPath+"\\wwwroot\\images\\", Friend.ImageName);
                using (FileStream filestream = new FileStream(path, FileMode.Create))
                {
                    ImageUpload.CopyTo(filestream);
                }
                return RedirectToPage("Index");
            }

            else
            {
                return Page();
            }

            //stop here
        }
    }
}
