using GetPhone.Database;
using GetPhone.Database.Interfaces;
using GetPhone.Services;

var builder = WebApplication.CreateBuilder(args);
BuilderUtils utils = new BuilderUtils(builder);

builder.Services.AddSwaggerGen(utils.ApplicationSwaggerOptions);
builder.Services.AddDbContext<ApplicationContext>(utils.ApplicationDatabaseOptions);
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAuthentication("Bearer").AddJwtBearer(utils.ApplicationJWTAuthenticationOptions);
builder.Services.AddAuthorization(utils.ApplicationAuthorizationOptions);
builder.Services.AddControllers();

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();