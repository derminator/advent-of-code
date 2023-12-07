import {readFileSync} from "fs";

const games = readFileSync("input.txt", {encoding: "utf-8"}).split("\n");
const sum = games.reduce((sum, game) => parseGame(game) + sum, 0);

console.log(sum);

/**
 * @param {string} game
 */
function parseGame(game) {
    game = game.split(":")[1];
    const MAXES = {
        red: 0,
        green: 0,
        blue: 0,
    };
    const reveals = game.split(";");
    reveals.forEach(reveal => {
        const colors = reveal.split(",");
        colors.forEach(colorString => {
            const color = colorString.match(/blue|red|green/).pop();
            const number = parseInt(colorString.match(/\d+/).pop());
            if (MAXES[color] < number) {
                MAXES[color] = number;
            }
        });
    });
    return MAXES.blue * MAXES.red * MAXES.green;
}
