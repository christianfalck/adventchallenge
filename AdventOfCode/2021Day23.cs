
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day23_2021
{

    public static void calculate()
    {
        var inputText = File.ReadAllText("./../../../inputfiles/2021day23.txt");

        var part1Map = inputText.Where(c => c == '.' || (c >= 'A' && c <= 'D')).ToArray();
        var hallway = part1Map.Where(c => c == '.').Count(); // How large is the hallway

        int[] cost = new[] { 1, 10, 100, 1000 }; // A cost 1 to move, B cost 10, C cost 100 and D cost 1000
        int[] directions = new[] { -1, 1 }; // Left/right

        // Part 1
        int answer1 = PlayGame(new GameState(part1Map, 0));

        // Part 2
        var inputTextPart2 = File.ReadAllText("./../../../inputfiles/2021day23part2.txt");
        var extraMapPiece = inputTextPart2.Where(c => c >= 'A' && c <= 'D').ToArray();
        var part2Map = part1Map.Take(hallway + 4).Concat(extraMapPiece).Concat(part1Map.Skip(hallway + 4)).ToArray();

        int answer2 = PlayGame(new GameState(part2Map, 0));

        System.Console.WriteLine("Answer: " + answer1 + " and: " + answer2);

        /**
         * Try all possible ways to get to the goal by moving one step at the time, all possible ways and 
         * let each of those result in another step with all possible ways, etc. 
         * Iterate by continue with the state with the lowest movement cost, which means the first 
         * movement that reaches goal is the one with the lowest movement cost. 
         */
        int PlayGame(GameState initialState)
        {
            var howManyInEachRoom = (initialState.Map.Length - hallway) / 4;
            var GameStatesToCalculate = new PriorityQueue<GameState, int>();
            GameStatesToCalculate.Enqueue(initialState, 0);
            var visited = new HashSet<string>();
            while (GameStatesToCalculate.Count > 0)
            {
                var currentGameState = GameStatesToCalculate.Dequeue();
                var mapAsString = new String(currentGameState.Map);
                if (visited.Contains(mapAsString))
                {
                    continue;  // We have already had this exact setup
                }
                if (AreWeThereYet(currentGameState, howManyInEachRoom)) // Game finished
                {
                    return currentGameState.MovementCost;
                }
                visited.Add(mapAsString);
                GameStatesToCalculate.EnqueueRange(GetPotentialNextMoves(howManyInEachRoom, currentGameState).Select(n => (n, n.MovementCost)));
            }
            throw new Exception("Should never get here");
        }

        /** 
         * This will return true if the room has only it's own amphipods in it (e.g. no BCD in the A room) or if the room is empty
         */
        bool IsRoomAvailable(char[] world, int howManyInEachRoom, int whichRoom) // which room: 0 for the first (A), 1 for second (B), etc.
        {
            for (int i = howManyInEachRoom - 1; i >= 0; i--)
            {
                var c = world[hallway + i * 4 + whichRoom];
                if (c == '.')
                {
                    return true;
                }
                if (c != 'A' + whichRoom)
                {
                    return false;
                }
            }
            return true;
        }

        /**
         * Returns how many amphipods there are in the current room (whichRoom = 0 for A, 1 for B, etc.). 0 for empty. 
         * All rooms are filled at start
         */
        int HowManyAmphipodsInThisRoom(char[] world, int howManyInEachRoom, int whichRoom)
        {
            int count = 0;
            for (int i = howManyInEachRoom - 1; i >= 0; i--)
            {
                var c = world[hallway + i * 4 + whichRoom];
                if (c == '.')
                {
                    return count;
                }
                count++;
            }
            return count;
        }

        /** 
         * Move amphipod into a room (no check it it's their room)
         */
        void MoveToRoom(char[] world, int howManyInEachRoom, int whichRoom, char amphipod)
        {
            for (int i = howManyInEachRoom - 1; i >= 0; i--)
            {
                var index = hallway + i * 4 + whichRoom;
                if (world[index] == '.')
                {
                    world[index] = amphipod;
                    return;
                }
            }
            throw new Exception("Should never get here");
        }

        /** 
         * Remove amphipod from a room
         */
        void RemoveFromRoom(char[] world, int howManyInEachRoom, int whichRoom)
        {
            for (int i = 0; i < howManyInEachRoom; i++)
            {
                var index = hallway + i * 4 + whichRoom;
                if (world[index] != '.')
                {
                    world[index] = '.';
                    return;
                }
            }
            throw new Exception("Should never get here");
        }

        /** 
         * Returns the aphipod closest to the door in a specific room
         */
        char WhoIsClosestToTheDoorInThisRoom(char[] world, int howManyInEachRoom, int whichRoom)
        {
            for (int i = 0; i < howManyInEachRoom; i++)
            {
                var index = hallway + i * 4 + whichRoom;
                if (world[index] != '.')
                {
                    return world[index];
                }
            }
            throw new Exception("Should never get here");
        }

        /** 
         * Returns all the potential moves from here.
         * If an amphipod is in the hallway, the path to its room is clear and it's free to move in, that is always the best move. 
         */
        List<GameState> GetPotentialNextMoves(int howManyInEachRoom, GameState state)
        {
            var potentialMoves = new List<GameState>();
            // First choice is always to move an amphipod to it's room
            for (int spotInHallway = 0; spotInHallway < hallway; spotInHallway++)
            {
                if (state.Map[spotInHallway] == '.')
                {
                    continue; // Empty hallway spot
                }
                // Amphipod found in hallway
                var currentAmphipod = state.Map[spotInHallway]; // The char representing the amphipod
                var currentAmphipodNumber = currentAmphipod - 'A'; // A = 0, B = 1, etc. 
                bool canMoveToRoom = IsRoomAvailable(state.Map, howManyInEachRoom, currentAmphipodNumber);
                if (!canMoveToRoom)
                {
                    continue;
                }
                // The location (index) of the current amphipod's room. A = room index 2 etc. 
                var currentAmphipodsRoomIndex = 2 + 2 * currentAmphipodNumber;
                // Check if the hallway is clear between the amphipod and its room. If one space is taken => canMove = false
                var whichWayToMyRoom = currentAmphipodsRoomIndex > spotInHallway ? 1 : -1;
                for (int step = whichWayToMyRoom; Math.Abs(step) <= Math.Abs(currentAmphipodsRoomIndex - spotInHallway); step += whichWayToMyRoom)
                {
                    if (state.Map[spotInHallway + step] != '.')
                    {
                        canMoveToRoom = false;
                        break;
                    }
                }
                if (!canMoveToRoom)
                {
                    continue;
                }
                var newWorld = new char[state.Map.Length];
                state.Map.CopyTo(newWorld, 0);
                newWorld[spotInHallway] = '.'; // Remove the amphipod from the hallway
                MoveToRoom(newWorld, howManyInEachRoom, currentAmphipodNumber, currentAmphipod);
                var costForTheNewState = state.MovementCost + (Math.Abs(currentAmphipodsRoomIndex - spotInHallway) + (howManyInEachRoom - HowManyAmphipodsInThisRoom(state.Map, howManyInEachRoom, currentAmphipodNumber))) * cost[currentAmphipodNumber];
                potentialMoves.Add(new GameState(newWorld, costForTheNewState));
            }
            // If we can move an amphipods from the hallway to its room, that it always the best move.
            // No need to check for other potential moves for now
            if (potentialMoves.Count > 0)
            {
                return potentialMoves;
            }
            // If we can't move from hallway to its room, we'll have to move from another room to the hallway
            for (int roomIndex = 0; roomIndex < 4; roomIndex++)
            {
                if (IsRoomAvailable(state.Map, howManyInEachRoom, roomIndex))
                {
                    // Either empty or only amphipods that lives here
                    continue;
                }
                var amphipodToMove = WhoIsClosestToTheDoorInThisRoom(state.Map, howManyInEachRoom, roomIndex);
                var amphipodToMoveNumber = amphipodToMove - 'A';
                // Possible targets are empty spaces on hallway, not in front of any room, with no blockers
                var movementCost = state.MovementCost + (howManyInEachRoom - HowManyAmphipodsInThisRoom(state.Map, howManyInEachRoom, roomIndex) + 1) * (cost[amphipodToMoveNumber]);
                var roomPosition = 2 + 2 * roomIndex;
                foreach (var direction in directions)
                {
                    var distance = direction;
                    while (roomPosition + distance >= 0 && roomPosition + distance < hallway && state.Map[roomPosition + distance] == '.')
                    {
                        if (roomPosition + distance == 2 || roomPosition + distance == 4 || roomPosition + distance == 6 || roomPosition + distance == 8)
                        {
                            // We're in front of a room
                            distance += direction;
                            continue;
                        }
                        var newMap = new char[state.Map.Length];
                        state.Map.CopyTo(newMap, 0);
                        newMap[roomPosition + distance] = amphipodToMove;
                        RemoveFromRoom(newMap, howManyInEachRoom, roomIndex);
                        potentialMoves.Add(new GameState(newMap, movementCost + Math.Abs(distance) * cost[amphipodToMoveNumber]));
                        distance += direction;
                    }
                }
            }
            return potentialMoves;
        }

        /**
         * Check if we've moved all amphipods to their room. 
         */
        bool AreWeThereYet(GameState state, int howManyInEachRoom)
        {
            // Is the hallway empty
            for (int i = 0; i < hallway; i++)
            {
                if (state.Map[i] != '.')
                {
                    return false;
                }
            }
            // Are everyone home
            for (int roomNumber = 0; roomNumber < 4; roomNumber++)
            {
                // Crappy name on this function but RoomAvailable = true means it's filled with correct amphipods
                if (!IsRoomAvailable(state.Map, howManyInEachRoom, roomNumber))
                {
                    return false;
                }
            }
            // If so, we've won! 
            return true;
        }
    }
}

// A gamestate is a potential setup with the cost to get there
record struct GameState(char[] Map, int MovementCost) { }