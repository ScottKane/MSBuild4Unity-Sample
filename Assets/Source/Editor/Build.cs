using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Game.Editor
{
    public static class Build
    {
        [MenuItem("Build/Build All")]
        public static void All()
        {
            WindowsClient();
            LinuxClient();
            WindowsServer();
            LinuxServer();
        }

        [MenuItem("Build/Build Client (Windows)")]
        public static void WindowsClient() => Client(BuildTarget.StandaloneWindows64);

        [MenuItem("Build/Build Client (Linux)")]
        public static void LinuxClient() => Client(BuildTarget.StandaloneLinux64);
        
        private static void Client(BuildTarget target)
        {
            var platform = target is BuildTarget.StandaloneWindows64 ? "Windows" : "Linux";
            var extension = target is BuildTarget.StandaloneWindows64 ? ".exe" : string.Empty;
            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = new[] {"Assets/Scenes/Sandbox.unity"},
                locationPathName = @$"Build/{platform}/Game{extension}",
                target = target,
                options = BuildOptions.CompressWithLz4HC
            };

            Debug.Log($"Building {platform} Client...");
            BuildPipeline.BuildPlayer(buildPlayerOptions);
            Debug.Log($"Built {platform} Client.");
        }
        
        [MenuItem("Build/Build Server (Windows)")]
        public static void WindowsServer() => Server(BuildTarget.StandaloneWindows64);

        [MenuItem("Build/Build Server (Linux)")]
        public static void LinuxServer() => Server(BuildTarget.StandaloneLinux64);

        private static void Server(BuildTarget target)
        {
            var platform = target is BuildTarget.StandaloneWindows64 ? "Windows" : "Linux";
            Debug.Log($"Building {platform} Server...");
            
            var platformTag = target is BuildTarget.StandaloneWindows64 ? "win-x64" : "linux-x64";
            
            var serverInputPath = @$"{Application.dataPath}/../Source/Game.Remoting/Server";
            var serverOutputPath = @$"{Application.dataPath}/../Build/{platform}/Server";
            
            var serverBuild = new Process(); 
            serverBuild.StartInfo = new ProcessStartInfo("dotnet")
            {
                Arguments = @$"publish {serverInputPath}/Game.Remoting.Server.csproj -c Release -r {platformTag} -p:PublishSingleFile=true -p:DebugType=None --self-contained false -o {serverOutputPath}",
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            
            serverBuild.Start();
            serverBuild.WaitForExit();
            if (serverBuild.ExitCode is not 0) throw new Exception("Failed to build Game.Remoting.Server.");
            Debug.Log($"Built {platform} Server.");
        }
    }
}