import {readFileSync} from "fs";
import _ from "lodash";

const histories = readFileSync("input.txt", {encoding: "utf-8"}).split("\n").map(history =>
    history.split(" ").map(entry => parseInt(entry)));

/**
 * @param {number[]} history
 * @return number
 */
function findNext(history) {
    const diffs = [];
    for (let i = 1; i < history.length; i++) {
        diffs.push(history[i] - history[i - 1]);
    }
    if (diffs.every(diff => diff === 0)) {
        return _.last(history);
    } else {
        return _.last(history) + findNext(diffs);
    }
}

const next = histories.map(findNext);
console.log(_.sum(next));
