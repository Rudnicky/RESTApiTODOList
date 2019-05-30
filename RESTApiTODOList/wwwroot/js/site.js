
// GET
function getTodos() {
    $.ajax({
        url: '/api/todo/',
        type: 'GET',
        dataType: 'json',
        success: function (todoItems) {
            todoListSuccess(todoItems);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function todoListSuccess(todoItems) {
    // Iterate over the collection of data
    $.each(todoItems, function (index, todoItem) {
        // Add a row to the Todo table
        todoAddRow(todoItem);
    });
}

function todoAddRow(todo) {
    // Check if <tbody> tag exists, add one if not
    if ($("#todoTable tbody").length == 0) {
        $("#todoTable").append("<tbody></tbody>");
    }
    // Append row to <table>
    $("#todoTable tbody").append(
        todoBuildTableRow(todo));
}

function todoBuildTableRow(todo) {
    var ret =
        "<tr>" +
        "<td>" + "<button type='button' " +
                    "onclick='todoGet(this);' " +
                    "class='btn btn-default' " +
                    "data-id='" + todo.id + "'>" +
                    "<span class='glyphicon glyphicon-edit' />" + "</button>" + "</td >" +
        "<td>" + todo.id + "</td>" +
        "<td>" + todo.name + "</td>" + 
        "<td>" + "<button type='button' " +
                "onclick='todoItemDelete(this);' " +
                "class='btn btn-default' " +
                "data-id='" + todo.id + "'>" +
                "<span class='glyphicon glyphicon-remove' />" +
                "</button>" + "</td>" + 
        "</tr>";
    return ret;
}

function todoGet(ctl) {
    // Get todoItem id from data- attribute
    var id = $(ctl).data("id");

    // Store product id in hidden field
    $("#todoid").val(id);

    // Call Web API to get a list of Todo(s)
    $.ajax({
        url: "/api/todo/" + id,
        type: 'GET',
        dataType: 'json',
        success: function (todoItem) {
            todoToFields(todoItem);

            // Change Update Button Text
            $("#updateButton").text("Update");
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

// DELETE
function todoItemDelete(ctl) {
    var id = $(ctl).data("id");

    $.ajax({
        url: "/api/todo/" + id,
        type: 'DELETE',
        success: function (todoItem) {
            $(ctl).parents("tr").remove();
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

// PUT
function todoUpdate(todoItem) {
    $.ajax({
        url: "/api/todo",
        type: 'PUT',
        contentType:
            "application/json;charset=utf-8",
        data: JSON.stringify(todoItem),
        success: function (todoItem) {
            todoUpdateSuccess(todoItem);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function updateClick() {
    var number = document.getElementById("todoid").value;
    TodoItem = new Object();
    TodoItem.Id = number;
    TodoItem.Name = $("#todoname").val();
    if ($("#updateButton").text().trim() ==
        "Add") {
        todoAdd(TodoItem);
    }
    else {
        todoUpdate(TodoItem);
    }
}

function todoUpdateSuccess(todoItem) {
    todoUpdateInTable(todoItem);
}

function todoUpdateInTable(todoItem) {
    // Find TodoItem in <table>
    var row = $("#todoTable button[data-id='" +
        todoItem.id + "']").parents("tr")[0];
    // Add changed todoItem to table
    $(row).after(todoBuildTableRow(todoItem));
    // Remove original product
    $(row).remove();
    formClear(); // Clear form fields
    // Change Update Button Text
    $("#updateButton").text("Add");
}

// POST
function todoAdd(todoItem) {
    $.ajax({
        url: "/api/todo",
        type: 'POST',
        contentType:
            "application/json;charset=utf-8",
        data: JSON.stringify(todoItem),
        success: function (todoItem) {
            todoAddSuccess(todoItem);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function todoAddSuccess(todoItem) {
    todoAddRow(todoItem);
    formClear();
}

function formClear() {
    $("#todoname").val("");
}

function todoToFields(todoItem) {
    $("#todoname").val(todoItem.name);
}

function handleException(request, message, error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}

// this will make getTodos() run once
// the page is loaded.
$(document).ready(function () {
    //debugger
    getTodos();
});