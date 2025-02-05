// using Microsoft.EntityFrameworkCore;
// using TodoApi;
// using Microsoft.OpenApi.Models;

// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();


// builder.Services.AddDbContext<ToDoDbContext>(options =>
//     options.UseMySql("name=ToDoDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"))
// );

//  // Add Cors
// builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
//     {
//         builder.AllowAnyOrigin()
//                .AllowAnyMethod()
//               .AllowAnyHeader();
//  }));

// builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo
//     {
//         Title = "My API",
//         Version = "v1",
//         Description = "This is my API documentation",
//     });
// });


// app.UseCors("myPolicy");



// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(c =>
//     {
//         c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//         c.RoutePrefix = string.Empty; // כך שהדף הראשי יהיה ה-Swagger
//     });
// }

// app.MapGet("/items", async (ToDoDbContext db) =>
// {
//     return await db.Items.ToListAsync();
// });

// app.MapPost("/items", async (ToDoDbContext db, Item newItem) =>
// {
//     db.Items.Add(newItem);
//     await db.SaveChangesAsync();
//     return Results.Created($"/items/{newItem.Id}", newItem);
// });

// app.MapPut("/items/{id}", async (ToDoDbContext db, int id, Item updatedItem) =>
// {
//     var existingItem = await db.Items.FindAsync(id);
//     if (existingItem is null) return Results.NotFound();

//     existingItem.Name = updatedItem.Name;
//     existingItem.IsComplete = updatedItem.IsComplete;

//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });

// app.MapDelete("/items/{id}", async (ToDoDbContext db, int id) =>
// {
//     var item = await db.Items.FindAsync(id);
//     if (item is null) return Results.NotFound();

//     db.Items.Remove(item);
//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });


// app.UseAuthorization();
// app.MapControllers();

// app.Run();

using Microsoft.EntityFrameworkCore;
using TodoApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql("name=ToDoDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"))
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API V1");
    c.RoutePrefix = string.Empty; 
});

app.MapGet("/items", async (ToDoDbContext db) =>
{
    return await db.Items.ToListAsync();
});

app.MapPost("/items", async (ToDoDbContext db, Item newItem) =>
{
    db.Items.Add(newItem);
    await db.SaveChangesAsync();
    return Results.Created($"/items/{newItem.Id}", newItem);
});

app.MapPut("/items/{id}", async (ToDoDbContext db, int id, Item updatedItem) =>
{
    var existingItem = await db.Items.FindAsync(id);
    if (existingItem is null) return Results.NotFound();

   // existingItem.Name = updatedItem.Name;
    existingItem.IsComplete = updatedItem.IsComplete;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/items/{id}", async (ToDoDbContext db, int id) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();

    db.Items.Remove(item);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
