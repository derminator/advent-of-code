import {readFileSync} from "fs";

const [instructionsStr, networkStr] = readFileSync("input.txt", {encoding: "utf-8"}).split('\n\n');

export const instructions = instructionsStr.split("");

export const network = {};
networkStr.split("\n").forEach(node => {
    const [start, left, right] = node.match(/[A-Z]{3}/g);
    network[start] = {L: left, R: right};
});

export function navigate(location, direction) {
    return network[location][direction];
}