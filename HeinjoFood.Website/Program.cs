using HeinjoFood.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

// Add HeinjoFoodApiClient generated with NSwagStudio
// https://blog.sanderaernouts.com/autogenerate-csharp-api-client-with-nswag
// https://elanderson.net/2019/11/use-http-client-factory-with-nswag-generated-classes-in-asp-net-core-3/
var baseAddress = builder.Configuration.GetSection("HeinjoFoodApiUrl").Value;
builder.Services.AddHttpClient<IHeinjoFoodApiClient, HeinjoFoodApiClient>(client => client.BaseAddress = new Uri(baseAddress));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();

app.Run();
