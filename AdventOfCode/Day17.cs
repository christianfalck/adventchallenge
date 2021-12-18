
namespace AdventOfCode
{
    class Day17
    {
        public static void calculate()
        {
            // target area: x=265..287, y=-103..-58
            int answerPart1 = 0; // Max Y
            int answerPart2 = 0; // number of hits
            for (int x = 0; x < 287; x++)
                for(int y = -103; y < 103; y++)
                {
                    // try all combinations - brute force 
                    var myCoord = new Coordinate(0, 0);
                    int iteration = 0;
                    int maxY = 0;
                    while(myCoord.x <= 287 && myCoord.y > -103)
                    {
                        if(x > iteration)
                            myCoord.x += (x - iteration); // else x speed = 0
                        myCoord.y += (y - iteration);
                        if(myCoord.y > maxY)
                            maxY = myCoord.y;
                        iteration++;
                        if(myCoord.y >= -103 && myCoord.y <= -58 && myCoord.x >= 265 && myCoord.x <= 287)
                        {
                            // HIT! 
                            if(answerPart1 < maxY)
                                answerPart1 = maxY;
                            answerPart2++;
                        }
                    }
                }

            System.Console.WriteLine("Answer: " + answerPart1 + ", and " + answerPart2);
        }
    }

    class Coordinate
    {
        public int x;
        public int y;
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}

