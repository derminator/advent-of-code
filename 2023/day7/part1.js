import {readFileSync} from "fs";

const hands = readFileSync("input.txt", {encoding: "utf8"}).split("\n").map(line => {
    const [hand, bid] = line.split(" ");
    return {
        hand: hand.split(""),
        bid: parseInt(bid),
    };
});

console.log(hands);