using EscapeRoomWeb.Models;

namespace EscapeRoomWeb.Data;

public static class SeedData
{
    public static void Initialize(GameDbContext db)
    {
        if (db.Puzzles.Any()) return;

        db.Puzzles.AddRange(
            new Puzzle {
                RoomNumber=1, RoomTitle="The Study Room",
                Story="The college archive room is locked. A note near the desk says the answer is hidden in the order of four marked books.",
                Clue="The note reads: 2, 4, 1, 3. Take the first letter of each book title in that order. The visible titles begin with C, A, M and P.",
                Question="Enter the four-letter code.",
                Answer="CAMP", Points=100
            },
            new Puzzle {
                RoomNumber=2, RoomTitle="The Old Laboratory",
                Story="A small box has three switches. The instruction says only one combination is correct.",
                Clue="First switch is off. Second is on. Third is on. Use 0 for off and 1 for on.",
                Question="Enter the three-digit switch code.",
                Answer="011", Points=120
            },
            new Puzzle {
                RoomNumber=3, RoomTitle="The Library Corridor",
                Story="Four numbers are written on the wall: 3, 6, 12, 24.",
                Clue="Each number follows the same operation.",
                Question="What is the next number?",
                Answer="48", Points=140
            },
            new Puzzle {
                RoomNumber=4, RoomTitle="The Final Gate",
                Story="The final gate asks for a number. The previous rooms have given you CAMP, 011 and 48.",
                Clue="Count the letters in CAMP, then add the number of 1s in 011, then add the number of digits in 48.",
                Question="Enter the final number.",
                Answer="7", Points=180
            }
        );
        db.SaveChanges();
    }
}