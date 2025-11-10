namespace HardWork._01_ReduceCyclomaticComplexity;

using System;
using System.Collections.Generic;
using System.Linq;

#region Before

public class User
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }
    public bool IsActive { get; set; }
    public DateTime RegistrationDate { get; set; }
}

public class UserValidator
{
    public ValidationResult ValidateUser(User user)
    {
        var result = new ValidationResult();
        
        // Валидация имени пользователя
        if (string.IsNullOrEmpty(user.Username))
        {
            result.AddError("Username cannot be empty");
        }
        else if (user.Username.Length < 3)
        {
            result.AddError("Username must be at least 3 characters");
        }
        else if (user.Username.Length > 20)
        {
            result.AddError("Username cannot exceed 20 characters");
        }
        else if (!user.Username.All(char.IsLetterOrDigit))
        {
            result.AddError("Username can only contain letters and numbers");
        }

        // Валидация email
        if (string.IsNullOrEmpty(user.Email))
        {
            result.AddError("Email cannot be empty");
        }
        else if (!user.Email.Contains("@"))
        {
            result.AddError("Invalid email format");
        }
        else if (user.Email.Split('@').Length != 2)
        {
            result.AddError("Invalid email format");
        }

        // Валидация пароля
        if (string.IsNullOrEmpty(user.Password))
        {
            result.AddError("Password cannot be empty");
        }
        else
        {
            if (user.Password.Length < 8)
            {
                result.AddError("Password must be at least 8 characters");
            }
            
            bool hasUpper = user.Password.Any(char.IsUpper);
            bool hasLower = user.Password.Any(char.IsLower);
            bool hasDigit = user.Password.Any(char.IsDigit);
            
            if (!hasUpper || !hasLower || !hasDigit)
            {
                result.AddError("Password must contain uppercase, lowercase letters and digits");
            }
        }

        // Валидация возраста
        if (user.Age < 13)
        {
            result.AddError("User must be at least 13 years old");
        }
        else if (user.Age > 120)
        {
            result.AddError("Invalid age");
        }
        else if (user.Age < 18)
        {
            result.AddWarning("User is under 18 - parental consent recommended");
        }

        // Проверка страны
        if (!string.IsNullOrEmpty(user.Country))
        {
            string[] restrictedCountries = { "CountryA", "CountryB", "CountryC" };
            if (restrictedCountries.Contains(user.Country))
            {
                result.AddError("Registration not allowed from this country");
            }
        }

        return result;
    }
}

#endregion

public class ValidationResult
{
    public List<string> Errors { get; set; } = new List<string>();
    public List<string> Warnings { get; set; } = new List<string>();
    
    public void AddError(string error) => Errors.Add(error);
    public void AddWarning(string warning) => Warnings.Add(warning);
    public bool IsValid => Errors.Count == 0;
}

#region After
/*
 * Исходная ЦС метода ValidateUser = 20
 * Финальная ЦС метода ValudateUser = 1
 * Приёмы:
 * 1) Использован паттерн спецификация (+ полиморфизм) -- создан базовый класс ValidationRule,
 * от которого наследуются все правила проверки
 * Также сделан класс UserValidationHandler, агрегирующий в себе правила валидации и при вызове метода Validate выполняющий
 * валидацию всех необходимых полей
 * 2) в методе AddRule используется Ad-hoc полиморфизм -- принимается базовый класс ValidationRule, по факту являющийся
 * любым из правил валидации
 *
 * 3) Убраны все else (в одном месте сохранен switch для удобства)
 * 4) Замена примитивных значений структурами -- данный приём лишь шаг на пути к финальному рефакторингу
 * -- можно было бы правила валидации вынести в сами структуры валидировать их при создании,
 * а в каждом ValidationRule лишь вызывать метод проверки внутренних правил класса (или получать статус)
 *
 * Open-Closed принцип -- код может расширяться новыми правилами валидации без изменения текущих правил
 */

public class UserV2
{
    public UserName Username { get; set; }
    public Email Email { get; set; }
    public Password Password { get; set; }
    public Age Age { get; set; }
    public Country Country { get; set; }
    public bool IsActive { get; set; }
    public DateTime RegistrationDate { get; set; }
}

public record struct UserName(string Value);
public record struct Email(string Value);
public record struct Password(string Value);
public record struct Age(int Value);
public record struct Country(string Value);

public abstract class UserValidationRule
{
    public abstract void Validate(UserV2 user, ValidationResult result);
}

public sealed class UserNameValidationRule : UserValidationRule
{
    public override void Validate(UserV2 user, ValidationResult result)
    {
        if (string.IsNullOrEmpty(user.Username.Value))
        {
            result.AddError("Username cannot be empty");
            return;
        }
        
        ValidateLengtg(user.Username, result);
        
        if (!user.Username.Value.All(char.IsLetterOrDigit))
        {
            result.AddError("Username can only contain letters and numbers");
        }
    }

    private void ValidateLengtg(UserName name, ValidationResult result)
    {
        if (name.Value.Length < 3)
        {
            result.AddError("Username must be at least 3 characters");
        }
        
        if (name.Value.Length > 20)
        {
            result.AddError("Username cannot exceed 20 characters");
        }
    }
}

public sealed class EmailValidationRule : UserValidationRule
{
    public override void Validate(UserV2 user, ValidationResult result)
    {
        if (string.IsNullOrEmpty(user.Email.Value))
        {
            result.AddError("Email cannot be empty");
            return;
        }
        
        if (!user.Email.Value.Contains("@"))
        {
            result.AddError("Invalid email format");
        }
        
        if (user.Email.Value.Split('@').Length != 2)
        {
            result.AddError("Invalid email format");
        }
    }
}

public sealed class PasswordValidationRule : UserValidationRule
{
    public override void Validate(UserV2 user, ValidationResult result)
    {
        ValidatePassword(user.Password, result);
    }

    private void ValidatePassword(Password password, ValidationResult result)
    {
        if (string.IsNullOrEmpty(password.Value))
        {
            result.AddError("Password cannot be empty");
            return;
        }
        
        if (password.Value.Length < 8)
        {
            result.AddError("Password must be at least 8 characters");
        }
            
        bool hasUpper = password.Value.Any(char.IsUpper);
        bool hasLower = password.Value.Any(char.IsLower);
        bool hasDigit = password.Value.Any(char.IsDigit);
            
        if (!hasUpper || !hasLower || !hasDigit)
        {
            result.AddError("Password must contain uppercase, lowercase letters and digits");
        }
    }
}

public sealed class AgeValidationRule : UserValidationRule
{
    public override void Validate(UserV2 user, ValidationResult result)
    {
        switch (user.Age.Value)
        {
            case < 13:
                result.AddError("User must be at least 13 years old");
                break;
            case > 120:
                result.AddError("Invalid age");
                break;
            case < 18:
                result.AddWarning("User is under 18 - parental consent recommended");
                break;
        }
    }
}

public sealed class CountryValidationRule : UserValidationRule
{
    public override void Validate(UserV2 user, ValidationResult result)
    {
        if (string.IsNullOrEmpty(user.Country.Value))
        {
            result.AddError("Country cannot be empty");
            return;
        }
        
        string[] restrictedCountries = { "CountryA", "CountryB", "CountryC" };
        if (restrictedCountries.Contains(user.Country.Value))
        {
            result.AddError("Registration not allowed from this country");
        }
    }
}

public sealed class UserValidationHandler : UserValidationRule
{
    private readonly List<UserValidationRule> _rules = new List<UserValidationRule>();
    
    public void AddRule(UserValidationRule rule)
    {
        _rules.Add(rule);
    }
    
    public override void Validate(UserV2 user, ValidationResult result)
    {
        foreach (var rule in _rules)
        {
            rule.Validate(user, result);
        }
    }
}


public class UserValidatorV2
{
    public ValidationResult ValidateUser(UserV2 user)
    {
        var validationHandler = new UserValidationHandler();
        validationHandler.AddRule(new UserNameValidationRule());
        validationHandler.AddRule(new EmailValidationRule());
        validationHandler.AddRule(new PasswordValidationRule());
        validationHandler.AddRule(new AgeValidationRule());
        validationHandler.AddRule(new CountryValidationRule());
        
        var result = new ValidationResult();
        validationHandler.Validate(user, result);

        return result;
    }
}



#endregion