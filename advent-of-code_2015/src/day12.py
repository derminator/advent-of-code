import json

with open("../../.aoc/2015/12") as f:
    accounting = json.load(f)


def parse_dict(d: dict, ignore_red: bool = False) -> int:
    total = 0
    for k, v in d.items():
        if ignore_red and v == "red":
            return 0
        elif isinstance(v, int):
            total += v
        elif isinstance(v, dict):
            total += parse_dict(v, ignore_red)
        elif isinstance(v, list):
            total += parse_list(v, ignore_red)
    return total


def parse_list(l: list, ignore_red: bool = False) -> int:
    total = 0
    for i in l:
        if isinstance(i, int):
            total += i
        elif isinstance(i, dict):
            total += parse_dict(i, ignore_red)
        elif isinstance(i, list):
            total += parse_list(i, ignore_red)
    return total


if isinstance(accounting, dict):
    part_1_total = parse_dict(accounting)
else:
    part_1_total = parse_list(accounting)

print(part_1_total)

if isinstance(accounting, dict):
    part_2_total = parse_dict(accounting, True)
else:
    part_2_total = parse_list(accounting, True)

print(part_2_total)
