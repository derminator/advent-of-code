import re
from typing import List


class Reindeer:
    name: str
    speed: int
    fly_time: int
    rest_time: int

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

winning_distance = 0
for reindeer in reindeers:
    distance = reindeer.find_distance(2503)
    if distance > winning_distance:
        winning_distance = distance
print(winning_distance)
