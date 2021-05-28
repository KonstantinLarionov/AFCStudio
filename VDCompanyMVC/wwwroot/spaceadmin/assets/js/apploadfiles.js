var myform = new FormData();
/* Функция иммитации нажатия на fileinput для выбора нового изображения */
function addimage(elem){
    $(elem).children()[0].click(); // делаем клик по инпут-файлу
}
$('#files').on('change', function(){ // находим в элементе инпут-файл
    $('#addingfiles').empty();
    $('#addingfiles').append('<p>Выбранные файлы:</p>');
    for (let i = 0; i < this.files.length; i++) {
        myform.append('imgs', this.files[i]);
        $('#addingfiles').append('<b>' + this.files[i].name + ' (' + mathsize(this.files[i].size) + ')</b><br>');
    }
});
function clearmodal(){
    $('#phototype').find('option')[0].selected = true
    clearfiles();
}
function clearfiles() {
    $('#addingfiles').empty();
    $('#files').val('');
}
function openmodal(){
    $('#modal').modal('show');
}
function sendfile() {
    myform.append('id_case', caseid);
    //console.log(myform);
    $.ajax({
        url: '/Admin/GettingFiles',
        method: 'post',
        cache: false,
        contentType: false,
        processData: false,
        data: myform,        
        success: function (data) {

            var a = JSON.parse(data);
            console.log(a);
            if (a.status === 'success') {

                //console.log(files);
                var files = "";
                a.files.forEach(function (item) {
                    files += item.Name +", " ;
                });
                $('#message').val(files + "отправлено");
                a.files.forEach(function (item) {
                    var format = "fa-file";
                    switch (item.Type)
                    {
                        case 1: format = "fa-file-image";
                            break;
                        case 2: format = "fa-file-excel";
                            break;
                        case 3: format = "fa-file-word";
                            break;
                        case 4: format = "fa-file-pdf";
                            break;
                        case 5: format = "fa-file-audio";
                            break;
                        case 6: format = "fa-file-video";
                            break;
                    }

                    date = new Date(item.DateAdd);
                    var block = $(
                        '<a href="#" class="media file">' +
                        '<div class="card-file-thumb tx-color-03">' +
                        '<i class="far '+ format +' fa-2x"></i>' +
                        '</div>' +
                        '<div class="media-body mg-l-10">' +
                        '<span>' + item.Name + '</span><br>' +
                        '<small>' + date.getDate() + '.' + (date.getMonth() + 1) + '.' + date.getFullYear() + '</small>' +
                        '</div>' +
                        '</a>');
                    $('#uploaded-files').append(block);
                });
                send();

                // заменить на алерт
                // +  обновление файлов нва странице 
                // 
            }
            if (a.status == 'error') {
                alert('Ошибка', a.data, 'error'); // заменить на алерт
            }
        },
    });
    $.ajax({
        url: "/Admin/GettingFiles",
        cache: false,
        success: function (html) {
            $("#").html(html);
        }
    });
}
function mathsize(bytes){
    if((t = bytes) < 1024)
        return t + " bytes";
    if((t = bytes / 1024) < 1024)
        return t.toFixed(2) + " Kb";
    if((t = bytes / 1024 / 1024) < 1024)
        return t.toFixed(2) + " Mb";
    else
        return (t / 1024).toFixed(2) + " Gb";
}
$('.trash').on('click', function() {
    var id_img = $(this).attr('id_image');
    swal({
        title: "Подтверждение",
        text: "Удалить изображение?",
        buttons: {
            del: {
                text: "Удалить"
            },
            cancel_: {
                text: "Отмена"
            }
        }
    }).then((value) => {
        if (value === 'cancel_')
            return;
        if (value === 'del') {
            $.ajax({
                url: '/Admin/DelGallery',
                method: 'post',
                data: "Id=" + id_img,
                success: function(data) {
                    var a = JSON.parse(data);
                    if (a.status == 'success') {
                        swal('Успешно', a.data, 'success');
                    }
                    if (a.status == 'error') {
                        swal('Ошибка', a.data, 'error');
                    }
                }
            });
        }
    });
});