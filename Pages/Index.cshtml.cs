using HeinjoFood.Api;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeinjoFood.Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHeinjoFoodApiClient _apiClient;

        public IndexModel(ILogger<IndexModel> logger
            , IHeinjoFoodApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public IList<Dish>? Dishes { get; set; }
        public async Task OnGetAsync()
        {
            Dishes = (await _apiClient.GetAllDishesAsync()).ToList();
        }

    }
}