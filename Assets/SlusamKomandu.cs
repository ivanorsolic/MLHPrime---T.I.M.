using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlusamKomandu : MonoBehaviour
{

    // Use this for initialization
    string SceneName = "";
    bool inst = false;
    EchoHub hub;

    private bool _inst;
    public static bool IsRotating = false;
    private List<GameObject> _planets;

    void Start()
    {

        SceneName = "";

        if (inst == false)
        {
            hub = EchoHub.Instance;

            //if (hub.IsInstanced == false)
            //{
            hub.HttpRequestRecieved += (sender, eventArgs) =>
            {
                var data = (HttpRequestRecievedEventArgs)eventArgs;


                var url = data.Url.Split('/')[1];
                //print(data.Url);


                if (url != "favicon.ico")
                {
                    SceneName = url;
                }
                

                //what alexa will say
                hub.Message = SceneName;
                print(">>>>>> " + SceneName);
            };

            //}

            inst = true;
        }

        _planets = new List<GameObject>
        {
            GameObject.Find("Mercury"),
            GameObject.Find("Venera"),
            GameObject.Find("Earth"),
            GameObject.Find("Moon"),
            GameObject.Find("Mars"),
            GameObject.Find("Jupiter"),
            GameObject.Find("Saturn"),
            GameObject.Find("Uranus"),
            GameObject.Find("Pluton")
        };


    }
    private void TogglePlanets(bool val)
    {
        _planets.ForEach(x => x.SetActive(val));
    }

    public void ChangeScene()
    {

        switch (SceneName.ToLower())
        {
            case "whiteboard":
                print("Load");

                Application.LoadLevel("WhiteBoard_Room");


                break;
            case "main":
                print("mainmenu");

                Application.LoadLevel(0);


                break;
            case "space":
                print("space");

                Application.LoadLevel("Space");


                break;
            case "holodeck":
                print("Holodeck");
                Application.LoadLevel("Holodeck");
                break;
            case "space-start":
                IsRotating = true;
               
                break;

            case "space-stop":
                IsRotating = false;

                break;
            case "planet-show":
                this.TogglePlanets(true);

                break;
            case "planet-hide":
                this.TogglePlanets(false);

                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
        ChangeScene();
    }

    void Space(bool sto)
    {
       
    }
}

public class HttpRequestRecievedEventArgs : EventArgs
{
    public string Url { get; set; }
}

public class EchoHub
{
    public event EventHandler HttpRequestRecieved;
    public string Message { get; set; }
    private static EchoHub _instance = null;
    public bool IsInstanced = false;

    public static EchoHub Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EchoHub();
            }
            return _instance;

        }
    }
    private EchoHub()
    {
        thread = new Thread(() => this.StartServer("http://+:8080/"));
        thread.Start();
    }

    private HttpListener listener = new HttpListener();
    private Thread thread = null;

    private void StartServer(string baseUrl)
    {

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

        handler.Invoke(this, e);
    }

}