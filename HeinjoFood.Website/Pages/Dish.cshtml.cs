using HeinjoFood.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace HeinjoFood.Website.Pages
{
    public class DishPageModel : PageModel
    {
        private readonly ILogger<DishPageModel> _logger;
        private readonly IHeinjoFoodApiClient _apiClient;

        public DishPageModel(ILogger<DishPageModel> logger
            , IHeinjoFoodApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public Dish? Dish { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public async Task OnGetAsync(string id)
        {
            Dish = await _apiClient.GetDishByIdAsync(id);
            Title = Dish.Title;
            Description = Dish.Description;
        }


        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        public async Task OnPostAsync()
        {
            _logger.LogInformation("Properties: " + Title);
            var newDish = new Dish()
            {
                Title = Title,
                Description = Description
            };
            var createdDish = await _apiClient.PostNewDishAsync(newDish);
            JsonSerializerOptions options = new() { WriteIndented = true };
            var json = JsonSerializer.Serialize(createdDish, options);
            _logger.LogInformation(json);
        }
    }
}
