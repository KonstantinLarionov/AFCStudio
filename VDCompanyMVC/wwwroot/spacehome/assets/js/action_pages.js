function send_request(userName, userEmail, subject, text)
{
    return fetch(`https://api.telegram.org/bot7913274696:AAH-Zbe4Zq4e2mr-5bnoelcXuKsg92jPmlc/sendMessage?chat_id=-1002634874824&text=%0AНовая%20заявка%20от%0A${encodeURIComponent(userName)}%20|%20${encodeURIComponent(userEmail)}.%0AОбласть\\Тема:%0A${encodeURIComponent(subject)}%0A${encodeURIComponent(text)}`);
}