using System;
using static FunctionUtil;

class Program {
  public static void Main (string[] args)
  {
    // var f = sin(x) * sin(x) + cos(x) * cos(x) + e;
    var f = ln(x);
    
    Console.WriteLine("f(x) = " + f + "\nf(10) = " + f[10]);
  }
}