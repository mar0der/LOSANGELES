using System;
using BalloonsPops.Data;
using BalloonsPops.Interfaces;

namespace BalloonsPops 
{ 
   public class BaloonsPopGame
    {
       private static void Main(string[] args)
       {
           var engine = new Engine();
           engine.Run();
       }
    }
}
