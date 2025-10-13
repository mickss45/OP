using System;

namespace CSharp
{
	internal class Program
	{
		static void Main()
		{
			Console.Clear();
			string player1, player2;
			Console.SetCursorPosition(30, 0);
			Console.Write("Добро пожаловать в консольную игру: ");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("Морской бой!\n\n\n\n");
			Console.ResetColor();
			Console.Write("Введите имя первого игрока: ");
			player1 = Console.ReadLine();
			Console.Write("\n\nВведите имя второго игрока: ");
			player2 = Console.ReadLine();
			Console.Write("\n\n\nДля продолжения нажмите любую клавишу...");
			Console.ReadKey();
			Console.Clear();
			Console.SetCursorPosition(30, 0);
			Console.Write("Пришло время игроку ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write($"{player1} ");
			Console.ResetColor();
			Console.WriteLine("заполнить свое игрое поле\n");
			Console.SetCursorPosition(30, 2);
			Console.Write($"Возле экрана должен остаться только игрок ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"{player1}");
			Console.ResetColor();
			Console.Write("\n\n\nДля продолжения нажмите любую клавишу...");
			Console.ReadKey();
			Console.Clear();

			int[,] fieldPlayer1 = new int[10, 10];
			Console.WriteLine($"Это ваше игрвое поле, {player1}\n");
			DrawField(fieldPlayer1);
			PlaceShips(fieldPlayer1);

			Console.SetCursorPosition(30, 0);
			Console.Write("Пришло время игроку ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write($"{player2} ");
			Console.ResetColor();
			Console.WriteLine("заполнить свое игрое поле\n");
			Console.SetCursorPosition(30, 2);
			Console.Write("Возле экрана должен остаться только игрок ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"{player2}");
			Console.ResetColor();
			Console.Write("\n\n\nДля продолжения нажмите любую клавишу...");
			Console.ReadKey();
			Console.Clear();

			int[,] fieldPlayer2 = new int[10, 10];
			Console.WriteLine($"Это ваше игрвое поле, {player2}\n");
			DrawField(fieldPlayer2);
			PlaceShips(fieldPlayer2);

			Console.SetCursorPosition(30, 1);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Да начнется же морская баталия!!!");
			Console.ResetColor();
			Console.ReadKey();
			Console.Clear();

			int[,] fieldEnemy1 = new int[10, 10];
			int[,] fieldEnemy2 = new int[10, 10];

			while (CheckIfGameOver(fieldPlayer1, fieldPlayer2))
			{
				Console.SetCursorPosition(30, 0);
				Console.Write("Ход игрока ");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"{player1}\n");
				Console.ResetColor();
				Console.SetCursorPosition(30, 2);
				Console.Write($"Возле экрана должен остаться только игрок ");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"{player1}");
				Console.ResetColor();
				Console.Write("\n\n\nДля продолжения нажмите любую клавишу...");
				Console.ReadKey();
				Console.Clear();
				Attack(fieldPlayer1, fieldEnemy1, fieldPlayer2);
				Console.Clear();

				Console.SetCursorPosition(30, 0);
				Console.Write("Ход игрока ");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"{player2}\n");
				Console.ResetColor();
				Console.SetCursorPosition(30, 2);
				Console.Write($"Возле экрана должен остаться только игрок ");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"{player2}");
				Console.ResetColor();
				Console.Write("\n\n\nДля продолжения нажмите любую клавишу...");
				Console.ReadKey();
				Console.Clear();
				Attack(fieldPlayer2, fieldEnemy2, fieldPlayer1);
				Console.Clear();
			}

			Console.SetCursorPosition(30, 0);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("ИГРА ОКОНЧЕНА!!!\n\n");
			Console.ResetColor();
			Console.SetCursorPosition(30, 2);
			
			if (WhoWin(fieldPlayer1, fieldPlayer2) == 0)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("НИЧЬЯ!!!");
				Console.ResetColor();
			}
			else if (WhoWin(fieldPlayer1, fieldPlayer2) == 1)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"ПОБЕДИЛ {player2}!!!");
				Console.ResetColor();
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"ПОБЕДИЛ {player1}!!!");
				Console.ResetColor();
			}
			Console.ReadKey();
		}

		static void DrawField(int[,] field)
		{
			Console.WriteLine("  A B C D E F G H I J");
			for (int i = 0; i < field.GetLength(0); i++)
			{
				Console.Write($"{i} ");
				for (int j = 0; j < field.GetLength(1); j++)
				{
					switch (field[i, j])
					{
						case 0:
							Console.Write("~ ");
							break;
						case 1:
							Console.ForegroundColor = ConsoleColor.Green;
							Console.Write("■ ");
							break;
						case 2:
							Console.ForegroundColor = ConsoleColor.DarkYellow;
							Console.Write("* ");
							break;
						case 3:
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write("■ ");
							break;
					}
					Console.ResetColor();
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		static void PlaceShips(int[,] field)
		{
			string s1;
			int x, y;
			bool isOpen = true;
			int n = 0;
			while (isOpen)
			{
				Console.WriteLine($"осталось разместить {4 - n}");
				Console.Write("Введите координаты однопалубного корабля (например, A0): ");
				s1 = Console.ReadLine().ToUpper();

				if (s1.Length == 2)
				{
					x = Convert.ToInt32(s1[1]) - 48;
					y = s1[0] - 'A';

					if (y >= 0 && y <= 9 && x >= 0 && x <= 9)
					{
						bool canPlace = true;

						for (int i = -1; i <= 1; i++)
						{
							for (int j = -1; j <= 1; j++)
							{
								int checkX = x + i;
								int checkY = y + j;

								if (checkX >= 0 && checkX <= 9 && checkY >= 0 && checkY <= 9)
								{
									if (field[checkX, checkY] == 1)
									{
										canPlace = false;
										break;
									}
								}
							}

							if (!canPlace)
							{
								break;
							}
						}


						if (canPlace)
						{
							Console.Clear();
							field[x, y] = 1;
							Console.WriteLine($"Это ваше игровое поле\n");
							DrawField(field);
							n += 1;
						}
						else
						{
							Console.WriteLine("Вы не можете разместить корабль в этой клетке\n");
						}
					}
					else
					{
						Console.WriteLine("Корабль выходит за границы поля / вы ввели неправильные координаты\n");
					}

				}
				else
				{
					Console.WriteLine("Неверный формат ввода. Введите координа вида: A0\n");
				}

				if (n == 4)
				{
					isOpen = false;
				}
			}

			string s2;
			int x1, x2, y1, y2;
			char direction2;
			isOpen = true;
			n = 0;
			while (isOpen)
			{
				Console.WriteLine($"осталось разместить {3 - n}");
				Console.Write("Введите координаты двухпалубного корабля и его направление\n(H - горизантальное(вправо), V - вертикальное(вниз)) (например, A0 H): ");
				s2 = Console.ReadLine().ToUpper();
				if (s2.Length == 4 && (s2[3] == 'H' || s2[3] == 'V') && s2[2] == ' ')
				{
					x1 = Convert.ToInt32(s2[1]) - 48;
					y1 = s2[0] - 'A';
					direction2 = s2[3];

					if (direction2 == 'H')
					{
						x2 = x1;
						y2 = y1 + 1;
					}
					else
					{
						x2 = x1 + 1;
						y2 = y1;
					}

					if (y1 >= 0 && y1 <= 9 && x1 >= 0 && x1 <= 9 && y2 >= 0 && y2 <= 9 && x2 >= 0 && x2 <= 9)
					{
						bool canPlace = true;

						for (int offset = 0; offset < 2; offset++)
						{
							int currX, currY;
							if (direction2 == 'H')
							{
								currX = x1;
								currY = y1 + offset;
							}
							else
							{
								currX = x1 + offset;
								currY = y1;
							}

							for (int i = -1; i <= 1; i++)
							{
								for (int j = -1; j <= 1; j++)
								{
									int checkX = currX + i;
									int checkY = currY + j;

									if (checkX >= 0 && checkX <= 9 && checkY >= 0 && checkY <= 9)
									{
										if (field[checkX, checkY] == 1)
										{
											canPlace = false;
											break;
										}
									}
								}
								if (!canPlace)
								{
									break;
								}
							}
							if (!canPlace)
							{
								break;
							}
						}

						if (canPlace)
						{
							Console.Clear();
							field[x1, y1] = 1;
							field[x2, y2] = 1;
							Console.WriteLine($"Это ваше игровое поле\n");
							DrawField(field);
							n += 1;
						}
						else
						{
							Console.WriteLine("Вы не можете разместить корабль в этой позиции\n");
						}
					}
					else
					{
						Console.WriteLine("Корабль выходит за границы поля / вы ввели неправильные координаты\n");
					}
				}
				else
				{
					Console.WriteLine("Неверный формат ввода. Введите вида: A0 H или B1 V\n");
				}
				if (n == 3)
				{
					isOpen = false;
				}
			}

			string s3;
			int x3, y3;
			char direction3;
			isOpen = true;
			n = 0;
			while (isOpen)
			{
				Console.WriteLine($"осталось разместить {2 - n}");
				Console.Write("Введите координаты трехпалубного корабля и его направление\n(H - горизантальное(вправо), V - вертикальное(вниз)) (например, A0 H): ");
				s3 = Console.ReadLine().ToUpper();
				if (s3.Length == 4 && (s3[3] == 'H' || s3[3] == 'V') && s3[2] == ' ')
				{
					x1 = Convert.ToInt32(s3[1]) - 48;
					y1 = s3[0] - 'A';
					direction3 = s3[3];

					if (direction3 == 'H')
					{
						x2 = x1;
						y2 = y1 + 1;
						x3 = x1;
						y3 = y1 + 2;
					}
					else
					{
						x2 = x1 + 1;
						y2 = y1;
						x3 = x1 + 2;
						y3 = y1;
					}

					if (y1 >= 0 && y1 <= 9 && x1 >= 0 && x1 <= 9 && y2 >= 0 && y2 <= 9 && x2 >= 0 && x2 <= 9 && y3 >= 0 && y3 <= 9 && x3 >= 0 && x3 <= 9)
					{
						bool canPlace = true;

						for (int offset = 0; offset < 3; offset++)
						{
							int currX, currY;
							if (direction3 == 'H')
							{
								currX = x1;
								currY = y1 + offset;
							}
							else
							{
								currX = x1 + offset;
								currY = y1;
							}

							for (int i = -1; i <= 1; i++)
							{
								for (int j = -1; j <= 1; j++)
								{
									int checkX = currX + i;
									int checkY = currY + j;

									if (checkX >= 0 && checkX <= 9 && checkY >= 0 && checkY <= 9)
									{
										if (field[checkX, checkY] == 1)
										{
											canPlace = false;
											break;
										}
									}
								}
								if (!canPlace)
								{
									break;
								}
							}
							if (!canPlace)
							{
								break;
							}
						}

						if (canPlace)
						{
							Console.Clear();
							field[x1, y1] = 1;
							field[x2, y2] = 1;
							field[x3, y3] = 1;
							Console.WriteLine($"Это ваше игровое поле\n");
							DrawField(field);
							n += 1;
						}
						else
						{
							Console.WriteLine("Вы не можете разместить корабль в этой позиции\n");
						}
					}
					else
					{
						Console.WriteLine("Корабль выходит за границы поля / вы ввели неправильные координаты\n");
					}
				}
				else
				{
					Console.WriteLine("Неверный формат ввода. Введите вида: A0 H или B1 V\n");
				}
				if (n == 2)
				{
					isOpen = false;
				}
			}

			string s4;
			int x4, y4;
			char direction4;
			isOpen = true;
			n = 0;
			while (isOpen)
			{
				Console.WriteLine($"осталось разместить 1");
				Console.Write("Введите координаты четырехпалубного корабля и его направление\n(H - горизантальное(вправо), V - вертикальное(вниз)) (например, A0 H): ");
				s4 = Console.ReadLine().ToUpper();
				if (s4.Length == 4 && (s4[3] == 'H' || s4[3] == 'V') && s4[2] == ' ')
				{
					x1 = Convert.ToInt32(s4[1]) - 48;
					y1 = s4[0] - 'A';
					direction4 = s4[3];

					if (direction4 == 'H')
					{
						x2 = x1;
						y2 = y1 + 1;
						x3 = x1;
						y3 = y1 + 2;
						x4 = x1;
						y4 = y1 + 3;
					}
					else
					{
						x2 = x1 + 1;
						y2 = y1;
						x3 = x1 + 2;
						y3 = y1;
						x4 = x1 + 3;
						y4 = y1;
					}

					if (y1 >= 0 && y1 <= 9 && x1 >= 0 && x1 <= 9 && y2 >= 0 && y2 <= 9 && x2 >= 0 && x2 <= 9 && y3 >= 0 && y3 <= 9 && x3 >= 0 && x3 <= 9 && y4 >= 0 && y4 <= 9 && x4 >= 0 && x4 <= 9)
					{
						bool canPlace = true;

						for (int offset = 0; offset < 4; offset++)
						{
							int currX, currY;
							if (direction4 == 'H')
							{
								currX = x1;
								currY = y1 + offset;
							}
							else
							{
								currX = x1 + offset;
								currY = y1;
							}

							for (int i = -1; i <= 1; i++)
							{
								for (int j = -1; j <= 1; j++)
								{
									int checkX = currX + i;
									int checkY = currY + j;

									if (checkX >= 0 && checkX <= 9 && checkY >= 0 && checkY <= 9)
									{
										if (field[checkX, checkY] == 1)
										{
											canPlace = false;
											break;
										}
									}
								}
								if (!canPlace)
								{
									break;
								}
							}
							if (!canPlace)
							{
								break;
							}
						}

						if (canPlace)
						{
							Console.Clear();
							field[x1, y1] = 1;
							field[x2, y2] = 1;
							field[x3, y3] = 1;
							field[x4, y4] = 1;
							Console.WriteLine($"Это ваше игровое поле\n");
							DrawField(field);
							n += 1;
						}
						else
						{
							Console.WriteLine("Вы не можете разместить корабль в этой позиции\n");
						}
					}
					else
					{
						Console.WriteLine("Корабль выходит за границы поля / вы ввели неправильные координаты\n");
					}
				}
				else
				{
					Console.WriteLine("Неверный формат ввода. Введите вида: A0 H или B1 V\n");
				}
				if (n == 1)
				{
					isOpen = false;
				}
			}
			Console.WriteLine("Вы расставили все корабли! Нажмите любую клавишу, чтобы продолжить");
			Console.ReadKey();
			Console.Clear();
		}

		static bool CheckIfGameOver(int[,] field1, int[,] field2)
		{
			bool player1HasShips = false;
			bool player2HasShips = false;

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (field1[i, j] == 1)
					{
						player1HasShips = true;
						break;
					}
				}
				if (player1HasShips) break;
			}


			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (field2[i, j] == 1)
					{
						player2HasShips = true;
						break;
					}
				}
				if (player2HasShips) break;
			}

			return player1HasShips && player2HasShips;
		}

		static void Attack(int[,] myField, int[,] myEnemyField, int[,] enemyField)
		{
			Console.WriteLine("Ваше игровое поле");
			DrawField(myField);

			Console.WriteLine("\nИгровое поле вашего противника");
			DrawField(myEnemyField);

			string s;
			int x, y;
			bool isOpen = true;
			while (isOpen)
			{
				Console.Write("Введите координаты куда хотите ударить (например, A0): ");
				s = Console.ReadLine().ToUpper();
				if (s.Length == 2)
				{
					x = Convert.ToInt32(s[1]) - 48;
					y = s[0] - 'A';
					if (y >= 0 && y <= 9 && x >= 0 && x <= 9)
					{
						if (enemyField[x, y] == 0)
						{
							Console.Clear();
							enemyField[x, y] = 2;
							myEnemyField[x, y] = 2;
							isOpen = false;
							Console.WriteLine("Ваше игровое поле");
							DrawField(myField);
							Console.WriteLine("\nИгровое поле вашего противника");
							DrawField(myEnemyField);
							Console.WriteLine("Вы промахнулись!");
							Console.Write("Нажмите любую клавишу, чтобы продолжить...");
							Console.ReadKey();
							Console.Clear();
						}
						else if (enemyField[x, y] == 2 || enemyField[x, y] == 3)
						{
							Console.WriteLine("Вы уже ударяли в это место, сделайте другой удар");
							Console.Write("Нажмите любую клавишу, чтобы продолжить...");
							Console.ReadKey();
							Console.Clear();
						}
						else
						{

							Console.Clear();
							enemyField[x, y] = 3;
							myEnemyField[x, y] = 3;
							bool killed = CheckShipKilled(enemyField, x, y);

							if (killed)
							{
								MarkAroundShip(enemyField, myEnemyField, x, y);
								Console.WriteLine("Ваше игровое поле");
								DrawField(myField);
								Console.WriteLine("\nИгровое поле вашего противника");
								DrawField(myEnemyField);
								Console.Write("Вы попали и корабль ");
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("УБИТ");
								Console.ResetColor();
								Console.Write("Нажмите любую клавишу, чтобы продолжить...");
								Console.ReadKey();
								Console.Clear();
							}
							else
							{
								Console.WriteLine("Ваше игровое поле");
								DrawField(myField);
								Console.WriteLine("\nИгровое поле вашего противника");
								DrawField(myEnemyField);
								Console.Write("Вы попали и корабль ");
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.WriteLine("РАНЕН");
								Console.ResetColor();
								Console.Write("Нажмите любую клавишу, чтобы продолжить...");
								Console.ReadKey();
								Console.Clear();
							}
						}

						Console.WriteLine("Ваше игровое поле");
						DrawField(myField);
						Console.WriteLine("\nИгровое поле вашего противника");
						DrawField(myEnemyField);
						Console.WriteLine("");
					}
					else
					{
						Console.WriteLine("Вы ввели неправильные координаты\n");
					}
				}
				else
				{
					Console.WriteLine("Неверный формат ввода. Введите координа вида: A0\n");
				}
			}
		}

		static int WhoWin(int[,] field1, int[,] field2)
		{
			int count1 = 0;
			int count2 = 0;
			for (int i = 0; i < field1.GetLength(0); i++)
			{
				for (int j = 0; j < field1.GetLength(1); j++)
				{
					if (field1[i, j] == 1)
					{
						count1 += 1;
					}
				}
			}
			for (int ii = 0; ii < field2.GetLength(0); ii++)
			{
				for (int jj = 0; jj < field2.GetLength(1); jj++)
				{
					if (field2[ii, jj] == 1)
					{
						count2 += 1;
					}
				}
			}
			if (count1 == 0 && count2 == 0)
			{
				return 0;
			}
			else if (count1 == 0)
			{
				return 1;
			}
			else
			{
				return 2;
			}
		}

		static bool CheckShipKilled(int[,] field, int x, int y)
		{
			List<(int, int)> shipCells = new List<(int, int)>();
			FindShipCells(field, x, y, shipCells);

			foreach (var cell in shipCells)
			{
				if (field[cell.Item1, cell.Item2] == 1)
				{
					return false;
				}
			}
			return true;
		}

		static void FindShipCells(int[,] field, int x, int y, List<(int, int)> shipCells)
		{
			if (x < 0 || x >= 10 || y < 0 || y >= 10) return;
			if (field[x, y] != 1 && field[x, y] != 3) return;
			if (shipCells.Contains((x, y))) return;

			shipCells.Add((x, y));

			FindShipCells(field, x + 1, y, shipCells);
			FindShipCells(field, x - 1, y, shipCells);
			FindShipCells(field, x, y + 1, shipCells);
			FindShipCells(field, x, y - 1, shipCells);
		}

		static void MarkAroundShip(int[,] enemyField, int[,] myEnemyField, int x, int y)
		{
			List<(int, int)> shipCells = new List<(int, int)>();
			FindShipCells(enemyField, x, y, shipCells);

			foreach (var cell in shipCells)
			{
				int cellX = cell.Item1;
				int cellY = cell.Item2;

				for (int i = -1; i <= 1; i++)
				{
					for (int j = -1; j <= 1; j++)
					{
						int checkX = cellX + i;
						int checkY = cellY + j;

						if (checkX >= 0 && checkX < 10 && checkY >= 0 && checkY < 10)
						{
							if (enemyField[checkX, checkY] == 0)
							{
								enemyField[checkX, checkY] = 2;
								myEnemyField[checkX, checkY] = 2;
							}
						}
					}
				}
			}
		}
	}
 }