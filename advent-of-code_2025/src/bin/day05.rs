use advent_of_code::input::read_input_lines;

fn main() {
    let raw_input = read_input_lines(5);
    let input_sections = raw_input.split(|str| str.is_empty()).collect::<Vec<_>>();
    let ranges = input_sections[0]
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

    let mut valid_ids = Vec::new();
    for range in ranges {
        for id in range {
            if !valid_ids.contains(&id) {
                valid_ids.push(id);
            }
        }
    }
    println!("Part 2: {}", valid_ids.len());
}
