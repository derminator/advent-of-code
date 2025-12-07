use advent_of_code::input;
use crate::puzzle_structs::Dial;

mod puzzle_structs {
    pub struct Dial {
        pos: i32,
        times_stopped_at_0: i32,
        times_passed_0: i32,
    }

    impl Dial {
        pub fn turn(&mut self, amount: i32) {
            self.pos += amount;

            while self.pos > 99 {
                self.times_passed_0 += 1;
                self.pos -= 100;
            }
            while self.pos < 0 {
                self.times_passed_0 += 1;
                self.pos += 100;
            }

            if self.pos == 0 {
                self.times_stopped_at_0 += 1;
            }
        }

        pub fn new() -> Self {
            Self { pos: 50, times_stopped_at_0: 0, times_passed_0: 0 }
        }

        pub fn times_stopped_at_0(&self) -> i32 {
            self.times_stopped_at_0
        }

        pub fn times_passed_0(&self) -> i32 {
            self.times_passed_0
        }
    }
}

fn main() {
    let mut dial = Dial::new();
    let instructions = input::read_input_lines(1);
    for instruction in instructions {
        let mut parser = instruction.chars();
        let dir_char = parser.next().unwrap();
        let direction = match dir_char {
            'L' => -1,
            'R' => 1,
            _ => panic!("Invalid direction character: {}", dir_char),
        };
        let amount: i32 = parser.as_str().parse().unwrap();
        dial.turn(amount * direction);
    }
    println!("Part 1: {}", dial.times_stopped_at_0());
    println!("Part 2: {}", dial.times_passed_0());
}