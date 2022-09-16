const btnHander = (btn, handler) => btn.click(handler);

function findRec(id) {
    let tempUrl = url + '/' + id;//Append the url to pass the id as Query string. 
    $.get(tempUrl, (data) => {
        alert(data.EmpName)
    })
}

function populateData(data) {
    $("table").find("tr:gt(0)").remove();
    $.each(data, function (index, element) {
        let row = `<tr><td>${element.EmpID}</td><td>${element.EmpName}</td><td>${element.EmpAddress}</td><td>${element.EmpSalary}</td><td><a href="#" onClick="findRec(${element.EmpID})">Edit</td></tr>`;
        $("table").append(row);
    })
}

function deleteRec(id) {
    let tempUrl = url + '/' + id;//Append the url to pass the id as Query string. 
    $.ajax({
        "method": "DELETE",
        "url": tempUrl,
        "success": (msg) => alert(msg)
    })
}

function createEmpObject() {
    const obj = {};
    obj.EmpName = $("#txtName").val();
    obj.EmpAddress = $("#txtAddress").val();
    obj.EmpSalary = $("#txtSalary").val();
    obj.DeptId = $("#txtDept").val();
    obj.DateOfBirth = $("#txtDob").val();
    return obj;
}