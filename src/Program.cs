using GetPhone.Database;
using GetPhone.Database.Interfaces;
using GetPhone.Services;

var builder = WebApplication.CreateBuilder(args);
BuilderUtils utils = new BuilderUtils(builder);

builder.Services.AddSwaggerGen(utils.ApplicationSwaggerOptions);
builder.Services.AddDbContext<ApplicationContext>(utils.ApplicationDatabaseOptions);
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddControllers();

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    // Допиши пж
    
}

app.MapControllers();

app.Run();