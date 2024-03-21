using Microsoft.Extensions.Logging;
using MyApp.Interfaces;

namespace MyApp.Services
{
    public class MyService : IMyService
    {
        private readonly ILogger<MyService> logger;
        private readonly AlertService alert;

        public MyService(ILogger<MyService> logger, AlertService alert)
        {
            this.logger = logger;
            this.alert = alert;
        }

        public string ReturnStringMethod(string test)
        {
            logger.LogInformation($"original string: {test}");
            var newStr = test + "abc";
            alert.DisplayOkAlert("Result String Is", newStr);
            return newStr;
        }
    }
}
