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

const CARD_VALUES = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];

/**
 * @param {string[]} hand
 */
function checkType(hand) {
    const cards = {};
    hand.forEach(value => {
        if (cards[value]) {
            cards[value]++;
        } else {
            cards[value] = 1;
        }
    });

    switch (Object.keys(cards).length) {
        case 1:
            return TYPE_VALUES.FIVE_KIND;
        case 2:
            if (Object.values(cards).some(value => value === 4)) {
                return TYPE_VALUES.FOUR_KIND;
            } else {
                return TYPE_VALUES.FULL_HOUSE;
            }
        case 3:
            if (Object.values(cards).some(value => value === 3)) {
                return TYPE_VALUES.THREE_KIND;
            } else {
                return TYPE_VALUES.TWO_PAIR;
            }
        case 4:
            return TYPE_VALUES.ONE_PAIR;
        default:
            return TYPE_VALUES.HIGH_CARD;
    }
}

/**
 * @param {string} card
 */
function checkValue(card) {
    return CARD_VALUES.findIndex(value => card === value);
}

const hands = readFileSync("input.txt", {encoding: "utf8"}).split("\n").map(line => {
    const [hand, bid] = line.split(" ");
    return {
        hand: hand.split(""),
        bid: parseInt(bid),
    };
});

hands.sort((a, b) => {
    let typeComp = checkType(a.hand) - checkType(b.hand);
    if (typeComp !== 0) {
        return typeComp;
    }

    for (let i = 0; i < a.hand.length; i++) {
        const valComp = checkValue(a.hand[i]) - checkValue(b.hand[i]);
        if (valComp !== 0) {
            return valComp;
        }
    }

    console.error("Could not sort hands", a, b);
    process.exit(1);
});

const winnings = hands.reduce((prev, current, currentIndex) => prev + (current.bid * (currentIndex + 1)), 0);

console.log(winnings);