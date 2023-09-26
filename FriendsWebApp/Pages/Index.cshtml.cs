using FriendsWebApp.Database;
using FriendsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FriendsWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Friend> friends { get; set; }= new List<Friend>();//Public, so the cshtml page can use it. give it a null value

        private string? connectionString = null;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration _configuration)
        {
            _logger = logger;
            connectionString = _configuration.GetConnectionString("sqlConnection");
        }

        public void OnGet()
        {
            if(connectionString != null)
            {
                DbContext dbContext = new DbContext(connectionString);
                friends = dbContext.GetAllFriends();
            }
        }
    }
}