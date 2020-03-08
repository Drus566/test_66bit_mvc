"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (id, name, surname, birth, team, gender, country) {
    var man = document.getElementById(id);

    if (man) {
        man.getElementsByClassName("Name")[0].innerHTML = name;
        man.getElementsByClassName("Surname")[0].innerHTML = surname;
        man.getElementsByClassName("Gender")[0].innerHTML = gender;
        man.getElementsByClassName("Birth")[0].innerHTML = birth;
        man.getElementsByClassName("Team")[0].innerHTML = team;
        man.getElementsByClassName("Country")[0].innerHTML = country;
    } else {
        var tr = document.createElement("tr");
        tr.setAttribute('id', id);
        tr.className = "text-center";

        [name, surname, gender, birth, team, country].forEach(function (item) {
            var td = document.createElement("td");
            td.className = getName(item)[0].ToUpperCase();
            td.innerText = item;

            tr.insertAdjacentElement('afterend', td);
        });

        //var td = document.createElement("td");
        //var a = document.createElement("a")
        //<td>
        //    <a asp-action="Upsert" asp-route-id="@p.Id" asp-controller="Players" class="btn btn-outline-dark form-control">
        //        Edit
        //    </a>
        //</td>

    }
});

connection.start().then(function () {
    console.log('Start connection');
}).catch(function (err) {
    return console.error(err.toString());
});
