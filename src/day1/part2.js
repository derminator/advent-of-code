import {readFileSync} from "fs";

const data = readFileSync('input.txt', {encoding: "utf-8"});
const lines = data.split("\n");

const digits = lines.map(line => line.match(/\d|one|two|three|four|five|six|seven|eight|nine/g));
console.log(digits.reduce((sum, digit) => {
    let first = digit.slice(0, 1).pop();
    let last = digit.slice(-1).pop();

    first = convertWordToNum(first);
    last = convertWordToNum(last);

    const num = first + last;
    return sum + parseInt(num);
}, 0));


function convertWordToNum(digit) {
    switch (digit) {
        case "one":
            return "1";
        case "two":
            return "2";
        case "three":
            return "3";
        case "four":
            return "4";
        case "five":
            return "5";
        case "six":
            return "6";
        case "seven":
            return "7";
        case "eight":
            return "8";
        case "nine":
            return "9";
        default:
            return digit;
    }
}