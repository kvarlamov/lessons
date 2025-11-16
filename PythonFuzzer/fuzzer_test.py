import atheris
import sys
import os

# Добавляем путь к нашим модулям
sys.path.append(os.path.dirname(__file__))

from validation import validate_email, validate_password, process_user_data

def test_email_fuzzing(data):
    """
    Функция для фаззинга проверки email
    """
    try:
        # Пытаемся преобразовать данные в строку
        if isinstance(data, bytes):
            email_str = data.decode('utf-8', errors='ignore')
        else:
            email_str = str(data)
        
        # Вызываем нашу функцию валидации
        result = validate_email(email_str)
        
        # Если валидация прошла успешно, пробуем обработать
        if result:
            # Это может выявить дополнительные проблемы
            test_str = email_str.lower().strip()
            
    except Exception as e:
        # Игнорируем ожидаемые ошибки (неверный формат и т.д.)
        if "utf-8" in str(e).lower() or "unicode" in str(e).lower():
            return
        # Другие исключения могут быть интересны
        print(f"Неожиданная ошибка с email: {e}")
        print(f"Ввод: {data}")
        raise

def test_password_fuzzing(data):
    """
    Функция для фаззинга проверки пароля
    """
    try:
        if isinstance(data, bytes):
            password_str = data.decode('utf-8', errors='ignore')
        else:
            password_str = str(data)
        
        result = validate_password(password_str)
        
        # Дополнительные проверки для валидных паролей
        if result:
            # Симулируем некоторые операции
            if len(password_str) > 1000:
                # Очень длинные пароли могут вызвать проблемы
                processed = password_str.upper().lower()
                
    except Exception as e:
        if "utf-8" in str(e).lower():
            return
        print(f"Неожиданная ошибка с паролем: {e}")
        print(f"Ввод: {data}")
        raise

def test_complete_fuzzing(data):
    """
    Комплексный фаззинг всей системы
    """
    try:
        if isinstance(data, bytes):
            input_str = data.decode('utf-8', errors='ignore')
        else:
            input_str = str(data)
        
        # Пробуем использовать строку как email и пароль одновременно
        # Это может выявить неожиданные взаимодействия
        email = input_str[:len(input_str)//2] if len(input_str) > 1 else input_str
        password = input_str[len(input_str)//2:] if len(input_str) > 1 else input_str
        
        try:
            process_user_data(email, password)
        except ValueError:
            # Ожидаемые ошибки валидации - это нормально
            pass
            
    except Exception as e:
        print(f"Критическая ошибка: {e}")
        print(f"Ввод: {data}")
        raise

def setup_email_fuzzer():
    """
    Настройка фаззера для email
    """
    print("Запуск фаззинга email...")
    atheris.Setup(sys.argv, test_email_fuzzing)
    atheris.Fuzz()

def setup_password_fuzzer():
    """
    Настройка фаззера для паролей
    """
    print("Запуск фаззинга паролей...")
    atheris.Setup(sys.argv, test_password_fuzzing)
    atheris.Fuzz()

def setup_complete_fuzzer():
    """
    Настройка комплексного фаззера
    """
    print("Запуск комплексного фаззинга...")
    atheris.Setup(sys.argv, test_complete_fuzzing)
    atheris.Fuzz()

if __name__ == "__main__":
    # По умолчанию запускаем комплексный фаззинг
    setup_complete_fuzzer()