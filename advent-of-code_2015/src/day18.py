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
current_grid = list(map(map_input, data))


def find_neighbors(grid, x, y):
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


def animate(grid):
    new_map = []
    for y in range(len(grid)):
        new_map.append([])
        for x in range(len(grid[y])):
            neighbors = find_neighbors(grid, x, y)
            if grid[y][x]:
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
    current_grid = animate(current_grid)

print(sum(countOf(f, True) for f in current_grid))
