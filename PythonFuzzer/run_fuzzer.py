#!/usr/bin/env python3
"""
run_fuzzer.py - Простой запускатель фаззера
"""

import os
import sys

# Добавляем текущую папку в путь Python
sys.path.append(os.path.dirname(__file__))

def main():
    print("=== 🚀 ЗАПУСК ФАЗЗЕРА ===")
    print()
    
    try:
        # Пробуем импортировать наш улучшенный фаззер
        from advanced_python_fuzzer import AdvancedFuzzer, main as fuzzer_main
        print("✅ Улучшенный фаззер найден! Запускаем...")
        print()
        fuzzer_main()
        
    except ImportError:
        print("❌ Улучшенный фаззер не найден. Создаем базовую версию...")
        print()
        
        # Запускаем простейший фаззер
        from simple_fuzzer import main as simple_main
        simple_main()

if __name__ == "__main__":
    main()