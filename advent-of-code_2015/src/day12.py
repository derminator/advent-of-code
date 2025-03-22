import json

with open("../../.aoc/2015/12") as f:
    accounting = json.load(f)

total_number = 0


def parse_dict(d: dict):
    global total_number
    for k, v in d.items():
        if isinstance(v, int):
            total_number += v
        elif isinstance(v, dict):
            parse_dict(v)
        elif isinstance(v, list):
            parse_list(v)


def parse_list(l: list):
    global total_number
    for i in l:
        if isinstance(i, int):
            total_number += i
        elif isinstance(i, dict):
            parse_dict(i)
        elif isinstance(i, list):
            parse_list(i)


if isinstance(accounting, dict):
    parse_dict(accounting)
elif isinstance(accounting, list):
    parse_list(accounting)

print(total_number)
