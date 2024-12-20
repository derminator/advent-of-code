from day6.shared import parse_file, LIGHTS_GRID_SIZE

lights = [[False for x in range(LIGHTS_GRID_SIZE)] for y in range(LIGHTS_GRID_SIZE)]


def perform_instruction(op: str, x: int, y: int) -> None:
    if op == "turn on":
        lights[x][y] = True
    elif op == "turn off":
        lights[x][y] = False
    elif op == "toggle":
        lights[x][y] = not lights[x][y]
    else:
        raise KeyError(f"Unknown operation: {op}")


parse_file(lights, perform_instruction)
