namespace mvcCRUD.Models;

public class Employee
{
    public int idEmployee { get; set; }

    public string fullName { get; set; }

    public Department refDepartment { get; set;}

    public int salary { get; set; }

    public string startDate { get; set;}
}
