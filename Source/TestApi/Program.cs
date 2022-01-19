// See https://aka.ms/new-console-template for more information
using TestApi;

var list = new List<TestClass>(){
    new TestClass("egyf", "egyl"),
    new TestClass("kettof", "kettol"),
    new TestClass("haromf", "haroml"),
};

var list2 = list.Where(t => GetFieldSelector("f")(t) == "kettof").ToList();

Console.WriteLine("Hello, World!");


Func<TestClass, object> GetFieldSelector(string fieldName)
{
    return fieldName switch
    {
        "f" => t => t.FirstName,
        "l" => t => t.LastName,
        _ => null,
    };
}
