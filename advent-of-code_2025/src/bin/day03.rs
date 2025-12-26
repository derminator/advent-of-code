use advent_of_code::concat_nums;
use advent_of_code::input::read_input_lines;

fn parse_bank(line: &String) -> Vec<u8> {
    line.chars()
        .map(|c| c.to_digit(10).unwrap() as u8)
        .collect()
}

fn get_bank_joltage(bank_vals: &Vec<u8>, num_digits: usize) -> u64 {
    let mut digits = Vec::with_capacity(num_digits);

    let mut last_index = 0;
    for digit in 0..num_digits {
        let mut potential_digit = 0;
        for i in last_index..(bank_vals.len() - num_digits + 1 + digit) {
            if bank_vals[i] > potential_digit {
                potential_digit = bank_vals[i];
                last_index = i + 1;
            }
        }
        digits.push(potential_digit);
    }

    concat_nums(&digits)
}

fn main() {
    let banks = read_input_lines(3)
        .iter()
        .map(|line| parse_bank(line))
        .collect::<Vec<_>>();
    let joltages = banks.iter().map(|bank| get_bank_joltage(bank, 2));
    println!("Part 1: {}", joltages.fold(0u64, |acc, x| acc + x));

    let expanded_joltages = banks.iter().map(|bank| get_bank_joltage(bank, 12));
    println!("Part 2: {}", expanded_joltages.fold(0u64, |acc, x| acc + x));
}
