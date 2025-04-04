function send_request(userName, userEmail, subject, text)
{
    fetch(`https://api.telegram.org/bot7913274696:AAH-Zbe4Zq4e2mr-5bnoelcXuKsg92jPmlc/sendMessage?chat_id=-1002634874824&text=%0AНовая%20заявка%20от%0A${userName}%20|%20${userEmail}.%0AОбласть\\Тема:%0A${subject}%0A${text}`);
}