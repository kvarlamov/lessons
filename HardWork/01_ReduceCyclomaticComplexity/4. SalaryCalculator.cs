namespace HardWork._01_ReduceCyclomaticComplexity;

using System;

#region Before

public class Employee
{
    public string Position { get; set; }
    public decimal BaseSalary { get; set; }
    public int YearsOfExperience { get; set; }
    public int OvertimeHours { get; set; }
    public bool HasCertification { get; set; }
    public string PerformanceGrade { get; set; }
    public int CompletedProjects { get; set; }
    public bool IsRemote { get; set; }
}

public class SalaryCalculator
{
    public decimal CalculateSalary(Employee employee)
    {
        decimal salary = employee.BaseSalary;
        
        // Бонус за опыт
        if (employee.YearsOfExperience > 10)
        {
            salary += salary * 0.15m;
        }
        else if (employee.YearsOfExperience > 5)
        {
            salary += salary * 0.10m;
        }
        else if (employee.YearsOfExperience > 2)
        {
            salary += salary * 0.05m;
        }

        // Бонус за позицию
        switch (employee.Position)
        {
            case "Manager":
                salary += 2000;
                if (employee.CompletedProjects > 5)
                {
                    salary += 1000;
                }
                break;
            case "SeniorDeveloper":
                salary += 1500;
                if (employee.HasCertification)
                {
                    salary += 500;
                }
                break;
            case "Developer":
                salary += 800;
                break;
            case "JuniorDeveloper":
                salary += 300;
                break;
            default:
                salary += 100;
                break;
        }

        // Бонус за производительность
        if (employee.PerformanceGrade == "A")
        {
            salary += salary * 0.10m;
        }
        else if (employee.PerformanceGrade == "B")
        {
            salary += salary * 0.05m;
        }
        else if (employee.PerformanceGrade == "C")
        {
            // без бонуса
        }
        else if (employee.PerformanceGrade == "D")
        {
            salary -= salary * 0.05m;
        }

        // Оплата сверхурочных
        if (employee.OvertimeHours > 0)
        {
            if (employee.Position == "Manager")
            {
                salary += employee.OvertimeHours * 50;
            }
            else if (employee.Position == "SeniorDeveloper")
            {
                salary += employee.OvertimeHours * 40;
            }
            else
            {
                salary += employee.OvertimeHours * 30;
            }
        }

        // Удаленная работа
        if (employee.IsRemote)
        {
            if (employee.Position == "Manager" || employee.Position == "SeniorDeveloper")
            {
                salary -= 500; // снижение за удаленную работу для старших позиций
            }
            else
            {
                salary += 200; // бонус за удаленную работу для младших позиций
            }
        }

        return Math.Max(salary, employee.BaseSalary); // гарантированный минимум
    }
}

#endregion

#region After

/* Исходная цикломатическая сложность метода CalculateSalary = 27
 * Измененная ЦС указанного метода = 1
 * Приёмы:
 * 1) динамическое добавление чистой функциональности
 * Создан словарь, в котором по ключу (имя поля Employee) хранятся функции, отвечающие за расчёт данного поля
 * Имеется функция добавления функций для расчёта
 * 
 * 2) Выполнен рефакторинг Employee - Position из строкового поля заменено на свойство Position,
 *  которое с помощью полиморфизма определяет модификаторы для расчёта для каждого отдельного класса - позиции
 *
 * Данный приём можно было бы масштабировать и на другие поля, однако для упрощения примера не стал этого делать.
 */

public class EmployeeV2
{
    public Position Position { get; set; }
    public decimal BaseSalary { get; set; }
    public int YearsOfExperience { get; set; }
    public int OvertimeHours { get; set; }
    
    public string PerformanceGrade { get; set; }
    public int CompletedProjects { get; set; }
    public bool IsRemote { get; set; }
}

public abstract class Position
{
    public bool HasSertification { get; set; }

    public abstract double GetOvertimeMultiplier();
    public abstract double GetCompletedProjectsMultiplier();
    public abstract double GetSertificatesMultiplier();
}

public class Manager : Position
{
    public override double GetOvertimeMultiplier()
    {
        throw new NotImplementedException();
    }

    public override double GetCompletedProjectsMultiplier()
    {
        throw new NotImplementedException();
    }

    public override double GetSertificatesMultiplier()
    {
        throw new NotImplementedException();
    }
}

public class Senior : Position
{
    public override double GetOvertimeMultiplier()
    {
        throw new NotImplementedException();
    }

    public override double GetCompletedProjectsMultiplier()
    {
        throw new NotImplementedException();
    }

    public override double GetSertificatesMultiplier()
    {
        throw new NotImplementedException();
    }
}

public class Middle : Position
{
    public override double GetOvertimeMultiplier()
    {
        throw new NotImplementedException();
    }

    public override double GetCompletedProjectsMultiplier()
    {
        throw new NotImplementedException();
    }

    public override double GetSertificatesMultiplier()
    {
        throw new NotImplementedException();
    }
}

public class Junior : Position
{
    public override double GetOvertimeMultiplier()
    {
        throw new NotImplementedException();
    }

    public override double GetCompletedProjectsMultiplier()
    {
        throw new NotImplementedException();
    }

    public override double GetSertificatesMultiplier()
    {
        throw new NotImplementedException();
    }
}

public class SalaryCalculatorV2
{
    private readonly Dictionary<string, Func<EmployeeV2, decimal>> _salaryFunc = new();

    public SalaryCalculatorV2()
    {
        _salaryFunc.Add(nameof(EmployeeV2.YearsOfExperience), CalculateByExperience);
        _salaryFunc.Add(nameof(EmployeeV2.Position), CalculatePosition);
        _salaryFunc.Add(nameof(EmployeeV2.PerformanceGrade), CalculateByPerformanceGrade);
        _salaryFunc.Add(nameof(EmployeeV2.OvertimeHours), CalculateByOverTimeHours);
        _salaryFunc.Add(nameof(EmployeeV2.IsRemote), CalculateByRemote);
    }

    public void AddFunc(string key, Func<EmployeeV2, decimal> func)
    {
        _salaryFunc.Add(key, func);
    }
    
    public decimal CalculateSalary(EmployeeV2 employee)
    {
        decimal salary = employee.BaseSalary;
        foreach (var key in _salaryFunc.Keys)
        {
            salary += _salaryFunc[key](employee);
        }
        
        return Math.Max(salary, employee.BaseSalary);
    }

    private decimal CallFunction(string key, EmployeeV2 employee)
    {
        if (!_salaryFunc.TryGetValue(key, out var func))
        {
            throw new Exception($"Function for calculate {key} doesn't exist");
        }
        
        return func(employee);
    }

    private decimal CalculateByExperience(EmployeeV2 employee)
    {
        decimal salary = employee.BaseSalary;
        switch (employee.YearsOfExperience)
        {
            case > 10:
                salary += salary * 0.15m;
                break;
            case > 5:
                salary += salary * 0.10m;
                break;
            case > 2:
                salary += salary * 0.05m;
                break;
        }
        
        return salary;
    }

    private decimal CalculatePosition(EmployeeV2 employee)
    {
        decimal salary = employee.BaseSalary;
        salary *= (decimal)employee.Position.GetOvertimeMultiplier();
        salary *= (decimal)employee.Position.GetCompletedProjectsMultiplier();
        salary *= (decimal)employee.Position.GetSertificatesMultiplier();
        
        return salary;
    }

    private decimal CalculateByOverTimeHours(EmployeeV2 employee)
    {
        decimal salary = employee.BaseSalary;
        salary *= employee.OvertimeHours * (decimal)employee.Position.GetOvertimeMultiplier();
        return salary;
    }

    private decimal CalculateByPerformanceGrade(EmployeeV2 employee)
    {
        decimal salary = employee.BaseSalary;
        switch (employee.PerformanceGrade)
        {
            case "A":
                salary += salary * 0.10m;
                break;
            case "B":
                salary += salary * 0.05m;
                break;
            case "C":
                // без бонуса
                break;
            case "D":
                salary -= salary * 0.05m;
                break;
        }
        
        return salary;
    }

    private decimal CalculateByRemote(EmployeeV2 employee)
    {
        decimal salary = employee.BaseSalary;
        if (!employee.IsRemote) return employee.BaseSalary;
        if (employee.Position is  Manager || employee.Position is Senior)
        {
            salary -= 500; // снижение за удаленную работу для старших позиций
        }
        else
        {
            salary += 200; // бонус за удаленную работу для младших позиций
        }

        return salary;
    }
}

#endregion