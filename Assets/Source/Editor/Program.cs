using System;
using System.Diagnostics;
using System.Reactive.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public static class Program
    {
        private static Process _server;

        [InitializeOnLoadMethod]
        private static void Main() =>
            Observable
                .FromEvent<Action<PlayModeStateChange>, PlayModeStateChange>(
                    handler => EditorApplication.playModeStateChanged += handler,
                    handler => EditorApplication.playModeStateChanged -= handler)
                .Subscribe(playMode => (playMode switch
                {
                    PlayModeStateChange.EnteredPlayMode => () =>
                    {
                        // Build.WindowsServer();
                        _server = new Process(); 
                        _server.StartInfo = new ProcessStartInfo("Game.Remoting.Server.exe")
                        {
                            WorkingDirectory = $"{Application.dataPath}/../Build/Windows/Server",
                            UseShellExecute = true,
                            // WindowStyle = ProcessWindowStyle.Hidden
                        };
                        _server.Start();
                    },
                    PlayModeStateChange.ExitingPlayMode => () => _server?.Kill(),
                    _ => (Action)(() => { })
                })());
    }
}