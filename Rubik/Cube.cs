using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    class Cube
    {
        public enum Letter
        {
            N = 0,
            F,
            L,
            R,
            B,
            D,
            U
        }

        public enum Turn
        {
            CW_90 = 1,
            CW_180,
            CW_270,
            Nop
        }

        class Node
        {
            private readonly Letter[] _standard;

            public Node(string combination, params Letter[] sides)
            {
                _standard = sides;

                Sides = new Dictionary<Letter, Letter>
                {
                    {Letter.F, Letter.N},
                    {Letter.L, Letter.N},
                    {Letter.R, Letter.N},
                    {Letter.B, Letter.N},
                    {Letter.D, Letter.N},
                    {Letter.U, Letter.N}
                };

                for (int i = 0; i < combination.Length; i++)
                {
                    Sides[sides[i]] = combination[i].ParseLetter();
                }
            }

            private Node(Letter[] vals, Letter[] sides)
            {
                _standard = sides;
                Sides = new Dictionary<Letter, Letter>
                {
                    {Letter.F, Letter.N},
                    {Letter.L, Letter.N},
                    {Letter.R, Letter.N},
                    {Letter.B, Letter.N},
                    {Letter.D, Letter.N},
                    {Letter.U, Letter.N}
                };

                for (int i = 0; i < 6; i++)
                {
                    Sides[sides[i]] = vals[i];
                }
            }

            public Node Clone()
            {
                Letter[] sides = new Letter[6];
                for (int i = 0; i < _standard.Length; i++)
                {
                    sides[i] = _standard[i];
                }

                Letter[] vals = new Letter[6];
                for (int i = 0; i < 6; i++)
                {
                    vals[i] = Sides[sides[i]];
                }

                return new Node(vals, sides);
            }

            public short GetShortForm()
            {
                List<Letter> letters = _standard.Select(letter => Sides[letter]).ToList();

                int mult = 1;
                int ans = 0;
                for (int i = letters.Count -1; i > -1; i--)
                {
                    ans += (int)letters[i] * mult;
                    mult *= 10;
                }

                return (short)ans;
            }

            public Dictionary<Letter, Letter> Sides { get; set; }

            public string GetState()
            {
                return _standard.Aggregate("", (current, letter) => current + Sides[letter]);
            }
        }

        // Top Ring
        Node N1,
            N2,
            N3,
            N4,
            N13,
            N14,
            N15,
            N16;
        // 15 03 14
        // 04 xx 02
        // 16 01 13

        // Middle Ring
        Node N9, N10, N11, N12;
        // 12 xx 11
        // xx xx xx
        // 10 xx 09

        // Bottom Ring
        Node N5,
            N6,
            N7,
            N8,
            N17,
            N18,
            N19,
            N20;
        // 19 07 20
        // 08 xx 06
        // 18 05 17

        private Node[] _nodes;

        private readonly short[] _standardCombination = 
        {
            // Числовая версия UF UR UB ... эталона
            61, 63, 64, 62, 51, 53, 54, 52, 13, 12, 43, 42, 613, 634, 642, 621, 531, 512, 524, 543
        };


        public Cube(string start)
        {
            ParseNodes(start);
        }

        public bool IsStandard()
        {
            for (int i = 0; i < 20; i++)
            {
                if (_nodes[i].GetShortForm() != _standardCombination[i])
                    return false;
            }
            return true;
        }

        public void MakeMoves(string moves)
        {
            ParseMoves(moves);
        }

        public string PrintCube()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(N1.GetState());
            for (int i = 1; i < 20; i++)
            {
                sb.Append(' ');
                sb.Append(_nodes[i].GetState());
            }

            return sb.ToString();
        }

        private void ParseNodes(string input)
        {
            // UF UR UB UL  DF DR DB DL  FR FL  BR BL  UFR URB UBL ULF  DRF DFL DLB DBR
            // N1 N2 N3 N4  N5 N6 N7 N8  N9 10  11 12  N13 N14 N15 N16  N17 N18 N19 N20

            string[] blocks = input.Split(' ');

            N1 = new Node(blocks[0], Letter.U, Letter.F);
            N2 = new Node(blocks[1], Letter.U, Letter.R);
            N3 = new Node(blocks[2], Letter.U, Letter.B);
            N4 = new Node(blocks[3], Letter.U, Letter.L);
            N5 = new Node(blocks[4], Letter.D, Letter.F);
            N6 = new Node(blocks[5], Letter.D, Letter.R);
            N7 = new Node(blocks[6], Letter.D, Letter.B);
            N8 = new Node(blocks[7], Letter.D, Letter.L);
            N9 = new Node(blocks[8], Letter.F, Letter.R);
            N10 = new Node(blocks[9], Letter.F, Letter.L);
            N11 = new Node(blocks[10], Letter.B, Letter.R);
            N12 = new Node(blocks[11], Letter.B, Letter.L);
            N13 = new Node(blocks[12], Letter.U, Letter.F, Letter.R);
            N14 = new Node(blocks[13], Letter.U, Letter.R, Letter.B);
            N15 = new Node(blocks[14], Letter.U, Letter.B, Letter.L);
            N16 = new Node(blocks[15], Letter.U, Letter.L, Letter.F);
            N17 = new Node(blocks[16], Letter.D, Letter.R, Letter.F);
            N18 = new Node(blocks[17], Letter.D, Letter.F, Letter.L);
            N19 = new Node(blocks[18], Letter.D, Letter.L, Letter.B);
            N20 = new Node(blocks[19], Letter.D, Letter.B, Letter.R);

            _nodes = new Node[20];
            _nodes[0] = N1;
            _nodes[1] = N2;
            _nodes[2] = N3;
            _nodes[3] = N4;
            _nodes[4] = N5;
            _nodes[5] = N6;
            _nodes[6] = N7;
            _nodes[7] = N8;
            _nodes[8] = N9;
            _nodes[9] = N10;
            _nodes[10] = N11;
            _nodes[11] = N12;
            _nodes[12] = N13;
            _nodes[13] = N14;
            _nodes[14] = N15;
            _nodes[15] = N16;
            _nodes[16] = N17;
            _nodes[17] = N18;
            _nodes[18] = N19;
            _nodes[19] = N20;

        }


        private void ParseMoves(string input)
        {
            // L+ U+ F2 U+ R-
            string[] blocks = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string block in blocks)
            {
                Letter side = block[0].ParseLetter();
                Turn turn = block[1].ParseTurn();
                for (int i = 0; i < (int)turn; i++)
                {
                    MakeTurn(side);
                }
            }
        }

        private void MakeTurn(Letter side)
        {
            Node[] Side = new Node[8];
            switch (side)
            {
                case Letter.F:
                    Side[0] = N16;
                    Side[1] = N1;
                    Side[2] = N13;
                    Side[3] = N9;
                    Side[4] = N17;
                    Side[5] = N5;
                    Side[6] = N18;
                    Side[7] = N10;
                    break;
                case Letter.L:
                    Side[0] = N15;
                    Side[1] = N4;
                    Side[2] = N16;
                    Side[3] = N10;
                    Side[4] = N18;
                    Side[5] = N8;
                    Side[6] = N19;
                    Side[7] = N12;
                    break;
                case Letter.R:
                    Side[0] = N13;
                    Side[1] = N2;
                    Side[2] = N14;
                    Side[3] = N11;
                    Side[4] = N20;
                    Side[5] = N6;
                    Side[6] = N17;
                    Side[7] = N9;
                    break;
                case Letter.B:
                    Side[0] = N14;
                    Side[1] = N3;
                    Side[2] = N15;
                    Side[3] = N12;
                    Side[4] = N19;
                    Side[5] = N7;
                    Side[6] = N20;
                    Side[7] = N11;
                    break;
                case Letter.D:
                    Side[0] = N18;
                    Side[1] = N5;
                    Side[2] = N17;
                    Side[3] = N6;
                    Side[4] = N20;
                    Side[5] = N7;
                    Side[6] = N19;
                    Side[7] = N8;
                    break;
                case Letter.U:
                    Side[0] = N15;
                    Side[1] = N3;
                    Side[2] = N14;
                    Side[3] = N2;
                    Side[4] = N13;
                    Side[5] = N1;
                    Side[6] = N16;
                    Side[7] = N4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, "..?");
            }
            TurnSide(Side, side);
        }

        /// <summary>
        /// Вращает на 90 градусов по часовой
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="side"></param>
        private void TurnSide(Node[] blocks, Letter side)
        {
            Letter
                U_side = Letter.N,
                L_side = Letter.N,
                R_side = Letter.N,
                D_side = Letter.N;
            switch (side)
            {
                case Letter.F:
                    U_side = Letter.U;
                    L_side = Letter.L;
                    R_side = Letter.R;
                    D_side = Letter.D;
                    break;
                case Letter.L:
                    U_side = Letter.U;
                    L_side = Letter.B;
                    R_side = Letter.F;
                    D_side = Letter.D;
                    break;
                case Letter.R:
                    U_side = Letter.U;
                    L_side = Letter.F;
                    R_side = Letter.B;
                    D_side = Letter.D;
                    break;
                case Letter.B:
                    U_side = Letter.U;
                    L_side = Letter.R;
                    R_side = Letter.L;
                    D_side = Letter.D;
                    break;
                case Letter.D:
                    U_side = Letter.F;
                    L_side = Letter.L;
                    R_side = Letter.R;
                    D_side = Letter.B;
                    break;
                case Letter.U:
                    U_side = Letter.B;
                    L_side = Letter.L;
                    R_side = Letter.R;
                    D_side = Letter.F;
                    break;
            }
            // Повернуть касающиеся грани двойных блоков
            var t = blocks[1].Sides[U_side];
            blocks[1].Sides[U_side] = blocks[7].Sides[L_side];
            blocks[7].Sides[L_side] = blocks[5].Sides[D_side];
            blocks[5].Sides[D_side] = blocks[3].Sides[R_side];
            blocks[3].Sides[R_side] = t;

            // Повернуть переднюю грань
            var f = blocks[1].Sides[side];
            blocks[1].Sides[side] = blocks[7].Sides[side];
            blocks[7].Sides[side] = blocks[5].Sides[side];
            blocks[5].Sides[side] = blocks[3].Sides[side];
            blocks[3].Sides[side] = f;

            f = blocks[0].Sides[side];
            blocks[0].Sides[side] = blocks[6].Sides[side];
            blocks[6].Sides[side] = blocks[4].Sides[side];
            blocks[4].Sides[side] = blocks[2].Sides[side];
            blocks[2].Sides[side] = f;

            // Повернуть грани тройных блоков
            t = blocks[0].Sides[U_side];
            f = blocks[0].Sides[L_side];

            blocks[0].Sides[U_side] = blocks[6].Sides[L_side];
            blocks[0].Sides[L_side] = blocks[6].Sides[D_side];
            blocks[6].Sides[L_side] = blocks[4].Sides[D_side];
            blocks[6].Sides[D_side] = blocks[4].Sides[R_side];
            blocks[4].Sides[D_side] = blocks[2].Sides[R_side];
            blocks[4].Sides[R_side] = blocks[2].Sides[U_side];
            blocks[2].Sides[R_side] = t;
            blocks[2].Sides[U_side] = f;
        }
    }
}
