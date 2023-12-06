import {readFileSync} from "fs";

const data = readFileSync('input.txt', {encoding: "utf-8"});
console.log(data);