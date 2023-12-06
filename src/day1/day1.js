import {readFileSync} from "fs";

const data = readFileSync('input.txt', {encoding: "utf-8"});
const lines = data.split("\n");

const digits = lines.map(line => line.match(/\d/g));
console.log(digits.reduce((sum, digit) => {
    const num = digit.slice(0, 1) + digit.slice(-1);
    return sum + parseInt(num);
}, 0));
