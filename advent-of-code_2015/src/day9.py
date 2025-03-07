from typing import TypedDict, Dict


class Path(TypedDict):
    destination: str
    distance: int


paths: Dict[TypedDict] = {}
with open("../../.aoc/2015/9") as file:
    for path in file:
