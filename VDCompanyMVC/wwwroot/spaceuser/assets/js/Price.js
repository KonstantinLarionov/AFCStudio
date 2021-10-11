var table = document.querySelector('table');
var servicearr = [];
var filtered  = [];


table.addEventListener('click', function (e) {
    e.target.classList.toggle('highlite')
})

function AddService(id_op, Price, Name) {
    
    let service = { id: id_op, Price: Price, Name: Name };
    var check = 1;
    var Priceall = 0;
    if (servicearr.length == 0 ||( servicearr[0] == undefined && servicearr.length == 1)) {
        servicearr.push(service);
    }
    else {
        for (var i in servicearr) {
            if (servicearr[i].Price == service.Price) {
                delete servicearr[i];
                check = 0;
            }
        }
        if (check == 1) {
            servicearr.push(service);
        }
    }
   
    
    
   filtered = servicearr.filter(function (el) {
        return el != null;
    });
    for (var i in filtered) {
        Priceall += filtered[i].Price;
    }
   document.querySelector('#price').innerHTML = '';
    document.querySelector('#price').innerHTML = "Итого к оплате:   " + Priceall;

    

   
        $("#buttonhidden").css("visibility", "visible");
        $("#buttonpayready").css("visibility", "visible");
        $("#table").css("visibility", "visible");
        $("#price").css("visibility", "visible");
        var tbody = document.getElementById('table');

        for (var i = 0; i < filtered.length; i++) {
            var tr = document.createElement('tr');
            tr.innerHTML =
                '<td>' + filtered[i].Name + '</td>' +
                '<td>' + filtered[i].Price + '</td>';

        }
        tbody.appendChild(tr);


    
    $("#buttonhidden").click(function (event) {
        $("#text").text("Для оплаты введите данные ниже реквезиты и нажмите кнопку я оплатил  ");
        $("#buttonpayready").css("visibility", "visible");
    })
  
}
 

function CreateService() {
    $.ajax({
        url: '/User/CreateService',
        method: 'post',
        dataType: 'json',
        data: { filtered: filtered },
        success: function (data) {
            var a = JSON.parse(data);
            if (a.info === "success") {
                alert("Ваши данные успешно добавлены ");

            }
            else if (a.info === "error"){
                alert("Ваши данные не были добавлены обратитесь в службу поддержки  ");
            }
        },
    });
    
}

