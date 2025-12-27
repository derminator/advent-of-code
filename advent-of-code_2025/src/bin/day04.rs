use advent_of_code::input::read_input_lines;

fn is_forklift(grid: &Vec<Vec<bool>>, row: i64, col: i64) -> bool {
    if row < 0 || col < 0 {
        return false;
    }
    grid.get(row as usize)
        .and_then(|row| row.get(col as usize))
        .map_or(false, |&b| b)
}

fn adjacent_rolls_of_paper(grid: &Vec<Vec<bool>>, row: i64, col: i64) -> u8 {
    vec![
        is_forklift(&grid, row - 1, col - 1),
        is_forklift(&grid, row - 1, col),
        is_forklift(&grid, row - 1, col + 1),
        is_forklift(&grid, row, col - 1),
        is_forklift(&grid, row, col + 1),
        is_forklift(&grid, row + 1, col - 1),
        is_forklift(&grid, row + 1, col),
        is_forklift(&grid, row + 1, col + 1),
    ]
    .iter()
    .fold(0, |acc, x| acc + if *x { 1 } else { 0 })
}

fn can_access_roll(grid: &Vec<Vec<bool>>, row: usize, column: usize) -> bool {
    let adjacent_roll_count = adjacent_rolls_of_paper(grid, row as i64, column as i64);
    grid[row][column] && adjacent_roll_count < 4
}

fn main() {
    let grid: Vec<_> = read_input_lines(4)
        .iter()
        .map(|line| line.chars().map(|c| c == '@').collect::<Vec<_>>())
        .collect();
    let mut accessible_papers = 0;
    for row in 0..grid.len() {
        for col in 0..grid[row].len() {
            if can_access_roll(&grid, row, col) {
                accessible_papers += 1;
            }
        }
    }
    println!("Part 1: {}", accessible_papers);
}
