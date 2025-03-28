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

all_orders = itertools.permutations(rules.keys())
max_happiness = 0
for order in all_orders:
    happiness = 0
    for i in range(len(order)):
        a = order[i]
        b = order[(i + 1) % len(order)]  # wrap around to the first person
        happiness += rules[a].get(b, 0) + rules[b].get(a, 0)
    max_happiness = max(max_happiness, happiness)

print(max_happiness)
