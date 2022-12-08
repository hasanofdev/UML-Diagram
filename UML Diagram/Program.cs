// Task 1 - Creational Pattern - Factory Method

using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

public interface IProduct
{
    public double Width { get; set; }
    public double Height { get; set; }
    public string Color { get; set; }
    public string ShipFrom();
}
public class Sofa : IProduct
{
    public double Width { get; set; }
    public double Height { get; set; }
    public string? Color { get; set; }

    public string ShipFrom()
    {
        return "Sofa";
    }
}

public class Chair : IProduct
{
    public double Width { get; set; }
    public double Height { get; set; }
    public string Color { get; set; }

    public string ShipFrom()
    {
        return "Chair";
    }
}

public class None : IProduct
{
    public double Width { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public double Height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public string ShipFrom()
    {
        return "None";
    }
}

class FurnitureFactory
{
    public IProduct FactoryMethod(string request)
    {
        if (request == "Sofa") return new Sofa();
        else if (request == "Chair") return new Chair();
        else return new None();
    }
}

// Task 2 - Structural Pattern - Proxy
public abstract class Subject
{
    public abstract void Request();
}
public class RealSubject : Subject
{
    public override void Request()
    {
        Console.WriteLine("Called RealSubject.Request()");
    }
}
public class Proxy : Subject
{
    private RealSubject realSubject;
    public override void Request()
    {
        if (realSubject == null)
        {
            realSubject = new RealSubject();
        }
        realSubject.Request();
    }
}

// Task 3 - Behavioral Pattern - Visitor
public interface IElement
{
    void Accept(IVisitor visitor);
}
public interface IVisitor
{
    void Visit(IElement element);
}
public class Kid : IElement
{
    public string KidName { get; set; }

    public Kid(string name)
    {
        KidName = name;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
public class Doctor : IVisitor
{
    public string Name { get; set; }
    public Doctor(string name)
    {
        Name = name;
    }

    public void Visit(IElement element)
    {
        Kid kid = (Kid)element;
        Console.WriteLine("Doctor: " + this.Name + " did the health checkup of the child: " + kid.KidName);
    }
}
class Salesman : IVisitor
{
    public string Name { get; set; }
    public Salesman(string name)
    {
        Name = name;
    }
    public void Visit(IElement element)
    {
        Kid kid = (Kid)element;
        Console.WriteLine("Salesman: " + this.Name + " gave the school bag to the child: "
                        + kid.KidName);
    }
}
public class School
{
    private static List<IElement> elements;
    static School()
    {
        elements = new List<IElement>
            {
                new Kid("Ram"),
                new Kid("Sara"),
                new Kid("Pam")
            };
    }
    public void PerformOperation(IVisitor visitor)
    {
        foreach (var kid in elements)
        {
            kid.Accept(visitor);
        }
    }
}
class Program
{
    //static void Main()
    //{
    //    FurnitureFactory c = new FurnitureFactory();
    //    IProduct product;

    //    List<string> requests = new List<string>() { "Sofa","Chair","None" };

    //    foreach (var request in requests)
    //    {
    //        product = c.FactoryMethod(request);
    //        Console.WriteLine("Furniture " + product.ShipFrom());
    //    }
    //}


    //static void Main()
    //{
    //    Proxy proxy = new Proxy();
    //    proxy.Request();
    //    Console.ReadKey();
    //}

    static void Main(string[] args)
    {
        School school = new School();
        var visitor1 = new Doctor("James");
        school.PerformOperation(visitor1);
        Console.WriteLine();
        var visitor2 = new Salesman("John");
        school.PerformOperation(visitor2);
        Console.Read();
    }

}


