using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{

    static class CharExtensions
    {
        public static Cube.Letter ParseLetter(this char letter)
        {
            switch (letter)
            {
                case 'F':
                    return Cube.Letter.F;
                case 'L':
                    return Cube.Letter.L;
                case 'R':
                    return Cube.Letter.R;
                case 'B':
                    return Cube.Letter.B;
                case 'D':
                    return Cube.Letter.D;
                case 'U':
                    return Cube.Letter.U;
                default:
                    return Cube.Letter.N;
            }
        }
        public static Cube.Turn ParseTurn(this char turn)
        {
            switch (turn)
            {
                case '1':
                case '+':
                    return Cube.Turn.CW_90;
                case '2':
                    return Cube.Turn.CW_180;
                case '3':
                case '-':
                    return Cube.Turn.CW_270;
                default:
                    return Cube.Turn.Nop;
            }
        }
    }

}
