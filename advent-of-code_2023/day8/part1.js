import {instructions, navigate} from "./util.js";

let steps = 0;
let location = 'AAA';
let pos = 0;

while (location !== 'ZZZ') {
    location = navigate(location, instructions[pos]);
    steps++;
    pos++;
    if (pos === instructions.length) {
        pos = 0;
    }
}

console.log(steps);
