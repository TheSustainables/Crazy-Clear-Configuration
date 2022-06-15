// See https://aka.ms/new-console-template for more information

using CrazyClearConfiguration.Adapter.Configuration.Memory;
using CrazyClearConfiguration.Core.UseCases;

Console.WriteLine("Hello, World!");
var adapter = new InMemoryConfigAdapter();
var usecase = new GetConfigurationUseCase(adapter);
var data = usecase.Execute().Result;

void Test(dynamic data)
{
    foreach (var test in data)
    {
        if (test is string)
        {
            Console.WriteLine(test);
        }
        else if (test.Value is string)
        {
            Console.WriteLine(test.Value);
        }
        else
        {
            Test(test.Value);
        }
    } 
}
Test(data);