with open("input.txt") as file:
    data = file.read()

santa_bots = [[0, 0], [0, 0]]
houses = [(0, 0)]
santa = 0

for direction in data:
    if direction == '^':
        santa_bots[santa][1] += 1
    elif direction == 'v':
        santa_bots[santa][1] -= 1
    elif direction == '>':
        santa_bots[santa][0] += 1
    elif direction == '<':
        santa_bots[santa][0] -= 1
    else:
        raise KeyError("Invalid Direction " + direction)

    houses.append((santa_bots[santa][0], santa_bots[santa][1]))
    santa = not santa

uniqueHouses = set(houses)
print(len(uniqueHouses))
