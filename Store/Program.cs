using System.Text;

namespace Store
{
    // Простейший магазин, написанный с использованием только System и System.Text и без использования программных типов.
    sealed class Program
    {
        // Выносим литералы в константы.
        const string STORE_NAME = "Магазин Одежды";
        const string HELLO = "Здравствуйте и добро пожаловать в {0}. Пожалуйста, выберите действие:\n{1}";
        const string GOODBYE = "Спасибо и до свидания!";
        const char INPUT_SEPARATOR = ' ';
        // Константу ошибки начинаем с "ERROR".
        const string ERROR_NO_INPUT = "Действие {0} не найдено.";
        // Константу каждого действия начинаем с "INPUT".
        const string INPUT_EXIT = "выйти";
        const string INPUT_ACCOUNT = "баланс";
        // Константу ответа в случае успеха действия при наличии начинаем с "OUTPUT" и называем именем действия.
        const string OUTPUT_ACCOUNT = "Баланс: {0}.";
        const string INPUT_PRODUCTS = "продукты";
        const string OUTPUT_PRODUCTS = "Продукты:{0}";
        // Константу, используемую в константе ответа, начинаем с имени константы ответа.
        const string OUTPUT_PRODUCTS_PRODUCT = "\n- {0} - {1}₽.";
        const string INPUT_BUY_PRODUCT = "купить";
        // Ошибку, относящуюся к действию, начинаем с имени действия.
        const string ERROR_PRODUCT_NO_NAME = "Отсутствует имя продукта.";
        const string ERROR_PRODUCT_NO_PRODUCT = "Продукт {0} не найден.";
        const string ERROR_BUY_PRODUCT_INSUFFICIENT_BALANCE = "Недостаточный баланс. Требуется: {0}₽. Доступно: {1}₽. Разница: {2}₽.";
        const string OUTPUT_BUY_PRODUCT = "Вы приобрели {0} за {1}₽. Баланс: {2}₽.";
        const string ACTIONS =
    $@"- {INPUT_EXIT} - выйти;
- {INPUT_ACCOUNT} - посмотреть баланс;
- {INPUT_PRODUCTS} - посмотреть продукты;
- {INPUT_BUY_PRODUCT} - купить продукт.";

        static readonly Product[] products =
        {
            new Product("Рубашка мужская белая", 2500),
            new Product("Брюки мужские чёрные", 7500)
        };
        static Product GetProduct(string name)
        {
            if (name.Length == 0)
                throw new Exception(ERROR_PRODUCT_NO_NAME);
            foreach (var product in products)
                if (product.name.Equals(name))
                    return product;
            return null;
        }

        // Храним данные о балансе между методами.
        static decimal account = 10000;

        // Выносим действия в методы.
        static void Account()
        {
            Console.WriteLine(string.Format(OUTPUT_ACCOUNT, account));
        }
        static void Products()
        {
            // Используем String Builder, чтобы оптимально преобразовать множество строк в одну.
            var products_to_output = new StringBuilder(products.Length);
            for (int i = 0; i < products.Length; ++i)
                products_to_output.Append(string.Format(OUTPUT_PRODUCTS_PRODUCT, products[i].name, products[i].price));
            Console.WriteLine(string.Format(OUTPUT_PRODUCTS, products_to_output));
        }
        static void BuyProduct(string name)
        {
            if (name.Length == 0)
                throw new Exception(ERROR_PRODUCT_NO_NAME);
            var product = GetProduct(name);
            if (product == null)
                throw new ArgumentException(string.Format(ERROR_PRODUCT_NO_PRODUCT, name));
            if (product.price > account)
                throw new ArgumentException(string.Format(ERROR_BUY_PRODUCT_INSUFFICIENT_BALANCE, name));
            account -= product.price;
            Console.WriteLine(string.Format(OUTPUT_BUY_PRODUCT, name, product.price, account));
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Console.WriteLine(string.Format(HELLO, STORE_NAME, ACTIONS));
            // Обявляем используемые в цикле переменные до цикла.
            string input, name, arguments;
            int separator;
            var is_active = true;
            // Выполняем приложение, пока пользователь не выйдет.
            while (is_active)
            {
                input = Console.ReadLine().TrimStart();
                separator = input.IndexOf(INPUT_SEPARATOR);
                if (separator == -1)
                    separator = input.Length;
                name = input.Substring(0, separator);
                arguments = input.Substring(separator != input.Length ? separator + 1 : separator).Trim();
                try
                {
                    switch (name)
                    {
                        case INPUT_EXIT:
                            is_active = false;
                            break;
                        case INPUT_ACCOUNT:
                            Account();
                            break;
                        case INPUT_PRODUCTS:
                            Products();
                            break;
                        case INPUT_BUY_PRODUCT:
                            BuyProduct(arguments);
                            break;
                        default:
                            throw new Exception(string.Format(ERROR_NO_INPUT, name));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine(GOODBYE);
        }
    }
}
