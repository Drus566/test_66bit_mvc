"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (id, name, surname, birth, team, gender, country) {
    // Find Players table
    if (document.getElementById("PlayersList")) {
        // Find Player
        var man = document.getElementById(id);
        // If exists, then update, else add new player
        if (man) {
            man.getElementsByClassName("1")[0].innerHTML = name;
            man.getElementsByClassName("2")[0].innerHTML = surname;
            man.getElementsByClassName("3")[0].innerHTML = gender;
            man.getElementsByClassName("4")[0].innerHTML = birth;
            man.getElementsByClassName("5")[0].innerHTML = team;
            man.getElementsByClassName("6")[0].innerHTML = country;
        } else {
            var tr = document.createElement("tr");
            var tbody = document.querySelector("tbody");

            tr.setAttribute('id', id);
            tr.className = "text-center";

            [name, surname, gender, birth, team, country].forEach(function (item, index) {
                var td = document.createElement("td");

                td.className = `${index + 1}`;
                td.innerText = item;

                tr.append(td);
            });

            var editWrapper = document.createElement('td');
            var edit = document.createElement('a');

            edit.className = "btn btn-outline-dark form-control";
            edit.setAttribute("href", "/Players/Upsert/" + id);
            edit.innerText = "Edit";

            editWrapper.append(edit);

            tr.append(editWrapper);

            tbody.append(tr);
        }
    }
});

connection.start().then(function () {
    console.log('Start connection');
}).catch(function (err) {
    return console.error(err.toString());
});
