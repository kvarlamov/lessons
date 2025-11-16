def validate_email(email):
    """
    Простая функция проверки email адреса
    Возвращает True если email валидный, False если нет
    """
    # Проверяем что это строка
    if not isinstance(email, str):
        return False
    
    # Проверяем что есть символ @
    if '@' not in email:
        return False
    
    # Разделяем на имя пользователя и домен
    parts = email.split('@')
    if len(parts) != 2:
        return False
    
    username, domain = parts
    
    # Проверяем что имя пользователя не пустое
    if len(username) == 0:
        return False
    
    # Проверяем что домен содержит точку
    if '.' not in domain:
        return False
    
    # Проверяем что домен не заканчивается на точку
    if domain.endswith('.'):
        return False
    
    return True


def validate_password(password):
    """
    Функция проверки пароля
    """
    if not isinstance(password, str):
        return False
    
    # Пароль должен быть не менее 8 символов
    if len(password) < 8:
        return False
    
    # Пароль должен содержать хотя бы одну цифру
    has_digit = any(char.isdigit() for char in password)
    if not has_digit:
        return False
    
    # Пароль должен содержать хотя бы одну букву
    has_letter = any(char.isalpha() for char in password)
    if not has_letter:
        return False
    
    return True


def process_user_data(email, password):
    """
    Основная функция обработки пользовательских данных
    """
    if not validate_email(email):
        raise ValueError("Неверный формат email")
    
    if not validate_password(password):
        raise ValueError("Слабый пароль")
    
    # Если все проверки пройдены
    return {
        'email': email,
        'password': password,
        'status': 'valid'
    }