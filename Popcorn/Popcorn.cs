using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Pet
{
    Random random = new Random();
    public bool GameOver = false;
    public int health = 100;
    public int sleep = 100;
    public int mood = 100;
    public int command = 0;
    public int step = 0;

    public int Feed()
    {
        return health += random.Next(10, 30);
    }

    public int ToSleep()
    {
        return sleep += random.Next(10, 30);
    }

    public int Play()
    {
        return mood += random.Next(10, 30);
    }

    public void Update()
    {
        if (random.Next(1, 4) == 1)
        {
            health -= random.Next(30, 40);
        }

        if (random.Next(1, 4) == 2)
        {
            sleep -= random.Next(30, 40);
        }

        if (random.Next(1, 4) == 3)
        {
            mood -= random.Next(30, 40);
        }
    }

    public int HealthInfo()
    {
        return health;
    }

    public int SleepInfo()
    {
        return sleep;
    }

    public int MoodInfo()
    {
        return mood;
    }

    public bool Check()
    {   

        if ((health <= 0) || (sleep <= 0) || (mood <= 0))
            GameOver = true;
        return GameOver;
    }

    public void CheckStats()
    {
        if (health > 100)
        {
            health = 100;
        }

        if (sleep > 100)
        {
            sleep = 100;
        }

        if (mood > 100)
        {
            mood = 100;
        }

        if (health < 40 && health > 0)
        {
            Console.WriteLine("You pet is hungry");
        }

        if (sleep < 40 && sleep > 0)
        {
            Console.WriteLine("You pet wants to sleep");
        }

        if (mood < 40 && mood > 0)
        {
            Console.WriteLine("You pet wants to play");
        }
    }

    public void Command()
    {
         if (command == 1)
            Feed();

        if (command == 2)
            ToSleep();

        if (command == 3)
            Play();
    }
}

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {  
            Pet pet = new Pet();
           
            

            while (pet.GameOver == false)
            {
                Console.WriteLine("    _______");
                Console.WriteLine("    |  + + | ");
                Console.WriteLine("     | -- | ");
                Console.WriteLine("     |    |");
                Console.WriteLine("    @@@@@@@");
                Console.WriteLine("    @@   @@");

                pet.Update();
                pet.Check();
                pet.CheckStats();
                pet.Command();
  
                Console.WriteLine(pet.step++ + " Step ");
                Console.WriteLine("Health: " + pet.HealthInfo());
                Console.WriteLine("Sleep: " + pet.SleepInfo());
                Console.WriteLine("Mood: " + pet.MoodInfo());
                Console.WriteLine("Write command 1 - food | 2 - sleep | 3 - mood");                              
                pet.command = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
               
            }
            Console.WriteLine("Game Over");
            Console.ReadKey();
        }
       
    }
}

   


