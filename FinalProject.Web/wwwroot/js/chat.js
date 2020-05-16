"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
//document.querySelector("#sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, content) {
    //var msg = content.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    //var encodedMsg = user + " says " + msg;
    //var li = document.createElement("li");
    //li.textContent = encodedMsg;
    //document.getElementById("messageList").appendChild(li);
    
    var d = new Date();
    var date = d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
    var msg = content;
    var kullanici = user;
    
    document.querySelector("#messageList").innerHTML += "<li class='left clearfix'>" +
        "<span class='chat-img pull-left'>" +
        "<img width='40' height='40' src='~/media/user/@Url.Content(Model.User.ImagePath)'>"+
        "</span>" +
        "<div class='chat-body clearfix' style='color: white'>" +
        " <div class='header'>" +
        "<strong class='primary-font' style='color:blue'>" + kullanici + "</strong>" + "<small class='pull-right text-muted'>" +
        "<span class='glyphicon glyphicon - time'>" + "</span>" + date +
        " </small >" +
        "</div >" +
        " <p style='color:white'>" +
        msg +
        "</p >" +
        "</div >" +
        " </li >";

});

connection.start().then(function () {
    document.querySelector("#sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.querySelector("#sendButton").addEventListener("click", function (event) {
    var user = document.querySelector("#userInput").value;
    var content = document.querySelector("#messageInput").value;
    var recipientId = document.querySelector("#recipient").value;
    
    connection.invoke("Send", user, content, recipientId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});