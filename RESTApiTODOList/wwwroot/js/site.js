
// api call to get list of todos
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

// foo which creates divs with given list
// from the ajax call
function appendTodos(todos) {
    var mainContainer = document.getElementById("todoContainer");
    mainContainer.innerHTML = "";

    for (var i = 0; i < todos.length; i++) {
        var div = document.createElement("div");
        var currentTaskId = todos[i].id;
        div.innerHTML = 'Task: ' + todos[i].name + '<input type="button" value="X" onClick="deleteTodo(\'' + currentTaskId + '\')" />';
        mainContainer.appendChild(div);
    }
}

// function that's making a post call through ajax
// and deletes tapped item by given id
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

// this is kind of callback
// for the given id form
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