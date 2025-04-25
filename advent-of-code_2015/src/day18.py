from copy import deepcopy
from operator import countOf
from typing import List


def map_input(value: str) -> List[bool]:
    result = []
    for c in value:
        if c == '.':
            result.append(False)
        elif c == '#':
            result.append(True)
        else:
            raise Exception("Invalid input: " + value)
    return result


with open('../../.aoc/2015/18') as f:
    data = f.read().splitlines()
part1_grid = list(map(map_input, data))
part2_grid = deepcopy(part1_grid)

corners = [(0, 0), (0, len(part2_grid[0]) - 1), (len(part2_grid) - 1, 0), (len(part2_grid) - 1, len(part2_grid[0]) - 1)]
for corner in corners:
    part2_grid[corner[1]][corner[0]] = True


def find_neighbors(grid: List[List[bool]], x: int, y: int):
    neighbors = []
    for i in range(-1, 2):
        for j in range(-1, 2):
            if i == 0 and j == 0:
                continue
            if x + i < 0 or x + i >= len(grid[0]) or y + j < 0 or y + j >= len(grid):
                neighbors.append(False)
            else:
                neighbors.append(grid[y + j][x + i])
    return neighbors


def animate(grid: List[List[bool]], lights_stuck: bool = False):
    new_map = []
    for y in range(len(grid)):
        new_map.append([])
        for x in range(len(grid[y])):
            neighbors = find_neighbors(grid, x, y)
            if lights_stuck and (x, y) in corners:
                new_map[y].append(True)
            elif grid[y][x]:
                if neighbors.count(True) in [2, 3]:
                    new_map[y].append(True)
                else:
                    new_map[y].append(False)
            else:
                if neighbors.count(True) == 3:
                    new_map[y].append(True)
                else:
                    new_map[y].append(False)
    return new_map


for _ in range(100):
    part1_grid = animate(part1_grid)
    part2_grid = animate(part2_grid, True)

print(sum(countOf(f, True) for f in part1_grid))
print(sum(countOf(f, True) for f in part2_grid))
