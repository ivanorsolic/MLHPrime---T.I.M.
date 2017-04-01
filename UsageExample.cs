using System;

namespace HttpListenerTest

{
    public class Program
    {
        static void Main(string[] args)
        {
            var hub = new EchoHub(baseUrl: "http://+:8080/");

            hub.HttpRequestRecieved += (sender, eventArgs) =>
            {
                var data = (HttpRequestRecievedEventArgs) eventArgs;


                Console.WriteLine(data.Url);

                //what alexa will says
                hub.Message = DateTime.Now.ToString("HH:mm:ss");
            };

            Console.ReadLine();
        }
    }
}