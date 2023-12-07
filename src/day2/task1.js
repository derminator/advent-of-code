import {readFileSync} from "fs";

const MAXES = {
    red: 12,
    green: 13,
    blue: 14,
};

const games = readFileSync("input.txt", {encoding: "utf-8"}).split("\n");
const sum = games.reduce((sum, game, i) => {
    if (parseGame(game)) {
        return sum + i + 1;
    } else {
        return sum;
    }
}, 0);

console.log(sum);

/**
 * @param {string} game
 */
function parseGame(game) {
    let res = true;
    game = game.split(":")[1];
    const reveals = game.split(";");
    reveals.forEach(reveal => {
        const colors = reveal.split(",");
        colors.forEach(colorString => {
            const color = colorString.match(/blue|red|green/).pop();
            const number = parseInt(colorString.match(/\d+/).pop());
            if (MAXES[color] < number) {
                res = false;
            }
        });
    });
    return res;
}
