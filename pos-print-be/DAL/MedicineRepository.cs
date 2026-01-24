using Npgsql;
using Dapper;
using pos_print_be.DTO;

namespace pos_print_be.DAL;

public class MedicineRepository
{
    private readonly string _connectionString = Environment.GetEnvironmentVariable("CONN_STR") ?? "";

    public async Task<List<Medicine>> GetAll()
    {
        string query = "SELECT * FROM medicine order by name";
        await using var conn = new NpgsqlConnection(_connectionString);
        var list = await conn.QueryAsync<Medicine>(query);
        return list.ToList();
    }
    
    public async Task<List<Medicine>> Search(string name)
    {
        string query = "SELECT * FROM medicine WHERE name ilike @Term";
        var searchTermWithWildcards = $"%{name}%"; 
        await using var conn = new NpgsqlConnection(_connectionString);
        var list = await conn.QueryAsync<Medicine>(query, new { Term = searchTermWithWildcards });
        return list.ToList();
    }
    
    public async Task<bool> Delete(int id)
    {
        string query = "DELETE FROM medicine WHERE id = @DeleteId";
        await using var conn = new NpgsqlConnection(_connectionString);
        var list = await conn.ExecuteAsync(query, new { DeleteId = id });
        return true;
    }

    public async Task<Medicine?> SaveAsync(Medicine medicine)
    {
        string query = """
            INSERT INTO medicine (name, price) VALUES (@Name, @Price)
            on conflict (name)
            do update set price=@Price
            RETURNING *
        """;
        await using var conn = new NpgsqlConnection(_connectionString);
        var saved = await conn.QuerySingleOrDefaultAsync<Medicine>(query, medicine);
        return saved;
    }
}