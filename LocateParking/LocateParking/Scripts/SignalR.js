
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

        var date = row.insertCell(0);
        var user = row.insertCell(1);
        var parking = row.insertCell(2);
        var type = row.insertCell(3);
        var adder = row.insertCell(4);

        date.innerHTML = new Date(data.dateTime).toLocaleString();
        user.innerHTML = data.userName;
        parking.innerHTML = data.parkingName;
        type.innerHTML = data.parkingType;
        adder.innerHTML = data.parkingAdderName;
    }

    $.connection.myHub.client.sort = function ()
    {
        sortTable(0);
    }
})()

function sortTable(n)
{
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("myTable");
    switching = true;
    dir = "asc";
    
    while (switching)
    {
        switching = false;
        rows = table.getElementsByTagName("TR");
        
        for (i = 1; i < (rows.length - 1) ; i++)
        {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            
            if (dir == "asc")
            {
                if (n == 0)
                {
                    //kontra da bi se prvo videle poslednje promene
                    if (toMyISODate(x.innerHTML.toString()) < toMyISODate(y.innerHTML.toString()))
                    {
                        shouldSwitch = true;
                        break;
                    }
                }
                else if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase())
                {
                    shouldSwitch = true;
                    break;
                }
            }
            else if (dir == "desc")
            {
                if (n == 0)
                {
                    //kontra
                    if (toMyISODate(x.innerHTML.toString()) > toMyISODate(y.innerHTML.toString()))
                    {
                        shouldSwitch = true;
                        break;
                    }
                }
                else if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase())
                {
                    shouldSwitch = true;
                    break;
                }
            }
        }

        if (shouldSwitch)
        {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        }
        else if (switchcount == 0 && dir == "asc")
        {
            dir = "desc";
            switching = true;
        }
    }
}

function toMyISODate(localString)
{
    var dateAndTime = localString.split(' ');
    var date = dateAndTime[0].split('.').reverse(); 
    var time = dateAndTime[1].split('.');
    dateAndTime = date.concat(time).join('');
    return dateAndTime;
}