// See https://aka.ms/new-console-template for more information

using Skovorodki;

var connection = new SkovorodkiConnection();

var skovorodki = connection.GetSkovorodki();

var brands = connection.GetBrand();

Console.WriteLine(skovorodki[0].Name);
foreach (var b in brands)
{
    Console.WriteLine(b);
}
