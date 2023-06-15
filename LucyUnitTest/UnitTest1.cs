using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant;
using System.IO;
using System;
using System.Collections.Generic;
//de unit testen werken niet met de json files, omdat de @datasource niet gelezen kan worden door de test
//1 van mijn unit test werkt niet met json en die word wel goed gerekend (TestChangeMenu)
//om de tests uit te voeren kan u cd LucyUnitTest in de console typen, en dan dotnet test.
namespace Test;

[TestClass]
public class MenuTests
{
    [TestMethod]
    public void TestDishDetails()
    {
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        menuDis.Month = "April";
        List<Dish> dishes = Dish.LoadDishesFromJson(menuDis.Month); 

        Dish.dishDetails(6);

        string expected = "Grilled Snapper\n\nGrilled Snapper with Four-Herb Gremolata.\nthis dish is not vegan\nThis dish is gluten free\nThis dish has fish in it\nthis dish does not have meat in it";
        string actual = stringWriter.ToString().Trim();

        expected = expected.Replace("\r\n", " ").Replace("\n", " ");
        actual = actual.Replace("\r\n", " ").Replace("\n", " ");
        Assert.AreEqual(expected, actual);
        Assert.AreEqual(expected.Trim(), actual);
            // string.Format("Expected: {0}; Actual: {1}",
            //                 expected, actual));

    }

    [TestMethod]
    public void TestChangemenu()
    {
        Dish.ChangeDish(1, "June", 1, "TEST1");

        List<Dish> dishes = Dish.LoadDishesFromJson("June");
        string expected = "TEST1";
        string actual = dishes[0].Name;
        Assert.AreEqual(expected.Trim(), actual,
            string.Format("Expected: {0}; Actual: {1}",
                            expected, actual));
    }

    [TestMethod]
    public void TestChangeMonth(){  
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        string correctInput = "June";
        menuDis.changeMonth(correctInput);

        string expected = $"You changed the menu to the month: {correctInput}\nPress escape to go back to the menu".Trim();
        string actual = stringWriter.ToString().Trim();
        expected = expected.Replace("\r\n", " ").Replace("\n", " ");
        actual = actual.Replace("\r\n", " ").Replace("\n", " ");
        Assert.AreEqual(expected, actual);
            //string.Format("Expected: {0}; Actual: {1}",
                           // expected, actual));                 
    }

    [TestMethod]
    public void TestAddDish(){
        Dish.addDish("January", new Dish(8, "Test", 34, "yuh", false, false, false, false, false));
        int expected = 8;
        int actual = Dish.GetDishCount("January");
        Assert.AreEqual(expected, actual);

    }
}


