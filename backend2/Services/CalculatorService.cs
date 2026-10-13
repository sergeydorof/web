namespace backend2.Services
{
    public class CalculatorService : ICalculatorService
    {
        public string Calculate(int a, int b, string operation)
        {
            if (operation == "/" && b == 0)
                return "Ошибка: Деление на ноль";

            return operation switch
            {
                "+" => (a + b).ToString(),
                "-" => (a - b).ToString(),
                "*" => (a * b).ToString(),
                "/" => (a / b).ToString(),
                _ => "Неизвестная операция"
            };
        }
    }
}
