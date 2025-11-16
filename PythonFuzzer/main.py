from validation import validate_email, validate_password, process_user_data

def main():
    print("=== Тестирование валидации ===")
    
    # Тестовые примеры
    test_emails = [
        "user@example.com",      # валидный
        "invalid.email",         # невалидный
        "another@test.ru",       # валидный
        "noat.com",              # невалидный
    ]
    
    test_passwords = [
        "Password123",           # валидный
        "short",                 # невалидный
        "nouppercase123",        # невалидный
        "GoodPass42",            # валидный
    ]
    
    print("\n--- Проверка email адресов ---")
    for email in test_emails:
        result = validate_email(email)
        print(f"{email} -> {'✅ Валидный' if result else '❌ Невалидный'}")
    
    print("\n--- Проверка паролей ---")
    for password in test_passwords:
        result = validate_password(password)
        print(f"{password} -> {'✅ Валидный' if result else '❌ Невалидный'}")
    
    print("\n--- Проверка обработки данных ---")
    try:
        result = process_user_data("test@example.com", "GoodPassword123")
        print(f"✅ Успех: {result}")
    except ValueError as e:
        print(f"❌ Ошибка: {e}")
    
    try:
        result = process_user_data("bad-email", "short")
        print(f"✅ Успех: {result}")
    except ValueError as e:
        print(f"❌ Ошибка: {e}")

if __name__ == "__main__":
    main()