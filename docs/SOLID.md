# SOLID Principles
This document is designed to give an overview of the 5 SOLID principles. These principles should be considered and applied where appropriate when writing any code in any language.

## (S) Single Responsibility
**A class/method should only be concerned with one thing**

### Example (Bad) 
```
public class UserManager
{
    public void Add(User user)
    {
        try
        {
            //Responsibility 1: How to add a user to database
            _context.Users.Add(user);
        }
        catch(Exception ex)
        {
            //Responsibility 2: How to handle logs
            System.IO.File.WriteAllText(@"D:\errorlogs.txt", ex.Message);
        }
    }
}
```
### Example (Good)
```
public class UserManager
{
    private Logger _logger;
    public void Add(User user)
    {
        try
        {
            _context.Users.Add(user);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}

public class Logger
{
    public void LogError(string message)
    {
        System.IO.File.WriteAllText(@"D:\errorlogs.txt", message);
    }
}
```

## (O) Open / Closed
**Open for extension. Closed for modification.**

### Example (Method - Bad)
```
public void RenderPlayer(Player p)
{
    var canvas = new Canvas();
    if(c.HasGlasses)
        RenderGlasses(canvas, p);
    if(c.HasItem)
        RenderItem(canvas, p);
    else
        RenderHands(canvas, p);
}
```
### Example (Method - Good)
```
var IList<IRenderer> renderers;
public void RenderPLayer(Player p)
{
    var canvas = new Canvas()
    foreach(var renderer in renderers)
        renderer.RenderWhereApplicable(canvas, p);
}
```

## (L) Liskov Substitution
**An inheritor should be able to replace its child without changing the behaviour of the code**

i.e. If class B inherits from class A then we should be able to use class B anywhere we currently use class A.
### Example (BAD)
```
public class Car
{
    public void TurnOnEngine(){}
    public void Accelerate(){}
    public void Brake(){}
}

public class ElectricCar : Car
{
    public void TurnOnEngine(){
        throw new Exception("I dont have an engine.")
    }
    public void Accelerate(){}
    public void Brake(){} 
}
```

## (I) Interface Segregation
**Simply put large interfaces should be split into smaller ones.**

More specifically classes should not be forced to implement contracts from interfaces they do not need.

### Example (Bad)
```
public class IAnimal 
{
    void Eat();
    void Sleep();
    void Swim();
}

public class Whale : IAnimal
{
    public void Eat(){}
    public void Sleep(){}
    public void Swim(){}
}

public class Elephant : IAnimal 
{
    public void Eat(){}
    public void Sleep(){}
    public void Swim(){} // An elephant cannot use this method but needs to implement the contract
}
```

### Example (Good)
```
public class IAnimal
{
    void Eat();
    void Sleep();
}

public interface IAquatic
{
    void Swim();
}

public class Whale : IAnimal, IAquatic
{
    public void Eat(){}
    public void Sleep(){}
    public void Swim(){}
}

public class Elephant : IAnimal 
{
    public void Eat(){}
    public void Sleep(){}
}
```

## (D) Dependency Inversion
**Summary**

Possibly the most important of all the SOLID principles.

### Example (Bad)
```
public class Programmer
{
    private Computer _computer;
    private Coffee _coffee;

    public Programmer()
    {
        _computer = new Computer();
        _coffee = new Coffee();
    }
}
```
### Example (Good)
```
public class Programmer
{
    private Computer _computer;
    private Coffee _coffee;

    public Programmer(Computer computer, Coffee coffee)
    {
        _computer = computer;
        _coffee = coffee;
    }
}
```

### Example (Best)
```
public class Programmer
{
    private IComputer _computer;
    private ICoffee _coffee;

    public Programmer(IComputer computer, ICoffee coffee)
    {
        _computer = computer;
        _coffee = coffee;
    }
}

public class Computer : IComputer {}
public class Coffee : ICoffee {}
```
By injecting an interface rather than an implementation it allows us to pass in different versions of the dependency without changing the code. This is commonly used for testing but can be utilised if a business rule changes, for example the above Programmers can now be supplied a laptop or a computer
```
public class Computer : IComputer {}
public class Laptop : IComputer {}
```