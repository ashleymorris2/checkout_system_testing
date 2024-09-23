using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Xunit;

namespace Checkout.Tests;

[TestSubject(typeof(Till))]
public class TillTest
{
    private static readonly Item ItemA = new("Item A", 10, 25, 3);
    private static readonly Item ItemB = new("Item B", 20, 30, 2);
    private static readonly Item ItemC = new("Item C", 30);

    public static IEnumerable<object[]> SingleItemTestData()
    {
        yield return [ItemA, 10];
        yield return [ItemB, 20];
        yield return [ItemC, 30];
    }

    public static IEnumerable<object[]> TwoItemTestData()
    {
        yield return [new List<Item> { ItemA, ItemA }, 20];
        yield return [new List<Item> { ItemB, ItemB }, 30];
    }

    public static IEnumerable<object[]> MixedItemsTestData()
    {
        yield return [new List<Item> { ItemA, ItemB, ItemC }, 60];
    }

    public static IEnumerable<object[]> SpecialPricingTestData()
    {
        yield return [new List<Item> { ItemA, ItemA, ItemA }, 25];
        yield return [new List<Item> { ItemB, ItemB }, 30];
        yield return [new List<Item> { ItemA, ItemA, ItemA, ItemA, ItemA, ItemA }, 50];
        yield return [new List<Item> { ItemB, ItemB, ItemB, ItemB }, 60];
        yield return [new List<Item> { ItemA, ItemA, ItemA, ItemB, ItemB }, 55];
        yield return [new List<Item> { ItemA, ItemA, ItemA, ItemB, ItemB, ItemC }, 85];
    }
    
    public static IEnumerable<object[]> TestingItemsSeven()
    {
        yield return [new List<Item>(), 0];
    }

    [Theory]
    [MemberData(nameof(SingleItemTestData))]
    public void Checkout_WithSingleItem_ShouldReturn_CorrectValue(Item testItem, int expectedValue)
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
    [MemberData(nameof(TwoItemTestData))]
    public void Checkout_WithTwoItem_ShouldReturn_CorrectValue(List<Item> testItems, int expectedValue)
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
    [MemberData(nameof(MixedItemsTestData))]
    public void Checkout_WithMixedItems_ShouldReturn_CorrectValue(List<Item> testItems, int expectedValue)
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
    [MemberData(nameof(SpecialPricingTestData))]
    public void Checkout_WithSpecialPricing_ShouldReturn_CorrectValue(List<Item> testItems, int expectedValue)
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
    public void Checkout_WithEmptyItems_ShouldReturn_0(List<Item> testItems, int expectedValue)
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
    
    [Fact]
    public void Checkout_WithNullItem_ShouldThrowArgumentException()
    {
        var till = new Till();
        Assert.Throws<ArgumentNullException>(() => till.AddToCart(null));
    }
}