using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Skovorodki;
public class SkovorodkiConnection
{
    public string URL { get; set; } = "https://ek.ua/ua/list/472/";

    private ChromeOptions _options = new ChromeOptions();

    private ChromeDriver _driver { get; set; }

    public SkovorodkiConnection()
    {
        _options.AddArgument("--disable-notifications");

        _driver = new ChromeDriver(_options);
    }

    public void GetSkovorodki()
    {
        
        _driver.Url = URL;
        
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        //var showMoreButton = _driver.FindElement(By.ClassName("list-more-div"));
        
        for (int i = 0; i < 5; i++)
        {
            _driver.ExecuteScript("document.querySelector('.list-more-div').click()");
           
            Thread.Sleep(2000);
        }

        return;
    }
}