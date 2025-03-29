import re
from typing import List


class Reindeer:
    name: str
    speed: int
    fly_time: int
    rest_time: int
    points = 0

    def __init__(self, name: str, speed: int, fly_time: int, rest_time: int):
        self.name = name
        self.speed = speed
        self.fly_time = fly_time
        self.rest_time = rest_time

    def find_distance(self, time: int) -> int:
        cycle_time = self.fly_time + self.rest_time
        cycles = time // cycle_time
        remaining_time = time % cycle_time
        extra_fly_time = min(remaining_time, self.fly_time)
        return cycles * self.fly_time * self.speed + extra_fly_time * self.speed


pattern = re.compile(r"(\w+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds.")

reindeers: List[Reindeer] = []

with open("../../.aoc/2015/14") as f:
    for lin in f.readlines():
        m = pattern.match(lin)
        if m:
            reindeers.append(Reindeer(m.group(1), int(m.group(2)), int(m.group(3)), int(m.group(4))))
        else:
            raise Exception("No match: " + lin)

# part 1
winning_distance = 0
for reindeer in reindeers:
    distance = reindeer.find_distance(2503)
    if distance > winning_distance:
        winning_distance = distance
print(winning_distance)

# part 2
for t in range(1, 2503):
    max_distance = 0
    winners: List[Reindeer] = []
    for reindeer in reindeers:
        distance = reindeer.find_distance(t)
        if distance > max_distance:
            max_distance = distance
            winners = [reindeer]
        elif distance == max_distance:
            winners.append(reindeer)
    for winner in winners:
        winner.points += 1
print(max(reindeers, key=lambda r: r.points).points)
