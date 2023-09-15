namespace Store
{
    class Product // объявление класса
    { // определение класса начинается
        public string name; // поле объекта
        public decimal price;

        public Product(string name, decimal price) // конструктор объекта
        {
            this.name = name;
            this.price = price;
        }
    } // определение класса заканчивается
}
