import re

LIGHTS_GRID_SIZE = 999

lights = [[False for x in range(LIGHTS_GRID_SIZE)] for y in range(LIGHTS_GRID_SIZE)]

pattern = re.compile(r"(turn on|turn off|toggle) (\d+,\d+) through (\d+,\d+)")


def parse_point(point: str) -> tuple[int, int]:
    coordinates = point.split(",")
    return int(coordinates[0]), int(coordinates[1])


with open("input.txt") as file:
    for instruction in file:
        match = pattern.match(instruction)
        op = match.group(1)
        start = parse_point(match.group(2))
        end = parse_point(match.group(3))

        for x in range(start[0], end[0]):
            for y in range(start[1], end[1]):
                if op == "turn on":
                    lights[x][y] = True
                elif op == "turn off":
                    lights[x][y] = False
                else:
                    lights[x][y] = not lights[x][y]

print(sum(sum(row) for row in lights))
