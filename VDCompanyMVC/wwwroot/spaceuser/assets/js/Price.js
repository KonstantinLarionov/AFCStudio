var table = document.querySelector('table');
var servicearr = [];

table.addEventListener('click', function (e) {
    e.target.classList.toggle('highlite')
})

function AddService(id_op, price) {
    let service = { id: id_op, price: price };
    var check = 1;
    var priceall = 0;
    if (servicearr.length == 0 ||( servicearr[0] == undefined && servicearr.length == 1)) {
        servicearr.push(service)
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
    document.querySelector('#price').innerHTML = priceall;
    console.log(filtered);
}


