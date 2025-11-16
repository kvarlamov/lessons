#!/usr/bin/env python3
"""
simple_fuzzer.py - Простейший фаззер для немедленного запуска
"""

import random
import string
import time

def validate_email(email):
    """Простая валидация email"""
    if not isinstance(email, str):
        return False
    return '@' in email and '.' in email.split('@')[-1]

def validate_password(password):
    """Простая валидация пароля"""
    if not isinstance(password, str):
        return False
    return len(password) >= 8 and any(c.isdigit() for c in password)

def generate_test_cases():
    """Генерирует тестовые случаи"""
    cases = []
    
    # Специальные случаи
    special_cases = [
        "", "a", "a" * 1000, "@", ".", "@.", "user@", "@domain", 
        "user@domain.", "user@.com", "..", "//", "\\", 
        "<script>", "' OR '1'='1", "12345678", "password"
    ]
    cases.extend(special_cases)
    
    # Случайные строки
    for _ in range(100):
        length = random.randint(1, 200)
        chars = string.ascii_letters + string.digits + string.punctuation + ' '
        random_str = ''.join(random.choice(chars) for _ in range(length))
        cases.append(random_str)
    
    return cases

def main():
    """Основная функция"""
    print("=== 🐛 ПРОСТЕЙШИЙ ФАЗЗЕР ===")
    print("Запуск на 15 секунд...")
    print()
    
    start_time = time.time()
    crashes = []
    test_cases = generate_test_cases()
    
    for i, case in enumerate(test_cases):
        if time.time() - start_time > 15:  # 15 секунд
            break
            
        # Тестируем email валидацию
        try:
            validate_email(case)
        except Exception as e:
            crashes.append(("validate_email", case, str(e)))
            print(f"💥 CRASH в validate_email: {e}")
            print(f"   Ввод: {case[:50]}...")
        
        # Тестируем password валидацию
        try:
            validate_password(case)
        except Exception as e:
            crashes.append(("validate_password", case, str(e)))
            print(f"💥 CRASH в validate_password: {e}")
            print(f"   Ввод: {case[:50]}...")
        
        if i % 20 == 0:
            print(f"📊 Прогресс: {i}/{len(test_cases)} тестов...")
    
    # Отчет
    print("\n" + "="*50)
    print("📊 РЕЗУЛЬТАТЫ:")
    print(f"Всего тестов: {len(test_cases)}")
    print(f"Найдено ошибок: {len(crashes)}")
    
    if crashes:
        print("\n💥 ОШИБКИ:")
        for i, (func, inp, error) in enumerate(crashes, 1):
            print(f"{i}. {func}: {error}")
            print(f"   Ввод: {inp[:100]}...")
    else:
        print("🎉 Ошибок не найдено!")
    
    print(f"\n⏰ Время выполнения: {time.time() - start_time:.1f} секунд")

if __name__ == "__main__":
    main()