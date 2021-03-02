using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDCompanyMVC.Models
{
    public static class Conf
    {                                                           

#if DEBUG
        public static string ConnectDb { get; private set; } = "Server=localhost;Database=afcstand;User=root;Password=root;";
        public static string DBChat { get; private set; } = "Server=localhost;Database=afcstand;User=root;Password=root;";
#else

        public static string ConnectDb { get; private set; } = "Server=localhost;Database=u0967433_afc;User=u0967_vd;Password=$lI2bg26;";
        public static string DBChat { get; private set; } = "Server=localhost;Database=u0967433_afc_chat;User=u0967_vd;Password=$lI2bg26;";
#endif
    }
    public static class MessagesUser
    {
        //TODO: Локализация всех сообщений
        public static string MessageCaseOk { get; private set; } = "Ваше дело передано на рассмотрение. Мы свяжемся с Вами в ближайшее время";
        public static string MessageCaseFail { get; private set; } = "Ваше дело не создано, возможны проблемы с авторизацией, обновите страницу!";
    }
}
