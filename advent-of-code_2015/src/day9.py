import re
from itertools import permutations
from typing import TypedDict, List


class Path(TypedDict):
    destination: str
    distance: int


regEx = re.compile(r"(\w+) to (\w+) = (\d+)")
paths = dict[str, List[Path]]()
with open("../../.aoc/2015/9") as file:
    for path in file:
        result = re.search(regEx, path)
        if result:
            origin = result.group(1)
            destination = result.group(2)
            distance = int(result.group(3))
            paths.setdefault(origin, []).append(Path(destination=destination, distance=distance))
            paths.setdefault(destination, []).append(Path(destination=origin, distance=distance))


def calculate_distance(route: List[str]) -> int:
    total_distance = 0
    for i in range(len(route) - 1):
        city = route[i]
        next_city = route[i + 1]
        for next_path in paths[city]:
            if next_path["destination"] == next_city:
                total_distance += next_path["distance"]
                break
    return total_distance


cities = list(paths.keys())
shortest_distance = float("inf")
longest_distance = 0

for perm in permutations(cities):
    distance = calculate_distance(perm)
    if distance < shortest_distance:
        shortest_distance = distance
    if distance > longest_distance:
        longest_distance = distance

print(shortest_distance)
print(longest_distance)
