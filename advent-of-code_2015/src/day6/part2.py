from day6.shared import parse_file, LIGHTS_GRID_SIZE

lights = [[0 for x in range(LIGHTS_GRID_SIZE)] for y in range(LIGHTS_GRID_SIZE)]


def perform_instruction(op: str, x: int, y: int) -> None:
    if op == "turn on":
        lights[x][y] += 1
    elif op == "turn off":
        if lights[x][y] > 0:
            lights[x][y] -= 1
    elif op == "toggle":
        lights[x][y] += 2
    else:
        raise KeyError(f"Unknown operation: {op}")


parse_file(lights, perform_instruction)
