using Microsoft.EntityFrameworkCore;
using PWT_project.Api.Data;
using PWT_project.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MyDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", policy =>
	{
		policy
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

var app = builder.Build();

app.UseCors("CorsPolicy");

app.MapGet("/api/varer", async (MyDbContext db) =>
{
	return await db.Vare.ToListAsync();
});

app.MapGet("/api/varer-beholdning", async (MyDbContext db) =>
{
	var query =
		from v in db.Vare
		join b in db.Beholdning on v.EAN equals b.ean
		select new VareBeholdningDto
		{
			// Varer
			SupplierNo = v.SupplierNo,
			ItemGroupId = v.ItemGroupId,
			ItemGroupName = v.ItemGroupName,
			StyleNo = v.StyleNo,
			ItemDescription = v.ItemDescription,
			Size = v.Size,
			Length = v.Length,
			EAN = v.EAN,
			ColorCodeName = v.ColorCodeName,
			Season = v.Season,
			CostPrice = v.CostPrice,
			CostPriceCurrency = v.CostPriceCurrency,
			SuggestedRetailPrice = v.SuggestedRetailPrice,

			// Beholdning
			ShopId = b.ShopId,
			SupplierId = b.SupplierId,
			InventoryQuantity = b.InventoryQuantity,		};

	return await query.ToListAsync();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
