using System.ComponentModel.DataAnnotations;

namespace OOAP.Poly_Inh;

internal sealed class TaxonomyConflict
{
    public static void StartExample()
    {
                
    }
}

public class PersonTaxonomyProblem
{
    public bool IsResident {get; private set;}
}

public class PersonFixed
{
    public NdsResident Resident {get; set;}
}

public abstract class NdsResident
{
    public abstract decimal GetPercent();
}

public sealed class RusResident : NdsResident
{
    public override decimal GetPercent()
    {
        return 13;
    }
}

public sealed class SerbResident : NdsResident
{
    public override decimal GetPercent()
    {
        return 25;
    }
}

public class NdsCalculator
{
    // Логика, рассчитанная на флаг
    // - например изначально расчет делался только для одной страны
    public decimal CalculateWrong(PersonTaxonomyProblem person)
    {
        decimal percent;
        // Неправильно
        if (person.IsResident)
        {
            // calculate percent for resident
            percent = 13;
        }
        else 
        {
            // calculate percent for non resident
            percent = 30;
        }

        return CalculateNds(percent);
    }
    
    public decimal Calculate(PersonFixed person)
    {
        decimal percent = person.Resident.GetPercent();
        return CalculateNds(percent);
    }
    
    private decimal CalculateNds(decimal percent)
    {
        // some logic
        return decimal.Zero;
    }
}