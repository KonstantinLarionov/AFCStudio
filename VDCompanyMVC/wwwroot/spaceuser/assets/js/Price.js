﻿var table = document.querySelector('table');
var servicearr = [];
var filtred = [];


table.addEventListener('click', function (e) {
    e.target.classList.toggle('highlite')
})

function AddService(id_op, price, name) {
    
    let service = { id: id_op, price: price, name: name };
    var check = 1;
    var priceall = 0;
    if (servicearr.length == 0 ||( servicearr[0] == undefined && servicearr.length == 1)) {
        servicearr.push(service);
    }
    else {
        for (var i in servicearr) {
            if (servicearr[i].price == service.price) {
                delete servicearr[i];
                check = 0;
            }
        }
        if (check == 1) {
            servicearr.push(service);
        }
    }
    
    
    var filtered = servicearr.filter(function (el) {
        return el != null;
    });
    for (var i in filtered) {
        priceall += filtered[i].price;
    }
   document.querySelector('#price').innerHTML = '';
    document.querySelector('#price').innerHTML = "Итого к оплате:   "  + priceall;
    $("#click_button").click(function (event) {
        $("#buttonhidden").css("visibility", "visible");
        $("#table").css("visibility", "visible");
        $("#price").css("visibility", "visible");
     var tbody = document.getElementById('table');

        for (var i = 0; i < filtered.length; i++) {
            var tr = document.createElement('tr');
            tr.innerHTML =
                '<td>' + filtered[i].name + '</td>' +
                '<td>' + filtered[i].price + '</td>';
           
       }
       tbody.appendChild(tr);
    })
    $("#buttonhidden").click(function (event) {
        $("#text").text("Для оплаты введите данные ниже реквезиты и нажмите кнопку я оплатил  ");
        $("#buttonpayready").css("visibility", "visible");
    })
    
}

function CreateService() {
    var filtered_ = servicearr.filter(function (el) {
        return el != null;
    });
            $.ajax({
            url: '/User/CreateService',
            method: 'post',
            data: { filtered: filtered },
            success: function (data) {
                var a = JSON.parse(data);
                if (a.status == "not_authorized") { // слетела сессия, обновим страничку - выкинет на авторизацию
                    window.location.reload();
                    return;
                }
                if (a.status == "success") {
                    location.reload();
                }
            },
        });
        alert("Готово");
}

