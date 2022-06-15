// See https://aka.ms/new-console-template for more information

using System.Dynamic;

Console.WriteLine("Hello, World!");

dynamic testObject = new ExpandoObject();
testObject.Value = "hfghf";
testObject.Array = new string[]
{
    "1",
    "2",
    "3",
};
dynamic expendo = new ExpandoObject();
expendo.Value = "asdsad";
expendo.Array = new string[]
{
    "1",
    "2",
    "3",
};
testObject.Object = expendo;

foreach (var test in testObject)
{
    if (test.Value is string)
    {
        Console.WriteLine(test.Value);
    }
    else
    {
        foreach (var tester in test.Value)
        {
            Console.WriteLine(tester.Value);
        }
    }
}