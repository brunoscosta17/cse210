using System;

class Program
{
    static void Main(string[] args)
    {
        Address usaAddress = new Address(
            "123 Main Street",
            "Provo",
            "UT",
            "USA");

        Customer usaCustomer = new Customer("John Smith", usaAddress);

        Order order1 = new Order(usaCustomer);

        Product product1 = new Product("USB-C Cable", "P001", 7.50, 3);
        Product product2 = new Product("Wireless Mouse", "P002", 25.00, 1);
        Product product3 = new Product("Laptop Stand", "P003", 30.00, 1);

        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        Address internationalAddress = new Address(
            "456 Queen Street",
            "Toronto",
            "ON",
            "Canada");

        Customer internationalCustomer = new Customer("Emily Johnson", internationalAddress);

        Order order2 = new Order(internationalCustomer);

        Product product4 = new Product("Mechanical Keyboard", "P010", 80.00, 1);
        Product product5 = new Product("Gaming Headset", "P011", 60.00, 2);

        order2.AddProduct(product4);
        order2.AddProduct(product5);

        Console.WriteLine("=== ORDER 1 ===");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice():0.00}");
        Console.WriteLine();

        Console.WriteLine("=== ORDER 2 ===");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice():0.00}");

        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
