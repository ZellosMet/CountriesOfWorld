using Microsoft.EntityFrameworkCore;
using CountriesOfWorld;

var builder = WebApplication.CreateBuilder(args);
//Добавляем сервис DBContext(должен быть перед сборкой)
builder.Services.AddDbContext<ApplicationDbContext>();
var app = builder.Build();


//Провера подключения
app.MapGet("/api", () => new { Message = "server is running" });
app.MapGet("/api/ping", () => new { Message = "pong" });

//Запрос на получения всех стран
app.MapGet("/api/country", async (ApplicationDbContext db) =>
{
    return await db.Сountries.ToListAsync();
});

//Запрос на получение страны по id
app.MapGet("/api/country/{id:int}", async (int id, ApplicationDbContext db) =>
{
    return await db.Сountries.FirstOrDefaultAsync(c => c.Id == id);
});

//Запрос на получение страны по коду
app.MapGet("/api/country/{code}", async (string code, ApplicationDbContext db) =>
{
    return await db.Сountries.FirstOrDefaultAsync(d => d.Alpha2Code == code);
});

//Добавление страны
app.MapPost("/api/country/list", async (List<Сountry> list, ApplicationDbContext db) =>
{
    await db.Сountries.AddRangeAsync(list);
    await db.SaveChangesAsync();
    return list;
});

//Добавление списка стран/////////
app.MapPost("/api/country", async (Сountry country, ApplicationDbContext db) =>
{
    await db.Сountries.AddAsync(country);
    await db.SaveChangesAsync();
    return country;
});

//Редактирование данных//////////
app.MapPut("/api/country/{id:int}", async (int id, Сountry country, ApplicationDbContext db) =>
{
    Сountry? update = await db.Сountries.FirstOrDefaultAsync(c => c.Id == id);
    if (update != null)
    {
        update.FullName = country.FullName;
        update.ShortName = country.ShortName;
        update.Alpha2Code = country.Alpha2Code;
        db.Сountries.Update(update);
        await db.SaveChangesAsync();
    }
    return update;
});

//Удаление страны
app.MapDelete("/api/country/{id:int}", async (int id, ApplicationDbContext db) =>
{
    Сountry? deleted = await db.Сountries.FirstOrDefaultAsync(c => c.Id == id);
    if (deleted != null)
    {
        db.Сountries.Remove(deleted);
        await db.SaveChangesAsync();
    }
});

//Удаление всех стран//////////
app.MapDelete("/api/country/alldelete", async (ApplicationDbContext db) =>
{
    List<Сountry> all_country = await db.Сountries.ToListAsync();
    db.Сountries.RemoveRange(all_country);
    await db.SaveChangesAsync();    
});

app.Run();
