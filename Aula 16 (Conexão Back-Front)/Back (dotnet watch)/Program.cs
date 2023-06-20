using webBack.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var env = builder.Environment;
const string url = "https://viacep.com.br/ws/80320330/json/";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MainPolicy",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
        });
});

builder.Services.AddScoped<RepoExemploContext>();
if (env.IsDevelopment())
    builder.Services.AddSingleton<IRepository<Mensagem>,
    FakeMessageRepository>();
else if (env.IsProduction())
    builder.Services.AddTransient<IRepository<Mensagem>, MessageRepository>();

//Para jogar no banco, deixa só o else if

builder.Services.AddSingleton<ICepService>(p => new CepService(url));

builder.Services.AddTransient<CpfService>();

var app = builder.Build();
app.UseCors(); // Usando Cors


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
