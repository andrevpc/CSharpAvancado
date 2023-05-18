using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

int qtSimu = 10_000;
int atkvictories = 0;
int defvictories = 0;

Stopwatch sw = new();

sw.Start();
Parallel.For (0, qtSimu, k =>
{
    int atksoldiers = 1000;
    int defsoldiers = 585;
    Random randNum = new();

    while (atksoldiers > 1 && defsoldiers > 0)
    {
        byte[] atkdices = new byte[3];
        randNum.NextBytes(atkdices);

        byte[] defdices = new byte[3];
        randNum.NextBytes(defdices);
        
        List<byte> atkdiceslist = atkdices.OrderByDescending(s => s)
                     .ToList();

        List<byte> defdiceslist = defdices.OrderByDescending(s => s)
                     .ToList();

        if (atksoldiers >= 3 && defsoldiers >= 3)
        {
            for(int i = 0; i < 3; i++)
            {
                if (atkdiceslist[i]/44+1 > defdiceslist[i]/44+1)
                {
                    defsoldiers--;
                }
                else
                {
                    atksoldiers--;
                }
            }
        }
        else
        {
            for (int i = 0; i < (atksoldiers > defsoldiers ? defsoldiers : atksoldiers); i++)
            {
                if (atkdiceslist[i]/44+1 > defdiceslist[i]/44+1)
                {
                    defsoldiers--;
                }
                else
                {
                    atksoldiers--;
                }
            }
        }
    }
    if (atksoldiers>defsoldiers)
        Interlocked.Increment(ref atkvictories);
    else
        Interlocked.Increment(ref defvictories);
});

sw.Stop();

Console.Write($"Atacantes: {(float)atkvictories/qtSimu*100}% de vitorias\nDefensores: {(float)defvictories/qtSimu*100}% de vitorias\n");
Console.Write(sw.ElapsedMilliseconds + " ms");