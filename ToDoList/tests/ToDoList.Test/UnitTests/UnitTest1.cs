namespace ToDoList.Test;

public class UnitTest1
{
    [Fact]   // atribut = test
    public void TestDeleniCelymCislem()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Divide(10, 5);
        //var result = calculator.Add(5, 5);

        // Assert
        Assert.Equal(2, result);
    }
}

public class UnitTest2
{
    [Fact]
    public void TestDeleniNulou()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = () => calculator.Divide(10, 0.0F);

        // Assert
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0.0F));  //lambda výraz
    }
}

public class UnitTest3
{
    [Fact]
    public void TestDeleniDesetinnymCioslem()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Divide(10, 2.5F);

        // Assert
        Assert.Equal(float.PositiveInfinity, result);
    }
}

public class Calculator
{
    public float Divide(float dividend, float devisor)
    {
        return dividend / devisor;
    }

    public int Add(int a, int b)
    {
        return a + b;
    }
}
