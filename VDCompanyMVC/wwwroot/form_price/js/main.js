var b2 = 0;
var b3 = 0;
var b4 = 0;
var b5 = 0;
$(document).ready(function () {
    $("#img1").animate({opacity: 1}, 2000);
    $(window).scroll(function(){
        if(b2 === 0)
        if($(window).scrollTop() > $("#block2").offset().top - $(window).height() / 2) {
            $("#img2").animate({opacity: 1}, 2000);
            b2 = 1;
        }
        if(b3 === 0)
		if($(window).scrollTop() > $("#block3").offset().top - $(window).height() / 2) {
            $("#img3").animate({opacity: 1}, 2000);
            b3 = 1;
        }
        if(b4 === 0)
        if($(window).scrollTop() > $("#block4").offset().top - $(window).height() / 2) {
            $("#img4").animate({opacity: 1}, 2000);
            $("#team-point1").animate({left: '10%', top: '65%'}, 2000);
            $("#team-point2").animate({left: '30.5%', top: '64%'}, 2000);
            $("#team-point3").animate({left: '51.5%', top: '66%'}, 2000);
            $("#team-point4").animate({left: '77%', top: '61%'}, 2000);
            $("#team-point5").animate({left: '86%', top: '27%'}, 2000);
            b4 = 1;
        }
        if(b5 === 0)
        if($(window).scrollTop() > ($("#block5").offset().top - $(window).height() / 2)) {
            $("#img5").animate({opacity: 1}, 2000);
            b5 = 1;
        }
	});
});
$(function(){
    $("a[href^='#']").click(function(){
            var _href = $(this).attr("href");
            $("html, body").animate({scrollTop: ($(_href).offset().top - 20) + "px"});
    });
});
function blank(){
    swal("Откроется персональная страница (в новой вкладке)");
}


function chat() {
    $("#chat-icon").toggleClass('chat-hidden')
    $('#chat-wrap').toggleClass('chat-hidden');
}
function add_clientmessage() {
    var dt = new Date();
    var time = (dt.getHours() + "").padStart(2, "0") + ":" + (dt.getMinutes() + "").padStart(2, "0");
    $('#chat-list').append('<div class="chat-msg-client">' + $('#chat-message').val() + '<br><span style="font-style: italic; color: #858585; text-align: right;">' + time + '</span></div>');
    $('#chat-list').animate({scrollTop: $('#chat-list').prop('scrollHeight')}, 500);
    $('#chat-message').val('');
}
function add_adminmessage() {
    var dt = new Date();
    var time = (dt.getHours() + "").padStart(2, "0") + ":" + (dt.getMinutes() + "").padStart(2, "0");
    $('#chat-list').append('<div class="chat-msg-admin">message<br><span style="font-style: italic; color: #858585; text-align: right;">' + time + '</span></div>');
    $('#chat-list').animate({scrollTop: $('#chat-list').prop('scrollHeight')}, 500); 
}


function adminchat() {
    $("#adminchat-icon").toggleClass('adminchat-hidden')
    $('#adminchat-wrap').toggleClass('adminchat-hidden');
}
function adminadd_clientmessage() {
    var dt = new Date();
    var time = (dt.getHours() + "").padStart(2, "0") + ":" + (dt.getMinutes() + "").padStart(2, "0");
    $('#adminchat-list').append('<div class="adminchat-msg-client">' + $('#adminchat-message').val() + '<br><span style="font-style: italic; color: #858585; text-align: right;">' + time + '</span></div>');
    $('#adminchat-list').animate({scrollTop: $('#cadminhat-list').prop('scrollHeight')}, 500);
    $('#adminchat-message').val('');
}
function adminadd_adminmessage() {
    var dt = new Date();
    var time = (dt.getHours() + "").padStart(2, "0") + ":" + (dt.getMinutes() + "").padStart(2, "0");
    $('#adminchat-list').append('<div class="adminchat-msg-admin">message<br><span style="font-style: italic; color: #858585; text-align: right;">' + time + '</span></div>');
    $('#adminchat-list').animate({scrollTop: $('#adminchat-list').prop('scrollHeight')}, 500); 
}