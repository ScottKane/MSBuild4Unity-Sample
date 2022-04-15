using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Game.Editor
{
    public class ServerBuildProcessor : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;
        
        public void OnPostprocessBuild(BuildReport report) => Build.WindowsServer();
    }
}