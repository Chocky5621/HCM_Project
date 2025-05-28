using Microsoft.EntityFrameworkCore;
using People.API.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseInMemoryDatabase("PeopleDb"));
var app = builder.Build();


app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	db.Database.EnsureCreated();
}
app.Run();
public partial class Program { }
