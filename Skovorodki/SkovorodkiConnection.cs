using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Skovorodki;
public class SkovorodkiConnection
{
    public string URL { get; set; } = "https://rozetka.com.ua/ua/skovorody/c4626754/#search_text=%D1%81%D0%BA%D0%BE%D0%B2%D0%BE%D1%80%D1%96%D0%B4%D0%BA%D0%B0";

    private ChromeOptions _options = new ChromeOptions();

    private ChromeDriver _driver { get; set; }

    public SkovorodkiConnection()
    {
        _options.AddArgument("--disable-notifications");

        _driver = new ChromeDriver(_options);
    }

    public List<Skovorodki> GetSkovorodki()
    {

        var result = new List<Skovorodki>();
        
        _driver.Url = URL;
        
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        //var showMoreButton = _driver.FindElement(By.ClassName("list-more-div"));
        
        //open 3 pages
        for (int i = 0; i < 2; i++)
        {
            _driver.ExecuteScript("document.querySelector('.show-more').click()");
           
            Thread.Sleep(2000);
        }

        var goods = _driver.FindElements(By.ClassName("goods-tile__content"));

        string pattern = @"\b[a-zA-Z]+\b";

        var regex = new Regex(pattern);
        
        
        foreach (var g in goods)
        {
            var name = g.FindElement(By.ClassName("goods-tile__title"));
            
            var price = g.FindElement(By.ClassName("goods-tile__price-value"));


            var matches = regex.Match(name.Text);

            var skovorodka = new Skovorodki
            {
                Name = name.Text,
                Brand = matches.Value,
                Price = price.Text
            };
            
            result.Add(skovorodka);

        }

        return  result;
    }

    public List<string> GetBrand()
    {
        var result = new List<string>();
        
        _driver.Url = URL;
        
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        //var showMoreButton = _driver.FindElement(By.ClassName("list-more-div"));
        
        //open 3 pages
        for (int i = 0; i < 2; i++)
        {
            _driver.ExecuteScript("document.querySelector('.show-more').click()");
           
            Thread.Sleep(2000);
        }

        var goods = _driver.FindElements(By.ClassName("goods-tile__content"));

        string pattern = @"\b[a-zA-Z]+\b";

        var regex = new Regex(pattern);
        
        
        foreach (var g in goods)
        {
            var name = g.FindElement(By.ClassName("goods-tile__title"));
            
            var matches = regex.Match(name.Text);
            
            
            result.Add(matches.Value);

        }

        var uniqueResult = result.Distinct().ToList();
        
        return  uniqueResult;
    }
}