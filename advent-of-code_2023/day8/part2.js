import {instructions, navigate, network} from "./util.js";

let steps = 0;
let pos = 0;

let locations = Object.keys(network).filter(location => location.endsWith('A'));

while (locations.some(location => !location.endsWith('Z'))) {
    locations = locations.map(location => navigate(location, instructions[pos]));
    steps++;
    pos++;
    if (pos === instructions.length) {
        pos = 0;
    }
}

console.log(steps);