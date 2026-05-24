using Microsoft.EntityFrameworkCore;
using TodoApi.Models; // Asegúrate de que este sea el nombre de tu carpeta de modelos

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DE LA BASE DE DATOS ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TodoContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// --- 2. CONFIGURACIÓN DE CORS (Permiso para React) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => 
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers();
var app = builder.Build();

app.UseCors("AllowAll");

// --- 3. MAPEO DE RUTAS (ROUTES MAPPING) ---

// Obtener todas las tareas (GET)
app.MapGet("/api/todo", async (TodoContext db) => 
    await db.Items.ToListAsync());

// Crear una nueva tarea (POST)
app.MapPost("/api/todo", async (TodoContext db, Item item) => {
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todo/{item.Id}", item);
});

// Actualizar una tarea (PUT)
app.MapPut("/api/todo/{id}", async (TodoContext db, int id, Item inputItem) => {
    var todo = await db.Items.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Name = inputItem.Name;
    todo.IsComplete = inputItem.IsComplete;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Borrar una tarea (DELETE)
app.MapDelete("/api/todo/{id}", async (TodoContext db, int id) => {
    var todo = await db.Items.FindAsync(id);
    if (todo is null) return Results.NotFound();

    db.Items.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();