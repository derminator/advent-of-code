total = 0

with open("input.txt") as file:
    for data in file:
        numbers = list(map(int, data.split('x')))

        surface_area = (2 * numbers[0] * numbers[1]) + (2 * numbers[1] * numbers[2]) + (2 * numbers[0] * numbers[2])
        numbers.sort()
        extra = numbers[0] * numbers[1]

        total += surface_area + extra

print(total)
