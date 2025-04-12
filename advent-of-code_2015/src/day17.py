from itertools import combinations

TOTAL_EGGNOG = 150

with open("../../.aoc/2015/17") as f:
    containers = [int(line.strip()) for line in f.readlines()]

valid_combos = 0
min_containers = len(containers)
combos_with_min_containers = 0

for r in range(1, len(containers) + 1):
    for combo in combinations(containers, r):
        if sum(combo) == TOTAL_EGGNOG:
            valid_combos += 1
            if len(combo) < min_containers:
                min_containers = len(combo)
                combos_with_min_containers = 1
            elif len(combo) == min_containers:
                combos_with_min_containers += 1

print(valid_combos)
print(combos_with_min_containers)
