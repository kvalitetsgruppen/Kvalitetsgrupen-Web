using Microsoft.EntityFrameworkCore;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);
//Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cWWdCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXlfcXVVRGRZUkBxW0FWYUo=");
SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBPh8sVXJ8S0d+X1JPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9mSX1SdkVnXXxaeXxWQWQ=");
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();  // added by Sudipta on 3.4.25

builder.Services.AddTransient<DocumentRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}
app.UseRouting();

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers(); // <- Required
  endpoints.MapDefaultControllerRoute();
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // added by Sudipta on 3.4.25
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FileExplorer}/{action=Index}/{id?}");



app.Run();
