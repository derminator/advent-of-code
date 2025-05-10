import math
from itertools import combinations
from typing import List

with open('../../.aoc/2015/24') as f:
    packages = list(map(lambda x: int(x), f.read().splitlines()))


def get_quantum_entanglement(group_packages: List[int]) -> int:
    return math.prod(group_packages)


def can_make_group(remaining_packages: List[int], weight: float) -> bool:
    for i in range(1, len(remaining_packages)):
        for potential_combo in combinations(remaining_packages, i):
            if sum(potential_combo) == weight:
                return True
    return False


def sort_packages(compartments):
    target_weight = sum(packages) / compartments

    lowest_quantum_entanglement = float('inf')
    for size in range(1, len(packages)):
        combos = combinations(packages, size)
        for combo in combos:
            if sum(combo) == target_weight:
                quantum_entanglement = get_quantum_entanglement(combo)
                if quantum_entanglement > lowest_quantum_entanglement:
                    continue
                if can_make_group([x for x in packages if x not in combo], target_weight):
                    lowest_quantum_entanglement = quantum_entanglement

        if lowest_quantum_entanglement < float('inf'):
            print(lowest_quantum_entanglement)
            break


sort_packages(3)
sort_packages(4)
