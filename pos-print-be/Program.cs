using pos_print_be.DAL;
using pos_print_be.WebServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Custom", policy =>
    {
        policy.WithOrigins([
                "https://print-app.abdalipaas.xyz",
                "http://localhost:4200"
            ])
            .AllowAnyHeader()
            .WithMethods(["GET", "POST", "DELETE", "OPTIONS"]);
    });
});
builder.Services.AddProblemDetails();
builder.Services.AddScoped<MedicineRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("Custom");
app.MapOpenApi();

var medicineGroup = app.MapGroup("/api/v1/medicine");
medicineGroup.MapGet("", MedicineAPI.GetMedicines).WithOpenApi(operation => new (operation)
{
    Summary = "Get all medicines records",
    OperationId = nameof(MedicineAPI.GetMedicines),
});
medicineGroup.MapGet("/search/{name}", MedicineAPI.SearchMedicines).WithOpenApi(operation => new (operation)
{
    Summary = "Get all medicines records with the matching name",
    OperationId = nameof(MedicineAPI.SearchMedicines),
});
medicineGroup.MapDelete("{id}", MedicineAPI.DeleteMedicine).WithOpenApi(operation => new (operation)
{
    Summary = "Delete the medicine with the proovided id",
    OperationId = nameof(MedicineAPI.DeleteMedicine),
});
medicineGroup.MapPost("", MedicineAPI.SaveMedicine).WithOpenApi(operation => new (operation)
{
    Summary = "Save the provided medicine",
    OperationId = nameof(MedicineAPI.SaveMedicine),
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});
await app.RunAsync();
