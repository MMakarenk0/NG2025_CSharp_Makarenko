using TreatmentAbstraction;
using TreatmentBL.Injections;
using TreatmentDal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddRefitClients(builder.Configuration);

builder.Services.AddTreatmentDalLogic(builder.Configuration);
builder.Services.AddTreatmentBusinessLogic();

builder.Services.Configure<PetAdoptionClientSettings>(
    builder.Configuration.GetSection(PetAdoptionClientSettings.SectionName)
);

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

app.Run();
