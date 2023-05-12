using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

Random randNum = new();
Army atk = new();
Army def = new();
int qtSimu = 10_000;

Stopwatch sw = new();

sw.Start();
for (int k = 0; k < qtSimu; k++)
{
    atk.soldiers = 1000;
    def.soldiers = 585;
    while (atk.soldiers > 1 && def.soldiers > 0)
    {
        atk.rollDices(randNum);
        def.rollDices(randNum);
        atk.attack(def);
    }
    if (atk.soldiers>def.soldiers)
        atk.victories++;
    else
        def.victories++;
}
sw.Stop();

Console.Write($"Atacantes: {(float)atk.victories/qtSimu*100}% de vitorias\nDefensores: {(float)def.victories/qtSimu*100}% de vitorias\n");
Console.Write(sw.ElapsedMilliseconds + " ms");

public class Army
{
    public int soldiers { get; set; }
    public int victories { get; set; } = 0;
    public List<int> dices = new List<int>();
    public List<int> rollDices(Random randNum)
    {
        dices.Clear();
        for (int i = 0; i < 3; i++)
        {
            dices.Add((randNum.Next(6) + 1));
        }
        dices = dices.OrderByDescending(s => s)
                     .ToList();
        return dices;
    }

    public void attack(Army def)
    {
        if (this.soldiers >= 3 && def.soldiers >= 3)
        {
            for(int i = 0; i < 3; i++)
            {
                if (this.dices[i] > def.dices[i])
                {
                    def.soldiers--;
                }
                else
                {
                    this.soldiers--;
                }
            }
        }
        else
        {
            for (int i = 0; i < (this.soldiers > def.soldiers ? def.soldiers : this.soldiers); i++)
            {
                if (this.dices[i] > def.dices[i])
                {
                    def.soldiers--;
                }
                else
                {
                    this.soldiers--;
                }
            }
        }
    }
}