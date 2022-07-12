using CheckoutApp.Business.Extensions;
using CheckoutApp.DataAccess.Extensions;
using CheckoutApp.Facade;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCheckoutDbContext(builder.Configuration);
builder.Services.AddRepositories();

builder.Services.AddScoped<IBasketFacade, BasketFacade>();

builder.Services.AddAutoMapperProfiles();
builder.Services.AddCheckoutServices();
builder.Services.AddModelValidators();
builder.Services.AddIdempotentApiServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandlingMiddleware();

app.Run();
