using mvcCRUD.Models;
using mvcCRUD.Repository.Interfaces;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace mvcCRUD.Repository.Implementation;

public class EmployeeRepository : IGenericRepository<Employee>
{
    private readonly string _sqlString = "";

    public EmployeeRepository(IConfiguration config)
    {
        _sqlString = config.GetConnectionString(_sqlString);
            
    }
    public async Task<List<Employee>> ModelList()
    {
        List<Employee> list = new List<Employee>();
        using (var connection = new SqlConnection(_sqlString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_EmployeesList", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (var employee = await cmd.ExecuteReaderAsync())
            {
                while (await employee.ReadAsync())
                {
                    list.Add(new Employee
                    {
                        idEmployee = Convert.ToInt32(employee["idEmployee"]),
                        fullName = employee["fullName"].ToString(),
                        refDepartment = new Department()
                        {
                            idDepartment = Convert.ToInt32(employee["idDepartment"]),
                            departmentName = employee["departmentName"].ToString()
                        },
                        salary = Convert.ToInt32(employee["salary"]),
                        startDate = employee["startDate"].ToString()
                    });
                }
            }
        }

        return list;
    }

    public async Task<bool> NewMemberModel(Employee model)
    {
        using (var connection = new SqlConnection(_sqlString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_createEmployee", connection);
            cmd.Parameters.AddWithValue("fullName", model.fullName);
            cmd.Parameters.AddWithValue("fullName", model.refDepartment.idDepartment);
            cmd.Parameters.AddWithValue("fullName", model.salary);
            cmd.Parameters.AddWithValue("fullName", model.startDate);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            int rows = await cmd.ExecuteNonQueryAsync();

            if (rows > 0)
                return true;
            else
                return false;
        }
    }

    public async  Task<bool> EditMemberModel(Employee model)
    {
        using (var connection = new SqlConnection(_sqlString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_editEmployee", connection);
            cmd.Parameters.AddWithValue("idEmployee", model.idEmployee);
            cmd.Parameters.AddWithValue("fullName", model.fullName);
            cmd.Parameters.AddWithValue("fullName", model.refDepartment.idDepartment);
            cmd.Parameters.AddWithValue("fullName", model.salary);
            cmd.Parameters.AddWithValue("fullName", model.startDate);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            int rows = await cmd.ExecuteNonQueryAsync();

            if (rows > 0)
                return true;
            else
                return false;
        }
    }

    public async Task<bool> DeleteMemberModel(int idEmployee)
    {
        using (var connection = new SqlConnection(_sqlString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_deleteEmployee", connection);
            cmd.Parameters.AddWithValue("idEmployee", idEmployee);

            int rows = await cmd.ExecuteNonQueryAsync();

            if (rows > 0)
                return true;
            else
                return false;
        }
    }
}
