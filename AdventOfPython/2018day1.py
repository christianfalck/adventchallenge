import itertools

# part 1
start = 0
numbers = [int(x) for x in open("C:\\Users\\falke\\Documents\\Projects\\adventchallenge\\AdventOfPython\\2018day1.txt").readlines()]
start = sum(numbers)
print(start)

# part 2
start = 0
seen = set()
for number in itertools.cycle(numbers):
    start += number
    if start in seen:
        print(start)
        break
    seen.add(start)