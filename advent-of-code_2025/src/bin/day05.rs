use advent_of_code::input::read_input_lines;

fn main() {
    let raw_input = read_input_lines(5);
    let input_sections = raw_input.split(|str| str.is_empty()).collect::<Vec<_>>();
    let mut ranges = input_sections[0]
        .iter()
        .map(|range| {
            let mut split = range.split('-');
            let low: u64 = split.next().unwrap().parse().unwrap();
            let high: u64 = split.next().unwrap().parse().unwrap();
            low..=high
        })
        .collect::<Vec<_>>();
    let fruits = input_sections[1]
        .iter()
        .map(|fruit| fruit.parse::<u64>().unwrap())
        .collect::<Vec<_>>();

    let fresh_fruits = fruits
        .iter()
        .filter(|fruit| ranges.iter().any(|range| range.contains(fruit)))
        .count();
    println!("Part 1: {}", fresh_fruits);

    ranges.sort_by_key(|range| *range.start());
    let mut num_valid_ids = 0u64;
    let mut range_iter = ranges.iter();
    let mut current_range = range_iter.next().unwrap().clone();
    for range in range_iter {
        if range.start() <= current_range.end() {
            current_range = *current_range.start()..=*range.end().max(current_range.end());
        } else {
            num_valid_ids += current_range.end() - current_range.start() + 1;
            current_range = range.clone();
        }
    }
    num_valid_ids += current_range.end() - current_range.start() + 1;
    println!("Part 2: {}", num_valid_ids);
}
