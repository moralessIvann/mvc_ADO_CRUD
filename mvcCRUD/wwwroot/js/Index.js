const employeeModel = {
    idEmployee: 0,
    fullName: "",
    idDepartment: 0,
    salary: 0,
    startDate: ""
}

function displayEmployees() {
    fetch("/Home/EmployeesList").then(response => {
        return response.ok ? response.json(): Promise.reject(response)
    })
        .then(responseJson => {
            if (responseJson.length > 0) {
                $("#EmployeesTable tbody").html("");
                responseJson.forEach((employee) => {
                    $("#EmployeesTable tbody").append(
                        $("<tr>").append(
                            $("<td>").text(employee.fullName),
                            $("<td>").text(employee.refDepartment.departmentName),
                            $("<td>").text(employee.salary),
                            $("<td>").text(employee.startDate),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm btn-edit-employee").text("Edit").data("employeeData", employee),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 btn-delete-employee").text("Delete").data("employeeData", employee),
                            )
                        )
                    )
                })
            }
        })
}

document.addEventListener("DOMContentLoaded", function () {
    displayEmployees();

    fetch("/Home/DepartmentsList").then(response => {
        return response.ok ? response.json() : Promise.reject(response)
    })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("cboDepartmen").append(
                        $("<option>").val(item.idDepartment).text(item.departmentName)
                    )
                })
            }
        })

}, false)