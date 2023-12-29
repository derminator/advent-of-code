import {readFileSync} from "fs";

const TYPE_VALUES = {
    FIVE_KIND: 7,
    FOUR_KIND: 6,
    FULL_HOUSE: 5,
    THREE_KIND: 4,
    TWO_PAIR: 3,
    ONE_PAIR: 2,
    HIGH_CARD: 1,
};

/**
 * @param {string[]} hand
 */
function checkType(hand) {
    if (hand.every(h => h === hand[0])) {
        return TYPE_VALUES.FIVE_KIND;
    }
}

const hands = readFileSync("input.txt", {encoding: "utf8"}).split("\n").map(line => {
    const [hand, bid] = line.split(" ");
    return {
        hand: hand.split(""),
        bid: parseInt(bid),
    };
});

hands.sort((a, b) => {
    return checkType(a.hand) - checkType(b.hand);
});

console.log(hands);