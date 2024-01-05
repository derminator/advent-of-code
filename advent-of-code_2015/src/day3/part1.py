with open("input.txt") as file:
    data = file.read()

x = 0
y = 0
houses = [(0, 0)]

for direction in data:
    if direction == '^':
        y += 1
    elif direction == 'v':
        y -= 1
    elif direction == '>':
        x += 1
    elif direction == '<':
        x -= 1
    else:
        raise KeyError("Invalid Direction " + direction)

    houses.append((x, y))

uniqueHouses = set(houses)
print(len(uniqueHouses))
