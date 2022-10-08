using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_safe
{
    internal class Game
    {
        public SafeState[] GameField = null!;
        public readonly int FieldSize = 5;

        public Game(int fieldSize)
        {
            FieldSize = fieldSize;
            GameField = new SafeState[FieldSize * FieldSize];
            for (int i = 0; i < FieldSize * FieldSize; i++)
            {
                GameField[i] = new SafeState();
            }
        }

        //запутывает игровое поле, все действия эквивалентны действиям игрока => обратимы => такие поля 100% решаемы
        public void CreateGame()
        {
            Random rand = new Random((int)new DateTime().TimeOfDay.TotalMilliseconds);
            int turnsToMake = rand.Next(40) + 10; //тут можно покрутить сложность
            for (int i = 0; i < turnsToMake; i++)
            {
                Switch(rand.Next(FieldSize), rand.Next(FieldSize));
            }
        }

        public void Switch(int _x, int _y)
        {
            for (int x = 0; x < FieldSize; x++)
            {
                for (int y = 0; y < FieldSize; y++)
                {
                    if(x == _x || y == _y)
                        GameField[x * FieldSize + y].State = !GameField[x * FieldSize + y].State;
                }
            }
        }

        public bool CheckIfOpen()
        {
            bool checkState = GameField[0].State;
            for (int i = 0; i < FieldSize*FieldSize; i++)
            {
                if (GameField[i].State != checkState) return false;
            }
            return true;
        }

        public bool Get(int _x, int _y)
        {
            return GameField[_x * FieldSize +  _y].State;
        }
    }
}
