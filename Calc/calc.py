def calculate_expression(expression):
    def get_priority(operator):
        if operator in ('+', '-'):
            return 1
        if operator in ('*', '/'):
            return 2
        return 0

    def apply_operator(operators, values):
        operator = operators.pop()
        right = values.pop()
        left = values.pop()

        if operator == '+':
            values.append(left + right)
        elif operator == '-':
            values.append(left - right)
        elif operator == '*':
            values.append(left * right)
        elif operator == '/':
            values.append(left / right)
    expression = expression.replace(' ', '')

    values = []
    operators = []

    i = 0
    while i < len(expression):
        if expression[i].isdigit() or expression[i] == '.':
            j = i
            while j < len(expression) and (expression[j].isdigit() or expression[j] == '.'):
                j += 1
            number = float(expression[i:j])
            values.append(number)
            i = j
        elif expression[i] == '(':
            operators.append(expression[i])
            i += 1
        elif expression[i] == ')':
            while operators and operators[-1] != '(':
                apply_operator(operators, values)
            operators.pop()
            i += 1
        else:
            if expression[i] == '-' and (i == 0 or expression[i - 1] in ('(', '+', '-', '*', '/')):
                j = i + 1
                while j < len(expression) and (expression[j].isdigit() or expression[j] == '.'):
                    j += 1
                number = -float(expression[i + 1:j])
                values.append(number)
                i = j
            else:
                while (operators and operators[-1] != '(' and
                       get_priority(operators[-1]) >= get_priority(expression[i])):
                    apply_operator(operators, values)
                operators.append(expression[i])
                i += 1
    while operators:
        apply_operator(operators, values)

    return values[0]


def main():
    print("Консольный калькулятор")
    print("Поддерживаемые операции: +, -, *, /, скобки ()")
    print("Для выхода введите 'exit'")

    while True:
        try:
            expression = input("\nВведите выражение: ").strip()

            if expression.lower() == 'exit':
                print("До свидания!")
                break

            if not expression:
                continue

            result = calculate_expression(expression)
            print(f"Результат: {result}")

        except ZeroDivisionError:
            print("Ошибка: Деление на ноль!")
        except Exception as e:
            print(f"Ошибка: Некорректное выражение! ({e})")


if __name__ == "__main__":
    main()