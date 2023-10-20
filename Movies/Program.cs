using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
		.AddRoles<IdentityRole>()
		.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// treba dodati za koristenje sessiona
app.UseSession();

app.MapControllerRoute(
name: "areas",
pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}/{pid?}");
app.MapRazorPages();

app.Run();


// 0*- drop-database
// 1*  add-mirgation InitialCreate
// 2*  update-database

//add-migration -o data/Migrations step01

// Mogli bi jedan update baze za podsjetnik :)

