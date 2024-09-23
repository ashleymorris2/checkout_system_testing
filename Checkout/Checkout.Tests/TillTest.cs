using System.Collections.Generic;
using Checkout;
using JetBrains.Annotations;
using Xunit;

namespace Checkout.Tests;

[TestSubject(typeof(Till))]
public class TillTest
{
    private static Item itemA = new Item("Item A", 10, 25);
    private static Item itemB = new Item("Item B", 20, 30);
    private static Item itemC = new Item("Item C", 30, 30);

    public static IEnumerable<object[]> TestingItemsOne()
    {
        yield return new object[] { itemA, 10 };
        yield return new object[] { itemB, 20 };
        yield return new object[] { itemC, 30 };
    }
    
    public static IEnumerable<object[]> TestingItemsTwo()
    {
        yield return new object[] { new List<Item>(){itemA, itemA}, 20 };
        yield return new object[] { new List<Item>(){itemB, itemB}, 30 };
    }
    
    public static IEnumerable<object[]> TestingItemsThree()
    {
        yield return new object[] { new List<Item>(){itemA, itemB, itemC}, 60 };
    }
    
    public static IEnumerable<object[]> TestingItemsFour()
    {
        yield return new object[] { new List<Item>(){itemA, itemA, itemA}, 25 };
        yield return new object[] { new List<Item>(){itemB, itemB}, 30 };
    }
    
    public static IEnumerable<object[]> TestingItemsFive()
    {
        yield return new object[] { new List<Item>(){itemA, itemA, itemA, itemB, itemB}, 55 };
        yield return new object[] { new List<Item>(){itemA, itemA, itemA, itemB, itemB, itemC}, 85 };
    }
    
    public static IEnumerable<object[]> TestingItemsSix()
    {
        yield return new object[] { new List<Item>(){itemA, itemA, itemA, itemA, itemA, itemA}, 50 };
        yield return new object[] { new List<Item>(){itemB, itemB, itemB, itemB}, 60 };
    }
    
    public static IEnumerable<object[]> TestingItemsSeven()
    {
        yield return new object[] { new List<Item>(){}, 0 };
     
    }
    
    

    [Theory]
    [MemberData(nameof(TestingItemsOne))]
    public void SingleItem_ShouldReturn_CorrectValue(Item testItem, int expectedValue)
    {
        //arrange
        var till = new Till();
        //act
        till.AddToCart(testItem);
        var result = till.Checkout();
        
        //Assert
        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [MemberData(nameof(TestingItemsTwo))]
    public void TwoItems_WithSameItem_ShouldReturn_CorrectValue(List<Item> testItems, int expectedValue)
    {
        //arrange
        var till = new Till();
        //act
        foreach (var testItem in testItems)
        {
            till.AddToCart(testItem);
        }
        
        var result = till.Checkout();
        
        //Assert
        Assert.Equal(expectedValue, result);
    }
    
    [Theory]
    [MemberData(nameof(TestingItemsThree))]
    public void TwoItems_WithDifferentItem_ShouldReturn_CorrectValue(List<Item> testItems, int expectedValue)
    {
        //arrange
        var till = new Till();
        //act
        foreach (var testItem in testItems)
        {
            till.AddToCart(testItem);
        }
        
        var result = till.Checkout();
        
        //Assert
        Assert.Equal(expectedValue, result);
    }
    
    [Theory]
    [MemberData(nameof(TestingItemsFour))]
    public void TwoItems_WithSpecialPrices_ShouldReturn_CorrectValue(List<Item> testItems, int expectedValue)
    {
        //arrange
        var till = new Till();
        //act
        foreach (var testItem in testItems)
        {
            till.AddToCart(testItem);
        }
        
        var result = till.Checkout();
        
        //Assert
        Assert.Equal(expectedValue, result);
    }
    
    [Theory]
    [MemberData(nameof(TestingItemsFive))]
    public void MultipleItems_WithSpecialPrices_ShouldReturn_CorrectValue(List<Item> testItems, int expectedValue)
    {
        //arrange
        var till = new Till();
        //act
        foreach (var testItem in testItems)
        {
            till.AddToCart(testItem);
        }
        
        var result = till.Checkout();
        
        //Assert
        Assert.Equal(expectedValue, result);
    }
    
    [Theory]
    [MemberData(nameof(TestingItemsSix))]
    public void MultipleOfSameIte_WithSpecialPrices_ShouldReturn_CorrectValue(List<Item> testItems, int expectedValue)
    {
        //arrange
        var till = new Till();
        //act
        foreach (var testItem in testItems)
        {
            till.AddToCart(testItem);
        }
        
        var result = till.Checkout();
        
        //Assert
        Assert.Equal(expectedValue, result);
    }
    
    [Theory]
    [MemberData(nameof(TestingItemsSeven))]
    public void EmptyItems_ShouldReturn_0(List<Item> testItems, int expectedValue)
    {
        //arrange
        var till = new Till();
        //act
        foreach (var testItem in testItems)
        {
            till.AddToCart(testItem);
        }
        
        var result = till.Checkout();
        
        //Assert
        Assert.Equal(expectedValue, result);
    }
}