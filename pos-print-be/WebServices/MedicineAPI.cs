using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using pos_print_be.DAL;
using pos_print_be.DTO;

namespace pos_print_be.WebServices;

public class MedicineAPI()
{
    public static async Task<Results<Ok<List<Medicine>>, BadRequest<ProblemDetails>>> GetMedicines(MedicineRepository repository)
    {
        var list = await repository.GetAll();
        return TypedResults.Ok(list);
    }
    
    public static async Task<Results<Ok<List<Medicine>>, BadRequest<ProblemDetails>>> SearchMedicines(MedicineRepository repository, string name)
    {
        var list = await repository.Search(name);
        return TypedResults.Ok(list);
    }
    
    public static async Task<Results<Ok<bool>, BadRequest<ProblemDetails>>> DeleteMedicine(MedicineRepository repository, int id)
    {
        var result = await repository.Delete(id);
        return TypedResults.Ok(result);
    }
    
    public static async Task<Results<Ok<Medicine?>, BadRequest<ProblemDetails>>> SaveMedicine(MedicineRepository repository, Medicine medicine)
    {
        var response = await repository.SaveAsync(medicine);
        return TypedResults.Ok(response);
    }
}