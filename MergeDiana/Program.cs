//using System;
//using System.Collections.Generic;
//using MergeDiana.GameLib;

//namespace MergeDiana {
//    class Program {
//        static void Main(string[] args) {
//            //MergeDianaGame game = new MergeDianaGame();
//            //game.PropertyChanged += Game_PropertyChanged;
//            ////game.GameCompleted += Game_GameCompleted;
//            //game.StartGame(4, 4, DianaStrawberryType.F);

//            //while (true) {
//            //    var moveDirection = Console.ReadKey();
//            //    switch (moveDirection.Key) {
//            //        case ConsoleKey.E:
//            //            game.MoveUp();
//            //            break;
//            //        case ConsoleKey.D:
//            //            game.MoveDown();
//            //            break;
//            //        case ConsoleKey.S:
//            //            game.MoveLeft();
//            //            break;
//            //        case ConsoleKey.F:
//            //            game.MoveRight();
//            //            break;
//            //        case ConsoleKey.R:
//            //            game.ActiveSkill(MergeDianaGameSkill.Randomize, null);
//            //            break;
//            //        case ConsoleKey.U:
//            //            game.ActiveSkill(MergeDianaGameSkill.UpgradeBase, null);
//            //            break;
//            //        case ConsoleKey.L:
//            //            game.ActiveSkill(MergeDianaGameSkill.DegradeAll, null);
//            //            break;
//            //        default:
//            //            break;
//            //    }
//            //}
//        }

//        private static void Game_GameCompleted(object sender, DianaStrawberryType e) {
//            Console.WriteLine($"Game Completed! Target = {e}");
//        }

//        private static void Game_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
//            if (e.PropertyName == "DianaStrawberryArray") {
//                Console.Clear();
//                Console.WriteLine("--- Display Strawberries ---");
//                MergeDianaGame t = (MergeDianaGame)sender;
//                List<DianaStrawberry> strawberries = new List<DianaStrawberry>(t.DianaStrawberryArray);
//                for (int row = 0; row < t.RowSize; row++) {
//                    if (row > 0) {
//                        Console.WriteLine("\n\n");
//                    }
//                    for (int col = 0; col < t.ColumnSize; col++) {
//                        Console.Write($"{strawberries[row * t.ColumnSize + col]} ");
//                    }
//                }
//                Console.WriteLine();
//                Console.WriteLine();
//            }
//        }
//    }
//}
