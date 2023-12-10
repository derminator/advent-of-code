import {readFileSync} from "fs";

const cards = readFileSync("input.txt", {encoding: "utf-8"}).split("\r\n");

let sum = 0;

/**
 * @param {string} input
 */
function getNumbers(input) {
    const strings = input.match(/\d+/g);
    return strings.map(str => parseInt(str));
}

cards.forEach(card => {
    const [, input] = card.split(":");
    const [winnersStr, haveStr] = input.split("|");
    const winners = getNumbers(winnersStr);
    const have = getNumbers(haveStr);
    let value = 0;
    have.forEach(num => {
        if (winners.includes(num)) {
            value = (value === 0) ? 1 : value * 2;
        }
    });
    sum += value;
});

console.log(sum);