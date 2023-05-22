using System;
using static FunctionUtil;
using Functions;

class Program {
  public static void Main (string[] args)
  {
    // var f = sin(x) * sin(x) + cos(x) * cos(x) + e;
    
    var f = x ^ new Constant(3);
    Console.WriteLine("f(x) = " + f + "\nf(10) = " + f[2]);
    f = f.Derive();
    Console.WriteLine("f(x) = " + f + "\nf(10) = " + f[2]);
  }
}