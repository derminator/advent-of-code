import itertools
import re
from typing import Dict

pattern = re.compile("([A-z]+) would (gain|lose) ([0-9]+) happiness units by sitting next to ([A-z]+).")

rules: Dict[str, Dict[str, int]] = {}

with open("../../.aoc/2015/13") as f:
    for line in f.read().strip().splitlines():
        match = pattern.match(line)
        if match:
            a, gain_lose, amount, b = match.groups()
            amount = int(amount) * (1 if gain_lose == "gain" else -1)
            if a not in rules:
                rules[a] = {}
            rules[a][b] = amount

guests = rules.keys()


def find_max_happiness():
    all_orders = itertools.permutations(guests)
    max_happiness = 0
    for order in all_orders:
        happiness = 0
        for i in range(len(order)):
            first = order[i]
            second = order[(i + 1) % len(order)]  # wrap around to the first person
            happiness += rules[first].get(second, 0) + rules[second].get(first, 0)
        max_happiness = max(max_happiness, happiness)
    print(max_happiness)


# Part 1
find_max_happiness()

# Part 2: Adding yourself to the table
rules["You"] = {}
for guest in guests:
    rules["You"][guest] = 0  # You gain 0 happiness units by sitting next to anyone
    rules[guest]["You"] = 0  # You lose 0 happiness units by sitting next to you
find_max_happiness()
