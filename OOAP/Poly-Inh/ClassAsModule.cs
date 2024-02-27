/*
Класс как модуль в C# определяется через пространство имён - т.о. создается глобально уникальный тип.
И с помощью этого можно создавать классы, которые называются одинаково, но фактически находясь в разных пространсвах имен будут разными модулями.
И чтобы использовать нужный модуль через ключевое слово 'using' добавляем этот модуль в другой модуль (класс).
 */

namespace Domain.Entities
{
    //Например класс Doctor находится в пространстве имён namespace Domain.Entities.
    public class Doctor
    { }
}

namespace Domain.Services
{
    // Для использования Doctor в каком-то модуле (в данном примере это DoctorService) "подключим" его в модуль:
    using Domain.Entities;

    public class DoctorService
    {
        // и далее можем использовать модуль в данном сервисе
        public Doctor GetDoctor(int id)
        {
            throw new NotImplementedException();
        }
    }
}