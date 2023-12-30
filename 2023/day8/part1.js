import {readFileSync} from "fs";

const [instructionsStr, networkStr] = readFileSync("input.txt", {encoding: "utf-8"}).split('\n\n');

const instructions = instructionsStr.split("");

const network = {};
networkStr.split("\n").forEach(node => {
    const [start, left, right] = node.match(/[A-Z]{3}/g);
    network[start] = {L: left, R: right};
});

let steps = 0;
let location = 'AAA';
let pos = 0;

while (location !== 'ZZZ') {
    location = network[location][instructions[pos]];
    steps++;
    pos++;
    if (pos === instructions.length) {
        pos = 0;
    }
}

console.log(steps);
