import math

total = 0

with open("input.txt") as file:
    for data in file:
        numbers = list(map(int, data.split('x')))

        volume = math.prod(numbers)
        numbers.sort()
        perimeter = (2 * numbers[0]) + (2 * numbers[1])

        total += volume + perimeter

print(total)
