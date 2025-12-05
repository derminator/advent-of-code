use std::fs;

fn get_day_file(day: i8) -> String {
    format!("../.aoc/2025/{:02}", day)
}

fn read_input_file(day: i8) -> String {
    fs::read_to_string(get_day_file(day)).expect("Something went wrong reading the file")
}

pub fn read_input_lines(day: i8) -> Vec<String> {
    read_input_file(day)
        .lines()
        .map(|s| s.to_string())
        .collect()
}