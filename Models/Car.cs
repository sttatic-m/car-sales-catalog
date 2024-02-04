namespace car_sales_catalog.Models;

public class Car(Guid id, int code, string name, double price, DateTime releaseDate, string imageDir)
{
    public Guid Id { get; private set; } = id;
    public int Code { get; set; } = code;
    public string Name { get; set; } = name;
    public double Price { get; set; } = price;
    public DateTime releaseDate { get; set; } = releaseDate;
    public string ImageDir { get; set; } = imageDir;
}