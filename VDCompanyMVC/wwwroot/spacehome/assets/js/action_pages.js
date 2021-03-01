function send_request()
{
    var message = document.getElementById('message1').value;
    var name = document.getElementById('name1').value;
    var email = document.getElementById('email1').value;
    var type = document.getElementById('subject1').value;
    var req = getXmlHttp();         var arr = '';



    req.open('GET', 'scripts/php_scr/send_request.php?name=' + name + '&message=' + message + '&email=' + email + '&type=' + type, true);
    req.send();

    req.onreadystatechange = function ()
    {
        if (req.readyState == 4)
        {
            if (req.status == 200)
            {
                arr = req.responseText;
                if (arr == true)
                {
                    document.getElementById('sendmessage').style.display= 'block';
                    document.getElementById('errormessage').style.display= 'none';
                }
                else
                {
                    document.getElementById('errormessage').style.display= 'block';
                    document.getElementById('sendmessage').style.display= 'none';
                }
            }
        }
    }
}
