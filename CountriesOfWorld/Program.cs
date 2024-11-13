using Microsoft.EntityFrameworkCore;
using CountriesOfWorld;

var builder = WebApplication.CreateBuilder(args);
//��������� ������ DBContext(������ ���� ����� �������)
builder.Services.AddDbContext<ApplicationDbContext>();
var app = builder.Build();


//������� �����������
app.MapGet("/api", () => new { Message = "server is running" });
app.MapGet("/api/ping", () => new { Message = "pong" });

//������ �� ��������� ���� �����
app.MapGet("/api/country", async (ApplicationDbContext db) =>
{
    return await db.�ountries.ToListAsync();
});

//������ �� ��������� ������ �� id
app.MapGet("/api/country/{id:int}", async (int id, ApplicationDbContext db) =>
{
    return await db.�ountries.FirstOrDefaultAsync(c => c.Id == id);
});

//������ �� ��������� ������ �� ����
app.MapGet("/api/country/{code}", async (string code, ApplicationDbContext db) =>
{
    return await db.�ountries.FirstOrDefaultAsync(d => d.Alpha2Code == code);
});

//���������� ������
app.MapPost("/api/country/list", async (List<�ountry> list, ApplicationDbContext db) =>
{
    await db.�ountries.AddRangeAsync(list);
    await db.SaveChangesAsync();
    return list;
});

//���������� ������ �����/////////
app.MapPost("/api/country", async (�ountry country, ApplicationDbContext db) =>
{
    await db.�ountries.AddAsync(country);
    await db.SaveChangesAsync();
    return country;
});

//�������������� ������//////////
app.MapPut("/api/country/{id:int}", async (int id, �ountry country, ApplicationDbContext db) =>
{
    �ountry? update = await db.�ountries.FirstOrDefaultAsync(c => c.Id == id);
    if (update != null)
    {
        update.FullName = country.FullName;
        update.ShortName = country.ShortName;
        update.Alpha2Code = country.Alpha2Code;
        db.�ountries.Update(update);
        await db.SaveChangesAsync();
    }
    return update;
});

//�������� ������
app.MapDelete("/api/country/{id:int}", async (int id, ApplicationDbContext db) =>
{
    �ountry? deleted = await db.�ountries.FirstOrDefaultAsync(c => c.Id == id);
    if (deleted != null)
    {
        db.�ountries.Remove(deleted);
        await db.SaveChangesAsync();
    }
});

//�������� ���� �����//////////
app.MapDelete("/api/country/alldelete", async (ApplicationDbContext db) =>
{
    List<�ountry> all_country = await db.�ountries.ToListAsync();
    db.�ountries.RemoveRange(all_country);
    await db.SaveChangesAsync();    
});

app.Run();
