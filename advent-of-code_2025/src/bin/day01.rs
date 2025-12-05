use advent_of_code::input;

struct Dial {
    pos: i32
}

impl Dial {
    fn turn(&mut self, amount: i32) {
        let new_pos = (self.pos + amount) % 100;
        self.pos = new_pos
    }
}

fn main() {
    let mut code = 0;
    let mut dial = Dial{pos: 0};
    let instructions = input::read_input_lines(1);
    for instruction in instructions {
        
    }
}