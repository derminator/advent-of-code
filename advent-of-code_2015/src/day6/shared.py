import re
from typing import Callable

LIGHTS_GRID_SIZE = 1000
pattern = re.compile(r"(turn on|turn off|toggle) (\d+,\d+) through (\d+,\d+)")


def parse_point(point: str) -> tuple[int, int]:
    coordinates = point.split(",")
    return int(coordinates[0]), int(coordinates[1])


def parse_file(lights: list[list], instruction_parser: Callable[[str, int, int], None]):
    with open("input.txt") as file:
        for instruction in file:
            match = pattern.match(instruction)
            op = match.group(1)
            start = parse_point(match.group(2))
            end = parse_point(match.group(3))

            for x in range(start[0], end[0] + 1):
                for y in range(start[1], end[1] + 1):
                    instruction_parser(op, x, y)
    print(sum(sum(row) for row in lights))
