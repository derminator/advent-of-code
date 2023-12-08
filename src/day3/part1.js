import {readFileSync} from "fs";

const input = readFileSync("input.txt", {encoding: "utf8"}).split("\r\n");
let sum = 0;
const rowLength = input[0].length;

function getSurrounding(i, j) {
    let surrounding = [];
    if (i > 0) {
        if (j > 0) {
            surrounding.push([i - 1, j - 1]);
        }
        surrounding.push([i-1, j]);
        if (j < rowLength - 1) {
            surrounding.push([i - 1, j + 1]);
        }
    }
    surrounding.push([i, j-1]);
    surrounding.push([i, j+1]);
    surrounding.push([i+1, j-1]);
    surrounding.push([i+1, j]);
    surrounding.push([i+1, j+1]);
    return surrounding;
}

input.forEach((row, i) => {
    let part = false;
    let num = '';
    [...row].forEach((char, j) => {
       if (char >= '0' && char <= '9') {
           num += char;
           const surrounding = [

           ];
       } else {
           if (part) {
               sum += parseInt(num);
               part = false;
           }
           num = '';
       }
    });
});

console.log(sum);
