using mvcCRUD.Models;
using mvcCRUD.Repository.Interfaces;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace mvcCRUD.Repository.Implementation;

public class DepartmentRepository : IGenericRepository<Department>
{
    private readonly string sqlString = "";

    public DepartmentRepository(IConfiguration config)
    {
        sqlString = config.GetConnectionString("stringConnection");
    }

    public Task<bool> DeleteMemberModel(int model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EditMemberModel(Department model)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Department>> ModelList()
    {
        List<Department> list = new List<Department>();
        using (var conection = new SqlConnection(sqlString))
        {
            conection.Open();
            SqlCommand cmd = new SqlCommand("sp_DepartmentsList", conection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (var deparment = await cmd.ExecuteReaderAsync())
            {
                while(await deparment.ReadAsync())
                {
                    list.Add(new Department
                        {
                        idDepartment = Convert.ToInt32(deparment["idDepartment"]),
                        departmentName = deparment["departmentName"].ToString()                    
                         });
                }
            }
        }

        return list;
    }

    public Task<bool> NewMemberModel(Department model)
    {
        throw new NotImplementedException();
    }
}
