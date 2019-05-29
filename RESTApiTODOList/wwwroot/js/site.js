
// GET ALL
function getTodos() {
    $.ajax({
        url: '/api/todo/',
        type: 'GET',
        dataType: 'json',
        success: function (todoItems) {
            appendTodos(todoItems);
        }
    });
}

// GET ALL foo
function appendTodos(todos) {

    // get containers from html
    var comboBox = document.getElementById('selectItemsComboBox');
    var mainContainer = document.getElementById("todoContainer");
    var updatedInput = document.getElementById('updatedNameInput');

    // clear it's content
    comboBox.options.length = 0;
    mainContainer.innerHTML = "";
    updatedInput.value = "";

    // go through each retrieved object 
    // create div list for each of them
    // and add these objects to combobox
    for (var i = 0; i < todos.length; i++) {

        // creates divs with btns
        var div = document.createElement("div");
        var currentTaskId = todos[i].id;
        div.innerHTML = 'Task: ' + todos[i].name + '<input type="button" value="X" onClick="deleteTodo(\'' + currentTaskId + '\')" />';
        mainContainer.appendChild(div);

        // appending items to the combobox
        var opt = document.createElement('option');
        opt.appendChild(document.createTextNode(todos[i].name));
        opt.value = todos[i].id;
        comboBox.appendChild(opt);
    }
}

// DELETE 
function deleteTodo(id) {
    var ans = confirm("Are you sure you want to delete this Todo item?");
    if (ans) {
        $.ajax({
            url: "/api/todo/" + id,
            type: "POST",
            method: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function () {
                getTodos();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }  
}

// PUT
function updateTodoItem() {

    // retrieve current selected index and entered data
    var comboBox = document.getElementById('selectItemsComboBox');
    var selectedTodoItemId = comboBox.options[comboBox.selectedIndex].value;
    var updatedTodoName = document.getElementById('updatedNameInput').value;

    // create an object to send through ajax POST
    var todoItemToUpdate = { Id: selectedTodoItemId, Name: updatedTodoName };

    $.ajax({
        url: "/api/todo",
        data: JSON.stringify(todoItemToUpdate),
        type: "POST",
        method: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function () {
            getTodos();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });  
}

// POST
$(function () {
    $('#add-todo-form').on("submit", function (e) {
        e.preventDefault(); 

        var todoName = $("[name='name']").val();
        var dataToPost = { Name: todoName };

        $.ajax(
            {
                type: "POST",
                data: JSON.stringify(dataToPost),
                url: "/api/todo/",
                contentType: 'application/json; charset=utf-8',
                success: function () {
                    getTodos();
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
    });
});

// this will make getTodos() run once
// the page is loaded.
$(document).ready(function () {
    debugger
    getTodos();
});