using System.Dynamic;

public class Test
{
    public void Example1()
    {
        // пример статического связывания
        string staticText = string.Empty;
        // на строке ниже - compile-time error, у объекта staticText нет свойства Size
        // Console.WriteLine(staticText.Size);

        // пример динамического связывания
        dynamic dynamicText = staticText;
        // на строке ниже нет compile-time error, однако будет runtime-error, поскольку у объекта также нет свойства
        // но в отличие от
        Console.WriteLine(dynamicText.Size);
    }

    public void Example2()
    {
        // пример динамического связывания - мы можем присвоить несуществующему свойству объекта класса Person значения
        // не получаем compile-time error, однако будет runtime-error
        dynamic p1 = new Person();
        p1.Name = "Ivan";

        // расширяемый тип из стандартной библиотеки, члены которого могут динамически добавляться и удаляться в runtime.
        // потребитель экземпляра имеет полный контроль над ним, нет возможности определить методы на уровне класса
        dynamic p2 = new ExpandoObject();
        p2.Name = "Ivan";
        p2.LastName = "Petrov";
        Console.WriteLine($"{p2.Name} {p2.LastName}");

        // собственный тип, позволяющий расширять экземпляр новыми членами (и удалять их)
        // можно модифицировать класс определяя поведения для всех экземпляров
        dynamic p3 = new DynamicPerson();
        p3.Name = "John";
        p3.LastName = "Doe";
        Console.WriteLine($"{p3.Name} {p3.LastName}");
    }
}

public class Person {}

public class DynamicPerson : DynamicObject
{
    private readonly Dictionary<string, object> _members = new Dictionary<string, object>();

    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        if (_members.TryGetValue(binder.Name, out var member))
        {
            result = member;
            return true;
        } else 
        {
            result = null;
            return false;
        }
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        _members[binder.Name] = value;
        return true;
    }
}

