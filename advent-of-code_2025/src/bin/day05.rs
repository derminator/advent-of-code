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
            low..high + 1
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
}
