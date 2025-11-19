using System;
using System.Collections.Generic;
using System.Text;

public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetTotalPrice()
    {
        double subtotal = 0;

        foreach (Product product in _products)
        {
            subtotal += product.GetTotalCost();
        }

        double shippingCost = _customer.LivesInUSA() ? 5.0 : 35.0;

        return subtotal + shippingCost;
    }

    public string GetPackingLabel()
    {
        StringBuilder labelBuilder = new StringBuilder();

        labelBuilder.AppendLine("Packing Label:");
        foreach (Product product in _products)
        {
            labelBuilder.AppendLine($"- {product.GetName()} (ID: {product.GetProductId()})");
        }

        return labelBuilder.ToString();
    }

    public string GetShippingLabel()
    {
        StringBuilder labelBuilder = new StringBuilder();

        labelBuilder.AppendLine("Shipping Label:");
        labelBuilder.AppendLine(_customer.GetName());
        labelBuilder.AppendLine(_customer.GetAddress().GetFullAddress());

        return labelBuilder.ToString();
    }
}
