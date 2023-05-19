using System;
using static FunctionUtil;
using Functions;

class Program {
  public static void Main (string[] args)
  {
    // var f = sin(x) * sin(x) + cos(x) * cos(x) + e;
    
    var f = new Constant(2)*x + new Constant(1);
    Console.WriteLine("f(x) = " + f + "\nf(10) = " + f[10]);
    f = f.Derive();
    Console.WriteLine("f(x) = " + f + "\nf(10) = " + f[10]);
  }
}