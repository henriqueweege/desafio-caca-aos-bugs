using System.Runtime.InteropServices;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace CreditAccountingService.FunctionalTests;

public static class ContainerKiller
{
    public static void KillUpContainers()
    {
        var dockerClient = new DockerClientConfiguration(new Uri(DockerApiUri())).CreateClient();
        var containersUp =   dockerClient.Containers.ListContainersAsync(new ContainersListParameters()).Result;
        foreach (var container in containersUp)
        {
             dockerClient.Containers.KillContainerAsync(container.ID, new ContainerKillParameters()).Wait();
        }
    }
    
    private static string DockerApiUri()
    {
        
        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
 
        if (isWindows)
        {
            return "npipe://./pipe/docker_engine";
        }
 
        var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
 
        if (isLinux)
        {
            return "unix:/var/run/docker.sock";
        }
 
        throw new Exception("Was unable to determine what OS this is running on, does not appear to be Windows or Linux.");
    }
}