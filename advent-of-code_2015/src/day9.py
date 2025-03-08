import re
from typing import TypedDict, List


class Path(TypedDict):
    destination: str
    distance: int


regEx = re.compile(r"(\w+) to (\w+) = (\d+)")
paths = dict[List[TypedDict]]()
with open("../../.aoc/2015/9") as file:
    for path in file:
        result = re.search(regEx, path)
