
(function ()
{
    $.connection.hub.start()
        .done(function ()
        {
            $.connection.myHub.server.addRows();
        })
        .fail(function ()
        {
            alert("SignalR connection ERROR!");
        });

    $.connection.myHub.client.add = function (data)
    {
        var table = document.getElementById("myTable");

        var rowCount = table.rows.length;
        var row = table.insertRow(rowCount);

        var number = row.insertCell(0);
        var date = row.insertCell(1);
        var user = row.insertCell(2);
        var parking = row.insertCell(3);
        var type = row.insertCell(4);

        number.innerHTML = rowCount;
        date.innerHTML = new Date(data.dateTime).toLocaleString();
        user.innerHTML = data.userName;
        parking.innerHTML = data.parkingName;
        type.innerHTML = data.parkingType;
    }
})()