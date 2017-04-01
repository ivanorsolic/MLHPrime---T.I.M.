using System;
using System.Net;
using System.Text;
using System.Threading;

namespace HttpListenerTest
{
    public class HttpRequestRecievedEventArgs : EventArgs
    {
        public string Url { get; set; }
    }

    public class EchoHub
    {
        public event EventHandler HttpRequestRecieved;
        public string Message { get; set; }
        public EchoHub(string baseUrl)
        {
            var thread = new Thread(() => this.StartServer(baseUrl));
            thread.Start();
        }

        private void StartServer(string baseUrl)
        {
            var listener = new HttpListener();

            listener.Prefixes.Add(baseUrl);

            try
            {
                listener.Start();
            }

            catch (HttpListenerException hlex)
            {
                return;
            }


            while (listener.IsListening)
            {
                var context = listener.GetContext();
                ProcessRequest(context);
            }


            listener.Close();
        }


        private void ProcessRequest(HttpListenerContext context)
        {
            // Get the data from the HTTP stream
            //var body = new StreamReader(context.Request.InputStream).ReadToEnd();
            OnHttpRequestRecieved(new HttpRequestRecievedEventArgs
            {
                Url = context.Request.RawUrl
            });

            var body = Encoding.UTF8.GetBytes(Message);

            context.Response.StatusCode = 200;
            context.Response.KeepAlive = true;


            context.Response.ContentLength64 = body.Length;
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "text/html";



            var output = context.Response.OutputStream;


            output.Write(body, 0, body.Length);


            context.Response.Close();
        }

        protected virtual void OnHttpRequestRecieved(HttpRequestRecievedEventArgs e)
        {
            var handler = HttpRequestRecieved;

            handler?.Invoke(this, e);
        }
    }
}
