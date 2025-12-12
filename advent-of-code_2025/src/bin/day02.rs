use advent_of_code::input::read_input_file;

fn is_valid_part_1(num: i64) -> bool {
    let num_str = num.to_string();
    if !num_str.len().is_multiple_of(2) {
        return true;
    }
    let mid = num_str.len() / 2;
    num_str[..mid] != num_str[mid..]
}

fn is_valid_part_2(num: i64) -> bool {
    let num_str = num.to_string();
    for test_len in 1..num_str.len() {
        let mut parser = num_str.chars().peekable();
        let test_str = parser.by_ref().take(test_len).collect::<String>();
        let mut has_remaining = true;
        let mut all_match = true;
        while has_remaining && all_match {
            let compare_str = parser.by_ref().take(test_len).collect::<String>();
            has_remaining = !parser.peek().is_none();
            all_match = compare_str == test_str;
        }
        if all_match {
            return false;
        }
    }
    true
}

fn main() {
    let input = read_input_file(2);
    let ranges = input.split(",");
    let mut invalid_nums_part_1 = Vec::new();
    let mut invalid_nums_part_2 = Vec::new();
    for range in ranges {
        let (start, end) = range.split_once("-").unwrap();
        let start = start.parse::<i64>().unwrap();
        let end = end.parse::<i64>().unwrap();
        for i in start..end + 1 {
            if !is_valid_part_1(i) {
                invalid_nums_part_1.push(i);
                invalid_nums_part_2.push(i);
            } else if !is_valid_part_2(i) {
                invalid_nums_part_2.push(i);
            }
        }
    }
    println!("Part 1: {:?}", invalid_nums_part_1.into_iter().sum::<i64>());
    println!("Part 2: {:?}", invalid_nums_part_2.into_iter().sum::<i64>());
}