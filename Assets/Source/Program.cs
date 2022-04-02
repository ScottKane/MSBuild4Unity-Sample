using System;
using Game.Domain.Models;
using UnityEngine;

namespace Game
{
    internal static class Program
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Main()
        {
            var player = new Player { Id = Guid.NewGuid() };
            Debug.Log(player.Id);
        }
    }
}
