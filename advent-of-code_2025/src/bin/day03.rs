use advent_of_code::concat_nums;
use advent_of_code::input::read_input_lines;

fn parse_bank(line: &String) -> Vec<u8> {
    line.chars()
        .map(|c| c.to_digit(10).unwrap() as u8)
        .collect()
}

fn get_bank_joltage(bank_vals: Vec<u8>) -> u8 {
    let mut first = 0;
    let mut second = 0;

    for i in 0..bank_vals.len() {
        if bank_vals[i] > first && i < bank_vals.len() - 1 {
            first = bank_vals[i];
            second = 0
        } else if bank_vals[i] > second {
            second = bank_vals[i]
        }
    }

    concat_nums(&[first, second])
}

fn main() {
    let banks = read_input_lines(3)
        .iter()
        .map(|line| parse_bank(line))
        .collect::<Vec<_>>();
    let joltages = banks.iter().map(|bank| get_bank_joltage(bank.clone()));
    println!("Part 1: {}", joltages.fold(0u64, |acc, x| acc + x as u64))
}
