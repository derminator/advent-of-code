with open("input.txt") as file:
    data = file.readline()
    numbers = list(map(int, data.split('x')))

    surface_area = (2 * numbers[0] * numbers[1]) + (2 * numbers[1] * numbers[2]) + (2 * numbers[0] + numbers[2])

print(surface_area)