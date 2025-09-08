using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectX
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }

        static bool Check(int[,] pole, int pocetNaVyhru, int hrac, int[] pozice)
        {
            return CheckRow(pole, pocetNaVyhru, hrac, pozice) || CheckColumn(pole, pocetNaVyhru, hrac, pozice) || CheckDiagon1(pole, pocetNaVyhru, hrac, pozice) || checkDiagon2(pole, pocetNaVyhru, hrac, pozice);
        }

        static bool CheckRow(int[,] pole, int pocetNaVyhru, int hrac, int[] pozice)
        {
            int row = pozice[0];
            int left = 0;
            int right = 0;
            int column = pozice[1];
            while (true)
            {
                column--;
                if (column < 0)
                {
                    break;
                }
                else if (pole[row,column] == hrac)
                {
                    left++;
                }
                else
                {
                    break;
                }
            }
            column = pozice[1];
            while (true)
            {
                column++;
                if (column >= pole.GetLength(1))
                {
                    break;
                }
                else if (pole[row, column] == hrac)
                {
                    right++;
                }
                else
                {
                    break;
                }
            }
            if (left + right + 1 >= pocetNaVyhru)
                return true;
            return false;
        }

        static bool CheckColumn(int[,] pole, int pocetNaVyhru, int hrac, int[] pozice)
        {
            int row = pozice[0];
            int up = 0;
            int down = 0;
            int column = pozice[1];
            while (true)
            {
                row--;
                if (row < 0)
                {
                    break;
                }
                else if (pole[row, column] == hrac)
                {
                    up++;
                }
                else
                {
                    break;
                }
            }
            row = pozice[0];
            while (true)
            {
                row++;
                if (row >= pole.GetLength(0))
                {
                    break;
                }
                else if (pole[row, column] == hrac)
                {
                    down++;
                }
                else
                {
                    break;
                }
            }
            if (up + down + 1 >= pocetNaVyhru)
                return true;
            return false;
        }

        static bool CheckDiagon1(int[,] pole, int pocetNaVyhru, int hrac, int[] pozice)
        {
            int row = pozice[0];
            int left = 0;
            int right = 0;
            int column = pozice[1];
            while (true)
            {
                column--;
                row--;
                if (column < 0 || row < 0)
                {
                    break;
                }
                else if (pole[row, column] == hrac)
                {
                    left++;
                }
                else
                {
                    break;
                }
            }
            row = pozice[0];
            column = pozice[1];
            while (true)
            {
                column++;
                row++;
                if (column >= pole.GetLength(1) || row >= pole.GetLength(0))
                {
                    break;
                }
                else if (pole[row, column] == hrac)
                {
                    right++;
                }
                else
                {
                    break;
                }
            }
            if (left + right + 1 >= pocetNaVyhru)
                return true;
            return false;
        }

        static bool CheckDiagon2(int[,] pole, int pocetNaVyhru, int hrac, int[] pozice) //TODO
        {
            int row = pozice[0];
            int left = 0;
            int right = 0;
            int column = pozice[1];
            while (true)
            {
                column--;
                row++;
                if (column < 0 || row < 0)
                {
                    break;
                }
                else if (pole[row, column] == hrac)
                {
                    left++;
                }
                else
                {
                    break;
                }
            }
            row = pozice[0];
            column = pozice[1];
            while (true)
            {
                column++;
                row--;
                if (column >= pole.GetLength(1) || row >= pole.GetLength(0))
                {
                    break;
                }
                else if (pole[row, column] == hrac)
                {
                    right++;
                }
                else
                {
                    break;
                }
            }
            if (left + right + 1 >= pocetNaVyhru)
                return true;
            return false;
        }
    }
}
