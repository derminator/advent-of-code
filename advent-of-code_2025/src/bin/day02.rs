use advent_of_code::input::read_input_file;

fn is_valid(num: i64) -> bool {
    let num_str = num.to_string();
    if !num_str.len().is_multiple_of(2) {
        return true;
    }
    let mid = num_str.len() / 2;
    num_str[..mid] != num_str[mid..]
}

fn main() {
    let input = read_input_file(2);
    let ranges = input.split(",");
    let mut invalid_nums = Vec::new();
    for range in ranges {
        let (start, end) = range.split_once("-").unwrap();
        let start = start.parse::<i64>().unwrap();
        let end = end.parse::<i64>().unwrap();
        for i in start..end + 1 {
            if !is_valid(i) {
                invalid_nums.push(i);
            }
        }
    }
    println!("{:?}", invalid_nums.into_iter().sum::<i64>());
}