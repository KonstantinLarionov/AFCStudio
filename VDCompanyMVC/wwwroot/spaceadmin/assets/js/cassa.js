$(function datep() {

    $('#datep').datepicker();

});

$(function datepi() {

    $('#datepi').datepicker();

});

function New_operation() {
    var is_open = document.getElementById("is_open").value;
    if (is_open == 1) {

        var id_cassa = document.getElementById("id_cassa").value;
        var datap = document.getElementById("datep").value;
        var amount = document.getElementById("amount").value;
        var coment = document.getElementById("coment").value;
        var operationtype = document.getElementById("operationtype").value;
        var timelesstype = 1;
        var typeop;
        if (operationtype == 0) {
            typeop = '<span style="color: black">Не выбрано</span>'
        }
        else if (operationtype == 1) {
            typeop = '<span style="color: green">Приход</span>'
        }
        else {
            typeop = '<span style="color: orangered">Уход</span>'
        }
        $.ajax({
            url: '/Admin/NewOperation',
            method: 'post',
            data: { id_cassa: id_cassa, amount: amount, operationtype: operationtype, coment: coment, datap: datap, timelesstype: timelesstype },
            success: function (data) {
                var a = JSON.parse(data);
                if (a.status == "not_authorized") { // слетела сессия, обновим страничку - выкинет на авторизацию
                    window.location.reload();
                    return;
                }
                if (a.status == "success") {
                    var row = '<tr onclick="Del_Op(id)">' +
                        '<td scope="row">' + coment + '</td>' +
                        '<td>' + amount + '</td>' +
                        '<td>' + datap + '</td>' +
                        '<td>' + typeop + '</td>' +
                        '<td>Текущая операция</td>' +
                        //'<td>' +
                        //'<div class="btn-group w-100" role="group">' +
                        //'<button type="button" class="btn btn-outline-primary" onclick="editlawyer(' + a.id + ')">Редакт.</button>' +
                        //'<button type="button" class="btn btn-outline-danger" onclick="Remove_Lawyer(' + a.id + ')">Удалить</button>' +
                        //'</div>' +
                        //'</td>' +
                        '</tr>';
                    $('#tbody0').append(row);

                    var balance = parseFloat(document.getElementById("balancecurrent").innerHTML);
                    if (operationtype == 1) {
                        var operation = parseFloat(document.getElementById("incomecurrent").innerHTML);
                        operation += parseFloat(amount);
                        balance += parseFloat(amount);
                        document.querySelector('#incomecurrent').innerHTML = '';
                        $('#incomecurrent').append(operation);

                    }
                    else if (operationtype == 2) {
                        var operation = parseFloat(document.getElementById("consumptioncurrent").innerHTML);
                        operation += parseFloat(amount);
                        balance -= parseFloat(amount);
                        document.querySelector('#consumptioncurrent').innerHTML = '';
                        $('#consumptioncurrent').append(operation);
                    }
                    document.querySelector('#balancecurrent').innerHTML = '';
                    $('#balancecurrent').append(balance);
                }
            }
        });
    }
    else {
        alert('Касса закрыта если хотите ее изменить обратитесь к администратору')
    }
}

function New_operation_planing() {
    var is_open = document.getElementById("is_open").value;
    if (is_open == 1) {

        var id_cassa = document.getElementById("id_cassa").value;
        var datap = document.getElementById("datepi").value;
        var amount = document.getElementById("amount_planing").value;
        var coment = document.getElementById("coment_planing").value;
        var operationtype = document.getElementById("operationtype_planing").value;
        var timelesstype = 2;
        var typeop;
        if (operationtype == 0) {
            typeop = '<span style="color: black">Не выбрано</span>'
        }
        else if (operationtype == 1) {
            typeop = '<span style="color: green">Приход</span>'
        }
        else {
            typeop = '<span style="color: orangered">Уход</span>'
        }
        $.ajax({
            url: '/Admin/NewOperation',
            method: 'post',
            data: { id_cassa: id_cassa, amount: amount, operationtype: operationtype, coment: coment, datap: datap, timelesstype: timelesstype },
            success: function (data) {
                var a = JSON.parse(data);
                if (a.status == "not_authorized") { // слетела сессия, обновим страничку - выкинет на авторизацию
                    window.location.reload();
                    return;
                }
                if (a.status == "success") {
                    var row = '<tr onclick="Del_Op(id)">' +
                        '<td scope="row">' + coment + '</td>' +
                        '<td>' + amount + '</td>' +
                        '<td>' + datap + '</td>' +
                        '<td>' + typeop + '</td>' +
                        '<td>Плановая операция</td>' +
                        //'<td>' +
                        //'<div class="btn-group w-100" role="group">' +
                        //'<button type="button" class="btn btn-outline-primary" onclick="editlawyer(' + a.id + ')">Редакт.</button>' +
                        //'<button type="button" class="btn btn-outline-danger" onclick="Remove_Lawyer(' + a.id + ')">Удалить</button>' +
                        //'</div>' +
                        //'</td>' +
                        '</tr>';
                    $('#tbody1').append(row);

                    var balance = parseFloat(document.getElementById("balanceplaning").innerHTML);
                    if (operationtype == 1) {
                        var operation = parseFloat(document.getElementById("incomeplaning").innerHTML);
                        operation += parseFloat(amount);
                        balance += parseFloat(amount);
                        document.querySelector('#incomeplaning').innerHTML = '';
                        $('#incomeplaning').append(operation);

                    }
                    else if (operationtype == 2) {
                        var operation = parseFloat(document.getElementById("consumptionplaning").innerHTML);
                        operation += parseFloat(amount);
                        balance -= parseFloat(amount);
                        document.querySelector('#consumptionplaning').innerHTML = '';
                        $('#consumptionplaning').append(operation);
                    }
                    document.querySelector('#balanceplaning').innerHTML = '';
                    $('#balanceplaning').append(balance);
                }
            }
        });
    }
    else {
        alert('Касса закрыта если хотите ее изменить обратитесь к администратору')
    }
}
function newcassa() {
    $('#name').val("");
    $('#ModalLabel').text('Добавление новой кассы')
    $('#newcassa').show();
    $('#modal').modal('show');
}

function New_Cassa() {
    var name = $('#name').val(); // нет имени

    $.ajax({
        url: '/Admin/NewCassa',
        method: 'post',
        data: { name: name },
        success: function (data) {
            var a = JSON.parse(data);
            if (a.status == "not_authorized") { // слетела сессия, обновим страничку - выкинет на авторизацию
                window.location.reload();
                return;
            }
            if (a.status == "success") {
                //    var row = '<tr>' +
                //        '<th scope="row">' + a.id + '</th>' +
                //        '<td>' + fio + '</td>' +
                //        '<td>' + log + '</td>' +
                //        '<td>' + psw + '</td>' +
                //        '<td>' +
                //        '<div class="btn-group w-100" role="group">' +
                //        '<button type="button" class="btn btn-outline-primary" onclick="editlawyer(' + a.id + ')">Редакт.</button>' +
                //        '<button type="button" class="btn btn-outline-danger" onclick="Remove_Lawyer(' + a.id + ')">Удалить</button>' +
                //        '</div>' +
                //        '</td>' +
                //        '</tr>';
                //    $('tbody').append(row);
                $('#modal').modal('hide');
                location.reload();
            }
            else {
                alert("Сегодня уже была создана касса");
                $('#modal').modal('hide');
            }          
        },
    });
}

function closemodal() {
    id_case = 0;
    $('#modal').modal('hide');
}

function Closecassa() {
    var id_cassa = document.getElementById("id_cassa").value;
    var is_open = document.getElementById("is_open").value;
    if (is_open == 1) {
        $.ajax({
            url: '/Admin/CloseCassa',
            method: 'post',
            data: { id_cassa: id_cassa },
            success: function (data) {
                var a = JSON.parse(data);
                if (a.status == "not_authorized") { // слетела сессия, обновим страничку - выкинет на авторизацию
                window.location.reload();
                return;
                }
                if (a.status == "success") {
                    location.reload();
                    alert('Касса закрыта')
                }
            }
        });
    }
    else {
        alert("Касса уже закрыта")
    }
}

function closemodal1() {
    id_case = 0;
    $('#modal1').modal('hide');
}

function Del_Op(item, isopen) {
    $('#ModalLabel1').text('Удаление операции');
    $('#id_operation').text(item);
    $('#isopen').text(isopen);
    $('#closemodal').show();
    $('#deleteoperation').show();
    $('#modal1').modal('show');
}

function Delete_Operation() {
    var operation = document.getElementById("id_operation").innerText;
    var cassa = document.getElementById("id_operation").value;
    var is_open = document.getElementById("isopen").value;
    if (is_open == 1) {
        if (operation == '') {
            alert('презагрузите страницу чтобы удалить операцию')
        }
        else {
            $.ajax({
                url: '/Admin/DeleteOperation',
                method: 'post',
                data: { cassa: cassa, operation: operation },
                success: function (data) {
                    var a = JSON.parse(data);
                    if (a.status == "not_authorized") { // слетела сессия, обновим страничку - выкинет на авторизацию
                        window.location.reload();
                        return;
                    }
                    if (a.status == "success") {
                        location.reload();
                    }
                }
            });
        }
    }
    else {
        alert('Касса закрыта вы не можете удалить операции')
    }
}


