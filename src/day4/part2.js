import {readFileSync} from "fs";

const cards = readFileSync("input.txt", {encoding: "utf-8"}).split("\n").map(string => ({count: 1, text: string}));

/**
 * @param {string} input
 */
function getNumbers(input) {
    const strings = input.match(/\d+/g);
    return strings.map(str => parseInt(str));
}

cards.forEach((card, i) => {
    let nextDup = i + 1;
    const [, input] = card.text.split(":");
    const [winnersStr, haveStr] = input.split("|");
    const winners = getNumbers(winnersStr);
    const have = getNumbers(haveStr);
    have.forEach(num => {
        if (winners.includes(num) && nextDup < cards.length) {
            cards[nextDup].count += card.count;
            nextDup++;
        }
    });
});

console.log(cards.reduce((sum, card) => sum += card.count, 0));