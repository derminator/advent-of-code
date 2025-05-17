STARTING_NUMBER = 20151125


def get_next_number(number):
    return (number * 252533) % 33554393


grid = [[STARTING_NUMBER]]


def find_next_diag():
    global STARTING_NUMBER
    next_row = len(grid)
    next_col = 0
    grid.append([])
    while next_row >= 0:
        STARTING_NUMBER = get_next_number(STARTING_NUMBER)
        grid[next_row].append(STARTING_NUMBER)
        next_col += 1
        next_row -= 1


while len(grid) < 2950 or len(grid[2949]) < 3031:
    find_next_diag()

print(grid[2946][3028])
