/*
MIT License

Copyright (c) 2020

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using ExoGame2D.Interfaces;
using Microsoft.Xna.Framework;

namespace ExoGame2D.GameOfLife
{
	public class Grid : IRenderNode
	{
		private Point Size { get; set; }
        public string Name { get; set;}
        private Cell[,] _currentCellState;
		private Cell[,] _nextCellStates;
		private TimeSpan updateTimer;

		public Grid(int sizeX, int sizeY)
		{
			Size = new Point(sizeX, sizeY);
			
			_currentCellState = new Cell[Size.X, Size.Y];
			_nextCellStates = new Cell[Size.X, Size.Y];

			for (int i = 0; i < Size.X; i++)
			{
				for (int j = 0; j < Size.Y; j++)
				{
					_currentCellState[i, j] = new Cell(new Point(i, j));
					_nextCellStates[i, j] = new Cell(new Point(i, j));
				}
			}

			updateTimer = TimeSpan.Zero;
		}

		public void ClearGrid()
		{
			for (int x = 0; x < Size.X; x++)
			{
				for (int y = 0; y < Size.Y; y++)
				{
					_nextCellStates[x, y] = new Cell(new Point(x, y));
				}
			}

			ApplyNewCellStates();
		}

		public void Update(GameTime gameTime)
		{		
			foreach (Cell cell in _currentCellState)
            {
                cell.Update();
            }

            if (GameLoop.Paused)
            {
                return;
            }

            updateTimer += gameTime.ElapsedGameTime;

			if (updateTimer.TotalMilliseconds > 1000f / GameLoop.GenerationsPerSecond)
			{
				updateTimer = TimeSpan.Zero;
	
				for (int i = 0; i < Size.X; i++)
				{
					for (int j = 0; j < Size.Y; j++)
					{
						// Check the cell's current state, count its living neighbors, and apply the rules to set its next state.
						bool living = _currentCellState[i, j].IsAlive;
						int count = CountAliveNeighbours(i, j);

						bool result = false;

						if (living && count < 2)
						{
							result = false;
						}

						if (living && (count == 2 || count == 3))
						{
							result = true;
						}

						if (living && count > 3)
						{
							result = false;		
						}

						if (!living && count == 3)
						{
							result = true;
						}
                        
						_nextCellStates[i, j].IsAlive = result;                
                    }
				}

				ApplyNewCellStates();
			}
		}

		private int CountAliveNeighbours(int x, int y)
		{
			int count = 0;

			// Cells to the right.
			if (x != Size.X - 1)
			{
				if (_currentCellState[x + 1, y].IsAlive)
				{
					count++;
				}
			}

			// Celss to the bottom right.
			if (x != Size.X - 1 && y != Size.Y - 1)
			{
				if (_currentCellState[x + 1, y + 1].IsAlive)
				{
					count++;
				}
			}

			// Cells on the bottom.
			if (y != Size.Y - 1)
			{
				if (_currentCellState[x, y + 1].IsAlive)
				{
					count++;
				}
			}

			// Cells on the bottom left.
			if (x != 0 && y != Size.Y - 1)
			{
				if (_currentCellState[x - 1, y + 1].IsAlive)
				{
					count++;
				}
			}

			// Cells to the left.
			if (x != 0)
			{
				if (_currentCellState[x - 1, y].IsAlive)
				{
					count++;
				}
			}

			// Cells to the top left.
			if (x != 0 && y != 0)
			{
				if (_currentCellState[x - 1, y - 1].IsAlive)
				{
					count++;
				}
			}

			// Cells on top.
			if (y != 0)
			{
				if (_currentCellState[x, y - 1].IsAlive)
				{
					count++;
				}
			}

			// Cells to the top right.
			if (x != Size.X - 1 && y != 0)
			{
				if (_currentCellState[x + 1, y - 1].IsAlive)
				{
					count++;
				}
			}

			return count;
		}

		private void ApplyNewCellStates()
		{
			for (int i = 0; i < Size.X; i++)
			{
				for (int j = 0; j < Size.Y; j++)
				{
					// If the current cell state is dead and we are spawing a new cell there, set its generation left to live to max
					if (!_currentCellState[i, j].IsAlive && _nextCellStates[i, j].IsAlive)
                    {
						_currentCellState[i, j].IsAlive = _nextCellStates[i, j].IsAlive;
						_currentCellState[i, j].GenerationsLeftToLive = 255;
					}
                    else
                    {
						_currentCellState[i, j].IsAlive = _nextCellStates[i, j].IsAlive;
					}
				}
			}
		}	

        public void Draw(GameTime gameTime)
        {
			foreach (Cell cell in _currentCellState)
			{
				cell.Draw(Engine.SpriteBatch);
			}
        }

        public void Draw(GameTime gameTime, Color tint)
        {
            throw new NotImplementedException();
        }

        public ISprite GetSprite()
        {
			throw new NotImplementedException();
		}

        public bool IsAssetOfType(Type type)
        {
			return GetType() == type;
		}
    }
}
