using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cube c = new Cube("UF UR UB UL DF DR DB DL FR FL BR BL UFR URB UBL ULF DRF DFL DLB DBR", "L2 B-"); 
            string start = "UF UR UB UL DF DR DB DL FR FL BR BL UFR URB UBL ULF DRF DFL DLB DBR";
            //string start = "UB DF UF DR UL BR DB BL FL DL UR FR BRD DFL UFR LFU UBL BDL DRF BUR";
            Cube c = new Cube(start);
            Console.WriteLine("Starting combination: ");
            Console.WriteLine(c.PrintCube());
            Console.WriteLine(c.IsStandard());
            //Test2(c);
            Console.ReadLine();
        }

        static void Test2(Cube c)
        {
            string moves = "F3 U+ F3 U+ F3 U+ F3 U+ F3 U+";
            Console.WriteLine("Moves: ");
            Console.WriteLine(moves);
            Console.WriteLine("-- Making moves --");
            c.MakeMoves(moves);
            Console.WriteLine("-- Done --");
            string real = "UF UR LF DL DF DR RB UL FR BL BU BD UFR LDF LFU DLB DRF UBL RDB RBU";
            string res = c.PrintCube();
            Console.WriteLine("Current combination: ");
            Console.WriteLine(res);
            Console.WriteLine("Real combination: ");
            Console.WriteLine(real);
            Console.Write("Is equals? ");
            Console.WriteLine(real == res);
        }

        static void Test(Cube c)
        {
            string move = "L+";
            string real = "UB DF UF FR UL BR DB DL FL DR UR BL BRD DFL FDR FRU UBL ULF DLB BUR";
            c.MakeMoves(move);
            string res = c.PrintCube();
            Console.WriteLine(res);
            Console.WriteLine(real);
            Console.WriteLine(real == res);
            move = "U+";
            real = "DF UF FR UB UL BR DB DL FL DR UR BL DFL FDR FRU BRD UBL ULF DLB BUR"; 
            c.MakeMoves(move);
            res = c.PrintCube();
            Console.WriteLine(res);
            Console.WriteLine(real);
            Console.WriteLine(real == res);
            move = "F2";
            real = "UL UF FR UB DF BR DB DL DR FL UR BL ULF FDR FRU UBL BRD DFL DLB BUR";
            c.MakeMoves(move);
            res = c.PrintCube();
            Console.WriteLine(res);
            Console.WriteLine(real);
            Console.WriteLine(real == res);
            move = "U+";
            real = "UF FR UB UL DF BR DB DL DR FL UR BL FDR FRU UBL ULF BRD DFL DLB BUR";
            c.MakeMoves(move);
            res = c.PrintCube();
            Console.WriteLine(res);
            Console.WriteLine(real);
            Console.WriteLine(real == res);
            move = "R-";
            real = "UF UR UB UL DF DR DB DL FR FL BR BL UFR URB UBL ULF DRF DFL DLB DBR";
            c.MakeMoves(move);
            res = c.PrintCube();
            Console.WriteLine(res);
            Console.WriteLine(real);
            Console.WriteLine(real == res);
        }
    }


}
