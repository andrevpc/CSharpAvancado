using System;
using static FunctionUtil;

class Program {
  public static void Main (string[] args)
  {
    var f = sin(x) * sin(x) + cos(x) * cos(x);
    
    // var f = -log(2, x);

    Console.WriteLine("f(x) = " + f + "\nf(10) = " + f[10]);
    f = f.Derive();
    Console.WriteLine("f(x) = " + f + "\nf(10) = " + f[10]);
  }
}