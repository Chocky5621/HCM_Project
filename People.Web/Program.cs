var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddHttpClient(); 

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
