import {readFileSync} from "fs";

const hands = readFileSync("input.txt", {encoding: "utf8"}).split("\n");

console.log(hands);