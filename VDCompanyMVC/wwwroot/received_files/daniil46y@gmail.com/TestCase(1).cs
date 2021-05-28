//Не используемые юзинги можно убрать
using System.Data.SqlServerCe;
//Если юзинг относится не к системным, отделять строкой от других 
//Можно групировать юзинги по типу между собой если они не системные и отделять строкой от других 

//Оформление архитектуры идет отдельно однако следить за тем что бы класс находил в пространстве имен равной директории в которой он находится 
namespace TestProj.UX
{ 

    /// <summary>
    /// Даже если и пустой то все равно добавить -> /// к каждому методу и классу
    /// </summary>
    public class TestCase
    {
        /// <summary>
        /// Публичные свойства пишутся так
        /// </summary>
        public string PublicProps { get; set; }
        /// <summary>
        /// У публичных полей всегда есть значение и прописываются сначала с _потомСМаленькойВерблюжьей
        /// </summary>
        public string _publicField = string.Empty;
        /// <summary>
        /// Приватные поля без модификатора private так как он не нужен
        /// Поля класса без модификаторов по умолчанию private
        /// Поля интерфейсов по умолчанию public. не перепутать.
        /// </summary>
        string PrivateProps { get; set; }
        /// <summary>
        /// Передача параметров идет с использованием snake_case
        /// А внутри методов промежуточные элементы с помощью cameCase с маленькой буквы  
        /// </summary>
        public TestCase(string delegating_field, string delegating_field_trade)
        {
            //По возможности избегать var в generic типах
            //Если тип не generic а например List<> или Dictionary то использование var нормально.
            string delegField = delegating_field;
            string delegTrade = delegating_field_trade;
        }
        /// <summary>
        /// Приватные методы тоже без модификатора private
        /// </summary>
        /// <returns></returns>
        void PrivateVoidMethode(string delegating_field, string delegating_field_trade)
        {
            //По возможности избегать var в generic типах
            //Если тип не generic а например List<> или Dictionary то использование var нормально.
            string delegField = delegating_field;
            string delegTrade = delegating_field_trade;
            
            PublicField = delegField;
            PrivateProps = delegTrade;
        }
        /// <summary>
        /// Публичный метод
        /// </summary>
        /// <returns></returns>
        public void PublicVoidMethode(string delegating_field, string delegating_field_trade)
        {
            //По возможности избегать var в generic типах
            //Если тип не generic а например List<> или Dictionary то использование var нормально.
            string delegField = delegating_field;
            string delegTrade = delegating_field_trade;
            
            PublicField = delegField;
            PrivateProps = delegTrade;
        }

        /// <summary>
        /// Однострочные методы которые возвращают объекты писать через лямбду
        /// </summary>
        /// <returns></returns>
        string ReturningMethode() => string.Empty;
        
        //Регионы использовать всегда даже если кода не много. (это его по началу не много)
        
        //Регион отвечающий за поля класса публичные и приватные
        #region [Fields]
        #endregion
        //Регион отвечающий за приватные свойства класса 
        #region [PrivateProps]
        #endregion
        //Регион отвечающий за публичные свойства класса 
        #region [PublicProps]
        #endregion
        //Регион отвечающий за конструктор 
        #region [Constructor]
        #endregion
        //Регион отвечающий за публичные методы класса 
        #region [PublicMethods]
        #endregion
        //Регион отвечающий за приватные методы класса 
        #region [PrivateMethods]
        #endregion
        //Регион отвечающий за лямбда-методы  
        #region [ReturnMethods]
        #endregion
        //Регион отвечающий за вспомогательные не основные методы класса   
        #region [Helpers]
        #endregion
        
        //Последовательность полей и методов и свойств класса такая как последовательность регионов выше
        
    }
}